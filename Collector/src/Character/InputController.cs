using System.Collections.Generic;
using Collector;
using Collector.Dimension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class InputController : IRestrictions {

    private Player player;
    private Mouse mouse;
    //private Dictionary<string, Animation<TextureAtlas.AtlasRegion>> animations;
    int i = 0;

    public InputController(Player player, Mouse mouse)
    {
        this.player = player;
        this.mouse = mouse;
    }

    public InputController() {

        /*
        animations = new HashMap<>();
        animations.put("Up", new Animation<>(1 / 4f, player.getTextureAtlas().findRegions(player.getSpriteName() + "_" + "Up")));
        animations.put("UpRight", new Animation<>(1 / 4f, player.getTextureAtlas().findRegions(player.getSpriteName() + "_" + "UpRight")));
        animations.put("UpLeft", new Animation<>(1 / 4f, player.getTextureAtlas().findRegions(player.getSpriteName() + "_" + "UpLeft")));
        animations.put("Down", new Animation<>(1 / 4f, player.getTextureAtlas().findRegions(player.getSpriteName() + "_" + "Down")));
        animations.put("DownRight", new Animation<>(1 / 4f, player.getTextureAtlas().findRegions(player.getSpriteName() + "_" + "DownRight")));
        animations.put("DownLeft", new Animation<>(1 / 4f, player.getTextureAtlas().findRegions(player.getSpriteName() + "_" + "DownLeft")));
        animations.put("Left", new Animation<>(1 / 4f, player.getTextureAtlas().findRegions(player.getSpriteName() + "_" + "Left")));
        animations.put("Right", new Animation<>(1 / 4f, player.getTextureAtlas().findRegions(player.getSpriteName() + "_" + "Right")));
        */
    }
    
    private void PlayerInput()
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            Exit();
        }
        else if(Keyboard.GetState().IsKeyDown(Keys.W))
        {
            
        }
        else if(Keyboard.GetState().IsKeyDown(Keys.A))
        {
            
        }
        else if(Keyboard.GetState().IsKeyDown(Keys.S))
        {
            
        }
        else if(Keyboard.GetState().IsKeyDown(Keys.D))
        {
            
        }
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
    
}
