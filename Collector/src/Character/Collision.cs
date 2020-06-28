
using Humper.Base;

namespace Collector.Character
{
    public class Collision
    {
        public RectangleF Rectangle { get; }
        
        public Collision(int x, int y)
        {
            Rectangle = new RectangleF(x,y,IRestrictions.TileSize,IRestrictions.TileSize);
        }
    }
}