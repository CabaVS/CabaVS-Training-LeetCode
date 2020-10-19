using CabaVS.LeetCode.Tasks.Abstractions;
using System;
using System.Collections.Generic;

namespace CabaVS.LeetCode.Tasks
{
    public class TwoSumTask : ITask
    {
        private readonly (int[] Nums, int Target, int[] Output)[] _inputData =
        {
            (new[] {2,7,11,15}, 9, new[] {0,1}),
            (new[] {3,2,4}, 6, new[] {1,2}),
            (new[] {3,3}, 6, new[] {0,1}),
            (new[] {2,7,11,15}, 17, new[] {0,3})
        };

        private int[] TwoSum(int[] nums, int target)
        {
            if (nums == null || nums.Length < 2) throw new ArgumentException($"Invalid input data.", nameof(nums));

            var calculated = new List<int> { target - nums[0] };
            for (var i = 1; i < nums.Length; i++)
            {
                var indexOf = calculated.IndexOf(nums[i]);
                if (indexOf != -1)
                {
                    return new[] {indexOf, i};
                }

                calculated.Add(target - nums[i]);
            }

            throw new ApplicationException("Requirements never fulfilled. Check input array and target.");
        }

        // taken from popular answers
        // Brute Force
        private int[] TwoSum_Solution1(int[] nums, int target)
        {
            for (var i = 0; i < nums.Length; i++)
            {
                for (var j = i + 1; j < nums.Length; j++)
                {
                    if (nums[j] == target - nums[i])
                    {
                        return new[] { i, j };
                    }
                }
            }

            throw new ApplicationException("No two sum solution");
        }

        // taken from popular answers
        // Two-pass Hash Table
        private int[] TwoSum_Solution2(int[] nums, int target)
        {
            var dictionary = new Dictionary<int, int>();
            for (var i = 0; i < nums.Length; i++)
            {
                dictionary[nums[i]] = i;
            }

            for (var i = 0; i < nums.Length; i++)
            {
                var complement = target - nums[i];
                if (dictionary.ContainsKey(complement) && dictionary[complement] != i)
                {
                    return new[] { i, dictionary[complement] };
                }
            }

            throw new ApplicationException("No two sum solution");
        }

        // taken from popular answers
        // One-pass Hash Table
        private int[] TwoSum_Solution3(int[] nums, int target)
        {
            var dictionary = new Dictionary<int, int>();

            for (var i = 0; i < nums.Length; i++)
            {
                var complement = target - nums[i];

                if (dictionary.ContainsKey(complement))
                {
                    return new[] { dictionary[complement], i };
                }

                dictionary[nums[i]] = i;
            }

            throw new ApplicationException("No two sum solution");
        }

        public void Run()
        {
            foreach (var (nums, target, expectedOutput) in _inputData)
            {
                Console.WriteLine($"Input: [{string.Join(',', nums)}]");

                var output = TwoSum_Solution3(nums, target);

                Console.WriteLine($"Actual output: [{string.Join(',', output)}]");
                Console.WriteLine($"Expected output: [{string.Join(',', expectedOutput)}]");

                Console.WriteLine(new string('-', 80));
            }
        }
    }
}