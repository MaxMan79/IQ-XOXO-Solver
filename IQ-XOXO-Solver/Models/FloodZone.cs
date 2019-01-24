using System;
using System.Collections.Generic;
using System.Linq;

namespace IQ_XOXO_Solver.Models
{
    /// <summary>
    /// Represents a group of contiguous, unoccupied grid cells.
    /// </summary>
    /// <remarks>
    /// The idea is to think of the game board and pieces in a topographical manner; the
    /// game board is the low land, or valley, and the game pieces are the high land,
    /// or mountains.  If water is poured into the 'seedCell', which is part of a valley,
    /// the water will spill out into the adjacent valley cells until the localized valley
    /// has been completely flooded.  The resulting lake is the FloodZone.
    /// 
    /// A FloodZone consists of one or more cells.  A game board may zero or more FloodZones 
    /// at any given time.  Zero would mean the game board has been solved.
    /// </remarks>
    public class FloodZone
    {
        // ***************************************************************************
        // *                               Fields                                    *
        // ***************************************************************************

        private List<GridCell> _floodedCells;

        private int _currentCellIndex;

        // ***************************************************************************
        // *                            Constructors                                 *
        // ***************************************************************************

        /// <summary>
        /// Initializes a new instance of the <see cref="FloodZone"/> class.
        /// </summary>
        /// <param name="seedCell">The starting cell of the flood</param>
        public FloodZone(GridCell seedCell)
        {
            _floodedCells = new List<GridCell>();
            _currentCellIndex = -1;
            Extents = new Extents();

            if (!seedCell.IsOccupied)
            {
                _floodedCells.Add(seedCell);
                Extents.Extend(seedCell);

                Flood(seedCell.GetUnoccupiedNeighborsCardinal());          
            }
        }

        // ***************************************************************************
        // *                             Properties                                  *
        // ***************************************************************************

        /// <summary>
        /// Gets the extents of the flood zone
        /// </summary>
        public Extents Extents { get; private set; }

        /// <summary>
        /// Gets the number of cells in the flood zone
        /// </summary>
        public int ContainsCount
        {
            get
            {
                return _floodedCells.Count;
            }
        }

        // ***************************************************************************
        // *                            Public Methods                               *
        // ***************************************************************************

        /// <summary>
        ///  Sort the flooded cells in descending order based on the number or occupied neighbors
        /// </summary>
        public void Sort()
        {
            var sorter = new List<Tuple<GridCell, int>>();

            foreach (var cell in _floodedCells)
            {
                sorter.Add(new Tuple<GridCell, int>(cell, cell.GetOccupiedNeighborCount()));
            }

            sorter.Sort(new DescendingTupleComparer());

            _floodedCells = (from sorted in sorter select sorted.Item1).ToList();
        }

        /// <summary>
        /// Determines whether the flood zone contains a given cell.
        /// </summary>
        /// <param name="cell">The cell to test</param>
        /// <returns>True if the cell belongs to this flood zone; false otherwise.</returns>
        public bool Contains(GridCell cell)
        {
            return _floodedCells.Contains(cell);
        }

        /// <summary>
        /// Gets the next most-bounded cell.  Cells are bounded by neighbors that are occupied.
        /// </summary>
        /// <remarks>
        /// The flooded cells are sorted in descending order by the number of occupied
        /// neighbors.  This method walks the list one step at a time, starting with the 
        /// the most-bounded cell to the least-bounded cell in the flood zone.
        /// </remarks>
        /// <returns>The next most-bounded cell in the flood zone, or null if all cells are exhausted.</returns>
        public GridCell GetNextMostBoundedCell()
        {
            _currentCellIndex++;

            if (_currentCellIndex < _floodedCells.Count)
            {
                return _floodedCells[_currentCellIndex];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ToString override
        /// </summary>
        /// <returns>Formatted string</returns>
        public override string ToString()
        {
            return string.Format("{0} cells @ {1}", ContainsCount, Extents.ToString());
        }

        // ***************************************************************************
        // *                           Private Methods                               *
        // ***************************************************************************

        /// <summary>
        /// Recursively floods adjacent, unoccupied cells
        /// </summary>
        /// <param name="unoccupiedCells">List of unoccupied cells that are members of a contiguous group</param>
        private void Flood(List<GridCell> unoccupiedCells)
        {
            foreach (var cell in unoccupiedCells)
            {
                // If this cell has not been flooded yet, add it to the list of flooded cells
                if (!_floodedCells.Contains(cell))
                {
                    _floodedCells.Add(cell);
                    Extents.Extend(cell);

                    Flood(cell.GetUnoccupiedNeighborsCardinal());
                }
            }
        }

        // ***************************************************************************
        // *                           Private Classes                               *
        // ***************************************************************************

        /// <summary>
        /// Used to sort the flooded cells in descending order by their number of occupied neighbors
        /// </summary>
        private class DescendingTupleComparer : IComparer<Tuple<GridCell, int>>
        {
            public int Compare(Tuple<GridCell, int> a, Tuple<GridCell, int> b)
            {
                return Math.Sign(b.Item2 - a.Item2);
            }
        }
    }
}
