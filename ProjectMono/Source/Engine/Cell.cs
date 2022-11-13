using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ProjectMono.Source.Engine;

namespace ProjectMono.Source.Engine
{
    public abstract class Cell
    {
        protected Texture2D _texture;
        public Vector2 Position;
        public Direction Direction;
        public CellType CellType;
        public bool Have_Signal;

        public Cell(string texture, Vector2 position, Direction direction, CellType cellType, bool have_signal)
        {
            Position = position;
            _texture = Globals.Content.Load<Texture2D>(texture);
            Direction = direction; 
            CellType = cellType;
            Have_Signal = have_signal;
        }

        public virtual void Draw()
        {
            Globals.SpriteBatch.Draw(
                _texture,
                new Rectangle((int)Position.X, (int)Position.Y, Globals.CellSize, Globals.CellSize),
                null,
                Color.White,
                0f,
                new Vector2(_texture.Bounds.Width / 2, _texture.Bounds.Height / 2),
                new SpriteEffects(),
                0f);
        }
        public virtual void SnapToGrid()
        {
            Position = new Vector2(
                (int)Math.Round(Position.X / Globals.CellSize) * Globals.CellSize,
                (int)Math.Round(Position.Y / Globals.CellSize) * Globals.CellSize);
        }

    }

    public enum CellType
    {
        Arrow,
        Source
    }
}
