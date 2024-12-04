using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelSample;
internal class ParallelLocal
{
    /// <summary>
    /// 代码解释: https://learn.microsoft.com/zh-cn/dotnet/standard/parallel-programming/how-to-write-a-parallel-for-loop-with-thread-local-variables
    /// </summary>
    public static void TestFor()
    {
        int[] nums = Enumerable.Range(0, 1_0).ToArray();
        long total = 0;

        // Use type parameter to make subtotal a long, not an int
        Parallel.For<long>(0, nums.Length,
            // 此函数为每个线程创建一个局部变量的初始值
            // 在此例中，每个线程的局部变量初始值为1 
            // 故在最终的输出结果中，最终结果值为动态值:  45 + 线程个数*2
            () =>
            {
                Console.WriteLine($"Press {Thread.CurrentThread.ManagedThreadId}");
                return 2;
            },
            (j, loopState, subtotal) =>
            {
                subtotal += nums[j];
                return subtotal;
            },
            itemSubtotal => Interlocked.Add(ref total, itemSubtotal));

        Console.WriteLine("The total is {0:N0}", total);
        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
    }

    public static void TestForEach()
    {
        int[] nums = Enumerable.Range(0, 1000000).ToArray();
        long total = 0;

        // First type parameter is the type of the source elements
        // Second type parameter is the type of the thread-local variable (partition subtotal)
        Parallel.ForEach<int, long>(
            nums, // source collection
            () => 0, // method to initialize the local variable
            (j, loop, subtotal) => // method invoked by the loop on each iteration
            {
                subtotal += j; //modify local variable
                return subtotal; // value to be passed to next iteration
            },
            // Method to be executed when each partition has completed.
            // finalResult is the final value of subtotal for a particular partition.
            (finalResult) => Interlocked.Add(ref total, finalResult));

        Console.WriteLine("The total from Parallel.ForEach is {0:N0}", total);
        // The example displays the following output:
        //        The total from Parallel.ForEach is 499,999,500,000
    }
}
