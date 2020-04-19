using Collector;
using Collector.Dimension;

public class World {
    public static void generateWorld(int x, int y) {
        if (Chunks.IsEmpty(x, y)) {
            Chunks.GenerateChunk(x, y);
        }
    }

    public static void ungenerateWorld(int x, int y) {
        if (!Chunks.IsEmpty(x, y)) {
            Chunks.UngenerateChunk(x, y);
        }
    }

    public static void loadChunks() {
        for (var i = -(IRestrictions.RenderDistance); i < IRestrictions.RenderDistance; i++) {
            for (var j = -(IRestrictions.RenderDistance); j < IRestrictions.RenderDistance; j++) {
                generateWorld(
                        i + Player.getX() / (IRestrictions.TileSize * IRestrictions.SuperChunkSize * IRestrictions.ChunkSize),
                        j + Player.getY() / (IRestrictions.TileSize * IRestrictions.SuperChunkSize * IRestrictions.ChunkSize)
                );
            }
        }
    }

    public static void unloadChunks() {
        for (var i = -IRestrictions.RenderDistance; i < IRestrictions.RenderDistance+1; i++) {
            //Down
            ungenerateWorld(
                    Player.getX() / (IRestrictions.TileSize * IRestrictions.SuperChunkSize * IRestrictions.ChunkSize)+i,
                    (Player.getY() / (IRestrictions.TileSize * IRestrictions.SuperChunkSize * IRestrictions.ChunkSize))+3
            );
            //Up
            ungenerateWorld(
                    Player.getX() / (IRestrictions.TileSize * IRestrictions.SuperChunkSize * IRestrictions.ChunkSize)+i-1,
                    (Player.getY() / (IRestrictions.TileSize * IRestrictions.SuperChunkSize * IRestrictions.ChunkSize))-4
            );
            //Right
            ungenerateWorld(
                    Player.getX() / (IRestrictions.TileSize * IRestrictions.SuperChunkSize * IRestrictions.ChunkSize)-4,
                    (Player.getY() / (IRestrictions.TileSize * IRestrictions.SuperChunkSize * IRestrictions.ChunkSize))+i
            );
            //Left
            ungenerateWorld(
                    Player.getX() / (IRestrictions.TileSize * IRestrictions.SuperChunkSize * IRestrictions.ChunkSize)+3,
                    (Player.getY() / (IRestrictions.TileSize * IRestrictions.SuperChunkSize * IRestrictions.ChunkSize))+i
            );
        }

    }
}
