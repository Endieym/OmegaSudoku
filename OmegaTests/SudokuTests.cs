using Microsoft.VisualStudio.TestTools.UnitTesting;
using OmegaSudoku.UI;

namespace OmegaTests
{
    [TestClass]
    public class SudokuTests
    {
        [TestMethod]
        public void Test_1x1Empty()
        {
            string boardInput = "0";

            string result = UserInterface.GetInput(boardInput);
            Assert.AreEqual("1", result);
        }

        [TestMethod]
        public void Test_1x1NotEmpty()
        {
            string boardInput = "1";

            string result = UserInterface.GetInput(boardInput);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Test_4x4Empty()
        {
            string boardInput = "0000000000000000";

            string result = UserInterface.GetInput(boardInput);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Test_4x4NonEmpty()
        {
            string boardInput = "0000200004000000";

            string result = UserInterface.GetInput(boardInput);
            Assert.IsNotNull(result);
        }
        public void Test_9x9Empty()
        {
            string boardInput = "000000000000000000000000000000000000000000000000000000000000000000000000000000000";

            string result = UserInterface.GetInput(boardInput);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Test_9x9NonEmpty()
        {
            string input = "800000070006010053040600000000080400003000700020005038000000800004050061900002000";
            // Act
            string result = UserInterface.GetInput(input);
            // Assert
            Assert.AreEqual(result, "831529674796814253542637189159783426483296715627145938365471892274958361918362547");

        }
        [TestMethod]
        public void Test_16x16Empty()
        {
            string input = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            // Act
            string result = UserInterface.GetInput(input);
            // Assert
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void Test_16x16NonEmpty()
        {
            string input = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            // Act
            string result = UserInterface.GetInput(input);
            // Assert
            Assert.IsNotNull(result);

        }
    }
}