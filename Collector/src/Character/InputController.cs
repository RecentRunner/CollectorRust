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

public class InputController : IRestrictions {

    private Player _player;
    private Mouse _mouse;
    private OrthographicCamera _cam;
    private Dictionary<string, Rectangle> animations;
    public string Input { get; set; }
    private SpriteBatch _spriteBatch;
    private ContentManager _content;

    public InputController(Player player, Mouse mouse, OrthographicCamera cam, SpriteBatch spriteBatch, ContentManager contentManager)
    {
        _player = player;
        _mouse = mouse;
        _cam = cam;
        _spriteBatch = spriteBatch;
        _content = contentManager;
        Input = "Down";

        animations = new Dictionary<string, Rectangle>
        {
            ["Up"] = new Rectangle(0, 0, 32, 32),
            ["Right"] = new Rectangle(0, 0, 32, 32),
            ["Left"] = new Rectangle(0, 0, 32, 32),
            ["Right"] = new Rectangle(0, 0, 32, 32)
        };
    }

    public void PlayerInput(Main main, SpriteBatch spriteBatch)
    {
        var keyboardState = Keyboard.GetState();
        const int movementSpeed = IRestrictions.MovementSpeed;
        if (keyboardState.IsKeyDown(Keys.Escape))
        {
            Quit(main);
        }
        if (keyboardState.IsKeyDown(Keys.W))
        {
            Input = "Up";
            Player.Y += -movementSpeed;
            _cam.Move(new Vector2(0,-movementSpeed));
        }
        if (keyboardState.IsKeyDown(Keys.A))
        {
            Player.X += -movementSpeed;
            _cam.Move(new Vector2(-movementSpeed,0));
            Input = "Left";
        }
        if (keyboardState.IsKeyDown(Keys.S))
        {
            Player.Y += movementSpeed;
            _cam.Move(new Vector2(0,movementSpeed));
            Input = "Down";
        }

        if (keyboardState.IsKeyDown(Keys.D))
        {
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

    private static void Quit(Game main)
    {
        main.Exit();
    }
    
    /*

public void handleInput() {
    i++;
    if (Gdx.input.isKeyPressed(Input.Keys.Q)) {
        Main.cam.zoom += 5;
    }
    if (Gdx.input.isKeyPressed(Input.Keys.E)) {
        Main.cam.zoom -= 5;
    }
    if ((Gdx.input.isButtonJustPressed(Input.Buttons.LEFT) || Gdx.input.isButtonJustPressed(Input.Buttons.RIGHT) )&& i > KEY_DELAY) {
        Vector3 mousePos = new Vector3(Gdx.input.getX(), Gdx.input.getY(), 0);
        Main.cam.unproject(mousePos);
        int x = mouse.getSelectedX(mousePos) >> TILE_SHIFT;
        int y = mouse.getSelectedY(mousePos) >> TILE_SHIFT;
        if(Gdx.input.isButtonJustPressed(Input.Buttons.LEFT)) {
            i = 0;
            Chunks.placeBlock(x, y, "wood");
        }
        if (Gdx.input.isButtonJustPressed(Input.Buttons.RIGHT)) {
            i = 0;
            Chunks.removeBlock(x,y);
        }
    }

    if (Gdx.input.isKeyPressed(Input.Keys.W) && Gdx.input.isKeyPressed(Input.Keys.A) && i > KEY_DELAY) {
        i = 0;
        player.setAnimation(animations.get("UpLeft"));
        Player.addX(-MOVEMENT_SPEED);
        Player.addY(MOVEMENT_SPEED);
        Main.cam.translate(-MOVEMENT_SPEED, MOVEMENT_SPEED);
    }
    else if (Gdx.input.isKeyPressed(Input.Keys.W) && Gdx.input.isKeyPressed(Input.Keys.D) && i > KEY_DELAY) {
        i = 0;
        player.setAnimation(animations.get("UpRight"));
        Player.addX(MOVEMENT_SPEED);
        Player.addY(MOVEMENT_SPEED);
        Main.cam.translate(MOVEMENT_SPEED, MOVEMENT_SPEED);
    }
    else if (Gdx.input.isKeyPressed(Input.Keys.S) && Gdx.input.isKeyPressed(Input.Keys.A) && i > KEY_DELAY) {
        i = 0;
        player.setAnimation(animations.get("DownLeft"));
        Player.addX(-MOVEMENT_SPEED);
        Player.addY(-MOVEMENT_SPEED);
        Main.cam.translate(-MOVEMENT_SPEED, -MOVEMENT_SPEED);
    }
    else if (Gdx.input.isKeyPressed(Input.Keys.S) && Gdx.input.isKeyPressed(Input.Keys.D) && i > KEY_DELAY) {
        i = 0;
        player.setAnimation(animations.get("DownRight"));
        Player.addX(MOVEMENT_SPEED);
        Player.addY(-MOVEMENT_SPEED);

        Main.cam.translate(MOVEMENT_SPEED, -MOVEMENT_SPEED);
    }
    else if (Gdx.input.isKeyPressed(Input.Keys.A) && i > KEY_DELAY) {
        i = 0;
        player.setAnimation(animations.get("Left"));
        Player.addX(-MOVEMENT_SPEED);
        Main.cam.translate(-MOVEMENT_SPEED, 0);
    }
    else if (Gdx.input.isKeyPressed(Input.Keys.D) && i > KEY_DELAY) {
        i = 0;
        player.setAnimation(animations.get("Right"));
        Player.addX(MOVEMENT_SPEED);
        Main.cam.translate(MOVEMENT_SPEED, 0);
    }
    else if (Gdx.input.isKeyPressed(Input.Keys.S) && i > KEY_DELAY) {
        i = 0;
        player.setAnimation(animations.get("Down"));
        Player.addY(-MOVEMENT_SPEED);
        Main.cam.translate(0, -MOVEMENT_SPEED);
    }
    if (Gdx.input.isKeyPressed(Input.Keys.W) && i > KEY_DELAY) {
        i = 0;
        player.setAnimation(animations.get("Up"));
        Player.addY(MOVEMENT_SPEED);
        Main.cam.translate(0, MOVEMENT_SPEED);
    }
    Main.cam.update();
   
    }
     */

    public void Draw()
    {
        var texture = _content.Load<Texture2D>("man/man-0");
        _spriteBatch.Draw(texture,new Vector2(Player.X,Player.Y),Color.White);
    }
}
