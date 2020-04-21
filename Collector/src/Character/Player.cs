using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Collector.Character
{
    public class Player : IRestrictions
    {
        public static int X { get; set; }
        public static int Y { get; set; }
        public Dictionary<string,Rectangle> Animation { get; set; }
        //private Inventory PlayerInventory { get; }

        public Player(int x, int y) {
            X = x<<IRestrictions.TileShift;
            Y = y<<IRestrictions.TileShift;
            //PlayerInventory = new Inventory();
        }
    }
}
