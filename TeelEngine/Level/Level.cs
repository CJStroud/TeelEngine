using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
using Point = Microsoft.Xna.Framework.Point;

namespace TeelEngine.Level
{
    public class Level
    {
        public Level()
        {
            Entities = new List<Entity>();
        }

        public Level(Size mapSize)
        {
            Width = mapSize.Width;
            Height = mapSize.Height;
            GameTiles = new GameTile[mapSize.Width* mapSize.Height];
            Entities = new List<Entity>();
        }

        public Level(int width, int height)
        {
            Width = width;
            Height = height;
            GameTiles = new GameTile[width*height];
            Entities = new List<Entity>();
        }

        [ContentSerializer]
        public GameTile[] GameTiles { get; set; }


        [ContentSerializer]
        public List<Entity> Entities { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        private int _nextAvaliableId;

        public void AddTile(ITile tile, Point location)
        {
            if (location.X <= Width && location.Y <=Height)
            {
                if (GameTiles[location.X* location.Y] == null)
                    GameTiles[location.X* location.Y] = new GameTile {Location = location};

                tile.Location = new Vector2(location.X, location.Y);
                GameTiles[location.X* location.Y].SubTiles.Add(tile);
            }
        }

        public int GetNextAvailableEntityId()
        {
            return (_nextAvaliableId += 1);
        }

        public void AddEntity(Entity entity)
        {
            entity.EntityId = GetNextAvailableEntityId();
            Entities.Add(entity);
        }

        public IEnumerable<Entity> GetEntitesByGroup(string group)
        {
            return Entities.Where(ent => ent.Group == group);
        }

        public List<IRenderable> GetAllRenderables()
        {
            var renderables = new List<IRenderable>();

            foreach (var gameTile in GameTiles)
            {
                if (gameTile != null)
                {
                    foreach (var tile in gameTile.SubTiles)
                    {
                        IRenderable renderable = tile as IRenderable;
                        if (renderable != null) renderables.Add(renderable);
                    }
                }
            }

            foreach (var entity in Entities)
            {
                if (entity != null) renderables.Add(entity);
            }

            renderables = renderables.OrderBy(render => render.Layer).ToList();

            return renderables;
        }

        public void Save()
        {
            
        }

        public void Load(XmlReader reader)
        {
            
        }
    }
}