using CabaVS.LeetCode.Tasks.Abstractions;
using System;
using System.Collections.Generic;

namespace CabaVS.LeetCode.Tasks
{
    // https://leetcode.com/problems/longest-substring-without-repeating-characters/
    public class LongestSubstringWithoutRepeatingCharactersTask : ITask
    {
        private readonly (string Input, int Output)[] _inputData =
        {
            ("dvdf", 3),
            (" ", 1),
            ("abcabcbb", 3),
            ("bbbbb", 1),
            ("pwwkew", 3),
            ("", 0)
        };

        public void Run()
        {
            foreach (var(input, expectedOutput) in _inputData)
            {
                Console.WriteLine($"Input: {input}");

                var output = LengthOfLongestSubstring(input);

                Console.WriteLine($"Output: {output}");
                Console.WriteLine($"Expected output: {expectedOutput}");

                Console.WriteLine(new string('-', 80));
            }
        }

        private int LengthOfLongestSubstring(string s)
        {
            if (s == null) throw new ArgumentNullException(nameof(s));

            var longestSubstring = 0;
            var dictionary = new Dictionary<char, int>();
            var startIndex = 0;

            for (var i = startIndex; i < s.Length; i++)
            {
                var character = s[i];

                if (dictionary.ContainsKey(character))
                {
                    if (longestSubstring < dictionary.Count)
                    {
                        longestSubstring = dictionary.Count;
                    }

                    var positionToRemove = dictionary[character];
                    for (var j = startIndex; j <= positionToRemove; j++)
                    {
                        dictionary.Remove(s[j]);
                    }

                    startIndex = positionToRemove + 1;
                }

                dictionary.Add(character, i);
            }

            return longestSubstring < dictionary.Count
                ? dictionary.Count
                : longestSubstring;
        }
    }
}