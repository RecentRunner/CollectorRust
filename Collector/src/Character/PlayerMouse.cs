using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace Collector.Character
{
    public class PlayerMouse : IRestrictions
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly OrthographicCamera _cam;
        private Texture2D Crosshair { get; set; }

        public PlayerMouse(ContentManager contentManager, SpriteBatch spriteBatch, OrthographicCamera cam)
        {
            _spriteBatch = spriteBatch;
            _cam = cam;
            Crosshair = contentManager.Load<Texture2D>("crosshair");
        }

        public void Draw()
        {
            _spriteBatch.Draw(Crosshair,new Vector2(GetSelectedX(),GetSelectedY()), Color.White);
        }
        
        public int GetSelectedX()
        {
            return ((int) (Mouse.GetState().X + _cam.Position.X) >> 5) << 5;
        }

        public int GetSelectedY()
        {
            return ((int) (Mouse.GetState().Y + _cam.Position.Y) >> 5) << 5;
        }
        
        public int GetSelectedXTile()
        {
            return (int) (Mouse.GetState().X + _cam.Position.X) >> 5;
        }

        public int GetSelectedYTile()
        {
            return (int) (Mouse.GetState().Y + _cam.Position.Y) >> 5;
        }
    }
}