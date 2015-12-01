using System;
using eZustellSendPOC.Services;

namespace eZustellSendPOC.Services
{
    public interface ICertificateService
    {
        System.Security.Cryptography.X509Certificates.X509Certificate2 Certificate { get; }
        string EdID { get; }
        CertificateService.CertificateRC GenerateCertificateAndRegister(string edid);
        CertificateService.CertificateRC Load(string fn, string password);
        CertificateService.CertificateRC Save(string fn, string password);
        CertificateService.CertificateRC SaveAsCertificate(string fn);
    }
}
