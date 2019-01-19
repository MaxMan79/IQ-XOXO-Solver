namespace IQ_XOXO_Solver.Models
{
    /// <summary>
    /// Represents a cell that has a 2D position and a type (X, O, or Boundary)
    /// </summary>
    public abstract class Cell
    {
        // ***************************************************************************
        // *                               Enums                                     *
        // ***************************************************************************

        /// <summary>
        /// Cell type enum (X, O, or Boundary)
        /// </summary>
        public enum CellType
        {
            Boundary,
            X,
            O
        }

        // ***************************************************************************
        // *                             Properties                                  *
        // ***************************************************************************

        /// <summary>
        /// Gets the cell's type
        /// </summary>
        public CellType Type { get; protected set; }

        /// <summary>
        /// Gets the cell's X-Y-position
        /// </summary>
        public Point2D Position { get; protected set; }

        // ***************************************************************************
        // *                           Protected Methods                             *
        // ***************************************************************************

        /// <summary>
        /// Gets the opposite type of this cell
        /// </summary>
        /// <returns>X if the cell is an O, O if the cell is an X, and Boundary if this cell is a Boundary</returns>
        protected CellType GetOppositeType()
        {
            switch (Type)
            {
                case CellType.X:
                    return CellType.O;

                case CellType.O:
                    return CellType.X;

                default:
                    return CellType.Boundary;
            }
        }
    }
}
