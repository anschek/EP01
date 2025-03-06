
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
            if (int.TryParse(region, out var result)) return RegionIsCorrect(result);
            return false;
        }

        private static bool SeriesIsCorrect(string series)
        {
            series = series.ToLower();
            return series.All(allowedLetters.Contains);
        }

        private static bool RegNumIsCorrect(string regNum)
        {
            if (int.TryParse(regNum, out var result)) return true;
            
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

        private static string GetNextSeries(string series)
        {
            int nextIndex;
            if (series[1..3] == "xx")
            {
                nextIndex = allowedLetters.IndexOf(series[0]) + 1;
                return allowedLetters[nextIndex] + "aa";
            }
            else if (series[2] == 'x')
            {
                nextIndex = allowedLetters.IndexOf(series[1]) + 1;
                return series[0] + allowedLetters[nextIndex] + "a";
            }
            else
            {
                nextIndex = allowedLetters.IndexOf(series[2]) + 1;
                return series[0..2] + allowedLetters[nextIndex];
            }
        }

        // Метод принимает номерной знак в формате a999aa999 (латинскими буквами) и выдает следующий номер в данной серии или создает следующую серию
        public static string GetNextMarkAfter(string mark)
        {
            if (!CheckMark(mark)) return mark; // знак не корректен

            int currentRegNum = int.Parse(mark[1..4]);
            if (currentRegNum < 999) // следующий номер в этой серии
            {
                ++currentRegNum;
                string newRegNum = currentRegNum.ToString();
                if (currentRegNum < 10) newRegNum = "00" + newRegNum;
                else if (currentRegNum < 100) newRegNum = "0" + newRegNum;

                return mark[0] + newRegNum + mark[4..];
            }
            else // новый номер в новой серии
            {
                string series = mark[0] + mark[4..6];
                series = series.ToLower();
                if (series == "xxx") return mark; // знака больше данного не существует
                string newSeries = GetNextSeries(series);
                return newSeries[0] + "001" + newSeries[1..] + mark[6..];
            }
        }
        private static int SeriesSum(string series) =>
            allowedLetters.IndexOf(series[0]) * 100
            + allowedLetters.IndexOf(series[1]) * 10
            + allowedLetters.IndexOf(series[2]);
        
        private static bool RegMarkGreaterThan(string lhsMark, string rhsMark)
        {
            if (lhsMark[0] + lhsMark[4..6] == rhsMark[0] + rhsMark[4..6]) // если одинаковые серии
            {
                return int.Parse(lhsMark[1..4])  > int.Parse(rhsMark[1..4]);
            }
            else
            {
                return SeriesSum(lhsMark[0] + lhsMark[4..6]) > SeriesSum(rhsMark[0] + rhsMark[4..6]);
            }
        }

        // Метод принимает номерной знак в формате a999aa999 (латинскими буквами) и выдает следующий номер в данной данном промежутке
        // номеров rangeStart до rangeEnd (включая обе границы).
        // Если нет возможности выдать следующий номер, необходимо вернуть сообщение “out of stock”.
        public static string GetNextMarkAfterInRange(string prevMark, string rangeStart, string rangeEnd)
        {
            if (!CheckMark(prevMark) || !CheckMark(rangeStart) || !CheckMark(rangeEnd)) return prevMark;
            // знак за границами
            if (RegMarkGreaterThan(rangeStart, prevMark)) return "out of stock";
            if (RegMarkGreaterThan(prevMark, rangeEnd)) return "out of stock";

            // знак равен максимальному в границах
            if(prevMark == rangeEnd) return "out of stock";
            return GetNextMarkAfter(prevMark);
        }

        // Метод принимает два номера в формате a999aa999 (латинскими буквами) и возвращает количество возможных номеров между ними
        // (включая обе границы). Метод необходим, чтобы рассчитать оставшиеся свободные номера для региона.
        public static int GetCombinationsCountInRange(string mark1, string mark2)
        {
            if(!CheckMark(mark1) || !CheckMark(mark2)) return -1; // знаки не корректны
            if (mark1[6..] != mark2[6..]) return -1; // знаки разных регионов
            if (RegMarkGreaterThan(mark1, mark2))  return -1; // начало диапазона больше конца диапазона

            int markCount = -1;
            string currentMark = mark1;
            while (currentMark != "out of stock")
            {
                currentMark = GetNextMarkAfterInRange(currentMark, mark1, mark2);
                ++markCount;
            } 
            return markCount;
        }
    }
}
