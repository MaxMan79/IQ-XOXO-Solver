using System;
using IQ_XOXO_Solver.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IQ_XOXO_Solver_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GameBoardTest()
        {
            GameBoard invalidBoard = new GameBoard(-1, 3);
            Assert.IsTrue(invalidBoard.Width == 0);
            Assert.IsTrue(invalidBoard.Height == 0);

            GameBoard gameBoard = new GameBoard(2, 4);
            Assert.IsTrue(gameBoard.Width == 2);
            Assert.IsTrue(gameBoard.Height == 4);
            Assert.IsTrue(gameBoard.Cells[0, 0].NeighborN == gameBoard.Cells[1, 0].NeighborNW);
            Assert.IsTrue(gameBoard.Cells[1, 0].NeighborN == gameBoard.Cells[0, 0].NeighborNE);
            Assert.IsTrue(gameBoard.Cells[0, 3].NeighborS == gameBoard.Cells[1, 3].NeighborSW);
            Assert.IsTrue(gameBoard.Cells[1, 3].NeighborS == gameBoard.Cells[0, 3].NeighborSE);
            Assert.IsTrue(gameBoard.Cells[1, 3].NeighborW == gameBoard.Cells[0, 3]);
            Assert.IsTrue(gameBoard.Cells[0, 3].NeighborSW.Type == Cell.CellType.Boundary);
            Assert.IsTrue(gameBoard.Cells[1, 2].NeighborE.Type == Cell.CellType.Boundary);
        }
    }
}
