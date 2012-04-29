using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Diagnostics;

namespace Chopsticks.Resources
{
    class TextureAtlas
    {
        private GraphicsDevice device;
        private Dictionary<String, Texture2D> atlas;
        private FileSystemWatcher watcher;

        public TextureAtlas(GraphicsDevice device, string path, string defaultExtension = ".png")
        {
            this.device = device;
            this.Path = path;
            this.FileExtension = defaultExtension;

            this.atlas = new Dictionary<string, Texture2D>();

            this.watcher = new FileSystemWatcher(path, String.Format("*{0}", defaultExtension));
            watcher.Renamed += (sender, args) =>
            {
                var filename = System.IO.Path.GetFileNameWithoutExtension(args.FullPath);
                if (atlas.ContainsKey(filename))
                {
                    InitializeTexture(filename);
                }
            };
            watcher.Error += (sender, args) =>
            {
                Debug.Fail("File watcher event buffer overflowed");
            };
            watcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Fetches a texture from the atlas. If the texture is not present in the atlas, it is loaded and added.
        /// </summary>
        /// <param name="name">The name of the texture</param>
        /// <returns>The texture instance</returns>
        public Texture2D GetTexture(string name)
        {
            //lookup texture from atlas
            if (atlas.ContainsKey(name))
            {
                return atlas[name];
            }
            else
            {
                return InitializeTexture(name);
            }
        }

        /// <summary>
        /// Initialize texture in atlas
        /// </summary>
        /// <param name="name">Name of texture.</param>
        /// <returns>Texture2D that has been inserted into the atlas</returns>
        private Texture2D InitializeTexture(string name)
        {
            using (var file = File.OpenRead(Path + name + FileExtension))
            {
                var texture = Texture2D.FromStream(device, file);
                atlas[name] = texture;
                return texture;
            }
        }

        //NOTE: modifying this value will require re-initialization of watcher. 
        public string Path { get; private set; }

        //NOTE: modifying this value will require re-initialization of watcher. 
        public string FileExtension { get; private set; }
    }
}
