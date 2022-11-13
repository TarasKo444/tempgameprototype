using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectMono.Source.Engine;

namespace ProjectMono.Source
{
    public class Map
    {
        private int _cols;
        private int _rows;

        private Texture2D _texture1px;

        private Dictionary<Vector2, Cell> Cells = new Dictionary<Vector2, Cell>();

        private Direction ChoosedDirection;
        private CellType ChoosedCell;

        public Map(int cols, int rows)
        {
            _cols = cols;
            _rows = rows;
            _texture1px = new Texture2D(Globals.SpriteBatch.GraphicsDevice, 1, 1);
            _texture1px.SetData(new Color[] { Color.Black });
        }

        public void Update()
        {
            var mouseState = Mouse.GetState();
            var keyboardState = Keyboard.GetState();

            Cell cell = new Arrow(mouseState.Position.ToVector2(), ChoosedDirection);

            if (keyboardState.IsKeyDown(Keys.D1))
            {
                ChoosedDirection = Direction.Up;
            }
            else if (keyboardState.IsKeyDown(Keys.D2))
            {
                ChoosedDirection = Direction.Down;
            }
            else if (keyboardState.IsKeyDown(Keys.D3))
            {
                ChoosedDirection = Direction.Right;
            }
            else if (keyboardState.IsKeyDown(Keys.D4))
            {
                ChoosedDirection = Direction.Left;
            }
            else if (keyboardState.IsKeyDown(Keys.D5))
            {
                ChoosedCell = CellType.Arrow;
            }
            else if (keyboardState.IsKeyDown(Keys.D6))
            {
                ChoosedCell = CellType.Source;
            }
            else if (keyboardState.IsKeyDown(Keys.D0))
            {
                int i = 1;
                foreach(var cell2 in Cells)
                {
                    Debug.WriteLine($"\t{i} : ({cell2.Key.X} ; {cell2.Key.Y}) {cell2.Value.Have_Signal}");
                    i++;
                }
            }
            else if (keyboardState.IsKeyDown(Keys.Delete))
            {
                Cells.Clear();
            }

            


            if (mouseState.LeftButton == ButtonState.Pressed)
            {

                switch (ChoosedCell)
                {
                    case CellType.Arrow:
                        cell = new Arrow(mouseState.Position.ToVector2(), ChoosedDirection);
                        break;
                    case CellType.Source:
                        cell = new SignalSource(mouseState.Position.ToVector2());
                        break;
                }

                cell.SnapToGrid();

                if (Cells.ContainsKey(cell.Position) && Cells[cell.Position].Direction != cell.Direction)
                {
                    Cells[cell.Position] = cell;
                }
                else if (!Cells.ContainsKey(cell.Position))
                {
                    Cells.Add(cell.Position, cell);
                }
            }


            if (mouseState.RightButton == ButtonState.Pressed)
            {

                var temp = new Arrow(mouseState.Position.ToVector2(), Direction.Up);
                temp.SnapToGrid();

                if (Cells.ContainsKey(temp.Position))
                {
                    Cells.Remove(temp.Position);
                }
            }
        }

        public void NextStep()
        {
            var update_cells = new Dictionary<Vector2, Cell>();
            foreach (var item in Cells)
                update_cells[item.Key] = item.Value;

            foreach (var i in Cells)
            {
                var cell = i.Value;
                if (cell.Have_Signal)
                {
                    switch (cell.Direction)
                    {
                        case Direction.Up:

                            var upper = new Vector2(cell.Position.X, cell.Position.Y - Globals.CellSize);

                            if (Cells.ContainsKey(upper))
                            {
                                update_cells[upper].Have_Signal = true;
                            }

                            cell.Have_Signal = false;

                            break;

                        case Direction.Undefined:

                            List<Vector2> positions = new List<Vector2>()
                            {
                                new Vector2(cell.Position.X, cell.Position.Y - Globals.CellSize),
                                new Vector2(cell.Position.X, cell.Position.Y + Globals.CellSize),
                                new Vector2(cell.Position.X - Globals.CellSize, cell.Position.Y),
                                new Vector2(cell.Position.X + Globals.CellSize, cell.Position.Y),
                            };

                            foreach (var pos in positions)
                            {
                                if (Cells.ContainsKey(pos))
                                {
                                    update_cells[pos].Have_Signal = true;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            Cells = update_cells;
        }

        public void Draw()
        {
            for (float x = -1; x < _cols; x++)
            {
                Rectangle rectangle = new Rectangle(10 + (int)(Globals.CellSize + x * Globals.CellSize), 0, 1, Globals.CellSize * _cols + Globals.CellSize);
                Globals.SpriteBatch.Draw(_texture1px, rectangle, Color.Black);
            }
            for (float y = -1; y < _rows; y++)
            {
                Rectangle rectangle = new Rectangle(0, 10 + (int)(Globals.CellSize + y * Globals.CellSize), Globals.CellSize * _rows + Globals.CellSize, 1);
                Globals.SpriteBatch.Draw(_texture1px, rectangle, Color.Black);
            }

            foreach(var cell in Cells)
            {
                cell.Value.Draw();
            }
        }
    }
}
