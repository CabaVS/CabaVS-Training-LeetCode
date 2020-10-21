using CabaVS.LeetCode.Tasks.Abstractions;
using System;

namespace CabaVS.LeetCode.Tasks
{
    // https://leetcode.com/problems/longest-palindromic-substring/
    public class LongestPalindromicSubstringTask : ITask
    {
        private readonly (string Input, string ExpectedOutput)[] _inputData =
        {
            ("aaaa", "aaaa"),
            ("ccc", "ccc"),
            ("cbbd", "bb"),
            ("bb", "bb"),
            ("babad", "bab"),
            ("a", "a"),
            ("ac", "a")
        };

        public void Run()
        {
            foreach (var (input, expectedOutput) in _inputData)
            {
                Console.WriteLine($"Input: {input}");

                var output = LongestPalindrome(input);

                Console.WriteLine($"Actual output: {output}");
                Console.WriteLine($"Expected output: {expectedOutput}");

                Console.WriteLine(new string('-', 80));
            }
        }

        private string LongestPalindrome(string s)
        {
            if (s == null) throw new ArgumentNullException(nameof(s));
            if (s.Length <= 1) return s;

            var biggest = s[0].ToString();
            for (var i = 1; i < s.Length; i++)
            {
                if (i + 1 < s.Length && s[i - 1] == s[i + 1])
                {
                    int currentIteration;
                    var maxIteration = Math.Min(i - 1, s.Length - i);

                    for (currentIteration = 1; currentIteration <= maxIteration && i - currentIteration - 1 >= 0 && i + currentIteration + 1 < s.Length; currentIteration++)
                    {
                        if (s[i - currentIteration - 1] != s[i + currentIteration + 1])
                        {
                            break;
                        }
                    }

                    var palindrome = s.Substring(i - currentIteration, currentIteration * 2 + 1);
                    if (palindrome.Length > biggest.Length)
                    {
                        biggest = palindrome;
                    }
                }

                if (s[i - 1] == s[i])
                {
                    int currentIteration;
                    var maxIteration = Math.Min(i - 1, s.Length - i - 1);

                    for (currentIteration = 1; currentIteration <= maxIteration && i - currentIteration - 1 >= 0 && i + currentIteration < s.Length; currentIteration++)
                    {
                        if (s[i - currentIteration - 1] != s[i + currentIteration])
                        {
                            break;
                        }
                    }

                    var palindrome = s.Substring(i - currentIteration, currentIteration * 2);
                    if (palindrome.Length > biggest.Length)
                    {
                        biggest = palindrome;
                    }
                }
            }

            return biggest;
        }
    }
}