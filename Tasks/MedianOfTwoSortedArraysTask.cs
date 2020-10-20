using CabaVS.LeetCode.Tasks.Abstractions;
using System;

namespace CabaVS.LeetCode.Tasks
{
    // https://leetcode.com/problems/median-of-two-sorted-arrays/
    // good explanation: https://youtu.be/LPFhl65R7ww
    public class MedianOfTwoSortedArraysTask : ITask
    {
        private readonly (int[] Array1, int[] Array2, double ExpectedOutput)[] _inputData =
        {
            (new[] {1,3}, new[] {2}, 2),
            (new[] {1,2}, new[] {3,4}, 2.5),
            (new[] {0,0}, new[] {0,0}, 0),
            (new int[] {}, new[] {1}, 1),
            (new[] {2}, new int[] {}, 2),
            (new[] {1,3}, new[] {2,7}, 2.5)
        };

        public void Run()
        {
            foreach (var (array1, array2, expectedOutput) in _inputData)
            {
                Console.WriteLine($"Input (arr1): [{string.Join(',', array1)}]");
                Console.WriteLine($"Input (arr2): [{string.Join(',', array2)}]");

                var output = FindMedianSortedArrays(array1, array2);

                Console.WriteLine($"Actual output: {output}");
                Console.WriteLine($"Expected output: {expectedOutput}");

                Console.WriteLine(new string('-', 80));
            }
        }

        private double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            if (nums1 == null) throw new ArgumentNullException(nameof(nums1));
            if (nums2 == null) throw new ArgumentNullException(nameof(nums2));

            var mergedArray = new int[nums1.Length + nums2.Length];

            var p1 = 0;
            var p2 = 0;
            for (var i = 0; i < nums1.Length + nums2.Length; i++)
            {
                if (p1 < nums1.Length && (p2 >= nums2.Length || nums1[p1] <= nums2[p2]))
                {
                    mergedArray[i] = nums1[p1];
                    p1 += 1;
                }
                else
                {
                    mergedArray[i] = nums2[p2];
                    p2 += 1;
                }
            }

            return mergedArray.Length % 2 == 0
                ? (double)(mergedArray[mergedArray.Length / 2] + mergedArray[mergedArray.Length / 2 - 1]) / 2
                : mergedArray[(mergedArray.Length - 1) / 2];
        }

        #region Solution taken from https://medium.com/@hazemu/finding-the-median-of-2-sorted-arrays-in-logarithmic-time-1d3f2ecbeb46

        private double FindMedianSortedArrays_FromSolutions(int[] A, int[] B)
        {
            if (A == null) throw new ArgumentNullException(nameof(A));
            if (B == null) throw new ArgumentNullException(nameof(B));

            var aLen = A.Length;
            var bLen = B.Length;

            if (aLen > bLen)
            {
                Swap(ref A, ref B);
                Swap(ref aLen, ref bLen);
            }

            int leftHalfLen = (aLen + bLen + 1) / 2;

            int aMinCount = 0;
            int aMaxCount = aLen;

            while (aMinCount <= aMaxCount)
            {
                int aCount = aMinCount + ((aMaxCount - aMinCount) / 2);
                int bCount = leftHalfLen - aCount;

                int? x = (aCount > 0) ? A[aCount - 1] : (int?)null;
                int? y = (bCount > 0) ? B[bCount - 1] : (int?)null;

                int? xP = (aCount < aLen) ? A[aCount] : (int?)null;
                int? yP = (bCount < bLen) ? B[bCount] : (int?)null;

                if (x > yP)
                {
                    aMaxCount = aCount - 1;
                }
                else if (y > xP)
                {
                    aMinCount = aCount + 1;
                }
                else
                {
                    int leftHalfEnd = (x == null)
                        ? y.Value
                        : (y == null)
                            ? x.Value
                            : Math.Max(x.Value, y.Value);

                    if (IsOdd(aLen + bLen))
                    {
                        return leftHalfEnd;
                    }

                    int rightHalfStart = (xP == null)
                        ? yP.Value
                        : (yP == null)
                            ? xP.Value
                            : Math.Min(xP.Value, yP.Value);
                    return (leftHalfEnd + rightHalfStart) / 2.0;
                }
            }

            throw new InvalidOperationException("Unexpected code path reached");
        }

        private void Swap<T>(ref T x, ref T y)
        {
            T temp = x;
            x = y;
            y = temp;
        }

        // The least significant bit of any odd number is 1.
        private bool IsOdd(int x) => (x & 1) == 1;

        #endregion
    }
}