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
        public static void TransformXslt(string xsltPath, string dataPath, string outputPath)
        {
            XslCompiledTransform xslTrans = new XslCompiledTransform();
            xslTrans.Load(xsltPath);

            XmlDocument doc = new XmlDocument();
            doc.Load(dataPath);

            using (XmlTextWriter result = new XmlTextWriter(outputPath, Encoding.UTF8))
            {
                result.Formatting = Formatting.Indented;
                xslTrans.Transform(doc, result);
            }

       }
    }
}
