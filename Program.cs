using System.Threading;
using System;

class Program
{
    static EventWaitHandle wh = new AutoResetEvent(false);
    static EventWaitHandle wh2 = new AutoResetEvent(false);
    static EventWaitHandle wh3 = new AutoResetEvent(false);
    static EventWaitHandle[] w = new EventWaitHandle[2] { wh, wh2 };
    static void Main()
    {
        new Thread(TaskA_A).Start();
        new Thread(TaskA_B).Start();
        new Thread(TaskB_A).Start();
        new Thread(TaskA_C).Start();
        new Thread(TaskB_B).Start();
        new Thread(TaskB_C).Start();
        new Thread(TaskB_D).Start();
    }

    static void TaskA_A()
    {
        Console.WriteLine($"Task A started at {DateTime.Now}");
        Thread.Sleep(1000);
        Console.WriteLine($"Task A finished at {DateTime.Now}");
        wh.Set();
        wh2.Set();
       
    }
    static void TaskA_B()
    {   wh.WaitOne();
        Console.WriteLine("Task A_B: Signal from A received");
        Console.WriteLine($"Task A_B started at {DateTime.Now}");
        Thread.Sleep(1000);
        Console.WriteLine($"Task A_B finished at {DateTime.Now}");
        wh.Set();
    }
    static void TaskB_A()
    {
        wh2.WaitOne();
        Console.WriteLine("Task B_A: Signal from A received");
        Console.WriteLine($"Task B_A started at {DateTime.Now}");
        Thread.Sleep(300);
        Console.WriteLine($"Task B_A finished at {DateTime.Now}");
        wh2.Set();
    }
    static void TaskA_C()
    {
        wh.WaitOne();
        Console.WriteLine("Task A_C: Signal from A_B received");
        Console.WriteLine($"Task A_C started at {DateTime.Now}");
        Thread.Sleep(300);
        Console.WriteLine($"Task A_C finished at {DateTime.Now}");
        wh.Set();
    }
    static void TaskB_B()
    {
        wh2.WaitOne();
        Console.WriteLine("TaskB_B: Signal from B_A received");
        Console.WriteLine($"Task B_B started at {DateTime.Now}");
        Thread.Sleep(300);
        Console.WriteLine($"Task B_B finished at {DateTime.Now}");
        wh2.Set();
        wh3.Set();
    }
    static void TaskB_C()
    {   
        WaitHandle.WaitAll(w);
        Console.WriteLine("TaskB_C: Signals from B_B and A_C received");
        Console.WriteLine($"Task B_C started at {DateTime.Now}");
        Thread.Sleep(300);
        Console.WriteLine($"Task B_C finished at {DateTime.Now}");
    }
    static void TaskB_D()
    {
        wh3.WaitOne();
        Console.WriteLine("Task B_D: Signal from B_B received");
        Console.WriteLine($"Task B_D started at {DateTime.Now}");
        Thread.Sleep(300);
        Console.WriteLine($"Task B_D finished at {DateTime.Now}");
    }
}