using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace Chopsticks.Resources
{
    class TextureLibrary
    {
        private GraphicsDevice device;

        public TextureLibrary(GraphicsDevice device, string path, string defaultExtension = ".png")
        {
            this.device = device;
            this.Path = path;
            this.FileExtension = defaultExtension;
        }

        public Texture2D GetTexture(string name)
        {
            using (var file = File.OpenRead(Path + name + FileExtension))
            {
                return Texture2D.FromStream(device, file);
            }
        }

        public string Path { get; set; }

        public string FileExtension { get; set; }
    }
}
