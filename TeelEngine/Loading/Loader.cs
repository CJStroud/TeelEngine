using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml;
using Microsoft.Xna.Framework;
using TeelEngine.Level;

namespace TeelEngine.Loading
{
    public class Loader
    {
        public void Load()
        {
            XmlReader reader = new XmlTextReader(@"C:\Testing\test.xml");
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            var level = new Level.Level(50, 50);

            while (reader.Read())
            {
                    Type type = Type.GetType(reader.Name);
                    if (type != null)
                    {
                        Console.WriteLine(type.FullName);
                        MethodInfo method = type.GetMethod("Load");
                        ConstructorInfo constructorInfo = type.GetConstructor(Type.EmptyTypes);
                        object instance = constructorInfo.Invoke(null);

                        method.Invoke(instance, new[] {(object) reader});

                        ITile tile = instance as ITile;

                        level.AddTile(tile, new Point((int)tile.Location.X, (int)tile.Location.Y));
                    }
                
            }
        }
    }
}
