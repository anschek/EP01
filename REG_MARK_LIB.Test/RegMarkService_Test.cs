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
            [TestMethod]
            public void GetNextMarkAfterInRange_PassCorrectArgs_ResultEqualExpected()
            {
                string mark = "a120ab52";
                string start = "a001aa52";
                string end = "b001bb52";
                string expected = "a121ab52";

                string result = RegMarkService.GetNextMarkAfterInRange(mark, start, end);
                Assert.AreEqual(expected, result);
            }
            [TestMethod]
            public void GetNextMarkAfterInRange_PrevMarkEqualRangeEnd_ResultEqualExpected()
            {
                string mark = "a120ab52";
                string start = "a001aa52";
                string expected = "out of stock";

                string result = RegMarkService.GetNextMarkAfterInRange(mark, start, mark);
                Assert.AreEqual(expected, result);
            }
            [TestMethod]
            public void GetCombinationsCountInRange_PassCorrectArgs_ResultEqualExpected()
            {
                string mark1 = "a120ab52";
                string mark2 = "a130ab52";
                int expected = 10;

                int result = RegMarkService.GetCombinationsCountInRange(mark1, mark2);
                Assert.AreEqual(expected, result);
            }
            [TestMethod]
            public void GetCombinationsCountInRange_Mark1EqualMark2_ResultEqualExpected()
            {
                string mark1 = "a120ab52";
                int expected = 0;

                int result = RegMarkService.GetCombinationsCountInRange(mark1, mark1);
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
            [TestMethod]
            public void GetNextMarkAfterInRange_WrongRangeLimits_ResultEqualExpected()
            {
                string mark = "a120ab52";
                string start = "b001bb52";
                string end = "a001aa52";
                string expected = "out of stock";

                string result = RegMarkService.GetNextMarkAfterInRange(mark, start, end);
                Assert.AreEqual(expected, result);
            }
            [TestMethod]
            public void GetNextMarkAfterInRange_WrongRegNumber_ResultEqualArgument()
            {
                string mark = "aaaaaaaaa";
                string start = "a001aa100";
                string end = "b001bb100";

                string result = RegMarkService.GetNextMarkAfterInRange(mark, start, end);
                Assert.AreEqual(mark, mark);
            }
            [TestMethod]
            public void GetCombinationsCountInRange_WrongMarks_ResultEqualExpected()
            {
                string mark1 = "aa120ab52";
                string mark2 = "a130ab999";
                int expected = -1;

                int result = RegMarkService.GetCombinationsCountInRange(mark1, mark2);
                Assert.AreEqual(expected, result);
            }
            [TestMethod]
            public void GetCombinationsCountInRange_MarksRegionsNotEqual_ResultEqualExpected()
            {
                string mark1 = "a120ab52";
                string mark2 = "a130ab101";
                int expected = -1;

                int result = RegMarkService.GetCombinationsCountInRange(mark1, mark2);
                Assert.AreEqual(expected, result);
            }
            [TestMethod]
            public void GetCombinationsCountInRange_WrongRangeLimits_ResultEqualExpected()
            {
                string mark1 = "a130ab52";
                string mark2 = "a120ab52";
                int expected = -1;

                int result = RegMarkService.GetCombinationsCountInRange(mark1, mark2);
                Assert.AreEqual(expected, result);

            }
        }
    }
}