using System.Collections.Generic;
using Collector.Dimension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace Collector.Character
{
    public class InputController : IRestrictions
    {
        private readonly OrthographicCamera _cam;
        private readonly Dictionary<string, Rectangle[]> _animations;
        private readonly Texture2D _texture;
        private readonly SpriteBatch _spriteBatch;
        private string Input { get; set; }
        private int _frameNumber;
        private float _timeSinceLastFrame;


        public InputController(OrthographicCamera cam, SpriteBatch spriteBatch, ContentManager contentManager)
        {
            Input = "Down";
            _cam = cam;
            _spriteBatch = spriteBatch;
            _frameNumber = 0;
            _texture = contentManager.Load<Texture2D>("man");
            const int tileSize = 32;
            _animations = new Dictionary<string, Rectangle[]>
            {
                ["Idle"] = new[]
                {
                    new Rectangle((0 + 4)*tileSize, 0, tileSize, tileSize*2),
                    new Rectangle((0 + 4)*tileSize, 0, tileSize, tileSize*2),
                    new Rectangle((0 + 4)*tileSize, 0, tileSize, tileSize*2),
                    new Rectangle((0 + 4)*tileSize, 0, tileSize, tileSize*2),
                },
                ["Up"] = new[]
                {
                    new Rectangle(0*tileSize, 0, tileSize, tileSize*2),
                    new Rectangle(1*tileSize, 0, tileSize, tileSize*2),
                    new Rectangle(2*tileSize, 0, tileSize, tileSize*2),
                    new Rectangle(3*tileSize, 0, tileSize, tileSize*2)
                },
                ["Right"] = new[]
                {
                    new Rectangle((0 + 4)*tileSize, 2*tileSize, tileSize, tileSize*2),
                    new Rectangle((1 + 4)*tileSize, 2*tileSize, tileSize, tileSize*2),
                    new Rectangle((2 + 4)*tileSize, 2*tileSize, tileSize, tileSize*2),
                    new Rectangle((3 + 4)*tileSize, 2*tileSize, tileSize, tileSize*2)
                },
                ["UpRight"] = new[]
                {
                    new Rectangle((0 + 4)*tileSize,  4*tileSize, tileSize, tileSize*2),
                    new Rectangle((1 + 4)*tileSize,  4*tileSize, tileSize, tileSize*2),
                    new Rectangle((2 + 4)*tileSize,  4*tileSize, tileSize, tileSize*2),
                    new Rectangle((3 + 4)*tileSize,  4*tileSize, tileSize, tileSize*2)
                },
                ["DownRight"] = new[]
                {
                    new Rectangle((0 + 4)*tileSize,  6*tileSize, tileSize, tileSize*2),
                    new Rectangle((1 + 4)*tileSize,  6*tileSize, tileSize, tileSize*2),
                    new Rectangle((2 + 4)*tileSize,  6*tileSize, tileSize, tileSize*2),
                    new Rectangle((3 + 4)*tileSize,  6*tileSize, tileSize, tileSize*2)
                },
                ["Left"] = new[]
                {
                    new Rectangle(0*tileSize, 2*tileSize, tileSize, tileSize*2),
                    new Rectangle(1*tileSize, 2*tileSize, tileSize, tileSize*2),
                    new Rectangle(2*tileSize, 2*tileSize, tileSize, tileSize*2),
                    new Rectangle(3*tileSize, 2*tileSize, tileSize, tileSize*2)
                },
                ["UpLeft"] = new[]
                {
                    new Rectangle(0*tileSize, 4*tileSize, tileSize, tileSize*2),
                    new Rectangle(1*tileSize, 4*tileSize, tileSize, tileSize*2),
                    new Rectangle(2*tileSize, 4*tileSize, tileSize, tileSize*2),
                    new Rectangle(3*tileSize, 4*tileSize, tileSize, tileSize*2)
                },
                ["DownLeft"] = new[]
                {
                    new Rectangle(0*tileSize, 6*tileSize, tileSize, tileSize*2),
                    new Rectangle(1*tileSize, 6*tileSize, tileSize, tileSize*2),
                    new Rectangle(2*tileSize, 6*tileSize, tileSize, tileSize*2),
                    new Rectangle(3*tileSize, 6*tileSize, tileSize, tileSize*2)
                },
                ["Down"] = new[]
                {
                    new Rectangle((0 + 4)*tileSize, 0, tileSize, tileSize*2),
                    new Rectangle((1 + 4)*tileSize, 0, tileSize, tileSize*2),
                    new Rectangle((2 + 4)*tileSize, 0, tileSize, tileSize*2),
                    new Rectangle((3 + 4)*tileSize, 0, tileSize, tileSize*2)
                },
            };
        }

        public void PlayerInput(Main main, GameTime gameTime)
        {
            const float movementSpeed = IRestrictions.MovementSpeed;
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                Quit(main);
            }

            if (keyboardState.IsKeyUp(Keys.W) && keyboardState.IsKeyUp(Keys.A) && keyboardState.IsKeyUp(Keys.S) &&
                keyboardState.IsKeyUp(Keys.D))
            {
                Input = "Idle";
            }
            UpdateAnimationFrame(gameTime);
            const float diagonalMovement = 0.707f;
            if (keyboardState.IsKeyDown(Keys.W) && keyboardState.IsKeyDown(Keys.D))
            {
                Input = "UpRight";
                Player.Move(_cam,movementSpeed*diagonalMovement,-movementSpeed*diagonalMovement);
            }
            else if (keyboardState.IsKeyDown(Keys.W) && keyboardState.IsKeyDown(Keys.A))
            {
                Player.Move(_cam,-movementSpeed*diagonalMovement,-movementSpeed*diagonalMovement);
                Input = "UpLeft";
            }
            else if (keyboardState.IsKeyDown(Keys.S) && keyboardState.IsKeyDown(Keys.D))
            {
                Input = "DownRight";
                Player.Move(_cam,movementSpeed*diagonalMovement,movementSpeed*diagonalMovement);
            }
            else if (keyboardState.IsKeyDown(Keys.S) && keyboardState.IsKeyDown(Keys.A))
            {
                Input = "DownLeft";
                Player.Move(_cam,-movementSpeed*diagonalMovement,movementSpeed*diagonalMovement);
            }
            else if (keyboardState.IsKeyDown(Keys.W))
            {
                Input = "Up";
                Player.Move(_cam,0,-movementSpeed);
            }
            else if (keyboardState.IsKeyDown(Keys.A))
            {
                Input = "Left";
                Player.Move(_cam,-movementSpeed,0);
            }
            else if (keyboardState.IsKeyDown(Keys.S))
            {
                Input = "Down";
                Player.Move(_cam,0,movementSpeed);
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                Input = "Right";
                Player.Move(_cam,movementSpeed,0);
            }
            if (keyboardState.IsKeyDown(Keys.Q))
            {
                _cam.ZoomIn(1f);
            }
            if (keyboardState.IsKeyDown(Keys.E))
            {
                _cam.ZoomOut(1f);
            }
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                Chunks.RemoveBlock(PlayerMouse.GetSelectedX(),PlayerMouse.GetSelectedY());
            }
            if (mouseState.RightButton == ButtonState.Pressed)
            {
                Chunks.PlaceBlock(PlayerMouse.GetSelectedX(),PlayerMouse.GetSelectedY(),Blocks.BlockWood);
            }
        }

        private void UpdateAnimationFrame(GameTime gameTime)
        {
            _timeSinceLastFrame += gameTime.GetElapsedSeconds();
            if (_timeSinceLastFrame <= IRestrictions.AnimationDuration) return;
            _frameNumber++;
            _timeSinceLastFrame = 0;
            if (_frameNumber > 3) _frameNumber = 0;
        }

        private static void Quit(Game main)
        {
            main.Exit();
        }

        public void Draw()
        {
            _spriteBatch.Draw(
                _texture,
                new Vector2(Player.X,Player.Y),
                _animations[Input][_frameNumber],
                Color.White, 0f,
                Vector2.One, 
                1/32f,
                SpriteEffects.None,
                1f);
        }
    }
}