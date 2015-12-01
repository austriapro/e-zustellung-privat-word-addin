using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Pkcs;
using Asn1Util = Org.BouncyCastle.Asn1.Utilities;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Asn1;
using OpenSSL = Org.BouncyCastle.OpenSsl;
using Pem = Org.BouncyCastle.Utilities.IO.Pem;
using X509 = Org.BouncyCastle.X509;
using System.Collections;
using System.IO;
using System.Xml.Linq;
using HandySignatur;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto.Parameters;
using eZustellSendPOC.Services;
using Microsoft.Practices.Unity;
using System.ComponentModel;
using WinFormsMvvm.ExtensionMethods;
using LogService;


namespace eZustellSendPOC.Services
{
    public class CertificateService : ICertificateService
    {
        internal RsaKeyPairGenerator rkpg;
        internal AsymmetricCipherKeyPair ackp;

        public X509Certificate2 Certificate { get; private set; }
        public string EdID { get; private set; }
        private const string csrNamespace = "http://www.e-zustellung.at/namespaces/ed_remote_control_init_20150115";
        private const string dsigNamespace = "http://www.w3.org/2000/09/xmldsig#";
        private XNamespace csr = csrNamespace;
        private XNamespace dsig = dsigNamespace;

        private byte[] _signedCSR;

        private IUnityContainer _uc;
        private IRegistrationService _regService;

        public CertificateService(IUnityContainer uc, IRegistrationService regService)
        {
            _uc = uc;
            _regService = regService;
        }

        public enum CertificateRC
        {
            [Description("OK")]
            OK,
            [Description("Datei nicht gefunden")]
            CertificateFileNotFound,
            [Description("Fehler beim Erstellen des Zertifikates")]
            CertificateCreateError,
            [Description("Fehler beim registrieren des Zertifikates")]
            RegisterCertificateError,
            [Description("Zertifikat speichern ist fehlgeschlagen")]
            SaveCertificateError,
            [Description("Zertifikat laden ist fehlgeschlagen")]
            LoadCertificateError,
            [Description("Signierter CSR fehlt")]
            SignedCSRMissing
        }
        public enum CheckSumAlgorithm
        {
            SHA256
        }
        public static string GetChecksum(byte[] toHash, CheckSumAlgorithm alg)
        {
            switch (alg)
            {
                case CheckSumAlgorithm.SHA256:
                    SHA256 sha256 = SHA256Managed.Create();
                    byte[] hash = sha256.ComputeHash(toHash);
                    return Convert.ToBase64String(hash);
                default:
                    throw new NotImplementedException("Algoritmus nicht unterstützt");
            }
        }
        public CertificateRC Load(string fn, string password)
        {
            Certificate = null;
            _signedCSR = null;
            //CertificateRC rc;
            Log.Information("Lade SW-Zertifikat von {fn}", CallerInfo.Create(), fn);
            if (!File.Exists(fn))
            {
                return CertificateRC.CertificateFileNotFound;
            }
            try
            {
                Certificate = new X509Certificate2(fn, password);
                GetEdIdFromCertificate(fn, password);
                int i = password.Length;
                password = new String('*', i);

            }
            catch (Exception ex)
            {
                Log.Error(ex, CallerInfo.Create(), CertificateRC.LoadCertificateError.GetDescriptionFromValue());
                return CertificateRC.LoadCertificateError;
            }
            Log.Information("SW-Zertifikat für User {EdID} mit Fingerprint {Thumbprint} geladen.", CallerInfo.Create(), EdID, Certificate.Thumbprint);
            return CertificateRC.OK;
        }

        private void GetEdIdFromCertificate(string fn, string password)
        {
            var pkcs12 = new Pkcs12Store(File.Open(fn, FileMode.Open), password.ToCharArray());
            var bcCert = pkcs12.GetCertificate("KeyContainer");
            var uids = ((X509Name)bcCert.Certificate.SubjectDN).GetValues(X509Name.UID);
            EdID = (string)uids.ToArray()[0];
        }

        //public string GenerateSwCertificate(string edId)
        //{
        //    byte[] csrBinary = GenerateCSR(edId);
        //    return System.Convert.ToBase64String(csrBinary);
        //}

        private string GenerateCSR(string edId)
        {
            Dictionary<Org.BouncyCastle.Asn1.DerObjectIdentifier, string> Dn = new Dictionary<Org.BouncyCastle.Asn1.DerObjectIdentifier, string>();
            Dn.Add(X509Name.UID, edId);

            //X509Name name = new X509Name(new ArrayList(Dn.Keys), new ArrayList(Dn.Values)); //( "CN="+EdId+", C=AT"); //, E=" + EdId);
            X509Name name = new X509Name(new List<DerObjectIdentifier>(Dn.Keys), new List<string>(Dn.Values));
            //Key generation 2048bits
            rkpg = new RsaKeyPairGenerator();
            rkpg.Init(new KeyGenerationParameters(new SecureRandom(), 2048));

            ackp = rkpg.GenerateKeyPair();

            //PKCS #10 Certificate.Certificate Signing Request
            KeyUsage usage = new KeyUsage(KeyUsage.DigitalSignature | KeyUsage.KeyEncipherment);

            Pkcs10CertificationRequest csr = new Pkcs10CertificationRequest("SHA256WITHRSA", name, ackp.Public, null, ackp.Private);
            byte[] csrBinary = csr.GetDerEncoded();
            return System.Convert.ToBase64String(csrBinary);
        }

        public CertificateRC SaveAsCertificate(string fn)
        {
            File.WriteAllBytes(fn, Certificate.Export(X509ContentType.Cert));
            return CertificateRC.OK;
        }

        public CertificateRC Save(string fn, string password)
        {
            Log.Information("Speichere SW-Zertifikat in {fn}", CallerInfo.Create(), fn);
            if (_signedCSR == null)
            {
                return CertificateRC.SignedCSRMissing;
            }
            try
            {
                X509.X509CertificateParser certParser = new X509.X509CertificateParser();
                X509.X509Certificate bcCert = certParser.ReadCertificate(_signedCSR);

                // Convert BouncyCastle X509 Certificate.Certificate to .NET's X509Certificate
                var cert = DotNetUtilities.ToX509Certificate(bcCert);
                var certBytes = cert.Export(X509ContentType.Pkcs12, password);

                // Convert X509Certificate to X509Certificate2
                X509Certificate2 localCertificate = new X509Certificate2(certBytes, password);

                // Convert BouncyCastle Private Key to RSA
                var rsaPriv = DotNetUtilities.ToRSA(ackp.Private as RsaPrivateCrtKeyParameters);

                // Setup RSACryptoServiceProvider with "KeyContainerName" set
                var csp = new CspParameters();
                csp.KeyContainerName = "KeyContainer";

                var rsaPrivate = new RSACryptoServiceProvider(csp);

                // Import private key from BouncyCastle's rsa
                rsaPrivate.ImportParameters(rsaPriv.ExportParameters(true));

                // Set private key on our X509Certificate2
                localCertificate.PrivateKey = rsaPrivate;
                File.WriteAllBytes(fn, localCertificate.Export(X509ContentType.Pkcs12, password));
                GetEdIdFromCertificate(fn, password);
                Certificate = localCertificate;
                Log.Information("SW-Zertifikat für User {EdID} mit Fingerprint {Thumbprint} gespeichert.", CallerInfo.Create(), EdID, Certificate.Thumbprint);
                int i = password.Length;
                password = new String('*', i);
                //_signedCSR = null; 
            }
            catch (Exception ex)
            {
                Log.Error(ex, CallerInfo.Create(), "Zertifikat speichern fehlgeschlagen");
                int i = password.Length;
                password = new String('*', i);
                return CertificateRC.SaveCertificateError;
            }
            return CertificateRC.OK;
        }

        private XElement CreateXmlDoc()
        {
            XElement xelNew = new XElement(csr + "processAuthenticationCSRRequest",
                new XAttribute(XNamespace.Xmlns + "csr", csr),
                new XAttribute(XNamespace.Xmlns + "dsig", dsig),
                //new XElement(dsig + "Signature"),
                //new XElement(csr + "MandateEdid"),
                new XElement(csr + "validForYears"),
                new XElement(csr + "pkcs10CSR"));
            return xelNew;
        }

        /// <summary>
        /// Gets the base64 certificate.
        /// </summary>
        /// <param name="edid">The edid.</param>
        /// <returns>Base64 coded Certificate.Certificate</returns>
        public CertificateRC GenerateCertificateAndRegister(string edid)
        {
            Log.Information("Registrierung des SW-Zertifikates für '{edid}'", CallerInfo.Create(), edid);
            _signedCSR = null;
            try
            {
                XElement xele = CreateXmlDoc();
                var xValidFor = xele.Element(csr + "validForYears");
                xValidFor.Value = "P2Y";
                var xPKCS10Csr = xele.Element(csr + "pkcs10CSR");
                xPKCS10Csr.Value = GenerateCSR(edid);
                var xRoot = xele.Element(csr + "processAuthenticationCSRRequest");

                Debug.WriteLine(xele.ToString());
                string xmlele = xele.ToString();
                Log.Debug(xmlele, CallerInfo.Create());

                XMLHandySignatur hs = new XMLHandySignatur();
                hs.Target = XMLHandySignatur.RequestTarget.ProductionSystem;
                string result = hs.SignXML(xele);
                Log.Debug(xmlele, CallerInfo.Create());
                UTF8Encoding enc = new UTF8Encoding();
                byte[] signedCSR = enc.GetBytes(result);
                //string fnCSR = @"C:\GIT\eZustellung\TestDaten\TestDatenSammlung\TestDatenSammlung\Daten\eZustellPOC\CSR-Request.bin";
                //File.WriteAllBytes(fnCSR, signedCSR);
                byte[] bincert = _regService.GetCertificate(signedCSR);
                if (_regService.LastRC != eZustellService.ZustellServiceRC.OK)
                {
                    string Message = _regService.LastRC.GetDescriptionFromValue();
                    Log.Error("Fehler von WebService GetCertificate: {LastRC} - {Message}", CallerInfo.Create(), _regService.LastRC, Message);
                    return CertificateRC.RegisterCertificateError;
                }
                _signedCSR = bincert;
                return CertificateRC.OK;
            }
            catch (Exception ex)
            {
                Log.Error(ex, CallerInfo.Create(), "Fehler beim Erstellen des Zertifikates");
                return CertificateRC.CertificateCreateError;
            }

        }
    }
}
