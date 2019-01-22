using System;
using System.Collections.Generic;

namespace IQ_XOXO_Solver.Models
{
    /// <summary>
    /// Represents the extents of a group of cells.
    /// </summary>
    /// <remarks>
    /// The group of cells is meant to be contiguous, but this is not enforced
    /// by the Extents.
    /// </remarks>
    public class Extents
    {
        // ***************************************************************************
        // *                               Fields                                    *
        // ***************************************************************************

        private int _minX;

        private int _minY;

        private int _maxX;

        private int _maxY;

        // ***************************************************************************
        // *                            Constructors                                 *
        // ***************************************************************************

        /// <summary>
        /// Initializes a new instance of the <see cref="Extents"/> class.
        /// </summary>
        public Extents()
        {
            _minX = int.MaxValue;
            _maxX = int.MinValue;

            _minY = int.MaxValue;
            _maxY = int.MinValue;
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

        // ***************************************************************************
        // *                            Public Methods                               *
        // ***************************************************************************

        /// <summary>
        /// Extends the current extents by one cell's position.
        /// </summary>
        /// <remarks>
        /// If the cell's position lies completely within the current extents, nothing happens.
        /// </remarks>
        /// <param name="cell">Cell whose position can extend the extents.</param>
        public void Extend(Cell cell)
        {
            _minX = Math.Min(_minX, cell.Position.X);
            _maxX = Math.Max(_maxX, cell.Position.X);

            _minY = Math.Min(_minY, cell.Position.Y);
            _maxY = Math.Max(_maxY, cell.Position.Y);

            Width = _maxX - _minX + 1;
            Height = _maxY - _minY + 1;
        }

        /// <summary>
        /// Extends the current extents by a group of cells
        /// </summary>
        /// <param name="cells">List of cells by which to extend the extents.</param>
        public void Extend(List<Cell> cells)
        {
            foreach (var cell in cells)
            {
                Extend(cell);
            }
        }

        /// <summary>
        /// Tests to see if another extents, 'testFit', fits within this extents.
        /// </summary>
        /// <remarks>
        /// The testFit extents is checked to see whether it fits in its current orientation 
        /// as well as rotated 90 degrees.  Either orientation is a valid fit.
        /// </remarks>
        /// <param name="testFit">The extents which is checked to fit inside the calling extents.</param>
        /// <returns>True if testFit fits completely within the calling extents; false otherwise.</returns>
        public bool FitsWithin(Extents testFit)
        {
            return ((testFit.Width <= Width) && (testFit.Height <= Height)) ||
                   ((testFit.Height <= Width) && (testFit.Width <= Height));
        }
    }
}
