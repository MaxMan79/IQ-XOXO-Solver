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

        private bool _isOccupied;

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
                    return _isOccupied;
                }
            }
            set
            {
                _isOccupied = value;
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
        /// ToString override
        /// </summary>
        /// <returns>Formatted string</returns>
        public override string ToString()
        {
            return string.Format("{0} @ ({1}, {2})", Type.ToString(), Position.X, Position.Y);
        }
    }
}
