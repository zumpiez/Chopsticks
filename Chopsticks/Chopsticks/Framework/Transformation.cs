using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Chopsticks
{
    public struct Transformation
    {
        public Transformation(float x, float y, float z=1, float rotation = 0, float scaleX = 1, float scaleY = 1) : this()
        {
            Translation = new Vector2(x, y);
            SortDepth = z;
            Rotation = rotation;
            Scale = new Vector2(scaleX, scaleY);
        }

        public Vector2 Translation { get; set; }
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }
        public float SortDepth { get; set; }
    }
}
