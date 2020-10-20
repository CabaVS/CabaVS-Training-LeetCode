using System;
using System.Collections.Generic;
using System.Text;
using CabaVS.LeetCode.Tasks.Abstractions;

namespace CabaVS.LeetCode.Tasks
{
    // https://leetcode.com/problems/add-two-numbers/
    public class AddTwoNumbersTask : ITask
    {
        private readonly (ListNode L1, ListNode L2, ListNode ExpectedOutput)[] _inputData =
        {
            (new ListNode(2, new ListNode(4, new ListNode(3))), new ListNode(5, new ListNode(6, new ListNode(4))), new ListNode(7, new ListNode(0, new ListNode(8)))),
            (new ListNode(), new ListNode(), new ListNode()),
            (new ListNode(9, new ListNode(9, new ListNode(9, new ListNode(9, new ListNode(9, new ListNode(9, new ListNode(9))))))), new ListNode(9, new ListNode(9, new ListNode(9, new ListNode(9)))), new ListNode(8, new ListNode(9, new ListNode(9, new ListNode(9, new ListNode(0, new ListNode(0, new ListNode(0, new ListNode(1))))))))),
            (new ListNode(3), new ListNode(9), new ListNode(2, new ListNode(1)))
        };

        public void Run()
        {
            foreach (var (l1, l2, expectedOutput) in _inputData)
            {
                Console.WriteLine($"Input (l1): {l1}");
                Console.WriteLine($"Input (l2): {l2}");

                var output = AddTwoNumbers(l1, l2);

                Console.WriteLine($"Actual output: {output}");
                Console.WriteLine($"Expected output: {expectedOutput}");

                Console.WriteLine(new string('-', 80));
            }
        }

        private ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            if (l1 == null) throw new ArgumentNullException(nameof(l1));
            if (l2 == null) throw new ArgumentNullException(nameof(l2));

            var overflow = false;

            var result = new ListNode(l1.Val + l2.Val);
            if (result.Val >= 10)
            {
                result.Val -= 10;
                overflow = true;
            }

            var p1 = l1.Next;
            var p2 = l2.Next;
            var pResult = result;

            while (p1 != null || p2 != null)
            {
                var currentSum = (p1?.Val ?? 0) + (p2?.Val ?? 0);

                if (overflow)
                {
                    currentSum += 1;
                    overflow = false;
                }

                if (currentSum >= 10)
                {
                    currentSum -= 10;
                    overflow = true;
                }

                pResult.Next = new ListNode(currentSum);
                pResult = pResult.Next;

                p1 = p1?.Next;
                p2 = p2?.Next;
            }

            if (overflow)
            {
                pResult.Next = new ListNode(1);
            }

            return result;
        }

        public class ListNode
        {
            public int Val;
            public ListNode Next;

            public ListNode(int val = 0, ListNode next = null)
            {
                Val = val;
                Next = next;
            }

            public override string ToString()
            {
                var listOfValues = new List<int>();
                var pointer = this;

                do
                {
                    listOfValues.Add(pointer.Val);
                    pointer = pointer.Next;
                } while (pointer != null);

                listOfValues.Reverse();

                return $"[{string.Join(',', listOfValues)}]";
            }
        }
    }
}