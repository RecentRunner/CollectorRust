
using Humper.Base;

namespace Collector.Character
{
    public class Collision
    {
        private readonly int _tileWidth;
        public RectangleF Rectangle { get; private set; }
        
        public Collision(int x, int y,int tileWidth)
        {
            _tileWidth = tileWidth;
            Rectangle = new RectangleF(x,y,tileWidth,tileWidth);
        }
    }
}