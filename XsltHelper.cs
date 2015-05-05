using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;

namespace CommonTests
{
    public class XsltHelper
    {
        public static string TransformXslt(string xsltPath, object data)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(object));
            string body;

            XslCompiledTransform xslTrans = new XslCompiledTransform();
            xslTrans.Load(xsltPath);

            using (XmlTextWriter dataWriter = new XmlTextWriter(new MemoryStream(), Encoding.UTF8))
            {
                serializer.Serialize(dataWriter, data);
                dataWriter.Flush();
                dataWriter.BaseStream.Seek(0, SeekOrigin.Begin);

                using (XmlTextReader input = new XmlTextReader(dataWriter.BaseStream))
                {
                    using (XmlTextWriter result = new XmlTextWriter(new MemoryStream(), Encoding.UTF8))
                    {
                        xslTrans.Transform(input, result);
                        result.BaseStream.Seek(0, SeekOrigin.Begin);

                        using (StreamReader resultReader = new StreamReader(result.BaseStream, Encoding.UTF8))
                        {
                            body = resultReader.ReadToEnd();
                        }
                    }
                }
            }
            return body;
        }
    }
}
