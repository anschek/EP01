namespace REG_MARK_LIB.Test
{
    [TestClass]
    public class RegMarkService_Test
    {   
        [TestClass] 
        public class PositiveTests
        {
            [TestMethod]
            public void CheckMark_CorrectMarkLowerCase_ResultEqualExpected()
            {
                string mark = "a001aa52";
                bool expected = true;

                bool result = RegMarkService.CheckMark(mark);
                Assert.AreEqual(expected, result);
            }
            [TestMethod]
            public void CheckMark_CorrectMarkUpperCase_ResultIsTrue()
            {
                string mark = "B122BB152";

                bool result = RegMarkService.CheckMark(mark);
                Assert.IsTrue(result);
            }
            [TestMethod]
            public void CheckMark_WrongRegion_ResultNotEqualNotExpected()
            {
                string mark = "a001aa100";
                bool notExpected = true;

                bool result = RegMarkService.CheckMark(mark);
                Assert.AreNotEqual(notExpected, result);
            }
            [TestMethod]
            public void CheckMark_WrongRegNum_ResultIsFalse()
            {
                string mark = "a0a0aa152";

                bool result = RegMarkService.CheckMark(mark);
                Assert.IsFalse(result);
            }
            [TestMethod]
            public void GetNextMarkAfter_GetNextRegNum_ResultEqualExpected()
            {
                string mark = "a001aa52";
                string expected = "a002aa52";

                string result = RegMarkService.GetNextMarkAfter(mark);
                Assert.AreEqual(expected, result);
            }
            [TestMethod]
            public void GetNextMarkAfter_GetSeries_ResultEqualExpected()
            {
                string mark = "a999aa52";
                string expected = "a001ab52";

                string result = RegMarkService.GetNextMarkAfter(mark);
                Assert.AreEqual(expected, result);

            }
        }
        [TestClass]
        public class NegativeTests
        {
            [TestMethod]
            public void CheckMark_Cyrillic—haractersSeries_ResultIsFalse()
            {
                string mark = "Ù001aa10";

                bool result = RegMarkService.CheckMark(mark);
                Assert.IsFalse(result);
            }
            [TestMethod]
            public void GetNextMarkAfter_WrongRegNum_ResultEqualArgument()
            {
                string mark = "Ù001aa10";

                string result = RegMarkService.GetNextMarkAfter(mark);
                Assert.AreEqual(mark, result);

            }
            [TestMethod]
            public void GetNextMarkAfter_NumbersIsLast_ResultEqualArgument()
            {
                string mark = "ı999ıı10";

                string result = RegMarkService.GetNextMarkAfter(mark);
                Assert.AreEqual(mark, result);
            }
        }
    }
}