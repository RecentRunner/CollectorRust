using System;
using System.Collections.Generic;
using Collector.Dimension;
using Humper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended;
using MonoGame.Extended.Collections;
using Collision = Collector.Dimension.Collision;

namespace Collector.Character
{
    public class Player : IRestrictions
    {
        public static int X { get; private set; }
        public static int Y { get; private set; }
        private static Dictionary<string, Rectangle> Animation { get; set; }
        public static Rectangle PlayerBounds;

        public Player(int x, int y)
        {
            PlayerBounds = new Rectangle(X,Y,IRestrictions.TileSize,IRestrictions.TileSize);
            X = x;
            Y = y;
        }

        public static void Move(OrthographicCamera cam, int x, int y)
        {
            X += x;
            Y += y;
            cam.Move(new Vector2(x+IRestrictions.TileSize/2, y+IRestrictions.TileSize/2));
            PlayerBounds.Location = new Point(X,Y);
        }
    }
}