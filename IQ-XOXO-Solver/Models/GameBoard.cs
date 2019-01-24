using System.Collections.Generic;
using System.Linq;

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
            if (width > 0 && height > 0)
            {
                Cells = new GridCell[width, height];

                Width = width;
                Height = height;

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

                        Cells[x, y] = new GridCell(x, y, type);
                    }
                }

                // Create list of boundary cells for neighbor relationships below
                List<GridCell> boundaryCells = new List<GridCell>();

                for (int x = -1; x <= width; x++)
                {
                    for (int y = -1; y <= height; y++)
                    {
                        if ((x == -1) || (x == width) ||
                            (y == -1) || (y == height))
                        {
                            boundaryCells.Add(new GridCell(x, y, Cell.CellType.Boundary));
                        }
                    }
                }

                // Create neighbor relationships between game board cells
                List<GridCell> neighbors = new List<GridCell>(8);
                GridCell currentCell;
                GridCell boundaryNeighbor;
                int indexX;
                int indexY;

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        currentCell = Cells[x, y];
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
                                        // Boundary neighbor
                                        boundaryNeighbor = boundaryCells.FirstOrDefault(cell => cell.Position.X == indexX && cell.Position.Y == indexY);

                                        if (boundaryNeighbor != null)
                                        {
                                            neighbors.Add(boundaryNeighbor);
                                        }
                                    }
                                    else
                                    {
                                        // Interior neighbor
                                        neighbors.Add(Cells[indexX, indexY]);
                                    }   
                                }
                            }
                        }

                        currentCell.SetNeighbors(neighbors);
                    }
                }
            }
            else
            {
                Cells = new GridCell[0, 0];

                Width = 0;
                Height = 0;
            }
        }

        // ***************************************************************************
        // *                             Properties                                  *
        // ***************************************************************************

        /// <summary>
        /// Gets the grid cells
        /// </summary>
        public GridCell[,] Cells { get; private set; }

        /// <summary>
        /// Gets the width in number of cells
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the height in number of cells
        /// </summary>
        public int Height { get; private set; }

        // ***************************************************************************
        // *                            Public Methods                               *
        // ***************************************************************************

        /// <summary>
        /// Gets the flood zones
        /// </summary>
        /// <returns>List of flood zones</returns>
        public List<FloodZone> GetFloodZones()
        {
            GridCell seedCell;
            List<FloodZone> floodZones = new List<FloodZone>();

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    seedCell = Cells[x, y];

                    if (!seedCell.IsOccupied && !IsCellFlooded(seedCell, floodZones))
                    {
                        floodZones.Add(new FloodZone(seedCell));
                    }
                }
            }

            return floodZones;
        }

        // ***************************************************************************
        // *                           Private Methods                               *
        // ***************************************************************************

        /// <summary>
        /// Determines is a cell has been flooded.
        /// </summary>
        /// <param name="cell">The cell</param>
        /// <param name="floodZones">Current flood zones</param>
        /// <returns>True if the cell belongs to one of the current flood zones; false otherwise.</returns>
        private bool IsCellFlooded(GridCell cell, List<FloodZone> floodZones)
        {
            foreach (var floodZone in floodZones)
            {
                if (floodZone.Contains(cell))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
