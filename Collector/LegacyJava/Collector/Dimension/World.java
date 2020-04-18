package Collector.Dimension;

import Collector.Character.Player;

import static Collector.Restrictions.*;

public class World {
    public static void generateWorld(int x, int y) {
        if (Chunks.isEmpty(x, y)) {
            Chunks.generateChunk(x, y);
        }
    }

    public static void ungenerateWorld(int x, int y) {
        if (!Chunks.isEmpty(x, y)) {
            Chunks.ungenerateChunk(x, y);
        }
    }

    public static void loadChunks() {
        for (int i = -(RENDER_DISTANCE); i < RENDER_DISTANCE; i++) {
            for (int j = -(RENDER_DISTANCE); j < RENDER_DISTANCE; j++) {
                generateWorld(
                        i + Player.getX() / (TILE_SIZE * SUPER_CHUNK_SIZE * CHUNK_SIZE),
                        j + Player.getY() / (TILE_SIZE * SUPER_CHUNK_SIZE * CHUNK_SIZE)
                );
            }
        }
    }

    public static void unloadChunks() {
        for (int i = -RENDER_DISTANCE; i < RENDER_DISTANCE+1; i++) {
            //Down
            ungenerateWorld(
                    Player.getX() / (TILE_SIZE * SUPER_CHUNK_SIZE * CHUNK_SIZE)+i,
                    (Player.getY() / (TILE_SIZE * SUPER_CHUNK_SIZE * CHUNK_SIZE))+3
            );
            //Up
            ungenerateWorld(
                    Player.getX() / (TILE_SIZE * SUPER_CHUNK_SIZE * CHUNK_SIZE)+i-1,
                    (Player.getY() / (TILE_SIZE * SUPER_CHUNK_SIZE * CHUNK_SIZE))-4
            );
            //Right
            ungenerateWorld(
                    Player.getX() / (TILE_SIZE * SUPER_CHUNK_SIZE * CHUNK_SIZE)-4,
                    (Player.getY() / (TILE_SIZE * SUPER_CHUNK_SIZE * CHUNK_SIZE))+i
            );
            //Left
            ungenerateWorld(
                    Player.getX() / (TILE_SIZE * SUPER_CHUNK_SIZE * CHUNK_SIZE)+3,
                    (Player.getY() / (TILE_SIZE * SUPER_CHUNK_SIZE * CHUNK_SIZE))+i
            );
        }

    }
}
