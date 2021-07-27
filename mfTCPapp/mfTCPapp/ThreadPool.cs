using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace mfTCPapp
{

    public class ThreadPool<T>
    {
        private ConcurrentQueue<T> taskQueue;
        private int NumThreads;
        private bool running = false;
        private Action<T> handleTask;
        private Thread[] threads;
        public ThreadPool(int num, Action<T> func)
        {
            taskQueue = new ConcurrentQueue<T>();
            NumThreads = num;
            threads = new Thread[NumThreads];
            handleTask = func;
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(new ThreadStart(RunThread));
            }
        }
        public void AddTask(T task)
        {
            taskQueue.Enqueue(task);
        }
        public void Start()
        {
            running = true;
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Start();
            }




        }
        public void Join()
        {
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }

        }
        public void AskStop()
        {
            running = false;
        }
        public void RunThread()
        {
            while (running)
            {
                T data;
                if (taskQueue.TryDequeue(out data))
                {
                    handleTask(data);
                }
                Thread.Sleep(1);
            }
        }
    }
}
