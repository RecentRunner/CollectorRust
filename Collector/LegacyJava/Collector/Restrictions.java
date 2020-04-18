package Collector;

public interface Restrictions {
    int SEED = 1;

    float VIEWPORT_HEIGHT = 9/2f;
    float VIEWPORT_WIDTH = 16/2f;

    int MOVEMENT_SPEED = 16;
    int FREE_SPEED = 2;
    int KEY_DELAY = 0;

    int TILE_SIZE = 16;
    int CHUNK_SIZE = 8;
    int SUPER_CHUNK_SIZE = 1;

    int TILE_SHIFT = (int)(Math.log(TILE_SIZE)/Math.log(2));
    int CHUNK_SHIFT = (int)(Math.log(CHUNK_SIZE)/Math.log(2));
    //int SUPER_CHUNK_SHIFT = (int)(Math.log(SUPER_CHUNK_SIZE)/Math.log(2));

    int RENDER_DISTANCE = 3;
    float RENDER_TIME = 1/60f;
}
