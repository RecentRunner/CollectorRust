//https://www.redblobgames.com/maps/terrain-from-noise/

using System;
using System.Collections.Generic;
using Collector.ThirdPartyCode;

namespace Collector.Dimension
{
    public class Chunks: IRestrictions {
        public static Dictionary<Tuple<int, int, int>, Block> LoadedChunks = new Dictionary<Tuple<int, int, int>, Block>();
        public static Dictionary<Tuple<int, int, int>, Block> SavedChunks = new Dictionary<Tuple<int, int, int>, Block>();
        public static OpenSimplexNoise Gen1 = new OpenSimplexNoise(IRestrictions.Seed+ 1);
        public static OpenSimplexNoise Gen2 = new OpenSimplexNoise(IRestrictions.Seed);

        public static void CreateStructures() {
            var i = 10;
            SetBlock(3 + i, 2,1,"wall");
            SetBlock(4 + i, 2,1,"wall");
            SetBlock(3 + i, 3,1, "roof");
            SetBlock(4 + i, 3,1,"roof");
        }
        
        public static void SetBlock(int x, int y,int z, String name) {
            LoadedChunks.Add(new Tuple<int,int,int>(x, y, z), BlockMaterials.Materials[name]);
            SavedChunks.Add(new Tuple<int,int,int>(x, y, z), BlockMaterials.Materials[name]);
        }

        public static void PlaceBlock(int x, int y, String name){
            var triple = new Tuple<int, int, int>(x, y, 1);
            if (!Chunks.LoadedChunks[triple].getName().Equals("air")) return;
            Chunks.LoadedChunks[triple] = BlockMaterials.Materials[name];
            Chunks.SavedChunks[triple] =  BlockMaterials.Materials[name];
        }

        public static void RemoveBlock(int x, int y){
            var triple = new Tuple<int, int, int>(x, y, 1);
            if (Chunks.LoadedChunks[triple].getName().Equals("air")) return;
            Chunks.LoadedChunks[triple] = BlockMaterials.Materials["air"];
            Chunks.SavedChunks[triple] = BlockMaterials.Materials["air"];
        }

        public static void UngenerateChunk(int x, int y) {
            var startX = x << IRestrictions.ChunkShift;
            var startY = y << IRestrictions.ChunkShift;
            var endX = startX + IRestrictions.ChunkSize;
            var endY = startY + IRestrictions.ChunkSize;

            //Going from start of selected chunk to end of selected chunk in x and y
            for (var i = startX; i != endX; i++) {
                for (var j = startY; j != endY; j++) {
                    LoadedChunks.Remove(new Tuple<int,int,int>(i, j, 0));
                }
            }
            //Second Layer
            for (var i = startX; i != endX; i++){
                for (var j = startY; j != endY; j++) {
                    var triple = new Tuple<int, int, int>(i, j,1);
                    LoadedChunks.Remove(triple);
                }
            }
        }

        public static void GenerateChunk(int x, int y) {
            var startX = x << IRestrictions.ChunkShift;
            var startY = y << IRestrictions.ChunkShift;
            var endX = startX + IRestrictions.ChunkSize;
            var endY = startY + IRestrictions.ChunkSize;

            //Going from start of selected chunk to end of selected chunk in x and y
            for (var i = startX; i != endX; i++) {
                for (var j = startY; j != endY; j++) {
                    var triple = new Tuple<int, int, int>(i, j, 0);
                    if(SavedChunks[triple] != null){
                        LoadedChunks.Add(triple,SavedChunks[triple]);
                    }
                    else {
                        LoadedChunks.Add(triple, GetTerrain(i, j));
                        SavedChunks.Add(triple, GetTerrain(i,j));
                    }
                }
            }

            //Second Layer
            for (var i = startX; i != endX; i++){
                for (var j = startY; j != endY; j++) {
                    var triple = new Tuple<int, int, int>(i, j,1);
                    LoadedChunks.Add(triple, GetBlocks(i,j,1));
                    SavedChunks.Add(triple, GetBlocks(i,j,1));
                }
            }
        }

        private static Block GetBlocks(int x, int y, int z) {
            return SavedChunks.GetValueOrDefault(new Tuple<int, int, int>(x, y, z), new Block("air"));
        }

        private static double Noise1(double nx, double ny) {
            return Gen1.Evaluate(nx, ny) / 2 + 0.5;
        }

        private static double Noise2(double nx, double ny) {
            return Gen2.Evaluate(nx, ny) / 2 + 0.5;
        }

        private static Block GetTerrain(int x, int y) {
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

        private static Block GetBiomeBlock(double moisture, double elevation) {
            var biome = GetBiome(moisture, elevation);
            if (biome.Equals("Tundra")) return BlockMaterials.Materials["snow"];
            if (biome.Equals("Taiga")) return BlockMaterials.Materials["snow"];
            if (biome.Equals("Snow")) return BlockMaterials.Materials["snow"];

            if (biome.Equals("Grassland")) return BlockMaterials.Materials["grass"];
            if (biome.Equals("Shrubland")) return BlockMaterials.Materials["grass"];
            if (biome.Equals("TemperateDeciduousForest")) return BlockMaterials.Materials["grass"];

            if (biome.Equals("TemperateRainForest")) return BlockMaterials.Materials["grass"];
            if (biome.Equals("TropicalSeasonalForest")) return BlockMaterials.Materials["grass"];
            if (biome.Equals("TropicalForest")) return BlockMaterials.Materials["grass"];
            if (biome.Equals("TropicalRainForest")) return BlockMaterials.Materials["grass"];

            if (biome.Equals("Beach")) return BlockMaterials.Materials["sand"];
            if (biome.Equals("Bare")) return BlockMaterials.Materials["sand"];
            if (biome.Equals("Scorched")) return BlockMaterials.Materials["sand"];
            if (biome.Equals("TemperateDesert")) return BlockMaterials.Materials["sand"];
            return biome.Equals("SubtropicalDesert") ? BlockMaterials.Materials["sand"] : BlockMaterials.Materials["water"];
        }

        private static string GetBiome(double moisture, double elevation) {
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

        public static bool IsEmpty(int x, int y){
            return LoadedChunks[new Tuple<int,int,int>(x * 8, y * 8,0)] == null;
        }
    }
}
