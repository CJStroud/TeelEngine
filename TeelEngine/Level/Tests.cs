using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Moq;
using NUnit.Framework;

namespace TeelEngine.Level
{
    class Tests
    {
        [Test]
        public void Example()
        {
            var gameTile = new GameTile();
            var entityTile = new EntityTile() { TextureId = 10};
            gameTile.AddTile(entityTile);
            var collisionTile = new CollisionTile();
            gameTile.AddTile(collisionTile);

            var level1 = new Level(new Size(10, 10));
            level1.GameTiles[0, 0] = gameTile;

            var renderer = new GameRenderer();
            renderer.Render(level1, TODO);
        }

    }
}
