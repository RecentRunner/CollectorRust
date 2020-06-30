//https://www.redblobgames.com/maps/terrain-from-noise/

using System;
using System.Collections.Generic;
using Collector.Character;
using Collector.ThirdPartyCode;

namespace Collector.Dimension
{
    public class Chunks
    {
        public Dictionary<Tuple<int, int, int>, Blocks> LoadedChunks { get; } = new Dictionary<Tuple<int, int, int>, Blocks>();
        private readonly Dictionary<Tuple<int, int, int>, Blocks> _savedChunks = new Dictionary<Tuple<int, int, int>, Blocks>();
        public readonly Dictionary<Tuple<int, int>, Collision> LoadedCollisions = new Dictionary<Tuple<int, int>, Collision>();
        private readonly Dictionary<Tuple<int, int>, Collision> _savedCollisions = new Dictionary<Tuple<int, int>, Collision>();
        public readonly List<Blocks> Impassable = new List<Blocks>();
        private readonly OpenSimplexNoise _gen1 = new OpenSimplexNoise(IRestrictions.Seed+ 1);
        private readonly OpenSimplexNoise _gen2 = new OpenSimplexNoise(IRestrictions.Seed);
        
        private void GenerateWorld(float x, float y)
        {
            if (IsEmpty(x, y)) {
                GenerateChunk(x, y);
            }
        }

        private void UngenerateWorld(float x, float y) {
            if (!IsEmpty(x, y)) {
                UngenerateChunk(x, y);
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
        public void CreateStructures() {
            const int i = 10;
            SetBlock(3 + i, 2,1,Blocks.BlockWall);
            SetBlock(4 + i, 2,1,Blocks.BlockWall);
            SetBlock(3 + i, 3,1, Blocks.BlockRoof);
            SetBlock(4 + i, 3,1,Blocks.BlockRoof);
        }

        private void SetBlock(int x, int y, int z, Blocks name)
        {
            var tuple = new Tuple<int, int, int>(x, y, z);
            var pair = new Tuple<int, int>(x, y);
            
            LoadedCollisions[pair] = new Collision(x,y);
            _savedCollisions[pair] = new Collision(x,y);
            LoadedChunks[tuple] = name;
            _savedChunks[tuple] = name;
        }

        public void PlaceBlock(int x, int y, Blocks name)
        {
            var tuple = new Tuple<int, int, int>(x, y, 1);
            if (!LoadedChunks.ContainsKey(tuple)) return;
            if (LoadedChunks[tuple].Equals(Blocks.BlockAir))
            {
                SetBlock(x, y, 1, name);
            }
        }

        public void RemoveBlock(int x, int y){
            var tuple = new Tuple<int, int, int>(x, y, 1);
            var pair = new Tuple<int,int>(x,y);
            
            if (!LoadedChunks.ContainsKey(tuple)) return;
            if (LoadedChunks[tuple].Equals(Blocks.BlockAir)) return;
            
            LoadedCollisions.Remove(pair);
            _savedCollisions.Remove(pair);

            LoadedChunks[tuple] = Blocks.BlockAir;
            _savedChunks[tuple] = Blocks.BlockAir;
        }

        public void UngenerateChunk(float x, float y) {
            var startX = (int)x << IRestrictions.ChunkShift;
            var startY = (int)y << IRestrictions.ChunkShift;
            var endX = startX + IRestrictions.ChunkSize;
            var endY = startY + IRestrictions.ChunkSize;

            //Going from start of selected chunk to end of selected chunk in x and y
            for (var i = startX; i != endX; i++) {
                for (var j = startY; j != endY; j++) {
                    LoadedChunks.Remove(new Tuple<int,int,int>(i, j, 0));
                    LoadedChunks.Remove(new Tuple<int, int, int>(i, j,1));
                    LoadedCollisions.Remove(new Tuple<int, int>(i,j));
                }
            }
        }

        public void GenerateChunk(float x, float y) {
            var startX = (int)x << IRestrictions.ChunkShift;
            var startY = (int)y << IRestrictions.ChunkShift;
            var endX = startX + IRestrictions.ChunkSize;
            var endY = startY + IRestrictions.ChunkSize;

            //Going from start of selected chunk to end of selected chunk in x and y
            for (var i = startX; i != endX; i++) {
                for (var j = startY; j != endY; j++) {
                    var triple = new Tuple<int, int, int>(i, j, 0);
                    var pair = new Tuple<int, int>(i,j);
                    if (_savedCollisions.ContainsKey(pair))
                    {
                        LoadedCollisions.Add(pair, _savedCollisions[pair]);

                    }
                    if(_savedChunks.ContainsKey(triple)){
                        LoadedChunks.Add(triple,_savedChunks[triple]);
                    }
                    else {
                        var terrain = GetTerrain(i, j);
                        if (Impassable.Contains(terrain))
                        {
                            var collision = new Collision(i,j);
                            LoadedCollisions[pair] = collision;
                            _savedCollisions[pair] = collision;
                        }

                        LoadedChunks.Add(triple, terrain);
                        _savedChunks.Add(triple, terrain);
                    }
                }
            }

            //Second Layer
            for (var i = startX; i != endX; i++){
                for (var j = startY; j != endY; j++) {
                    var triple = new Tuple<int, int, int>(i, j,1);
                    LoadedChunks[triple] = GetBlocks(i,j,1);
                    _savedChunks[triple] = GetBlocks(i,j,1);
                }
            }
        }

        private Blocks GetBlocks(int x, int y, int z) {
            return _savedChunks.GetValueOrDefault(new Tuple<int, int, int>(x, y, z), Blocks.BlockAir);
        }

        private double Noise1(double nx, double ny) {
            return _gen1.Evaluate(nx, ny) / 2 + 0.5;
        }

        private double Noise2(double nx, double ny) {
            return _gen2.Evaluate(nx, ny) / 2 + 0.5;
        }

        private Blocks GetTerrain(int x, int y) {
            const double scale = 0.01;
            var nx = x * scale;
            var ny = y * scale;
            var elevation = (0.17 * Noise1(1 * nx, 1 * ny)
                             + 0.42 * Noise1(2 * nx, 2 * ny)
                             + 0.16 * Noise1(4 * nx, 4 * ny)
                             + 0.00 * Noise1(8 * nx, 8 * ny)
                             + 0.00 * Noise1(16 * nx, 16 * ny)
                             + 0.03 * Noise1(32 * nx, 32 * ny));
            elevation /= (0.17+0.42+0.16+0.00+0.00+0.03);
            elevation = Math.Pow(elevation, 3.00);
            var moisture = (1.00 * Noise2( 1 * nx,  1 * ny)
                            + 0.00 * Noise2( 2 * nx,  2 * ny)
                            + 1.00 * Noise2( 4 * nx,  4 * ny)
                            + 0.00 * Noise2( 8 * nx,  8 * ny)
                            + 0.00 * Noise2(16 * nx, 16 * ny)
                            + 0.00 * Noise2(32 * nx, 32 * ny));
            moisture /= (0.00+1.00+0.00+0.00+0.00+0.00);

            return GetBiomeBlock(moisture, elevation);
        }
        
        private Blocks GetBiomeBlock(double moisture, double elevation) {
            var biome = GetBiome(moisture, elevation);
            if (biome.Equals("Tundra")) return Blocks.BlockSnow;
            if (biome.Equals("Taiga")) return Blocks.BlockSnow;
            if (biome.Equals("Snow")) return Blocks.BlockSnow;

            if (biome.Equals("Grassland")) return Blocks.BlockGrass;
            if (biome.Equals("Shrubland")) return Blocks.BlockGrass;
            if (biome.Equals("TemperateDeciduousForest")) return Blocks.BlockGrass;

            if (biome.Equals("TemperateRainForest")) return Blocks.BlockGrass;
            if (biome.Equals("TropicalSeasonalForest")) return Blocks.BlockGrass;
            if (biome.Equals("TropicalForest")) return Blocks.BlockGrass;
            if (biome.Equals("TropicalRainForest")) return Blocks.BlockGrass;

            if (biome.Equals("Beach")) return Blocks.BlockSand;
            if (biome.Equals("Bare")) return Blocks.BlockSand;
            if (biome.Equals("Scorched")) return Blocks.BlockSand;
            if (biome.Equals("TemperateDesert")) return Blocks.BlockSand;
            return biome.Equals("SubtropicalDesert") ? Blocks.BlockSand : Blocks.BlockWater;
        }

        private string GetBiome(double moisture, double elevation) {
            if (elevation < 0.1) return "Ocean";
            if (elevation < 0.12) return "Beach";

            if (elevation > 0.8) {
                if (moisture < 0.1) return "Scorched";
                if (moisture < 0.2) return "Bare";
                return moisture < 0.5 ? "Tundra" : "Snow";
            }

            if (elevation > 0.6) {
                if (moisture < 0.33) return "TemperateDesert";
                return moisture < 0.66 ? "Shrubland" : "Taiga";
            }

            if (elevation > 0.3) {
                if (moisture < 0.16) return "TemperateDesert";
                if (moisture < 0.50) return "Grassland";
                return moisture < 0.83 ? "TemperateDeciduousForest" : "TemperateRainForest";
            }

            if (moisture < 0.16) return "SubtropicalDesert";
            if (moisture < 0.33) return "Grassland";
            return moisture < 0.66 ? "TropicalSeasonalForest" : "TropicalRainForest";
        }

        public bool IsEmpty(float x, float y){
            return !LoadedChunks.ContainsKey(new Tuple<int,int,int>((int)x * 8, (int)y * 8,0));
        }
    }
}
