using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CommonTests.IO
{
    public sealed class StringWriterUTF8 : StringWriter
    {
        public StringWriterUTF8(StringBuilder sb) : base(sb) { }

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }

    }
}
