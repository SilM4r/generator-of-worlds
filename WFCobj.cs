using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator
{
    
    public class WFCobj
    {
        public ObjectImage obj;
        public int rarity;

        public WFCobj(ObjectImage obj,int rarity) 
        {
            this.obj = obj;
            this.rarity = rarity;
        }
    }
}
