using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using TeelEngine;

namespace TestGame
{
    public class Events
    {

        public static void PlayerMoveUp()
        {
            var layer = Game1.layerController.GetEntityLayer();
            Unit player = GetPlayer(layer);
            var entityLocation = new Vector2(player.Location.X, player.Location.Y - 1);
            var entities = layer.GetEntitiesExcluding(player);
            bool collided = CheckForCollisions(entityLocation, entities);
            if (!player.IsMoving)
                player.GetAnimatedTexture().Row = (int)Direction.North;
            if (!collided)
            {
                player.MoveUp();
            }
        }

        public static void PlayerMoveDown()
        {
            var layer = Game1.layerController.GetEntityLayer();
            Unit player = GetPlayer(layer);

            var entityLocation = new Vector2(player.Location.X, player.Location.Y + 1);
            var entities = layer.GetEntitiesExcluding(player);
            bool collided = CheckForCollisions(entityLocation, entities);
            if (!player.IsMoving)
                player.GetAnimatedTexture().Row = (int)Direction.South;
            if (!collided)
            {
                player.MoveDown();
            }
        }

        public static void PlayerMoveRight()
        {
            var layer = Game1.layerController.GetEntityLayer();
            Unit player = GetPlayer(layer);
            var entityLocation = new Vector2(player.Location.X + 1, player.Location.Y);
            var entities = layer.GetEntitiesExcluding(player);
            bool collided = CheckForCollisions(entityLocation, entities);
            if (!player.IsMoving)
                player.GetAnimatedTexture().Row = (int)Direction.East;
            if (!collided)
            {
                player.MoveRight();
            }
        }
        public static void PlayerMoveLeft()
        {
            var layer = Game1.layerController.GetEntityLayer();
            Unit player = GetPlayer(layer);
            var entityLocation = new Vector2(player.Location.X - 1, player.Location.Y);
            var entities = layer.GetEntitiesExcluding(player);
            bool collided = CheckForCollisions(entityLocation, entities);
            if (!player.IsMoving)
                player.GetAnimatedTexture().Row = (int)Direction.West;
            if (!collided)
            {
                player.MoveLeft();
            }
        }

        public static bool CheckForCollisions(Vector2 entityLocation, IEnumerable<IEntity> entities)
        {
            var collideWithTerrain = CollisionDetection.EntityCollididesWithTerrain(entityLocation);
            var collideWithEntity = CollisionDetection.EntityCollidesWithEntity(entityLocation, entities);

            return collideWithEntity || collideWithTerrain;
        }

        public static Unit GetPlayer(EntityLayer layer)
        {
            var units = layer.GetOfType<Unit>();
            return units.First();
        }
    }
}
