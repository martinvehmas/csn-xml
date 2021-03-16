using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSNXML.Serialization
{
    internal sealed class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding { get { return Encoding.UTF8; } }
    }
}
