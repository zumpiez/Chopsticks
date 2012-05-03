using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chopsticks
{
    public class Map
    {
        /// <summary>
        /// Maps a texture name to a collection of positioning information.
        /// </summary>
        public Dictionary<string, IList<Transformation>> Scene { get; private set; }

        public void Insert(string textureName, Transformation transformation)
        {
            if (Scene.ContainsKey(textureName))
            {
                Scene[textureName].Add(transformation);
            }
            else
            {
                Scene.Add(textureName, new List<Transformation> { transformation });
            }
        }
        
        public Map()
        {
            Scene = new Dictionary<string, IList<Transformation>>();
        }

        public void Insert(string textureName, IEnumerable<Transformation> transformations)
        {
            if (Scene.ContainsKey(textureName))
            {
                Scene[textureName].Concat(transformations);
            }
            else
            {
                Scene.Add(textureName, transformations.ToList());
            }
        }
    }
}
