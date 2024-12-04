using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelSample;
internal class ParallelCancel
{
    public static void Test()
    {
        int[] nums = Enumerable.Range(0, 10_000_000).ToArray();
        CancellationTokenSource cts = new();

        // Use ParallelOptions instance to store the CancellationToken
        ParallelOptions options = new()
        {
            CancellationToken = cts.Token,
            MaxDegreeOfParallelism = Environment.ProcessorCount
        };
        Console.WriteLine("Press any key to start. Press 'c' to cancel.");
        Console.ReadKey();

        // Run a task so that we can cancel from another thread.
        Task.Factory.StartNew(() =>
        {
            if (Console.ReadKey().KeyChar is 'c')
                cts.Cancel();
            Console.WriteLine("press any key to exit");
        });

        try
        {
            Parallel.ForEach(nums, options, (num) =>
            {
                double d = Math.Sqrt(num);
                Console.WriteLine("{0} on {1}", d, Environment.CurrentManagedThreadId);
            });
        }
        catch (OperationCanceledException e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            cts.Dispose();
        }

        Console.ReadKey();
    }
}
