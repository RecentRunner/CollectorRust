using System.Collections.Generic;
using Collector;
using Collector.Character;
using Collector.Dimension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Mouse = Collector.Character.Mouse;

public class InputController : IRestrictions
{
    private Player _player;
    private Mouse _mouse;
    private OrthographicCamera _cam;
    private Dictionary<string, Rectangle[]> _animations;
    private string Input { get; set; }
    private readonly SpriteBatch _spriteBatch;
    private readonly Texture2D _texture;
    private int frameNumber;
    private int timeSinceLastFrame;


    public InputController(Player player, Mouse mouse, OrthographicCamera cam, SpriteBatch spriteBatch,
        ContentManager contentManager)
    {
        _player = player;
        _mouse = mouse;
        _cam = cam;
        _spriteBatch = spriteBatch;
        frameNumber = 0;
        _texture = contentManager.Load<Texture2D>("man");
        Input = "Down";


        _animations = new Dictionary<string, Rectangle[]>
        {
            ["Up"] = new Rectangle[4]
            {
                new Rectangle(32*0, 0, 32, 32),
                new Rectangle(32*1, 0, 32, 32),
                new Rectangle(32*2, 0, 32, 32),
                new Rectangle(32*3, 0, 32, 32)
            },
            ["Right"] = new Rectangle[4]
            {
                new Rectangle(32*0 + 32*4, 32, 32, 32),
                new Rectangle(32*1 + 32*4, 32, 32, 32),
                new Rectangle(32*2 + 32*4, 32, 32, 32),
                new Rectangle(32*3 + 32*4, 32, 32, 32)
            },
            ["UpRight"] = new Rectangle[4]
            {
                new Rectangle(32*0 + 32*4, 32*2, 32, 32),
                new Rectangle(32*1 + 32*4, 32*2, 32, 32),
                new Rectangle(32*2 + 32*4, 32*2, 32, 32),
                new Rectangle(32*3 + 32*4, 32*2, 32, 32)
            },
            ["DownRight"] = new Rectangle[4]
            {
                new Rectangle(32*0 + 32*4, 32*3, 32, 32),
                new Rectangle(32*1 + 32*4, 32*3, 32, 32),
                new Rectangle(32*2 + 32*4, 32*3, 32, 32),
                new Rectangle(32*3 + 32*4, 32*3, 32, 32)
            },
            ["Left"] = new Rectangle[4]
            {
                new Rectangle(32*0, 32, 32, 32),
                new Rectangle(32*1, 32, 32, 32),
                new Rectangle(32*2, 32, 32, 32),
                new Rectangle(32*3, 32, 32, 32)
            },
            ["UpLeft"] = new Rectangle[4]
            {
                new Rectangle(32*0, 32*2, 32, 32),
                new Rectangle(32*1, 32*2, 32, 32),
                new Rectangle(32*2, 32*2, 32, 32),
                new Rectangle(32*3, 32*2, 32, 32)
            },
            ["DownLeft"] = new Rectangle[4]
            {
                new Rectangle(32*0, 32, 32, 32),
                new Rectangle(32*1, 32, 32, 32),
                new Rectangle(32*2, 32, 32, 32),
                new Rectangle(32*3, 32, 32, 32)
            },
            ["Down"] = new Rectangle[4]
            {
                new Rectangle(32*0 + 32*4, 0, 32, 32),
                new Rectangle(32*1 + 32*4, 0, 32, 32),
                new Rectangle(32*2 + 32*4, 0, 32, 32),
                new Rectangle(32*3 + 32*4, 0, 32, 32)
            },
        };
    }

    public void PlayerInput(Main main, Mouse mouse, GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();
        const int movementSpeed = IRestrictions.MovementSpeed;
        if (keyboardState.IsKeyDown(Keys.Escape))
        {
            Quit(main);
        }

        if (keyboardState.IsKeyUp(Keys.W) && keyboardState.IsKeyUp(Keys.A) && keyboardState.IsKeyUp(Keys.S) && keyboardState.IsKeyUp(Keys.D))
        {
            Input = "Down";
            frameNumber = 0;
            _mouse.Draw();
        }

        if (keyboardState.IsKeyDown(Keys.W) && keyboardState.IsKeyDown(Keys.D))
        {
            updateAnimationFrame(gameTime);
            Input = "UpRight";
            Player.Y += -movementSpeed;
            Player.X += movementSpeed;
            _cam.Move(new Vector2(movementSpeed, -movementSpeed));
        }
        
        else if (keyboardState.IsKeyDown(Keys.W) && keyboardState.IsKeyDown(Keys.A))
        {
            updateAnimationFrame(gameTime);
            Input = "UpLeft";
            Player.Y += -movementSpeed;
            Player.X += -movementSpeed;
            _cam.Move(new Vector2(-movementSpeed, -movementSpeed));
        }

        else if (keyboardState.IsKeyDown(Keys.S) && keyboardState.IsKeyDown(Keys.D))
        {
            updateAnimationFrame(gameTime);
            Input = "DownRight";
            Player.Y += movementSpeed;
            Player.X += movementSpeed;
            _cam.Move(new Vector2(movementSpeed, movementSpeed));
        }
        else if (keyboardState.IsKeyDown(Keys.S) && keyboardState.IsKeyDown(Keys.A))
        {
            updateAnimationFrame(gameTime);
            Input = "DownLeft";
            Player.Y += movementSpeed;
            Player.X += -movementSpeed;
            _cam.Move(new Vector2(-movementSpeed, movementSpeed));
        }
        else if (keyboardState.IsKeyDown(Keys.W))
        {
            updateAnimationFrame(gameTime);
            Input = "Up";
            Player.Y += -movementSpeed;
            _cam.Move(new Vector2(0, -movementSpeed));
        }
        else if (keyboardState.IsKeyDown(Keys.A))
        {
            updateAnimationFrame(gameTime);
            Player.X += -movementSpeed;
            _cam.Move(new Vector2(-movementSpeed, 0));
            Input = "Left";
        }
        else if (keyboardState.IsKeyDown(Keys.S))
        {
            updateAnimationFrame(gameTime);
            Player.Y += movementSpeed;
            _cam.Move(new Vector2(0, movementSpeed));
            Input = "Down";
        }
        else if (keyboardState.IsKeyDown(Keys.D))
        {
            updateAnimationFrame(gameTime);
            Player.X += movementSpeed;
            _cam.Move(new Vector2(movementSpeed, 0));
            Input = "Right";
        }
        if (keyboardState.IsKeyDown(Keys.Q))
        {
            _cam.ZoomIn(0.01f);
        }
        if (keyboardState.IsKeyDown(Keys.E))
        {
            _cam.ZoomOut(0.01f);
        }
    }

    private void updateAnimationFrame(GameTime gameTime)
    {
        timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
        if (timeSinceLastFrame <= 99) return;
        frameNumber++;
        timeSinceLastFrame = 0;
        if (frameNumber > 3)
        {
            frameNumber = 0;
        }
    }

    private static void Quit(Game main)
    {
        main.Exit();
    }

    public void Draw()
    {
        _spriteBatch.Draw(_texture, new Vector2(Player.X, Player.Y), _animations[Input][frameNumber], Color.White);
    }
}