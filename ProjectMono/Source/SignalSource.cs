using ProjectMono.Source.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectMono.Source
{
    public class SignalSource : Cell
    {
        public SignalSource(Vector2 position) : base("Source", position, Direction.Undefined, CellType.Source, true) 
        {
        }
    }
}
