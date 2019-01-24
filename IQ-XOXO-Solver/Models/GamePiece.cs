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

        private int _indexOfCellUnderTest;

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

            Extents = new Extents();

            foreach (var cell in cells)
            {
                Extents.Extend(cell);
            }

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
        /// Gets a value indicating if this piece is placed on the board.
        /// </summary>
        public bool IsPlaced { get; private set; }

        /// <summary>
        /// Gets the extents of the game piece.
        /// </summary>
        public Extents Extents { get; private set; }

        /// <summary>
        /// Gets the number of cells in the game piece.
        /// </summary>
        public int CellCount
        {
            get
            {
                return _cells.Count;
            }
        }

        // ***************************************************************************
        // *                            Public Methods                               *
        // ***************************************************************************

        /// <summary>
        /// Moves the origin cell to the position of a grid cell, with the remaining cells 
        /// moving with the same offset.
        /// </summary>
        /// <remarks>
        /// Cannot move a piece that has been placed.
        /// </remarks>
        /// <param name="cell">The grid cell to move to.</param>
        /// <returns>True if move was successful; false otherwise.</returns>
        public bool MoveTo(GridCell cell)
        {
            return MoveTo(cell.Position.X, cell.Position.Y);
        }

        /// <summary>
        /// Moves the origin cell to a new position, with the remaining cells moving with
        /// the same offset.
        /// </summary>
        /// <remarks>
        /// Cannot move a piece that has been placed.
        /// </remarks>
        /// <param name="x">The new x-position</param>
        /// <param name="y">The new y-position</param>
        /// <returns>True if move was successful; false otherwise.</returns>
        public bool MoveTo(int x, int y)
        {
            if (IsPlaced)
            {
                return false;
            }

            int offsetX = x - _originCell.Position.X;
            int offsetY = y - _originCell.Position.Y;

            foreach (var cell in _cells)
            {
                cell.Position.X += offsetX;
                cell.Position.Y += offsetY;
            }

            return true;
        }

        /// <summary>
        /// Rotates the game piece 90 degrees clockwise about the origin cell.
        /// </summary>
        /// <remarks>
        /// Cannot rotate a piece that has been placed.
        /// </remarks>
        /// <returns>True if rotation was successful; false otherwise.</returns>
        public bool RotateCw90()
        {
            if (IsPlaced)
            {
                return false;
            }

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

            return true;
        }

        /// <summary>
        /// Flips the game piece over, centered about the origin cell.  
        /// </summary>
        /// <remarks>
        /// Each cell will be flipped and mirrored across the y-axis.  The game piece has the 
        /// same coordinate system as the game board, where the +x-direction is to the right 
        /// and +y-direction is downward.  Cannot flip a piece that has been placed.
        /// </remarks>
        /// <returns>True if flip was successful; false otherwise.</returns>
        public bool Flip()
        {
            if (IsPlaced)
            {
                return false;
            }

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

            return true;
        }

        /// <summary>
        /// Places this game piece on the given board.
        /// </summary>
        /// <remarks>
        /// A game piece may only be placed only once and on only one game board.
        /// </remarks>
        /// <param name="board">The board on which to place this piece</param>
        /// <returns>True if successfully placed; false if a boundary or another piece interfered.</returns>
        public bool PlaceOnGameBoard(GameBoard board)
        {
            if (IsPlaced)
            {
                return false;
            }

            foreach (var cell in _cells)
            {
                if (cell.Position.X < 0 || cell.Position.Y < 0 ||
                    cell.Position.X >= board.Width || cell.Position.Y >= board.Height)
                {
                    return false;
                }
            }

            GridCell cellUnderPiece;
            var cellsUnderPiece = new List<GridCell>();

            foreach (var cell in _cells)
            {
                cellUnderPiece = board.Cells[cell.Position.X, cell.Position.Y];

                if (cellUnderPiece.IsOccupied || (cellUnderPiece.Type != cell.Type))
                {
                    return false;
                }
                else
                {
                    cellsUnderPiece.Add(cellUnderPiece);
                }
            }

            cellsUnderPiece.ForEach(cell => cell.GamePiece = this);
            IsPlaced = true;

            return true;
        }

        /// <summary>
        /// Removes this game piece from the given board.
        /// </summary>
        /// <param name="board">The board from which to remove this piece</param>
        /// <returns>True if successfully removed; false if this piece is not placed on this board.</returns>
        public bool RemoveFromBoard(GameBoard board)
        {
            if (!IsPlaced)
            {
                return false;
            }

            GridCell cellUnderPiece;
            var cellsUnderPiece = new List<GridCell>();

            foreach (var cell in _cells)
            {
                cellUnderPiece = board.Cells[cell.Position.X, cell.Position.Y];

                if (!cellUnderPiece.IsOccupied || cellUnderPiece.GamePiece != this)
                {
                    return false;
                }
                else
                {
                    cellsUnderPiece.Add(cellUnderPiece);
                }
            }

            cellsUnderPiece.ForEach(cell => cell.GamePiece = null);
            IsPlaced = false;

            return true;
        }

        /// <summary>
        /// Tries to place the piece on the give cell in any position or orientation.
        /// </summary>
        /// <param name="cellUnderOrigin">The cell under the piece's origin</param>
        /// <returns>True if the piece was successfuly placed; false otherwise.</returns>
        public bool TryPlaceOnCell(GridCell cellUnderOrigin)
        {
            bool success;

            while (!IsPlaced && !HasBeenCompletelyTested)
            {
                success = SetOriginToNextCellOfType(cellUnderOrigin.Type);

                if (!success)
                {    
                    if (!_isFlipped)
                    {
                        // There are no more cells of the given type on this side of the
                        // piece, so flip it over and start testing again 
                        Reset();
                        Flip();
                    }
                    else
                    {
                        // The piece has been completely tested on both sides and not placed
                        Reset();
                        HasBeenCompletelyTested = true;
                    }

                    continue;
                }

                MoveTo(cellUnderOrigin);

                // Rotate through all the cardinal orientations

                do
                {
                    success = PlaceOnGameBoard(cellUnderOrigin);

                    if (success)
                    {
                        return true;
                    }

                    RotateCw90();
                }
                while (_rotationDeg > 0);
            }

            return false;
        }

        /// <summary>
        /// Resets the game piece back to its original position/orientation.
        /// </summary>
        public void Reset()
        {
            _originCell = _cells.First();
            _indexOfCellUnderTest = -1;

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

        /// <summary>
        /// ToString override
        /// </summary>
        /// <returns>Formatted string</returns>
        public override string ToString()
        {
            return string.Format("{0} cells @ {1}", CellCount, Extents.ToString());
        }

        // ***************************************************************************
        // *                           Private Methods                               *
        // ***************************************************************************

        /// <summary>
        /// Sets the origin cell to the next available game piece cell of the given type
        /// </summary>
        /// <remarks>
        /// The origin can not be changed if the piece is placed on a board.
        /// </remarks>
        /// <param name="type">Cell type</param>
        /// <returns>True if successful; false otherwise.</returns>
        private bool SetOriginToNextCellOfType(Cell.CellType type)
        {
            if (!IsPlaced)
            {
                GamePieceCell cell;

                for (_indexOfCellUnderTest = _indexOfCellUnderTest + 1; _indexOfCellUnderTest < _cells.Count; _indexOfCellUnderTest++)
                {
                    cell = _cells[_indexOfCellUnderTest];

                    if (cell.Type == type)
                    {
                        _originCell = cell;
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Places this game piece at the given cell.
        /// </summary>
        /// <remarks>
        /// If a game piece is already placed on the board, it must be removed before being placed again.
        /// </remarks>
        /// <param name="board">The grid cell under the origin of this game piece</param>
        /// <returns>True if successfully placed; false if a boundary or another piece interfered.</returns>
        private bool PlaceOnGameBoard(GridCell cellUnderOrigin)
        {
            if (IsPlaced ||
                (cellUnderOrigin.Type != _originCell.Type))
            {
                return false;
            }

            GridCell cellUnderPiece;
            var cellsUnderPiece = new List<GridCell>();

            foreach (var cell in _cells)
            {
                cellUnderPiece = cellUnderOrigin.GetCellAbsolute(cell.Position.X, cell.Position.Y);

                if (cellUnderPiece.IsOccupied)
                {
                    return false;
                }
                else
                {
                    cellsUnderPiece.Add(cellUnderPiece);
                }
            }

            cellsUnderPiece.ForEach(cell => cell.GamePiece = this);
            IsPlaced = true;

            return true;
        }

        /// <summary>
        /// Removes this game piece from the board.
        /// </summary>
        /// <param name="board">The cell under the origin of this game piece.</param>
        /// <returns>True if successfully removed; false if this piece is not placed on this cell.</returns>
        private bool RemoveFromGameBoard(GridCell cellUnderOrigin)
        {
            if (!IsPlaced)
            {
                return false;
            }

            GridCell cellUnderPiece;
            var cellsUnderPiece = new List<GridCell>();

            foreach (var cell in _cells)
            {
                cellUnderPiece = cellUnderOrigin.GetCellAbsolute(cell.Position.X, cell.Position.Y);

                if (!cellUnderPiece.IsOccupied || cellUnderPiece.GamePiece != this)
                {
                    return false;
                }
                else
                {
                    cellsUnderPiece.Add(cellUnderPiece);
                }
            }

            cellsUnderPiece.ForEach(cell => cell.GamePiece = null);
            IsPlaced = false;

            return true;
        }
    }
}
