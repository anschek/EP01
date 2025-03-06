
namespace REG_MARK_LIB
{
    public static class RegMarkService
    {
        private static List<int> threeDigitRegionNumbers = [101, 102, 103, 109, 111, 113, 116, 118, 121, 122, 123, 124, 125, 126, 130, 134, 136, 138, 139, 142, 147, 150, 152, 154, 155, 156, 158, 159, 161, 163, 164, 169, 172, 173, 174, 177, 178, 180, 181, 182, 184, 185, 186, 188, 190, 192, 193, 196, 197, 198, 199, 250, 252, 323, 550, 702, 716, 725, 750, 754, 761, 763, 774, 777, 790, 797, 799, 977,];
        private static List<char> allowedLetters = ['a', 'b', 'e', 'k', 'm', 'h', 'o', 'p', 'c', 't', 'y', 'x'];
        private static bool RegionIsCorrect(int region) =>
            region >= 1 && region <= 99 || threeDigitRegionNumbers.Contains(region);

        private static bool RegionIsCorrect(string region)
        {
            if (int.TryParse(region, out var result))
            {
                return RegionIsCorrect(result);
            }
            return false;
        }
        private static bool SeriesIsCorrect(string series)
        {
            series = series.ToLower();
            return series.All(allowedLetters.Contains);
        }       
        private static bool RegNumIsCorrect(string regNum)
        {
            if(int.TryParse(regNum, out var result))
            {
                return true;
            }
            return false;
        }
        // Метод проверяет правильность номерного знака в формате a999aa999 (латинскими буквами)
        public static bool CheckMark(string mark)
        {
            if (mark.Count() > 9 || mark.Count() < 8) return false;
            if (!RegNumIsCorrect(mark[1..4])) return false;
            if (!RegionIsCorrect(mark[6..])) return false;
            if (!SeriesIsCorrect(mark[0] + mark[4..6])) return false;
            return true;
        }
    }
}
