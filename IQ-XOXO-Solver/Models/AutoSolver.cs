using System;
using System.Collections.Generic;
using System.Linq;

namespace IQ_XOXO_Solver.Models
{
    /// <summary>
    /// Solves a game board by recursively searching down a search tree, where the root
    /// is the initial board configuration and each node represents a different valid 
    /// piece placement on the board.
    /// </summary>
    public class AutoSolver
    {
        // ***************************************************************************
        // *                               Fields                                    *
        // ***************************************************************************

        private GameBoard _gameBoard;

        private List<GamePiece> _allPieces;

        private List<GamePiece> _placeablePieces;

        private List<GamePiece> _initialPiecesOnBoard;

        private Random _rand;

        private ulong _leafCount;

        private ulong _abandonCount;

        private ulong _max_leaf_count;

        // ***************************************************************************
        // *                            Constructors                                 *
        // ***************************************************************************

        /// <summary>
        /// Initializes static members of the <see cref="AutoSolver"/> class.
        /// </summary>
        /// <remarks>
        /// The pieces should already be placed on the board, if playing a preset puzzle.
        /// </remarks>
        /// <param name="gameBoard">The game board</param>
        /// <param name="allGamePieces">All the pieces</param>
        public AutoSolver(GameBoard gameBoard, List<GamePiece> allGamePieces)
        {
            _gameBoard = gameBoard;
            _allPieces = allGamePieces;
            _placeablePieces = allGamePieces.Where(piece => !piece.IsPlaced).ToList();
            _initialPiecesOnBoard = allGamePieces.Where(piece => piece.IsPlaced).ToList();
            _rand = new Random();

            _max_leaf_count = (ulong)Math.Pow(4, _placeablePieces.Count);
        }

        // ***************************************************************************
        // *                             Properties                                  *
        // ***************************************************************************

        /// <summary>
        /// Gets the total leaf count
        /// </summary>
        public ulong TotalLeafCount
        {
            get
            {
                return (_max_leaf_count * _abandonCount) + _leafCount;
            }
        }

        /// <summary>
        /// Solves the current game board
        /// </summary>
        /// <returns>True if successful; false otherwise.</returns>
        public bool Solve()
        {
            return SolvePiece();
        }

        // ***************************************************************************
        // *                           Private Methods                               *
        // ***************************************************************************

        /// <summary>
        /// Recursively solves the current game board one piece at a time
        /// </summary>
        /// <remarks>
        /// The entry point (root) to this method will not pass in a parameter. Only the
        /// recursive entries will pass in the last piece placed. These entries are the
        /// branches of the search.  The leafs are where a search path ends, either in a
        /// failure or success.
        /// </remarks>
        /// <param name="pieceLastPlaced">The last piece placed on the board</param>
        /// <returns>
        /// True, if
        /// 
        /// All pieces have been placed on the board, which means successful solve.
        ///    OR
        /// Max leaf count has been exceeded. In this case, we assume there is no valid
        /// solution along this path, or it would take too long to find; therefore, unwind
        /// the recursion all the way back to the top and continue from the initial board
        /// configuration with the next available piece placement. This is a performance
        /// optimization that does not guarantee that a valid solution can be found, if it
        /// exists.
        /// </returns>
        private bool SolvePiece(GamePiece pieceLastPlaced = null)
        {
            if (_placeablePieces.Count == 0)
            {
                // All the placeable pieces have been placed... solve complete.
                return true;
            }

            if (_leafCount > _max_leaf_count)
            {
                // Exceeded max leaf count... abandon solution.
                _abandonCount++;
                return true;
            }
  
            List<FloodZone> floodZones = _gameBoard.GetFloodZones(pieceLastPlaced);
     
            if (floodZones.Count == 0)
            {
                // There are no flood zones adjacent to the last piece placed. The flood zone
                // being worked has been completely filled.  Geta any remaining flood zones.
                floodZones = _gameBoard.GetFloodZones();
            }

            // If there are any flood zones into which no placeable piece can fit,
            // this has to be an invalid solution.  Early out.
            bool hasPieceThatFits;

            foreach (var zone in floodZones)
            {
                hasPieceThatFits = false;

                foreach (var piece in _placeablePieces)
                {
                    hasPieceThatFits |= zone.Extents.FitsWithin(piece.Extents) &&
                                        zone.ContainsCount >= piece.CellCount;
                }

                if (!hasPieceThatFits)
                {
                    _leafCount++;
                    return false;
                }
            }

            bool success = false;
            GridCell mostBoundedCell = null;
            GamePiece currentPiece = null;
            List<GamePiece> untestedPieces = new List<GamePiece>();

            FloodZone currentFloodZone = floodZones.First();
            currentFloodZone.Sort();

            while ((mostBoundedCell = currentFloodZone.GetNextMostBoundedCell()) != null)
            {
                foreach (var piece in _placeablePieces)
                {
                    piece.Reset();
                    untestedPieces.Add(piece);
                }

                while (untestedPieces.Count > 0)
                {
                    currentPiece = GetRandomPiece(untestedPieces);

                    if (currentFloodZone.Extents.FitsWithin(currentPiece.Extents))
                    {
                        // Try to place the current piece on the most bounded cell
                        while (currentPiece.TryPlaceOnCell(mostBoundedCell) == true)
                        {
                            _placeablePieces.Remove(currentPiece);

                            // Continue the recursive solve
                            success = SolvePiece(currentPiece);

                            if (success)
                            {
                                if (_placeablePieces.Count == 0)
                                {
                                    return true;
                                }
                                else if (pieceLastPlaced != null)
                                {
                                    currentPiece.Reset(_gameBoard);
                                    _placeablePieces.Add(currentPiece);
                                    return true;
                                }
                                else
                                {
                                    // Top level. Reset leaf count. Continue with next piece.
                                    currentPiece.RemoveFromBoard(_gameBoard);
                                    _placeablePieces.Add(currentPiece);
                                    _leafCount = 0;
                                }
                            }
                            else
                            {
                                currentPiece.RemoveFromBoard(_gameBoard);
                                _placeablePieces.Add(currentPiece);
                            }
                        }
                    }

                    untestedPieces.Remove(currentPiece);
                }    
            }

            _leafCount++;
            return false;
        }

        /// <summary>
        /// Chooses a random game piece from the list of game pieces.
        /// </summary>
        /// <param name="pieces">The pieces to choose from.</param>
        /// <returns>A random game piece from the given list; null if the list is empty.</returns>
        private GamePiece GetRandomPiece(List<GamePiece> pieces)
        {
            if (pieces.Count > 0)
            {
                int randomIndex = _rand.Next(pieces.Count - 1);

                return pieces[randomIndex];
            }
            else
            {
                return null;
            }
        }
    }
}
