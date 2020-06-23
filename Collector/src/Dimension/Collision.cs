using Microsoft.Xna.Framework;

namespace Collector.Dimension
{
    public class Collision
    {
        private readonly int _tileWidth;
        private bool _entity;
        public Rectangle Rectangle { get; private set; }
        
        public Collision(int x, int y,int tileWidth, bool entity)
        {
            _entity = entity;
            _tileWidth = tileWidth;
            Rectangle = new Rectangle(x,y,tileWidth,tileWidth);
        }
    }
}