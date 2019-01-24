using IQ_XOXO_Solver.Models;
using IQ_XOXO_Solver.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Reflection;

namespace IQ_XOXO_Solver_Test
{
    [TestClass]
    public class GamePieceTests
    {
        [TestMethod]
        public void MoveAndRotateTest()
        {
            GamePiece piece = GamePieceFactory.DarkBluePiece();

            var cellsProperty = piece.GetType().GetField("_cells", BindingFlags.Instance | BindingFlags.NonPublic);

            Assert.IsNotNull(cellsProperty, "Could not find _cells field on GamePiece.");

            var cells = cellsProperty.GetValue(piece) as List<GamePieceCell>;

            Assert.IsNotNull(cells);
            Assert.IsTrue(cells.Count == 5);

            piece.MoveTo(3, 3);
            piece.RotateCw90();

            Assert.IsTrue(cells[0].Position.X == 3 && cells[0].Position.Y == 3);
            Assert.IsTrue(cells[1].Position.X == 2 && cells[1].Position.Y == 3);
            Assert.IsTrue(cells[2].Position.X == 1 && cells[2].Position.Y == 3);
            Assert.IsTrue(cells[3].Position.X == 0 && cells[3].Position.Y == 3);
            Assert.IsTrue(cells[4].Position.X == -1 && cells[4].Position.Y == 3);

            piece.MoveTo(1, 4);
            piece.RotateCw90();

            Assert.IsTrue(cells[0].Position.X == 1 && cells[0].Position.Y == 4);
            Assert.IsTrue(cells[1].Position.X == 1 && cells[1].Position.Y == 3);
            Assert.IsTrue(cells[2].Position.X == 1 && cells[2].Position.Y == 2);
            Assert.IsTrue(cells[3].Position.X == 1 && cells[3].Position.Y == 1);
            Assert.IsTrue(cells[4].Position.X == 1 && cells[4].Position.Y == 0);

            piece.MoveTo(-12, -9);
            piece.RotateCw90();

            Assert.IsTrue(cells[0].Position.X == -12 && cells[0].Position.Y == -9);
            Assert.IsTrue(cells[1].Position.X == -11 && cells[1].Position.Y == -9);
            Assert.IsTrue(cells[2].Position.X == -10 && cells[2].Position.Y == -9);
            Assert.IsTrue(cells[3].Position.X == -9 && cells[3].Position.Y == -9);
            Assert.IsTrue(cells[4].Position.X == -8 && cells[4].Position.Y == -9);

            piece.MoveTo(2, -2);
            piece.RotateCw90();

            Assert.IsTrue(cells[0].Position.X == 2 && cells[0].Position.Y == -2);
            Assert.IsTrue(cells[1].Position.X == 2 && cells[1].Position.Y == -1);
            Assert.IsTrue(cells[2].Position.X == 2 && cells[2].Position.Y == 0);
            Assert.IsTrue(cells[3].Position.X == 2 && cells[3].Position.Y == 1);
            Assert.IsTrue(cells[4].Position.X == 2 && cells[4].Position.Y == 2);
        }

        [TestMethod]
        public void RotationAngleTest()
        {
            GamePiece piece = GamePieceFactory.RedPiece();

            var rotationProperty = piece.GetType().GetField("_rotationDeg", BindingFlags.Instance | BindingFlags.NonPublic);

            Assert.IsNotNull(rotationProperty, "Could not find _rotationDeg field on GamePiece.");

            Assert.IsTrue((int)rotationProperty.GetValue(piece) == 0);

            piece.RotateCw90();
            Assert.IsTrue((int)rotationProperty.GetValue(piece) == 90);

            piece.RotateCw90();
            Assert.IsTrue((int)rotationProperty.GetValue(piece) == 180);

            piece.RotateCw90();
            Assert.IsTrue((int)rotationProperty.GetValue(piece) == 270);

            piece.RotateCw90();
            Assert.IsTrue((int)rotationProperty.GetValue(piece) == 0);
        }

        [TestMethod]
        public void FlipTest()
        {
            GamePiece piece = GamePieceFactory.OrangePiece();

            var cellsProperty = piece.GetType().GetField("_cells", BindingFlags.Instance | BindingFlags.NonPublic);

            Assert.IsNotNull(cellsProperty, "Could not find _cells field on GamePiece.");

            var cells = cellsProperty.GetValue(piece) as List<GamePieceCell>;

            Assert.IsNotNull(cells);
            Assert.IsTrue(cells.Count == 5);

            var rotationProperty = piece.GetType().GetField("_rotationDeg", BindingFlags.Instance | BindingFlags.NonPublic);

            Assert.IsNotNull(rotationProperty, "Could not find _rotationDeg field on GamePiece.");

            var isFlippedProperty = piece.GetType().GetField("_isFlipped", BindingFlags.Instance | BindingFlags.NonPublic);

            Assert.IsNotNull(isFlippedProperty, "Could not find _isFlipped field on GamePiece.");

            piece.Flip();

            Assert.IsTrue(cells[0].Position.X == 0 && cells[0].Position.Y == 0);
            Assert.IsTrue(cells[1].Position.X == 0 && cells[1].Position.Y == 1);
            Assert.IsTrue(cells[2].Position.X == -1 && cells[2].Position.Y == 1);
            Assert.IsTrue(cells[3].Position.X == -1 && cells[3].Position.Y == 2);
            Assert.IsTrue(cells[4].Position.X == -2 && cells[4].Position.Y == 2);

            Assert.IsTrue((bool)isFlippedProperty.GetValue(piece) == true);

            piece.RotateCw90();
            piece.MoveTo(3, 2);
            piece.Flip();

            Assert.IsTrue(cells[0].Position.X == 3 && cells[0].Position.Y == 2);
            Assert.IsTrue(cells[1].Position.X == 4 && cells[1].Position.Y == 2);
            Assert.IsTrue(cells[2].Position.X == 4 && cells[2].Position.Y == 1);
            Assert.IsTrue(cells[3].Position.X == 5 && cells[3].Position.Y == 1);
            Assert.IsTrue(cells[4].Position.X == 5 && cells[4].Position.Y == 0);

            Assert.IsTrue((bool)isFlippedProperty.GetValue(piece) == false);

            Assert.IsTrue((int)rotationProperty.GetValue(piece) == 270);
        }

        [TestMethod]
        public void PlacePieceTest()
        {
            GamePiece piece = GamePieceFactory.DarkGreenPiece();
        }
    }
}
