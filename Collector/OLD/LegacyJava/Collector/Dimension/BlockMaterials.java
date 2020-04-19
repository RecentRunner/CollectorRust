package Collector.Dimension;

import com.badlogic.gdx.graphics.Texture;

import java.util.HashMap;

public class BlockMaterials {
    public static HashMap<String,Block> materials = new HashMap<>();
    public static HashMap<String,Texture> textures = new HashMap<>();

    //Private so the singleton can't be instantiated
    private BlockMaterials() {}

    public static void create(){
        materials.put("grass",new Block("grass"));
        materials.put("wood",new Block("wood"));
        materials.put("water",new Block("water"));
        materials.put("stone",new Block("stone"));
        materials.put("snow",new Block("snow"));
        materials.put("sand",new Block("sand"));
        materials.put("air",new Block("air"));
        materials.put("roof",new Block("roof"));
        materials.put("wall",new Block("wall"));

        for (String s:BlockMaterials.materials.keySet()) {
            textures.put(s,new Texture("assets/" + s + ".png"));
        }
    }
}
