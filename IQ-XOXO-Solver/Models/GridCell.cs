using System;
using System.Collections.Generic;

namespace IQ_XOXO_Solver.Models
{
    /// <summary>
    /// Represents a single cell in a playing grid.  Grid cells are aware of their neighbors.
    /// </summary>
    public class GridCell : Cell
    {
        // ***************************************************************************
        // *                               Fields                                    *
        // ***************************************************************************

        private GridCell[,] _neighbors;

        // ***************************************************************************
        // *                            Constructors                                 *
        // ***************************************************************************

        /// <summary>
        /// Initializes a new instance of the <see cref="GridCell"/> class.
        /// </summary>
        /// <param name="x">X-Position</param>
        /// <param name="y">Y-Position</param>
        /// <param name="type">Cell Type</param>
        public GridCell(int x, int y, CellType type) : base(x, y, type)
        {           
            _neighbors = new GridCell[3, 3];
            _neighbors[1, 1] = this;
        }

        // ***************************************************************************
        // *                             Properties                                  *
        // ***************************************************************************

        /// <summary>
        /// Gets or sets the game piece that is occupying this cell
        /// </summary>
        public GamePiece GamePiece { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this cell is occupied by a game piece
        /// </summary>
        /// <remarks>
        /// Boundary cells always considered occupied
        /// </remarks>
        public bool IsOccupied
        {
            get
            {
                if (Type == CellType.Boundary)
                {
                    return true;
                }
                else
                {
                    return GamePiece != null;
                }
            }
        }

        /// <summary>
        /// Gets the neighbor to the North in the playing grid
        /// </summary>
        public GridCell NeighborN
        {
            get
            {
                return _neighbors[1, 0];
            }
        }

        /// <summary>
        /// Gets the neighbor to the Northeast in the playing grid
        /// </summary>
        public GridCell NeighborNE
        {
            get
            {
                return _neighbors[2, 0];
            }
        }

        /// <summary>
        /// Gets the neighbor to the East in the playing grid
        /// </summary>
        public GridCell NeighborE
        {
            get
            {
                return _neighbors[2, 1];
            }
        }

        /// <summary>
        /// Gets the neighbor to the Southeast in the playing grid
        /// </summary>
        public GridCell NeighborSE
        {
            get
            {
                return _neighbors[2, 2];
            }
        }

        /// <summary>
        /// Gets the neighbor to the South in the playing grid
        /// </summary>
        public GridCell NeighborS
        {
            get
            {
                return _neighbors[1, 2];
            }
        }

        /// <summary>
        /// Gets the neighbor to the Southwest in the playing grid
        /// </summary>
        public GridCell NeighborSW
        {
            get
            {
                return _neighbors[0, 2];
            }
        }

        /// <summary>
        /// Gets the neighbor to the West in the playing grid
        /// </summary>
        public GridCell NeighborW
        {
            get
            {
                return _neighbors[0, 1];
            }
        }

        /// <summary>
        /// Gets the neighbor to the Northwest in the playing grid
        /// </summary>
        public GridCell NeighborNW
        {
            get
            {
                return _neighbors[0, 0];
            }
        }

        // ***************************************************************************
        // *                            Public Methods                               *
        // ***************************************************************************

        /// <summary>
        /// Sets the cell's neighbors. 
        /// </summary>
        /// <remarks>
        /// This method should only be called once, when the game board is being initialized.
        /// </remarks>
        /// <param name="neighbors">List of neighbors</param>
        public void SetNeighbors(List<GridCell> neighbors)
        {
            int offsetX;
            int offsetY;
            int indexX;
            int indexY;

            foreach (var neighbor in neighbors)
            {
                offsetX = neighbor.Position.X - Position.X;
                offsetY = neighbor.Position.Y - Position.Y;

                if (!(offsetX == 0 && offsetY == 0))
                {
                    if (Math.Abs(offsetX) <= 1 && Math.Abs(offsetY) <= 1)
                    {
                        indexX = offsetX + 1;
                        indexY = offsetY + 1;

                        _neighbors[indexX, indexY] = neighbor;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the number of occupied neighbors, including neighboring boundary cells
        /// </summary>
        /// <returns>The number of occupied neighbors</returns>
        public int GetOccupiedNeighborCount()
        {
            int occupiedCount = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!(i == 1 && j == 1))
                    {
                        if (_neighbors[i, j].IsOccupied)
                        {
                            occupiedCount++;
                        }
                    }
                }
            }

            return occupiedCount;
        }

        /// <summary>
        /// Gets the list of unoccupied neighbor cells
        /// </summary>
        /// <returns>The list of unoccupied neighbor cells</returns>
        public List<GridCell> GetUnoccupiedNeighborsCardinal()
        {
            GridCell neighbor;
            var unoccupiedNeighbors = new List<GridCell>();

            // North neighbor
            neighbor = _neighbors[1, 0];

            if (!neighbor.IsOccupied)
            {
                unoccupiedNeighbors.Add(neighbor);
            }

            // East neighbor
            neighbor = _neighbors[2, 1];

            if (!neighbor.IsOccupied)
            {
                unoccupiedNeighbors.Add(neighbor);
            }

            // South neighbor
            neighbor = _neighbors[1, 2];

            if (!neighbor.IsOccupied)
            {
                unoccupiedNeighbors.Add(neighbor);
            }

            // West neighbor
            neighbor = _neighbors[0, 1];

            if (!neighbor.IsOccupied)
            {
                unoccupiedNeighbors.Add(neighbor);
            }

            return unoccupiedNeighbors;
        }

        /// <summary>
        /// Gets a grid cell lying at an absoute position.
        /// </summary>
        /// <remarks>
        /// The grid cell is retrieved without any direct knowledge of the game board.  The grid
        /// cell of interest is found by traversing the grid neighbor-by-neighbor.  If a boundary
        /// cell is encountered, the search ends immediately and the boundary is returned.
        /// </remarks>
        /// <param name="x">The x-position of the cell of interest</param>
        /// <param name="y">The y-position of the cell of interest</param>
        /// <returns>The grid cell lying at (x, y).  If such a cell does not exist, returns the first-encountered boundary cell.</returns>
        public GridCell GetCellAbsolute(int x, int y)
        {
            if ((Position.X == x && Position.Y == y) ||
                Type == CellType.Boundary)
            {
                return this;
            }
            else
            {
                int relativeOffsetX = Clamp(x - Position.X, -1, 1);
                int relativeOffsetY = Clamp(y - Position.Y, -1, 1);

                return _neighbors[relativeOffsetX + 1, relativeOffsetY + 1].GetCellAbsolute(x, y);
            }
        }

        /// <summary>
        /// ToString override
        /// </summary>
        /// <returns>Formatted string</returns>
        public override string ToString()
        {
            return string.Format("{0} @ {1}", Type.ToString(), Position.ToString());
        }

        // ***************************************************************************
        // *                           Private Methods                               *
        // ***************************************************************************

        /// <summary>
        /// Clamps an integer between a minimum and maximum
        /// </summary>
        /// <param name="value">The value to clamp</param>
        /// <param name="min">Minimum possible value</param>
        /// <param name="max">Maximum possible value</param>
        /// <returns>The clamped value</returns>
        private int Clamp(int value, int min, int max)
        {
            if (value > max)
            {
                return max;
            }
            else if (value < min)
            {
                return min;
            }
            else
            {
                return value;
            }
        }
    }
}
