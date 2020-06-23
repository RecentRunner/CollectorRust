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
        private readonly PlayerMouse _playerMouse;
        private readonly OrthographicCamera _cam;
        private readonly Dictionary<string, Rectangle[]> _animations;
        private readonly Texture2D _texture;
        private readonly SpriteBatch _spriteBatch;
        private string Input { get; set; }
        private int _frameNumber;
        private int _timeSinceLastFrame;


        public InputController(PlayerMouse playerMouse, OrthographicCamera cam, SpriteBatch spriteBatch, ContentManager contentManager)
        {
            Input = "Down";
            _playerMouse = playerMouse;
            _cam = cam;
            _spriteBatch = spriteBatch;
            _frameNumber = 0;
            _texture = contentManager.Load<Texture2D>("man");
            const int tileSize = 32;
            _animations = new Dictionary<string, Rectangle[]>
            {
                ["Up"] = new[]
                {
                    new Rectangle(0, 0, tileSize, tileSize),
                    new Rectangle(1, 0, tileSize, tileSize),
                    new Rectangle(2, 0, tileSize, tileSize),
                    new Rectangle(3, 0, tileSize, tileSize)
                },
                ["Right"] = new[]
                {
                    new Rectangle(0 + 4, 1, tileSize, tileSize),
                    new Rectangle(1 + 4, 1, tileSize, tileSize),
                    new Rectangle(2 + 4, 1, tileSize, tileSize),
                    new Rectangle(3 + 4, 1, tileSize, tileSize)
                },
                ["UpRight"] = new[]
                {
                    new Rectangle(0 + 4,  2, tileSize, tileSize),
                    new Rectangle(1 + 4,  2, tileSize, tileSize),
                    new Rectangle(2 + 4,  2, tileSize, tileSize),
                    new Rectangle(3 + 4,  2, tileSize, tileSize)
                },
                ["DownRight"] = new[]
                {
                    new Rectangle(0 + 4,  3, tileSize, tileSize),
                    new Rectangle(1 + 4,  3, tileSize, tileSize),
                    new Rectangle(2 + 4,  3, tileSize, tileSize),
                    new Rectangle(3 + 4,  3, tileSize, tileSize)
                },
                ["Left"] = new[]
                {
                    new Rectangle(0, 1, tileSize, tileSize),
                    new Rectangle(1, 1, tileSize, tileSize),
                    new Rectangle(2, 1, tileSize, tileSize),
                    new Rectangle(3, 1, tileSize, tileSize)
                },
                ["UpLeft"] = new[]
                {
                    new Rectangle(0, 2, tileSize, tileSize),
                    new Rectangle(1, 2, tileSize, tileSize),
                    new Rectangle(2, 2, tileSize, tileSize),
                    new Rectangle(3, 2, tileSize, tileSize)
                },
                ["DownLeft"] = new[]
                {
                    new Rectangle(0, 1, tileSize, tileSize),
                    new Rectangle(1, 1, tileSize, tileSize),
                    new Rectangle(2, 1, tileSize, tileSize),
                    new Rectangle(3, 1, tileSize, tileSize)
                },
                ["Down"] = new[]
                {
                    new Rectangle(0 + 4, 0, tileSize, tileSize),
                    new Rectangle(1 + 4, 0, tileSize, tileSize),
                    new Rectangle(2 + 4, 0, tileSize, tileSize),
                    new Rectangle(3 + 4, 0, tileSize, tileSize)
                },
            };
        }

        public void PlayerInput(Main main, PlayerMouse playerMouse, GameTime gameTime)
        {
            const int movementSpeed = IRestrictions.MovementSpeed;
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                Quit(main);
            }
            if (keyboardState.IsKeyUp(Keys.W) && keyboardState.IsKeyUp(Keys.A) && keyboardState.IsKeyUp(Keys.S) && keyboardState.IsKeyUp(Keys.D))
            {
                Input = Input.Contains("Right") ? "DownRight" : "DownLeft";

                _frameNumber = 0;
                _playerMouse.Draw();
            }
            if (keyboardState.IsKeyDown(Keys.W) && keyboardState.IsKeyDown(Keys.D))
            {
                UpdateAnimationFrame(gameTime);
                Input = "UpRight";
                Player.Move(_cam,movementSpeed,-movementSpeed);
            }
            else if (keyboardState.IsKeyDown(Keys.W) && keyboardState.IsKeyDown(Keys.A))
            {
                UpdateAnimationFrame(gameTime);
                Input = "UpLeft";
                Player.Move(_cam,-movementSpeed,-movementSpeed);
            }
            else if (keyboardState.IsKeyDown(Keys.S) && keyboardState.IsKeyDown(Keys.D))
            {
                UpdateAnimationFrame(gameTime);
                Input = "DownRight";
                Player.Move(_cam,movementSpeed,movementSpeed);
            }
            else if (keyboardState.IsKeyDown(Keys.S) && keyboardState.IsKeyDown(Keys.A))
            {
                UpdateAnimationFrame(gameTime);
                Input = "DownLeft";
                Player.Move(_cam,-movementSpeed,movementSpeed);
            }
            else if (keyboardState.IsKeyDown(Keys.W))
            {
                UpdateAnimationFrame(gameTime);
                Input = "Up";
                Player.Move(_cam,0,-movementSpeed);
            }
            else if (keyboardState.IsKeyDown(Keys.A))
            {
                UpdateAnimationFrame(gameTime);
                Player.Move(_cam,-movementSpeed,0);
                Input = "Left";
            }
            else if (keyboardState.IsKeyDown(Keys.S))
            {
                UpdateAnimationFrame(gameTime);
                Player.Move(_cam,0,movementSpeed);
                Input = "Down";
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                UpdateAnimationFrame(gameTime);
                Player.Move(_cam,movementSpeed,0);
                Input = "Right";
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
            _timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (_timeSinceLastFrame <= 99) return;
            _frameNumber++;
            _timeSinceLastFrame = 0;
            if (_frameNumber > 3)
            {
                _frameNumber = 0;
            }
        }

        private static void Quit(Game main)
        {
            main.Exit();
        }

        public void Draw()
        {
            _spriteBatch.Draw(
                _texture
                ,Player.PlayerBounds
                ,_animations[Input][_frameNumber]
                ,Color.White
                );
        }
    }
}