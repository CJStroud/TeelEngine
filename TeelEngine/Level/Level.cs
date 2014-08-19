using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using Point = Microsoft.Xna.Framework.Point;

namespace TeelEngine.Level
{
    public class Level
    {
        public Level(Size mapSize)
        {
            GameTiles = new GameTile[mapSize.Width, mapSize.Height];
            Entities = new List<Entity>();
        }

        public Level(int width, int height)
        {
            GameTiles = new GameTile[width, height];
            Entities = new List<Entity>();
        }

        public GameTile[,] GameTiles { get; private set; }
        public List<Entity> Entities { get; private set; }

        private int _nextAvaliableId;

        public void AddTile(ITile tile, Point location)
        {
            if (location.X < GameTiles.GetUpperBound(0) && location.Y < GameTiles.GetUpperBound(1))
            {
                if (GameTiles[location.X, location.Y] == null)
                    GameTiles[location.X, location.Y] = new GameTile {Location = location};

                GameTiles[location.X, location.Y].SubTiles.Add(tile);
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
    }
}