using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
using TeelEngine.Level;

namespace TeelEngine.Loading
{
    public static class Loader
    {
        public static Level.Level Load(string levelName)
        {
            Level.Level level = null;
            using (var reader = XmlReader.Create("level.xml"))
            {
                level = IntermediateSerializer.Deserialize<Level.Level>(reader, "level.xml");
            }

            return level;
        }
    }
}
