using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Chopsticks
{
    public struct Transformation
    {
        public Vector3 Translation { get; set; }
        public float Rotation { get; set; }
        public Vector2 scale { get; set; }
    }
}
