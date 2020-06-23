using System;
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
        private static OrthographicCamera _cam;
        private Texture2D Crosshair { get; set; }

        public PlayerMouse(ContentManager contentManager, SpriteBatch spriteBatch, OrthographicCamera cam)
        {
            _spriteBatch = spriteBatch;
            _cam = cam;
            Crosshair = contentManager.Load<Texture2D>("crosshair");
        }
        public void Draw()
        {
            _spriteBatch.Draw(
                Crosshair,
                new Rectangle(GetSelectedX(),GetSelectedY(),1,1), 
                new Rectangle(0,0,32,32),
                Color.White
                );
        }

        public static int GetSelectedX()
        {
            return (int) Math.Floor(_cam.ScreenToWorld(Mouse.GetState().X,Mouse.GetState().Y).X);
        }

        public static int GetSelectedY()
        {
            return (int) Math.Floor(_cam.ScreenToWorld(Mouse.GetState().X,Mouse.GetState().Y).Y);
        }
    }
}