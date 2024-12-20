namespace _02b
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt")
                            .Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            Dictionary<List<int>, bool> report = new Dictionary<List<int>, bool>();

            foreach (var line in input)
            {
                var levels = line.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                  .Select(int.Parse)
                                  .ToList();

                report.Add(levels, false);
            }

            foreach (var key in report.Keys.ToList())
            {
                bool valid = IsValid(key);
                report[key] = valid || CheckAgain(key);
            }

            int trueCount = report.Count(pair => pair.Value);
            Console.WriteLine(trueCount);
        }

        static bool PassLimits(List<int> key, bool increasing, bool decreasing)
        {
            if (!increasing && !decreasing) return false;

            for (int i = 0; i < key.Count - 1; i++)
            {
                if (Math.Abs(key[i] - key[i + 1]) > 3 || key[i] == key[i + 1])
                    return true;
            }
            return false;
        }

        static bool IsValid(List<int> key)
        {
            bool increasing = IsInARow(key, true);
            bool decreasing = IsInARow(key, false);
            bool difference = PassLimits(key, increasing, decreasing);

            return (increasing || decreasing) && !difference;
        }

        static bool CheckAgain(List<int> key)
        {
            for (int i = 0; i < key.Count; i++)
            {
                var modifiedKey = new List<int>(key);
                modifiedKey.RemoveAt(i);

                if (IsValid(modifiedKey))
                    return true;
            }
            return false;
        }

        static bool IsInARow(List<int> key, bool checkIncreasing)
        {
            for (int i = 0; i < key.Count - 1; i++)
            {
                if (checkIncreasing)
                {
                    if (key[i] >= key[i + 1])
                        return false;
                }
                else
                {
                    if (key[i] <= key[i + 1])
                        return false;
                }
            }
            return true;
        }
    }
}
