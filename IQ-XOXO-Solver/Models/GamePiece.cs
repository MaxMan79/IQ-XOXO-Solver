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
        /// Gets the game piece color.
        /// </summary>
        public Color Color { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this piece has been completely tested at its
        /// current game board location.
        /// </summary>
        public bool HasBeenCompletelyTested { get; private set; }
        
        /// <summary>
        /// Gets or sets a value indicating if this piece is placed on the board.
        /// </summary>
        public bool IsPlaced { get; set; }

        // ***************************************************************************
        // *                            Public Methods                               *
        // ***************************************************************************

        /// <summary>
        /// Moves the origin cell to a new position, with the remaining cells moving with
        /// the same offset.
        /// </summary>
        /// <param name="x">The new x-position</param>
        /// <param name="y">The new y-position</param>
        public void MoveTo(int x, int y)
        {
            int offsetX = x - _originCell.Position.X;
            int offsetY = y - _originCell.Position.Y;

            foreach (var cell in _cells)
            {
                cell.Position.X += offsetX;
                cell.Position.Y += offsetY;
            }
        }

        /// <summary>
        /// Rotates the game piece 90 degrees clockwise about the origin cell.
        /// </summary>
        public void RotateCw90()
        {
            int temp;
            int originCellX = _originCell.Position.X;
            int originCellY = _originCell.Position.Y;

            // 1) Move the piece such that the origin cell is located at (0,0)
            // 2) Rotate the piece by 90 degrees clockwise
            // 3) Move the piece back such that the origin cell is loated where it was prior to step 1
            foreach (var cell in _cells)
            {
                // Pseudo-code
                //
                // //----------------------------------------------------------------------
                // // Subtract out the origin cell's position, to move the piece to (0,0).
                // //----------------------------------------------------------------------
                //
                // aboutOriginX = cell.Position.X - originCellX;
                // aboutOriginY = cell.Position.Y - originCellY;
                //
                // //----------------------------------------------------------------------
                // // Swap the cell position's x- and y-values (negating the y-value) to rotate the
                // // cell 90 degrees clockwise about (0,0).  E.g. (3, 2) => (-2, 3)
                // //----------------------------------------------------------------------
                //
                // cell.Position.X = -1 * aboutOriginY;
                // cell.Position.Y = aboutOriginX;
                //
                // //----------------------------------------------------------------------
                // // Add back the origin cell's position, to move the piece back to its
                // // position before we began the rotation.
                // //----------------------------------------------------------------------
                //
                // cell.Position.X += originCellX;
                // cell.Position.Y += originCellY;

                temp = cell.Position.X;
                cell.Position.X = originCellX - (cell.Position.Y - originCellY);
                cell.Position.Y = (temp - originCellX) + originCellY;
            }

            _rotationDeg += 90;

            if (_rotationDeg == 360)
            {
                _rotationDeg = 0;
            }
        }

        /// <summary>
        /// Flips the game piece over, centered about the origin cell.  
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
                cell.Position.X = (2 * _originCell.Position.X)  - cell.Position.X;
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
            _originCell = _cells.First();

            MoveTo(0, 0);

            if (_isFlipped)
            {
                Flip();
            }

            while (_rotationDeg != 0)
            {
                RotateCw90();
            }

            foreach (var cell in _cells)
            {
                cell.HasBeenTested = false;
            }

            HasBeenCompletelyTested = false;
            IsPlaced = false;
        }
    }
}
