using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp10
{
   
        class Program
        {
            static void Main(string[] args)
            {
            Random rnd = new Random();
            int[] nums = new int[100];
            for (int i = 0; i < nums.Length; i++)
                nums[i] = rnd.Next(-10,10);

           
            Console.WriteLine("1.Степень параллелизма=2.\n2.Степень параллелизма = 5.\n3.Степень параллелизма = 15");
            string cmd = Console.ReadLine();
            switch (cmd)
            {
                case "1":
                    Stopwatch stw = new Stopwatch();
                    stw.Start();
                    var res = nums.AsParallel().AsOrdered().WithDegreeOfParallelism(2)
                        .Select(calc)
                        .GroupBy(n => n % 10).Select(n => n.Sum()).OrderBy(n => n)
                        .Where((n, index) => index >= 4 && index <=6 )
                        .ToArray();
                    stw.Stop();
                    Console.WriteLine($"Милисекунд: {stw.ElapsedMilliseconds}");
                    foreach (var r in res)
                        Console.WriteLine(r + " ");
                    break;
                case "2":
                    Stopwatch stw2 = new Stopwatch();
                    stw2.Start();
                    var res2 = nums.AsParallel().AsOrdered().WithDegreeOfParallelism(5)
                        .Select(calc)
                        .GroupBy(n => n % 10).Select(n => n.Sum()).OrderBy(n => n)
                        .Where((n, index) => index >= 4 && index <= 6)
                        .ToArray();
                    stw2.Stop();
                    Console.WriteLine($"Милисекунд: {stw2.ElapsedMilliseconds}");
                    foreach (var r in res2)
                        Console.WriteLine(r + " ");
                    break;
                case "3":
                    Stopwatch stw3 = new Stopwatch();
                    stw3.Start();
                    var res3 = nums.AsParallel().AsOrdered().WithDegreeOfParallelism(15)
                        .Select(calc)
                        .GroupBy(n => n % 10).Select(n => n.Sum()).OrderBy(n => n)
                        .Where((n, index) => index >= 4 && index <= 6)
                        .ToArray();
                    stw3.Stop();
                    Console.WriteLine($"Милисекунд: {stw3.ElapsedMilliseconds}");
                    foreach (var r in res3)
                        Console.WriteLine(r + " ");
                    break;
            }
            Console.ReadLine();

        }

        static int calc(int i)
            {
                int result = i*(i+2);
                return result;
            }
        
    }
}
