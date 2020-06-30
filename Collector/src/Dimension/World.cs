using System;
using Collector.Character;

namespace Collector.Dimension
{
    public class World {
        private void GenerateWorld(float x, float y)
        {
            if (Chunks.IsEmpty(x, y)) {
                Chunks.GenerateChunk(x, y);
            }
        }

        private void UngenerateWorld(float x, float y) {
            if (!Chunks.IsEmpty(x, y)) {
                Chunks.UngenerateChunk(x, y);
            }
        }

        public void LoadChunks()
        {
            for (var i = -(IRestrictions.RenderDistance); i < IRestrictions.RenderDistance; i++) {
                for (var j = -(IRestrictions.RenderDistance); j < IRestrictions.RenderDistance; j++) {
                    GenerateWorld(
                        i + Player.X / (IRestrictions.SuperChunkSize * IRestrictions.ChunkSize),
                        j + Player.Y / (IRestrictions.SuperChunkSize * IRestrictions.ChunkSize)
                    );
                }
            }
        }

        public void UnloadChunks() {
            for (var i = -IRestrictions.RenderDistance; i < IRestrictions.RenderDistance+1; i++) {
                //Down
                UngenerateWorld(
                    Player.X / (IRestrictions.SuperChunkSize * IRestrictions.ChunkSize)+i,
                    Player.Y / (IRestrictions.SuperChunkSize * IRestrictions.ChunkSize)+IRestrictions.RenderDistance
                );
                //Up
                UngenerateWorld(
                    Player.X / (IRestrictions.SuperChunkSize * IRestrictions.ChunkSize)+i,
                    Player.Y / (IRestrictions.SuperChunkSize * IRestrictions.ChunkSize)-IRestrictions.RenderDistance
                );
                //Right
                UngenerateWorld(
                    Player.X / (IRestrictions.SuperChunkSize * IRestrictions.ChunkSize)-IRestrictions.RenderDistance,
                    Player.Y / (IRestrictions.SuperChunkSize * IRestrictions.ChunkSize)+i
                );
                //Left
                UngenerateWorld(
                    Player.X / (IRestrictions.SuperChunkSize * IRestrictions.ChunkSize)+IRestrictions.RenderDistance,
                    Player.Y / (IRestrictions.SuperChunkSize * IRestrictions.ChunkSize)+i
                );
            }
        }
    }
}
