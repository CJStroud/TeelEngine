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


        //public static bool EntityCollididesWithTerrain(Vector2 entityLocation)
        //{
        //    IEnumerable<Vector2> viewableCollisions = Camera.GetViewableLocations(Collisions, Camera.Lens);
        //    return viewableCollisions.Any(location => PositionsAreSame(entityLocation, location));
        //}

        //public static bool EntityCollidesWithEntity(Vector2 entityLocation, IEnumerable<IEntity> colliedableEntities)
        //{
        //    IEnumerable<ISprite> viewableSprites = Camera.GetViewableSprites(colliedableEntities, Camera.Lens);
        //    return viewableSprites.Any(colliedableEntity => PositionsAreSame(entityLocation, colliedableEntity.Location));
        //}

        public static bool PositionsAreSame(Vector2 position1, Vector2 position2)
        {
            return (int)position1.X == (int)position2.X && (int)position1.Y == (int)position2.Y;
        }
    }
}
