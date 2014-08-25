using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;

namespace TeelEngine.Level
{
    public static class LevelLoader
    {

        public static void SaveTerrainMap(Level level)
        {
            var terrainTiles = new List<List<int>>();


            for (int y = 0; y < level.Height; y++)
            {
                var row = new List<int>();
                for (int x = 0; x < level.Width; x++)
                {
                    GameTile gameTile = level.GameTiles[x, y];
                    if (gameTile != null)
                    {
                        foreach (var subTile in gameTile.SubTiles)
                        {
                            if (subTile as TerrainTile != null)
                            {
                                IRenderable tile = subTile as IRenderable;
                                row.Add(tile.Texture.TextureId);
                            }
                        }
                    }
                }
                terrainTiles.Add(row);
            }

            var saveableLevel = new SaveableLevel();
            saveableLevel.Width = level.Width;
            saveableLevel.Height = level.Height;
            saveableLevel.TerrainTiles = terrainTiles;

            if (!File.Exists(@"C:\game\testsave.xml")) File.Create(@"C:\game\testsave.xml").Dispose();

            try
            {
                var serializer = new XmlSerializer(typeof(SaveableLevel));
                using (TextWriter writer = new StreamWriter(@"C:\game\testsave.xml"))
                {
                    serializer.Serialize(writer, saveableLevel);
                }

            }
            catch (Exception ex)
            {
                throw;
            }


        }

        public static Level LoadTerrainMap()
        {
            var saveableLevel = new SaveableLevel();
            string path = @"C:\game\testsave.xml";

            try
            {
                var serializer = new XmlSerializer(typeof(SaveableLevel));
                using (TextReader reader = new StreamReader(path))
                {
                    saveableLevel = (SaveableLevel) serializer.Deserialize(reader);
                }

            }
            catch (Exception ex)
            {
                throw;
            }

            return saveableLevel.ConvertToLevel();
        }

    }

    public class SaveableLevel
    {

        public int Width { get; set; }
        public int Height { get; set; }
        public List<List<int>> TerrainTiles { get; set; }
        

        public SaveableLevel()
        {}

        public SaveableLevel(Level level)
        {
            Width = level.Width;
            Height = level.Height;

           var gameTiles = new List<List<GameTile>>();
            for (int x = 0; x < level.Width; x++)
            {
                var row = new List<GameTile>();
                for (int y = 0; y < level.Height; y++)
                {
                    row.Add(level.GameTiles[x, y]);
                }
            }
        }

        public Level ConvertToLevel()
        {
            Level level = new Level();
            level.Height = Height;
            level.Width = Width;

            GameTile[,] gameTiles = new GameTile[Width, Height];
            for (int y = 0; y <= Height - 2; y++)
            {

                for (int x = 0; x <= Width - 2; x++)
                {
                    TerrainTile terrainTile = new TerrainTile();
                    ITexture texture = new SpriteTexture("background", TerrainTiles[x][y]);
                    terrainTile.Texture = texture;
                    level.AddTile(terrainTile, new Point(x, y));

                }
            }

            return level;

        }

    }
}
