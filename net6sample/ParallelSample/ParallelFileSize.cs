using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelSample;
internal class ParallelFileSize
{
    public static long GetFileSize(string filePath)
    {
        long totalSize = 0;

        if (string.IsNullOrWhiteSpace(filePath))
        {
            Console.WriteLine("There are no command line arguments.");
            return 0;
        }
        if (!Directory.Exists(filePath))
        {
            Console.WriteLine("The directory does not exist.");
            return 0;
        }

        String[] files = Directory.GetFiles(filePath);
        Parallel.For(0, files.Length,
                     index => {
                         FileInfo fi = new FileInfo(files[index]);
                         long size = fi.Length;
                         Interlocked.Add(ref totalSize, size);
                     });
        Console.WriteLine("Directory '{0}':", filePath);
        Console.WriteLine("{0:N0} files, {1:N0} bytes", files.Length, totalSize);
        return long.Parse(totalSize.ToString());
    }

}
