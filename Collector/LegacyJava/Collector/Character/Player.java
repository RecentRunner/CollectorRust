package Collector.Character;

import Collector.Restrictions;
import Collector.Dimension.Inventory;
import com.badlogic.gdx.graphics.g2d.Animation;
import com.badlogic.gdx.graphics.g2d.TextureAtlas;
import com.badlogic.gdx.utils.Array;

public class Player implements Restrictions {
    public static int x;
    public static int y;
    private String spriteName;
    private TextureAtlas textureAtlas;
    private Animation<TextureAtlas.AtlasRegion> animation;
    private Inventory playerInventory;

    public Player(int x, int y) {
        //Player location
        Player.x = x<<TILE_SHIFT;
        Player.y = y<<TILE_SHIFT;

        //Player Inventory
        playerInventory = new Inventory();

        //Player animation
        spriteName = "man";
        textureAtlas = new TextureAtlas("man.atlas");
        Array<TextureAtlas.AtlasRegion> keyFrames = textureAtlas.findRegions(spriteName + "_Down");
        float frameDuration = 1 / 4f;
        animation = new Animation<>(frameDuration, keyFrames);
    }

    public static int getX() {
        return x;
    }

    public static int getY() {
        return y;
    }

    public static void addX(int x){
        Player.x += x;
    }

    public static void addY(int y){
        Player.y += y;
    }

    public String getSpriteName() {
        return spriteName;
    }

    public Animation<TextureAtlas.AtlasRegion> getAnimation() {
        return animation;
    }

    public void setAnimation(Animation<TextureAtlas.AtlasRegion> animationTemp) {
        animation = animationTemp;
    }

    public void dispose(){
        textureAtlas.dispose();
    }

    public TextureAtlas getTextureAtlas() {
        return textureAtlas;
    }
}
