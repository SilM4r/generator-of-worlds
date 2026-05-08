using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator
{
    public class ObjectImage
    {
        public string name,source;
        public List<string> neighboringObjects;
        public List<int> rarity;
        public int id = 0;

        public ObjectImage(string name, string source, List<string> neighboringObjects, List<int> rarity, List<ObjectImage> listObjectImage)
        {
            this.name = name;
            this.source = source;

            this.rarity = rarity;
            
            this.neighboringObjects = neighboringObjects;

            listObjectImage.Add(this);

            this.id = listObjectImage.Count;
        }
    }
}
