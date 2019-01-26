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
        // *                            Constructors                                 *
        // ***************************************************************************

        /// <summary>
        /// Initializes a new instance of the <see cref="Extents"/> class.
        /// </summary>
        public Extents()
        {
            MinX = int.MaxValue;
            MaxX = int.MinValue;

            MinY = int.MaxValue;
            MaxY = int.MinValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Extents"/> class.
        /// </summary>
        /// <param name="minX">Minimum X</param>
        /// <param name="maxX">Maximum X</param>
        /// <param name="minY">Minimum Y</param>
        /// <param name="maxY">Maximum Y</param>
        public Extents(int minX, int maxX, int minY, int maxY)
        {
            if (maxX < minX)
            {
                MinX = MaxX;
                MaxX = minX;          

                Width = MaxX - MinX + 1;
            }
            else if (minX < maxX)
            {
                MinX = minX;
                MaxX = maxX;

                Width = MaxX - MinX + 1;
            }
            else
            {
                MinX = int.MaxValue;
                MaxX = int.MinValue;
            }

            if (maxY < minY)
            {
                MinY = maxY;
                MaxY = minY;

                Height = MaxY - MinY + 1;
            }
            else if (minY < maxY)
            {
                MinY = minY;
                MaxY = maxY;

                Height = MaxY - MinY + 1;
            }
            else
            {
                MinY = int.MaxValue;
                MaxY = int.MinValue;
            }  
        }

        // ***************************************************************************
        // *                             Properties                                  *
        // ***************************************************************************

        /// <summary>
        /// Gets the minimum X
        /// </summary>
        public int MinX { get; private set; }

        /// <summary>
        /// Gets the maximum X
        /// </summary>
        public int MaxX { get; private set; }

        /// <summary>
        /// Gets the minimum Y
        /// </summary>
        public int MinY { get; private set; }

        /// <summary>
        /// Gets the maximumY
        /// </summary>
        public int MaxY { get; private set; }

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
            MinX = Math.Min(MinX, cell.Position.X);
            MaxX = Math.Max(MaxX, cell.Position.X);

            MinY = Math.Min(MinY, cell.Position.Y);
            MaxY = Math.Max(MaxY, cell.Position.Y);

            Width = MaxX - MinX + 1;
            Height = MaxY - MinY + 1;
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

        /// <summary>
        /// Tests to see if a point is within the extents region.
        /// </summary>
        /// <param name="point">The point</param>
        /// <returns>True if the point is within the extents; false otherwise.</returns>
        public bool IsWithin(Point2D point)
        {
            return ((MinX <= point.X && point.X <= MaxX) &&
                    (MinY <= point.Y && point.Y <= MaxY));
        }

        /// <summary>
        /// ToString override
        /// </summary>
        /// <returns>Formatted string</returns>
        public override string ToString()
        {
            return string.Format("{0} x {1}", Width, Height);
        }
    }
}
