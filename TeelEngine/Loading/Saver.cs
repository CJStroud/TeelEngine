using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;

namespace TeelEngine.Loading
{
    public static class Saver
    {
        public static void Save(Level.Level level)
        {
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = Encoding.UTF8;

            using (var writer = XmlWriter.Create("level.xml", settings))
            {
                IntermediateSerializer.Serialize(writer, level, null);
            }
        }
    }
}
