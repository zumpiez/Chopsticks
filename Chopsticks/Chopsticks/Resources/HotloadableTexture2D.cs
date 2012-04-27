using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace Chopsticks
{
    class HotloadableTexture2D
    {
        public Texture2D Texture { get; private set; }
        private FileSystemWatcher textureChangeDetector;

        public HotloadableTexture2D(GraphicsDevice device, string path)
        {
            var file = Path.GetFileName(path);
            var directory = Path.GetDirectoryName(path);

            LoadTexture(device, path);

            textureChangeDetector = new FileSystemWatcher(directory, file);
            textureChangeDetector.Changed += (sender, args) =>
            {
                LoadTexture(device, args.FullPath);
            };
            textureChangeDetector.EnableRaisingEvents = true;
        }

        private void LoadTexture(GraphicsDevice device, string path)
        {
            using (var fs = new FileStream(path, FileMode.Open))
            {
                Texture = Texture2D.FromStream(device, fs);
            }
        }
    }
}
