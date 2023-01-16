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
            Assert.AreEqual(result, "4123231414323241");
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
            string boardInput = "800000070006010053040600000000080400003000700020005038000000800004050061900002000100000027000304015500170683430962001900007256006810000040600030012043500058001000";

            string result = UserInterface.GetInput(boardInput);
            Assert.AreEqual(result, "4123231414323241");
        }
    }
}