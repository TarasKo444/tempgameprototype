using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectMono.Source.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMono.Source
{
    public class Arrow : Cell
    {
        public Arrow(Vector2 position, Direction direction) : base("Arrow", position, direction, CellType.Arrow, false)
        {
            Have_Signal = false;
        }

        public override void Draw()
        {

            if(Have_Signal == false)
            {
                Globals.SpriteBatch.Draw(
                    _texture,
                    new Rectangle((int)Position.X, (int)Position.Y, Globals.CellSize, Globals.CellSize),
                    null,
                    Color.White,
                    MathHelper.ToRadians((float)Direction),
                    new Vector2(_texture.Bounds.Width / 2, _texture.Bounds.Height / 2),
                    new SpriteEffects(),
                    0f);
            }
            else
            {
                Globals.SpriteBatch.Draw(
                    _texture,
                    new Rectangle((int)Position.X, (int)Position.Y, Globals.CellSize, Globals.CellSize),
                    null,
                    Color.Red,
                    MathHelper.ToRadians((float)Direction),
                    new Vector2(_texture.Bounds.Width / 2, _texture.Bounds.Height / 2),
                    new SpriteEffects(),
                    0f);
            }

        }
    }

    public enum Direction
    {
        Up = 0,
        Down = 180,
        Left = -90,
        Right = 90,
        Undefined = -1
    }
}
