using Microsoft.VisualStudio.TestTools.UnitTesting;
using OmegaSudoku.Exceptions;
using OmegaSudoku.SudokuGame;
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
        [TestMethod]
        public void Test_4x4Full()
        {
            string input = "1234341221434321";
            // Act
            string result = UserInterface.GetInput(input);
            // Assert
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
        public void Test_9x9NonEmpty2()
        {
            string input = "100000027000304015500170683430962001900007256006810000040600030012043500058001000";
            // Act
            string result = UserInterface.GetInput(input);
            // Assert
            Assert.AreEqual(result, "193586427867324915524179683435962871981437256276815349749658132612743598358291764");

        }

        [TestMethod]
        public void Test_9x9NonEmpty3()
        {
            string input = "007080200600702000090501060700009008400307002300800009010408050000905006008060900";
            // Act
            string result = UserInterface.GetInput(input);
            // Assert
            Assert.AreEqual(result, "137684295654792831892531467765219348489357612321846579916428753243975186578163924");

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
            string input = ":24000<003=06000070000000<00=08000@00070:00000500<0080=0160024>:>00000000030060900700:2450<03000830001070:0400;5000;@8000067000>;0?<0@0000000:0000:00;5?00000007910000:2;000030@@00=000600020?00=000670104>:00?000500=0007004>:004>00<;0000800160900000:000?0800";
            // Act
            string result = UserInterface.GetInput(input);
            // Assert
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void Test_16x16NonEmpty2()
        {
            string input = "102000;680054<00>00;08:0<09007000<00000002700?090090070000:0>85;0:0@1002;40600080300000900000000;942050>00=030000000008@3920040000100:?39600000000060900@0<02;4>00000000200000102000@0>8100=<06054?10>0000600@0060@00250000000<000<00@0:0710=00400:>?00;43000501";
            // Act
            string result = UserInterface.GetInput(input);
            // Assert
            Assert.AreEqual(result, "172:93;68>?54<@=>?5;=8:4<@931726=<84>1@5627;:?39@693<72?=1:4>85;<:=@1?32;4569>787368;4=9?<>@51:2;942:5<>78=136?@?1>5768@392:;4=<4@1<2:?396;>7=85:836591=@?<72;4>9>;=6<472538@:1?257?@;>81:4=<96354?13>9<:=628@;76=@74251>;89?3<:3;<98@6:571?=2>482:>?=7;43@<6591");

        }
        [TestMethod]
        public void Test_25x25Empty()
        {
            string input = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            // Act
            string result = UserInterface.GetInput(input);
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Test_25x25NonEmpty()
        {
            string input = "7<00AH0D0000E:F80;00I?1>0090BDG000@06;300?000<05070000:6;008I00>0<05A09HB0240603?0>00<=5000HB02@0E0FCI?0>05A009HBD20G00F80000800:F030I0?1>0<=5A790000006;041>C<000079H002@0E0080?100507900BD0@G0:006;04I000A7B0200GE0086;30I?10C<@H0D000F8G60340?10C<=5A79=0>0<00000000@000F80;34I0H5009D0@G000F86;300?000<=G0000:F86E000I?00C<00009H0000034I?00>C<=50700B02@00;300000=000700BD2@GE:F0603400C0=0>07000000G00086;0>C<=09H0AD200E0000;000?1B00000@GE0:086030I01>0000E02@GF06;0340?100<=5A0900;:00040?130C0=5000HB02@0000@G0860004I?10C000A790B03F8600?0>40<05079H0D20GE:04I00<00A0090B000GE0F0003A0005000D02@000F80030I010079HB0GE00080;30001000000";
            // Act
            string result = UserInterface.GetInput(input);
            // Assert
            Assert.AreEqual(result, "7<=5AHBD29@GE:F86;34I?1>C29HBDGE:F@86;34I?1>C<=5A7F@GE:6;348I?1>C<=5A79HBD2486;3?1>CI<=5A79HBD2@GE:FCI?1>=5A7<9HBD2@GE:F86;348GE:F;34I6?1>C<=5A79HBD2@I6;341>C<?=5A79HBD2@GE:F8<?1>C5A79=HBD2@GE:F86;34I9=5A7BD2@HGE:F86;34I?1>C<@HBD2E:F8G6;34I?1>C<=5A79=1>C<A79H5BD2@GE:F86;34I?H5A79D2@GBE:F86;34I?1>C<=GBD2@:F86E;34I?1>C<=5A79H6E:F834I?;1>C<=5A79HBD2@G?;34I>C<=15A79HBD2@GE:F86134I?C<=5>A79HBD2@GE:F86;5>C<=79HBAD2@GE:F86;34I?1BA79H2@GED:F86;34I?1>C<=5ED2@GF86;:34I?1>C<=5A79HB;:F864I?13>C<=5A79HBD2@GE:2@GE86;3F4I?1>C<=5A79HBD3F86;I?1>4C<=5A79HBD2@GE:>4I?1<=5AC79HBD2@GE:F86;3AC<=59HBD72@GE:F86;34I?1>D79HB@GE:2F86;34I?1>C<=5A");
        }

        [TestMethod]
        public void Test_InvalidCharacterTest()
        {
            // Illegal input

            string input = "00002z0004000000";
            
            // Assert
            Assert.ThrowsException<IllegalCharacterException>(() => StringValidation.Validate(input));
        }
        [TestMethod]
        public void Test_EmptyStringTest()
        {
            // Illegal input

            string input = "";

            // Assert
            Assert.ThrowsException<EmptyStringException>(() => StringValidation.Validate(input));
        }

        [TestMethod]
        public void Test_IllegalLengthTest()
        {
            // Illegal input

            string input = "123";

            // Assert
            Assert.ThrowsException<IllegalSizeException>(() => StringValidation.Validate(input));
        }

        [TestMethod]
        public void Test_RowDuplicateTest()
        {
            // Illegal input

            string input = "1100000400000000";
            Board board = new(input, (int)Math.Sqrt(input.Length));
            // Assert
            Assert.ThrowsException<RowException>(() => BoardValidation.BoardValidate(board));
        }
        [TestMethod]
        public void Test_ColumnDuplicateTest()
        {
            // Illegal input

            string input = "1000000410000000";
            Board board = new(input, (int)Math.Sqrt(input.Length));
            // Assert
            Assert.ThrowsException<ColException>(() => BoardValidation.BoardValidate(board));
        }
        [TestMethod]
        public void Test_BoxDuplicateTest()
        {
            // Illegal input

            string input = "1000010400000000";
            Board board = new(input, (int)Math.Sqrt(input.Length));
            // Assert
            Assert.ThrowsException<BoxException>(() => BoardValidation.BoardValidate(board));
        }


        [TestMethod]
        public void Test_UnsolvableSudokuTest()
        {
            // Unsolvable input

            string input = "516849732307605000809700065135060907472591006968370050253186074684207500791050608";
            Board board = new(input, (int)Math.Sqrt(input.Length));
            Solver solve = new Solver(board);
            // Assert
            Assert.ThrowsException<UnsolvableBoardException>(() => solve.Solve());
        }

    }
}