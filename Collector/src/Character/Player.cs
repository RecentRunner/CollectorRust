using System;
using System.Linq;
using Collector.Dimension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using RectangleF = Humper.Base.RectangleF;
using Vector2 = Microsoft.Xna.Framework.Vector2;


namespace Collector.Character
{
    public class Player : IRestrictions
    {
        public static float X { get; private set; }
        public static float Y { get; private set; }

        public static RectangleF PlayerBounds;

        public Player(int x, int y)
        {
            X = x;
            Y = y;
            PlayerBounds = new RectangleF(x, y, IRestrictions.TileSize * 0.5f, IRestrictions.TileSize * 0.35f);
        }

        public static void Move(OrthographicCamera cam, float x, float y)
        {
            Teleport(cam, x, 0);
            if (Chunks.LoadedCollisions.Values.Any(collisionsValue => collisionsValue.Rectangle.Intersects(PlayerBounds)))
            {
                Teleport(cam, -x, 0);
            }

            Teleport(cam, 0, y);
            if (Chunks.LoadedCollisions.Values.Any(collisionsValue => collisionsValue.Rectangle.Intersects(PlayerBounds)))
            {
                Teleport(cam, 0, -y);
            }
        }

        private static void Teleport(OrthographicCamera cam, float x, float y)
        {
            X += x;
            Y += y;
            PlayerBounds.X = X + 0.2f;
            PlayerBounds.Y = Y + 1.5f;
            cam.Move(new Vector2(x, y));
        }
    }
}