using System.Collections.Generic;
namespace Collector.Dimension
{
    public class BlockMaterials {
        public static Dictionary<string,Block> Materials = new Dictionary<string, Block>();
        //public static Dictionary<String,Texture> Textures = new Dictionary<>();

        //Private so the singleton can't be instantiated
        private BlockMaterials() {}
    
        public static void create(){
            Materials.Add("grass",new Block("grass"));
            Materials.Add("wood",new Block("wood"));
            Materials.Add("water",new Block("water"));
            Materials.Add("stone",new Block("stone"));
            Materials.Add("snow",new Block("snow"));
            Materials.Add("sand",new Block("sand"));
            Materials.Add("air",new Block("air"));
            Materials.Add("roof",new Block("roof"));
            Materials.Add("wall",new Block("wall"));
            /*
        for (String s:BlockMaterials.Materials.keySet()) {
            Textures.Add(s,new Texture("assets/" + s + ".png"));
        }
        */
        }
    }
}
