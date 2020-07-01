using System.Linq;
using Collector.Dimension;
using MonoGame.Extended;
using Vector2 = Microsoft.Xna.Framework.Vector2;


namespace Collector.Character
{
    public class Player : IRestrictions
    {
        public static float X { get; private set; }
        public static float Y { get; private set; }
        private RectangleF _playerBounds;
        private World World { get; }

        public Player(int x, int y, World world)
        {
            X = x;
            Y = y;
            World = world;
            _playerBounds = new RectangleF(x, y, IRestrictions.TileSize * 0.5f, IRestrictions.TileSize * 0.35f);
        }

        public void Move(OrthographicCamera cam, float x, float y)
        {
            Teleport(cam, x, 0);
            if (World.LoadedCollisions.Values.Any(collisionsValue => collisionsValue.Rectangle.Intersects(_playerBounds)))
            {
                Teleport(cam, -x, 0);
            }

            Teleport(cam, 0, y);
            if (World.LoadedCollisions.Values.Any(collisionsValue => collisionsValue.Rectangle.Intersects(_playerBounds)))
            {
                Teleport(cam, 0, -y);
            }
        }

        private void Teleport(OrthographicCamera cam, float x, float y)
        {
            X += x;
            Y += y;
            _playerBounds.X = X + 0.2f;
            _playerBounds.Y = Y + 1.5f;
            cam.Move(new Vector2(x, y));
        }
    }
}