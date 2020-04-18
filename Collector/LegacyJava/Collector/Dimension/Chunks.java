//https://www.redblobgames.com/maps/terrain-from-noise/
package Collector.Dimension;

import com.github.czyzby.kiwi.util.tuple.immutable.Triple;
import Collector.ThirdPartyCode.OpenSimplexNoise;

import java.util.HashMap;

import static Collector.Dimension.BlockMaterials.materials;
import static Collector.Restrictions.*;

public class Chunks {
    public static HashMap<Triple<Integer, Integer, Integer>, Block> loadedChunks = new HashMap<>();
    public static HashMap<Triple<Integer, Integer,Integer>, Block> savedChunks = new HashMap<>();
    public static OpenSimplexNoise gen1 = new OpenSimplexNoise(SEED + 1);
    public static OpenSimplexNoise gen2 = new OpenSimplexNoise(SEED);

    public static void createStructures() {
        int i = 10;
        setBlock(3 + i, 2,1,"wall");
        setBlock(4 + i, 2,1,"wall");
        setBlock(3 + i, 3,1, "roof");
        setBlock(4 + i, 3,1,"roof");
    }

    public static void setBlock(int x, int y,int z, String name) {
        loadedChunks.put(new Triple<>(x, y, z), materials.get(name));
        savedChunks.put(new Triple<>(x, y, z), materials.get(name));
    }

    public static void placeBlock(int x, int y, String name){
        Triple<Integer, Integer, Integer> triple = new Triple<>(x, y, 1);
        if(Chunks.loadedChunks.get(triple).getName().equals("air")) {
            Chunks.loadedChunks.replace(triple, Chunks.loadedChunks.get(triple), materials.get(name));
            Chunks.savedChunks.replace(triple, Chunks.savedChunks.get(triple), materials.get(name));
        }
    }

    public static void removeBlock(int x, int y){
        Triple<Integer, Integer, Integer> triple = new Triple<>(x, y, 1);
        if(!Chunks.loadedChunks.get(triple).getName().equals("air")) {
            Chunks.loadedChunks.replace(triple, Chunks.loadedChunks.get(triple), materials.get("air"));
            Chunks.savedChunks.replace(triple, Chunks.savedChunks.get(triple), materials.get("air"));
        }
    }

    public static void ungenerateChunk(int x, int y) {
        int startX = x << CHUNK_SHIFT;
        int startY = y << CHUNK_SHIFT;
        int endX = startX + CHUNK_SIZE;
        int endY = startY + CHUNK_SIZE;

        //Going from start of selected chunk to end of selected chunk in x and y
        for (int i = startX; i != endX; i++) {
            for (int j = startY; j != endY; j++) {
                loadedChunks.remove(new Triple<>(i, j, 0), getTerrain(i, j));
            }
        }
        //Second Layer
        for (int i = startX; i != endX; i++){
            for (int j = startY; j != endY; j++) {
                Triple<Integer,Integer,Integer> triple = new Triple<>(i, j,1);
                loadedChunks.remove(triple, loadedChunks.get(triple));
            }
        }
    }

    public static void generateChunk(int x, int y) {
        int startX = x << CHUNK_SHIFT;
        int startY = y << CHUNK_SHIFT;
        int endX = startX + CHUNK_SIZE;
        int endY = startY + CHUNK_SIZE;

        //Going from start of selected chunk to end of selected chunk in x and y
        for (int i = startX; i != endX; i++) {
            for (int j = startY; j != endY; j++) {
                Triple<Integer, Integer, Integer> triple = new Triple<>(i, j, 0);
                if(savedChunks.get(triple) != null){
                    loadedChunks.put(triple,savedChunks.get(triple));
                }
                else {
                    loadedChunks.put(triple, getTerrain(i, j));
                    savedChunks.put(triple, getTerrain(i,j));
                }
            }
        }

        //Second Layer
        for (int i = startX; i != endX; i++){
            for (int j = startY; j != endY; j++) {
                Triple<Integer,Integer,Integer> triple = new Triple<>(i, j,1);
                loadedChunks.put(triple, getBlocks(i,j,1));
                savedChunks.put(triple, getBlocks(i,j,1));
            }
        }
    }

    public static Block getBlocks(int x, int y, int z) {
        return savedChunks.getOrDefault(new Triple<>(x, y, z), new Block("air"));
    }

    public static double noise1(double nx, double ny) {
        return gen1.eval(nx, ny) / 2 + 0.5;
    }

    public static double noise2(double nx, double ny) {
        return gen2.eval(nx, ny) / 2 + 0.5;
    }

    public static Block getTerrain(int x, int y) {
        double moisture, elevation, nx, ny;
        double scale = 0.01;
        nx = x * scale;
        ny = y * scale;
        elevation = (0.17 * noise1(1 * nx, 1 * ny)
                + 0.42 * noise1(2 * nx, 2 * ny)
                + 0.16 * noise1(4 * nx, 4 * ny)
                + 0.00 * noise1(8 * nx, 8 * ny)
                + 0.00 * noise1(16 * nx, 16 * ny)
                + 0.03 * noise1(32 * nx, 32 * ny));
        elevation /= (0.17+0.42+0.16+0.00+0.00+0.03);
        elevation = Math.pow(elevation, 3.00);
        moisture = (1.00 * noise2( 1 * nx,  1 * ny)
                + 0.00 * noise2( 2 * nx,  2 * ny)
                + 1.00 * noise2( 4 * nx,  4 * ny)
                + 0.00 * noise2( 8 * nx,  8 * ny)
                + 0.00 * noise2(16 * nx, 16 * ny)
                + 0.00 * noise2(32 * nx, 32 * ny));
        moisture /= (0.00+1.00+0.00+0.00+0.00+0.00);

        return getBiomeBlock(moisture, elevation);
    }

    public static Block getBiomeBlock(double moisture, double elevation) {
        String biome = getBiome(moisture, elevation);
        if (biome.equals("Tundra")) return materials.get("snow");
        if (biome.equals("Taiga")) return materials.get("snow");
        if (biome.equals("Snow")) return materials.get("snow");

        if (biome.equals("Grassland")) return materials.get("grass");
        if (biome.equals("Shrubland")) return materials.get("grass");
        if (biome.equals("TemperateDeciduousForest")) return materials.get("grass");

        if (biome.equals("TemperateRainForest")) return materials.get("grass");
        if (biome.equals("TropicalSeasonalForest")) return materials.get("grass");
        if (biome.equals("TropicalForest")) return materials.get("grass");
        if (biome.equals("TropicalRainForest")) return materials.get("grass");

        if (biome.equals("Beach")) return materials.get("sand");
        if (biome.equals("Bare")) return materials.get("sand");
        if (biome.equals("Scorched")) return materials.get("sand");
        if (biome.equals("TemperateDesert")) return materials.get("sand");
        if (biome.equals("SubtropicalDesert")) return materials.get("sand");

        return materials.get("water");
    }

    public static String getBiome(double moisture, double elevation) {
        if (elevation < 0.1) return "Ocean";
        if (elevation < 0.12) return "Beach";

        if (elevation > 0.8) {
            if (moisture < 0.1) return "Scorched";
            if (moisture < 0.2) return "Bare";
            if (moisture < 0.5) return "Tundra";
            return "Snow";
        }

        if (elevation > 0.6) {
            if (moisture < 0.33) return "TemperateDesert";
            if (moisture < 0.66) return "Shrubland";
            return "Taiga";
        }

        if (elevation > 0.3) {
            if (moisture < 0.16) return "TemperateDesert";
            if (moisture < 0.50) return "Grassland";
            if (moisture < 0.83) return "TemperateDeciduousForest";
            return "TemperateRainForest";
        }

        if (moisture < 0.16) return "SubtropicalDesert";
        if (moisture < 0.33) return "Grassland";
        if (moisture < 0.66) return "TropicalSeasonalForest";
        return "TropicalRainForest";
    }

    public static Boolean isEmpty(int x, int y){
        return loadedChunks.get(new Triple<>(x * 8, y * 8,0)) == null;
    }

}
