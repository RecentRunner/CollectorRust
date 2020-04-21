using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Collector.Character
{
    public class Mouse : IRestrictions {
        private SpriteBatch _spriteBatch;
        private ContentManager _contentManager;
        private Texture2D Crosshair { get; set; }

        public Mouse(ContentManager contentManager, SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
            _contentManager = contentManager;
            Crosshair = _contentManager.Load<Texture2D>("crosshair");
        }

        public void Draw()
        {
            _spriteBatch.Draw(Crosshair, new Vector2(Player.X, Player.Y),Color.Aqua);
        }

        
        public int GetSelectedX(Vector3 mousePos) {
            return ((int)(mousePos.X)>>4)<<4;
        }

        public int GetSelectedY(Vector3 mousePos) {
            return ((int)(mousePos.Y)>>4)<<4;
        }
    }
}
