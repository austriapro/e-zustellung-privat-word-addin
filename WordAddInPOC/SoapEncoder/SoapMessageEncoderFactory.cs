using LogService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SoapEncoder
{
    internal class SoapMessageEncoderFactory : MessageEncoderFactory
    {
        MessageEncoder encoder;

        public SoapMessageEncoderFactory(MessageEncoderFactory messageEncoderFactory)
        {
            if (messageEncoderFactory == null)
                throw new ArgumentNullException("messageEncoderFactory", "A valid message encoder factory must be passed to the GZipEncoder");
            encoder = new SoapMessageEncoder(messageEncoderFactory.Encoder);
        }

        //The service framework uses this property to obtain an encoder from this encoder factory
        public override MessageEncoder Encoder
        {
            get { return encoder; }
        }

        public override MessageVersion MessageVersion
        {
            get { return encoder.MessageVersion; }
        }

    }

    class SoapMessageEncoder : MessageEncoder
    {

        MessageEncoder innerEncoder;

        internal SoapMessageEncoder(MessageEncoder messageEncoder)
            : base()
        {
            if (messageEncoder == null)
                throw new ArgumentNullException("messageEncoder", "A valid message encoder must be passed to the GZipEncoder");
            innerEncoder = messageEncoder;

        }
        public override MessageVersion MessageVersion
        {
            get { return innerEncoder.MessageVersion; }
        }
        public override string ContentType
        {
            get
            {
                return innerEncoder.ContentType;
            }
        }
        public override string MediaType
        {
            get { return innerEncoder.MediaType; }
        }
        public override bool IsContentTypeSupported(string contentType)
        {
            return innerEncoder.IsContentTypeSupported(contentType);
        }
        public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
        {
            byte[] msgContents = new byte[buffer.Count];
            Array.Copy(buffer.Array, buffer.Offset, msgContents, 0, msgContents.Length);
            bufferManager.ReturnBuffer(buffer.Array);
            
            MemoryStream stream = new MemoryStream(msgContents);
            var sr = new StreamReader(stream);
            string xmlStr = sr.ReadToEnd();
            Log.Verbose("From Server: {String}", CallerInfo.Create(), xmlStr);
            stream.Position = 0;
            return ReadMessage(stream, int.MaxValue);
            //return innerEncoder.ReadMessage(buffer, bufferManager, contentType);
        }
        public override Message ReadMessage(Stream stream, int maxSizeOfHeaders, string contentType)
        {
            Message msg =innerEncoder.ReadMessage(stream, maxSizeOfHeaders, contentType);
            var xy = msg.ToString();
            return msg;
        }
        public override void WriteMessage(Message message, Stream stream)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Schreibt beim Überschreiben in einer abgeleiteten Klasse eine Nachricht, die kleiner ist als die angegebene Größe, im angegebenen Offset in einen Bytearraypuffer.
        /// Verwendet um das Mustunderstand Element zu entfernen.
        /// </summary>
        /// <param name="message">Die in den Nachrichtenpuffer zu schreibende <see cref="T:System.ServiceModel.Channels.Message" />.</param>
        /// <param name="maxMessageSize">Die maximal zulässige Nachrichtengröße, die geschrieben werden kann.</param>
        /// <param name="bufferManager">Der <see cref="T:System.ServiceModel.Channels.BufferManager" />, der den Puffer verwaltet, in den die Nachricht geschrieben wird.</param>
        /// <param name="messageOffset">Offset des Segments, das am Start des Bytearrays beginnt, das den Puffer bereitstellt.</param>
        /// <returns>
        /// Ein <see cref="T:System.ArraySegment`1" /> vom Typ byte, das den Puffer bereitstellt, in dem die Nachricht serialisiert wird.
        /// </returns>
        public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
        {
            MemoryStream stream = new MemoryStream();

            XmlWriter writer = XmlWriter.Create(stream); //, this.writerSettings);
            message.WriteMessage(writer);
            writer.Close();
            stream.Position = 0;

            var sr = new StreamReader(stream);
            string xmlString = sr.ReadToEnd();
            //sr.Close();
            XDocument xmlMessage = XDocument.Parse(xmlString);
            //Interesting part
            XNamespace soapEnvelope = "http://schemas.xmlsoap.org/soap/envelope/";

            XNamespace sec = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";
            XNamespace timeNsp = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd";

            var soapEnvNode = xmlMessage.Descendants(sec + "Security");
            var attribute = soapEnvNode.Attributes(soapEnvelope + "mustUnderstand").First();
            if (attribute != null)
            {
                attribute.Remove();
                //DateTime start = DateTime.Now;
                //DateTime end = start.AddMinutes(5);
                //string startStr = start.ToString("s");
                //string endStr = end.ToString("s");
                //XElement timeStamp = new XElement(timeNsp + "Timestamp",
                //    new XElement(timeNsp + "Created", startStr),
                //    new XElement(timeNsp + "Expires", endStr));
                //soapEnvNode.FirstOrDefault().AddFirst(timeStamp);
                ////End Interesting part
            }
            //stream.Position = 0;

            //xmlMessage.Save(stream);

            //byte[] messageBytes = stream.GetBuffer();
            //int messageLength = (int)stream.Position;
            stream.Close();
            string xmlStr = xmlMessage.ToString(SaveOptions.DisableFormatting);
            Log.Verbose("Outgoing: {String}", CallerInfo.Create(), xmlStr);
            DateTime start = DateTime.Now;
            byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(xmlStr);
            int messageLength = messageBytes.Length;
            int totalLength = messageLength + messageOffset;
            byte[] totalBytes = bufferManager.TakeBuffer(totalLength);
            Array.Copy(messageBytes, 0, totalBytes, messageOffset, messageLength);

            ArraySegment<byte> byteArray = new ArraySegment<byte>(totalBytes, messageOffset, messageLength);
            return byteArray;

        }
        private static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        private static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}
