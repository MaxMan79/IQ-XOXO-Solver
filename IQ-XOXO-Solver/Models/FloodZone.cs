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
            Extents = new Extents();

            if (!seedCell.IsOccupied)
            {
                _floodedCells.Add(seedCell);
                Extents.Extend(seedCell);

                Flood(_floodedCells);

                // Sort the flooded cells in descending order based on the number or occupied neighbors
                var sorter = new List<Tuple<GridCell, int>>();

                foreach (var cell in _floodedCells)
                {
                    sorter.Add(new Tuple<GridCell, int>(cell, cell.GetOccupiedNeighborCount()));
                }

                sorter.Sort(new DescendingTupleComparer());

                _floodedCells = (from sorted in sorter select sorted.Item1).ToList();
            }
        }

        // ***************************************************************************
        // *                             Properties                                  *
        // ***************************************************************************

        /// <summary>
        /// Gets the extents of the flood zone
        /// </summary>
        public Extents Extents { get; private set; }

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

                    Flood(cell.GetUnoccupiedNeighbors());
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
