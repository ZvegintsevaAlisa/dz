using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Async
{
    
    internal class TaskA { }
    internal class TaskB { }

    class Program
    {
        static EventWaitHandle wh = new AutoResetEvent(false);
        static EventWaitHandle wh2 = new AutoResetEvent(false);
        static EventWaitHandle wh3 = new AutoResetEvent(false);
        static EventWaitHandle[] w = new EventWaitHandle[2] { wh, wh2 };
        static async Task Main()
        {
            TaskA A = DoTaskAA();
            Console.WriteLine($"{DateTime.Now}: Finished Task A_A");

            var TaskA_B = DoTaskAB();
            var TaskB_A = DoTaskBA();
            var CurTasks = new List<Task> { TaskA_B, TaskB_A};
            while (CurTasks.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(CurTasks);
                if (finishedTask == TaskA_B)
                {
                    var TaskA_C = DoTaskAC();
                    CurTasks.Add(TaskA_C);
                    Console.WriteLine($"{DateTime.Now}: Finished Task A_C");
                }
                else if (finishedTask == TaskB_A)
                {
                    var TaskB_B = DoTaskBB();
                    CurTasks.Add(TaskB_B);
                    Console.WriteLine($"{DateTime.Now}: Finished Task B_A");
                }
                CurTasks.Remove(finishedTask);
            }

            var TaskB_C = DoTaskBC();
            var TaskB_D = DoTaskBD();
            await Task.WhenAll(TaskB_C, TaskB_D);
        }
       
        static TaskA DoTaskAA()
        {         
            Console.WriteLine($"{DateTime.Now}: Started task A_A");
            wh.Set(); wh2.Set();
            return new TaskA();
        }
        static async Task<TaskA> DoTaskAB()
        {
            wh.WaitOne();
            Console.WriteLine($"{DateTime.Now}: Started Task A_B");
            await Task.Delay(300);
            Console.WriteLine($"{DateTime.Now}: Finished Task A_B");
            wh.Set();
            return new TaskA();
        }
        static async Task<TaskB> DoTaskBA()
        {
            wh2.WaitOne();
            Console.WriteLine($"{DateTime.Now}: Started Task B_A");
            await Task.Delay(300);
            wh2.Set();
            return new TaskB();
        }

        static async Task<TaskA> DoTaskAC()
        {
            // var t = await DoTaskAB();
            wh.WaitOne();
            Console.WriteLine($"{DateTime.Now}: Started Task A_C");
            await Task.Delay(300);
            wh.Set();
            return new TaskA();
        }

        static async Task<TaskB> DoTaskBB()
        {
            //var t = await DoTaskBA();
            wh2.WaitOne();
            Console.WriteLine($"{DateTime.Now}: Started Task B_B");
            await Task.Delay(300);
            Console.WriteLine($"{DateTime.Now}: Finished Task B_B");
            wh2.Set();
            wh3.Set();
            return new TaskB();
        }

        static async Task<TaskB> DoTaskBC()
        {
            //await DoTaskBB();
            //await DoTaskAC();
            WaitHandle.WaitAll(w);
            Console.WriteLine($"{DateTime.Now}: Started Task B_C");
            await Task.Delay(700);
            Console.WriteLine($"{DateTime.Now}: Finished Task B_C");
            return new TaskB();
        }
        static async Task<TaskB> DoTaskBD()
        {
            //  await DoTaskBB();
            wh3.WaitOne();
            Console.WriteLine($"{DateTime.Now}: Started Task B_D");
            await Task.Delay(700);
            Console.WriteLine($"{DateTime.Now}: Finished Task B_D");
            return new TaskB();
        }
    }
    }
