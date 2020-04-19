package Collector.Character;

import Collector.Main;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Input;
import com.badlogic.gdx.graphics.g2d.Animation;
import com.badlogic.gdx.graphics.g2d.TextureAtlas;
import com.badlogic.gdx.math.Vector3;
import Collector.Dimension.Chunks;
import Collector.Restrictions;

import java.util.HashMap;

public class InputController  implements Restrictions {

    private Player player;
    private HashMap<String, Animation<TextureAtlas.AtlasRegion>> animations;
    private Mouse mouse;
    int i = 0;

    public InputController(Player player, Mouse mouse) {
        this.player = player;
        this.mouse = mouse;
        animations = new HashMap<>();
        animations.put("Up", new Animation<>(1 / 4f, player.getTextureAtlas().findRegions(player.getSpriteName() + "_" + "Up")));
        animations.put("UpRight", new Animation<>(1 / 4f, player.getTextureAtlas().findRegions(player.getSpriteName() + "_" + "UpRight")));
        animations.put("UpLeft", new Animation<>(1 / 4f, player.getTextureAtlas().findRegions(player.getSpriteName() + "_" + "UpLeft")));
        animations.put("Down", new Animation<>(1 / 4f, player.getTextureAtlas().findRegions(player.getSpriteName() + "_" + "Down")));
        animations.put("DownRight", new Animation<>(1 / 4f, player.getTextureAtlas().findRegions(player.getSpriteName() + "_" + "DownRight")));
        animations.put("DownLeft", new Animation<>(1 / 4f, player.getTextureAtlas().findRegions(player.getSpriteName() + "_" + "DownLeft")));
        animations.put("Left", new Animation<>(1 / 4f, player.getTextureAtlas().findRegions(player.getSpriteName() + "_" + "Left")));
        animations.put("Right", new Animation<>(1 / 4f, player.getTextureAtlas().findRegions(player.getSpriteName() + "_" + "Right")));
    }

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
}
