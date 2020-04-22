using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace Collector.Character
{
    public class Mouse : IRestrictions
    {
        private SpriteBatch _spriteBatch;
        private ContentManager _contentManager;
        private OrthographicCamera _cam;
        private Texture2D Crosshair { get; set; }

        public Mouse(ContentManager contentManager, SpriteBatch spriteBatch, OrthographicCamera cam)
        {
            _spriteBatch = spriteBatch;
            _cam = cam;
            _contentManager = contentManager;
            Crosshair = _contentManager.Load<Texture2D>("crosshair");
        }

        public void Draw()
        {
            _spriteBatch.Draw(
                Crosshair,
                new Vector2(
                    GetSelectedX(Microsoft.Xna.Framework.Input.Mouse.GetState().X-(IRestrictions.TileSize/2f)) + (_cam.Position.X)+(IRestrictions.TileSize/2f), 
                    GetSelectedY(Microsoft.Xna.Framework.Input.Mouse.GetState().Y-(IRestrictions.TileSize/2f)) + (_cam.Position.Y)+(IRestrictions.TileSize/2f)
                    ),
                Color.White
            );
        }


        private static int GetSelectedX(float x)
        {
            return ((int) x >> 5) << 5;
        }

        private static int GetSelectedY(float y)
        {
            return ((int) y >> 5) << 5;
        }
    }
}