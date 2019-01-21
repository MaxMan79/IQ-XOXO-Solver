using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace IQ_XOXO_Solver.Models
{
    /// <summary>
    /// Represents a game piece that can be flipped and rotated into different orientations
    /// for testing on the game board.
    /// </summary>
    public class GamePiece
    {
        // ***************************************************************************
        // *                               Fields                                    *
        // ***************************************************************************

        private List<GamePieceCell> _cells;

        private GamePieceCell _originCell;

        private int _rotationDeg;

        private bool _isFlipped;

        // ***************************************************************************
        // *                            Constructors                                 *
        // ***************************************************************************

        /// <summary>
        /// Initializes a new instance of the <see cref="GamePiece"/> class.
        /// </summary>
        /// <param name="cells">List of game piece cells</param>
        public GamePiece(List<GamePieceCell> cells, Color color)
        {
            _cells = cells;
            Color = color;

            Reset();
        }

        // ***************************************************************************
        // *                             Properties                                  *
        // ***************************************************************************

        /// <summary>
        /// Gets the game piece color
        /// </summary>
        public Color Color { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this piece has been completely tested at a
        /// certain game board location.
        /// </summary>
        public bool HasBeenTested { get; private set; }

        // ***************************************************************************
        // *                            Public Methods                               *
        // ***************************************************************************

        /// <summary>
        /// Rotates the game piece 90 degrees clockwise.
        /// </summary>
        public void RotateCw90()
        {
            int temp;

            foreach (var cell in _cells)
            {
                temp = cell.Position.X;
                cell.Position.X = -cell.Position.Y;
                cell.Position.Y = temp;
            }

            _rotationDeg += 90;

            if (_rotationDeg == 360)
            {
                _rotationDeg = 0;
            }
        }

        /// <summary>
        /// Flips the game piece over.  
        /// </summary>
        /// <remarks>
        /// Each cell will be flipped and mirrored across the y-axis.  The game piece has the 
        /// same coordinate system as the game board, where the +x-direction is to the right 
        /// and +y-direction is downward.
        /// </remarks>
        public void Flip()
        {
            foreach (var cell in _cells)
            {
                cell.Flip();
                cell.Position.X = -cell.Position.X;
            }

            _isFlipped = !_isFlipped;

            if (_rotationDeg == 90 || _rotationDeg == 270)
            {
                _rotationDeg += 180;

                if (_rotationDeg >= 360)
                {
                    _rotationDeg = _rotationDeg % 360;
                }
            }
        }

        // ***************************************************************************
        // *                           Private Methods                               *
        // ***************************************************************************

        /// <summary>
        /// Resets the game piece back to its original position/orientation.
        /// </summary>
        private void Reset()
        {
            if (_isFlipped)
            {
                Flip();
            }

            while (_rotationDeg != 0)
            {
                RotateCw90();
            }

            _originCell = _cells.FirstOrDefault(cell => cell.Position.X == 0 && cell.Position.Y == 0);

            if (_originCell == null)
            {
                _originCell = _cells.FirstOrDefault();
            }

            foreach (var cell in _cells)
            {
                cell.HasBeenTested = false;
            }

            HasBeenTested = false;
        }
    }
}
