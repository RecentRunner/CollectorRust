using System;
using Collector.Character;

namespace Collector.Dimension
{
    public static class World {
        private static void GenerateWorld(int x, int y) {
            if (Chunks.IsEmpty(x, y)) {
                Chunks.GenerateChunk(x, y);
            }
        }

        private static void UngenerateWorld(int x, int y) {
            if (!Chunks.IsEmpty(x, y)) {
                Chunks.UngenerateChunk(x, y);
            }
        }

        public static void LoadChunks()
        {
            for (var i = -(IRestrictions.RenderDistance); i < IRestrictions.RenderDistance; i++) {
                for (var j = -(IRestrictions.RenderDistance); j < IRestrictions.RenderDistance; j++) {
                    GenerateWorld(
                        i + Player.X / (IRestrictions.TileSize * IRestrictions.SuperChunkSize * IRestrictions.ChunkSize),
                        j + Player.Y / (IRestrictions.TileSize * IRestrictions.SuperChunkSize * IRestrictions.ChunkSize)
                    );
                }
            }
        }

        public static void UnloadChunks() {
            for (var i = -IRestrictions.RenderDistance; i < IRestrictions.RenderDistance+1; i++) {
                //Down
                UngenerateWorld(
                    Player.X / (IRestrictions.TileSize * IRestrictions.SuperChunkSize * IRestrictions.ChunkSize)+i,
                    (Player.Y / (IRestrictions.TileSize * IRestrictions.SuperChunkSize * IRestrictions.ChunkSize))+3
                );
                //Up
                UngenerateWorld(
                    Player.X / (IRestrictions.TileSize * IRestrictions.SuperChunkSize * IRestrictions.ChunkSize)+i-1,
                    (Player.Y / (IRestrictions.TileSize * IRestrictions.SuperChunkSize * IRestrictions.ChunkSize))-4
                );
                //Right
                UngenerateWorld(
                    Player.X / (IRestrictions.TileSize * IRestrictions.SuperChunkSize * IRestrictions.ChunkSize)-4,
                    (Player.Y / (IRestrictions.TileSize * IRestrictions.SuperChunkSize * IRestrictions.ChunkSize))+i
                );
                //Left
                UngenerateWorld(
                    Player.X / (IRestrictions.TileSize * IRestrictions.SuperChunkSize * IRestrictions.ChunkSize)+3,
                    (Player.Y / (IRestrictions.TileSize * IRestrictions.SuperChunkSize * IRestrictions.ChunkSize))+i
                );
            }
        }
    }
}
