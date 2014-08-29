using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace TeelEngine.Loading
{
    public class Saver
    {
        public void Save(Level.Level level)
        {
            var settings = new XmlWriterSettings();
            settings.Indent = true;

            TextWriter textWriter = new StreamWriter(@"C:\Testing\test.xml");
            XmlWriter writer = new XmlTextWriter(textWriter);

            writer.WriteStartDocument();
            writer.WriteStartElement("root");

            writer.WriteComment("This is level file for use with a Teelengine powered game");
            

            level.Save(writer);

            writer.WriteEndDocument();
            writer.Close();

        }
    }
}
