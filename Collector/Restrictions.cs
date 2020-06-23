using System;
using Collector.Character;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Collector.IRestrictions;

namespace Collector
{
    public interface IRestrictions
    {
        public const int Seed = 1;
        public const int TileSize = 1;
        public const float ViewportHeight = 9f;
        public const float ViewportWidth = 16f;
        public const int MovementSpeed = 1;
        public const int KeyDelay = 20;
        public const int ChunkSize = 8;
        public const int SuperChunkSize = 1;
        public const int ChunkShift = 3;
        public const int RenderDistance = 5;
        public const float RenderTime = 1/60f;
        public const float Zoom = 30;
        public const float Scale = ViewportWidth/ViewportHeight;
    }
}