using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace ProjectMono.Source.Engine
{
    public static class Globals
    {
        public static SpriteBatch SpriteBatch;
        public static ContentManager Content;
        public static GraphicsDeviceManager Graphics;
        public static readonly int CellSize = 20;
    }
}
