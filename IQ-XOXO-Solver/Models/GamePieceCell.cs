namespace IQ_XOXO_Solver.Models
{
    /// <summary>
    /// Represents a single game piece cell.  Game piece cells can be flipped over, which makes them
    /// take on the opposite cell type
    /// </summary>
    public class GamePieceCell : Cell
    {
        // ***************************************************************************
        // *                            Constructors                                 *
        // ***************************************************************************

        /// <summary>
        /// Initializes a new instance of the <see cref="GamePieceCell"/> class.
        /// </summary>
        /// <param name="x">X-Position</param>
        /// <param name="y">Y-Position</param>
        /// <param name="type">Cell Type</param>
        public GamePieceCell(int x, int y, CellType type) : base(x, y, type)
        {
        }

        // ***************************************************************************
        // *                             Properties                                  *
        // ***************************************************************************

        public bool HasBeenTested { get; set; }

        // ***************************************************************************
        // *                            Public Methods                               *
        // ***************************************************************************

        /// <summary>
        /// Flips the cell over, making it become the opposite type
        /// </summary>
        public void Flip()
        {
            Type = GetOppositeType();
        }
    }
}
