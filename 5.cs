using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            int[] arr = new int[1000];
            Random rnd = new Random();

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rnd.Next(-3000,3000);
            }

           
            List<int>[] l = new List<int>[10];
            for (int i = 0; i< 10; i++)
                l[i] = new List<int>();
            int temp = 0;
            for (int i = 0; i<arr.Length; i++)
            {   if (arr[i] < 0) temp = (arr[i] / 100 * -1)%10;
                else temp = (arr[i] / 100)%10;
                l[temp].Add(arr[i]);
            }

            Parallel.For(0, 10, ToFile =>
            {
                string path = $"file_{Task.CurrentId-1}.txt";
                int id = (int)Task.CurrentId-1;
                using (var tw = new StreamWriter(path))
                {
                    tw.WriteLine(string.Join(", ", l[id]));
                }
            });
           
        }
        
    }
}
