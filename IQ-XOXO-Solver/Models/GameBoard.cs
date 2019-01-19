using System.Collections.Generic;

namespace IQ_XOXO_Solver.Models
{
    /// <summary>
    /// Represents the game board, which consists of a 2D grid of alternating X-O-cells 
    /// which is surrounded by a wall of boundary cells.  The grid has unit spacing and
    /// supports any non-zero, non-negative board size.
    /// </summary>
    /// <remarks>
    /// Coordinate System:  The upper-leftmost cell is located at (0, 0).  Positive-X is to
    /// the right, and positive-Y is downward.  Each cell is 1 unit in width and height.
    /// 
    /// Example:  The IQ XOXO board from Smart Games is 10 x 5.
    /// 
    /// ---------------► +X
    /// |
    /// |  (0, 0)                                       (9, 0)
    /// |    X    O    X    O    X    O    X    O    X    O
    /// |    O    X    O    X    O    X    O    X    O    X
    /// |    X    O    X    O    X    O    X    O    X    O
    /// |    O    X    O    X    O    X    O    X    O    X
    /// |    X    O    X    O    X    O    X    O    X    O
    /// |  (0, 4)                                       (9, 4)
    /// ▼
    /// +Y
    /// 
    /// </remarks>
    public class GameBoard
    {
        // ***************************************************************************
        // *                               Fields                                    *
        // ***************************************************************************

        private GridCell[,] _cells;

        // ***************************************************************************
        // *                            Constructors                                 *
        // ***************************************************************************

        /// <summary>
        /// Initializes a new instance of the <see cref="GameBoard"/> class.
        /// </summary>
        /// <remarks>
        /// If either "width" or "height" are less than one, the result will be an
        /// invalid board.
        /// </remarks>
        /// <param name="width">Number of cells that make up the width</param>
        /// <param name="height">Number of cells that make up the height</param>
        public GameBoard(int width, int height)
        {
            _cells = new GridCell[width, height];

            if (width > 0 && height > 0)
            {
                Width = width;
                Height = Height;

                // Fill game board with cells, alternating X and O
                Cell.CellType type;
              
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if ((x + y) % 2 == 0)
                        {
                            type = Cell.CellType.X;
                        }
                        else
                        {
                            type = Cell.CellType.O;
                        }

                        _cells[x, y] = new GridCell(x, y, type);
                    }
                }

                // Create neighbor relationships between game board cells
                List<GridCell> neighbors = new List<GridCell>(8);
                GridCell currentCell;
                int indexX;
                int indexY;

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        currentCell = _cells[x, y];
                        neighbors.Clear();

                        // Collect surrounding cells into neighbor list
                        for (int offsetX = -1; offsetX <= 1; offsetX++)
                        {
                            for (int offsetY = -1; offsetY <= 1; offsetY++)
                            {
                                if (!(offsetX == 0 && offsetY == 0))
                                {
                                    indexX = x + offsetX;
                                    indexY = y + offsetY;

                                    if ((indexX < 0) || (indexX >= width) ||
                                        (indexY < 0) || (indexY >= height))
                                    {
                                        continue;
                                    }

                                    neighbors.Add(_cells[indexX, indexY]);
                                }
                            }
                        }

                        currentCell.SetNeighbors(neighbors);
                    }
                }
            }
            else
            {
                Width = 0;
                Height = 0;
            }
        }

        // ***************************************************************************
        // *                             Properties                                  *
        // ***************************************************************************

        /// <summary>
        /// Gets the width in number of cells
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the height in number of cells
        /// </summary>
        public int Height { get; private set; }
    }
}
