using Collector;

public class Player : IRestrictions {
    private static int X;
    private static int Y;
    private string SpriteName;
    private Inventory playerInventory;

    public Player(int x, int y) {
        //Player location
        X = x<<IRestrictions.TileShift;
        Y = y<<IRestrictions.TileShift;

        //Player Inventory
        playerInventory = new Inventory();

        //Player animation
        //spriteName = "man";
        //textureAtlas = new TextureAtlas("man.atlas");
        //Array<TextureAtlas.AtlasRegion> keyFrames = textureAtlas.findRegions(spriteName + "_Down");
        //float frameDuration = 1 / 4f;
        //animation = new Animation<>(frameDuration, keyFrames);
    }

    public static int getX() {
        return X;
    }

    public static int getY() {
        return Y;
    }

    public static void addX(int x){
        X += x;
    }

    public static void addY(int y){
        Y += y;
    }

    /*
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
        //textureAtlas.dispose();
    }

    public TextureAtlas getTextureAtlas() {
        return textureAtlas;
    }
    */
}
