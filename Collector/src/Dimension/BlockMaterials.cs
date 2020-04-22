using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Collector.Dimension
{
    public static class BlockMaterials {
        public static readonly Dictionary<string,Block> Materials = new Dictionary<string, Block>();
        public static readonly Dictionary<string,Texture2D> Textures = new Dictionary<string, Texture2D>();

        //Private so the singleton can't be instantiated
        static BlockMaterials() {}
    
        public static void Initialize(ContentManager content){
            Materials.Add("grass",new Block("grass"));
            Materials.Add("wood",new Block("wood"));
            Materials.Add("water",new Block("water"));
            Materials.Add("stone",new Block("stone"));
            Materials.Add("snow",new Block("snow"));
            Materials.Add("sand",new Block("sand"));
            Materials.Add("air",new Block("air"));
            Materials.Add("roof",new Block("roof"));
            Materials.Add("wall",new Block("wall"));

            foreach (var name in Materials.Keys)
            {
                Textures.Add(name,content.Load<Texture2D>(name));
            }
        }
    }
}
