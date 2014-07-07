using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TeelEngine
{
    public class CollisionDetection
    {
        public static List<Vector2> Collisions = new List<Vector2>();


        public static bool EntityCollididesWithTerrain(IEntity entity)
        {
            return Collisions.Any(location => PositionsAreSame(entity.Location, location));
        }

        public static bool EntityCollidesWithEntity(IEntity entity, List<IEntity> colliedableEntities)
        {
            return colliedableEntities.Any(colliedableEntity => PositionsAreSame(entity.Location, colliedableEntity.Location));
        }

        public static bool PositionsAreSame(Vector2 position1, Vector2 position2)
        {
            return (int)position1.X == (int)position2.X && (int)position1.Y == (int)position2.Y;
        }
    }
}
