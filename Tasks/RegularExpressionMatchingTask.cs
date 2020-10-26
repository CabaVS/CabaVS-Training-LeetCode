using CabaVS.LeetCode.Tasks.Abstractions;
using System;

namespace CabaVS.LeetCode.Tasks
{
    // (hard) https://leetcode.com/problems/regular-expression-matching/
    // TODO: Solve via dynamic programming
    public class RegularExpressionMatchingTask : ITask
    {
        private readonly (string Input, string Expression, bool ExpectedOutput)[] _inputData =
        {
            ("a", "ab*", true),
            ("aaa", "a*a", true),
            ("aa", "a", false),
            ("aa", "a*", true),
            ("ab", ".*", true),
            ("aab", "c*a*b", true),
            ("mississippi", "mis*is*p*.", false)
        };

        public void Run()
        {
            foreach (var (input, expression, expectedOutput) in _inputData)
            {
                Console.WriteLine($"Input string: {input}. Expression: {expression}");

                var output = IsMatch(input, expression);

                Console.WriteLine($"Actual output: {output}");
                Console.WriteLine($"Expected output: {expectedOutput}");

                Console.WriteLine(new string('-', 80));
            }
        }

        private bool IsMatch(string s, string p)
        {
            if (s == null) throw new ArgumentNullException(nameof(s));
            if (p == null) throw new ArgumentNullException(nameof(p));

            if (p == string.Empty)
            {
                return s == string.Empty;
            }

            return IsMatchRecursive(s, p);
        }

        private bool IsMatchRecursive(string s, string p, int sPointer = 0, int pPointer = 0)
        {
            bool anySymb;
            bool asterix;

            int asterixUsed = 0;

            while (pPointer < p.Length)
            {
                if (sPointer >= s.Length)
                {
                    var remainingPatternLength = p.Length - pPointer;
                    if (remainingPatternLength % 2 != 0) return false;

                    for (var i = 0; i < remainingPatternLength / 2; i++)
                    {
                        if (p[pPointer + i * 2 + 1] != '*') return false;
                    }

                    return true;
                }

                anySymb = p[pPointer] == '.';
                asterix = pPointer + 1 < p.Length && p[pPointer + 1] == '*';

                if (asterix)
                {
                    pPointer += 2;

                    while (anySymb || s[sPointer] == p[pPointer - 2])
                    {
                        sPointer += 1;
                        asterixUsed += 1;

                        if (sPointer >= s.Length) break;
                    }

                    for (var i = asterixUsed - 1; i >= 0; i--)
                    {
                        if (IsMatchRecursive(s, p, sPointer - asterixUsed + i, pPointer))
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    if (!anySymb && s[sPointer] != p[pPointer]) return false;

                    sPointer += 1;
                    pPointer += 1;
                }
            }

            return sPointer == s.Length;
        }
    }
}