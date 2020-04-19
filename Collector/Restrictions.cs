using System;
using static Collector.IRestrictions;

namespace Collector
{
    public interface IRestrictions
    {
        public const int Seed = 1;
        public const float ViewportHeight = 9/2f;
        public const float ViewportWidth = 16/2f; 
        public const int MovementSpeed = 16;
        public const int FreeSpeed = 2;
        public const int KeyDelay = 0;
        public const int TileSize = 16;
        public const int ChunkSize = 8;
        public const int SuperChunkSize = 1;
        public const int TileShift = 5;
        public const int ChunkShift = 3;
        public const int RenderDistance = 3;
        public const float RenderTime = 1/60f;
    }
}