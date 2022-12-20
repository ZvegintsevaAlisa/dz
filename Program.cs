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
                        .ToArray();
                    stw.Stop();
                    Console.WriteLine($"Милисекунд: {stw.ElapsedMilliseconds}");
                    if (res.Length < 5)
                        Console.WriteLine("Меньше 5 элементов");
                    else Console.WriteLine(res[4] + " " + res[5] + " " + res[6]);
                    break;
                case "2":
                    Stopwatch stw2 = new Stopwatch();
                    stw2.Start();
                    var res2 = nums.AsParallel().AsOrdered().WithDegreeOfParallelism(5)
                        .Select(calc)
                        .GroupBy(n => n % 10).Select(n => n.Sum()).OrderBy(n => n)
                        .ToArray();
                    stw2.Stop();
                    Console.WriteLine($"Милисекунд: {stw2.ElapsedMilliseconds}");
                    if (res2.Length < 5)
                        Console.WriteLine("Меньше 5 элементов");
                    else Console.WriteLine(res2[4] + " " + res2[5] + " " + res2[6]);
                    break;
                case "3":
                    Stopwatch stw3 = new Stopwatch();
                    stw3.Start();
                    var res3 = nums.AsParallel().AsOrdered().WithDegreeOfParallelism(15)
                        .Select(calc)
                        .GroupBy(n => n % 10).Select(n => n.Sum()).OrderBy(n => n)
                        .ToArray();
                    stw3.Stop();
                    Console.WriteLine($"Милисекунд: {stw3.ElapsedMilliseconds}");
                    if (res3.Length < 5)
                        Console.WriteLine("Меньше 5 элементов");
                    else Console.WriteLine(res3[4] + " " + res3[5] + " " + res3[6]);
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
