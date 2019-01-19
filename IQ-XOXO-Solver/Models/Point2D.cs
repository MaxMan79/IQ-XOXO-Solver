namespace IQ_XOXO_Solver.Models
{
    /// <summary>
    /// Represents a simple 2D position
    /// </summary>
    public class Point2D
    {
        // ***************************************************************************
        // *                            Constructors                                 *
        // ***************************************************************************

        /// <summary>
        /// Initializes a new instance of the <see cref="Point2D"/> class.
        /// </summary>
        public Point2D()
        {
            X = 0;
            Y = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point2D"/> class.
        /// </summary>
        /// <param name="x">X-Position</param>
        /// <param name="y">Y-Position</param>
        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        // ***************************************************************************
        // *                             Properties                                  *
        // ***************************************************************************

        /// <summary>
        /// Gets or sets the X-Position
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the Y-Position
        /// </summary>
        public int Y { get; set; }
    }
}
