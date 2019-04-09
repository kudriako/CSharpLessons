using System;
using System.Threading;

namespace CSharpLessons.Threads
{
    class Program
    {
        static void Main(string[] args)
        {
            // Uncomment the code one by one

            // 1. Threads
            // Thread thread1 = new Thread(PrintExclamation);
            // Thread thread2 = new Thread(PrintUnderscore);
            // thread1.Start();
            // thread2.Start();

            // 2. Join
            //Thread thread1 = new Thread(PrintExclamation);
            //Thread thread2 = new Thread(PrintUnderscore);
            //thread1.Start();
            //thread2.Start();
            //thread2.Join();
            //PrintPoint();

            // 3. ThreadPriority
            // Thread thread1 = new Thread(PrintExclamation) { Priority = ThreadPriority.AboveNormal };
            // Thread thread2 = new Thread(PrintUnderscore) { Priority = ThreadPriority.BelowNormal };
            // thread1.Start();
            // thread2.Start();
            // thread1.Join();
            // thread2.Join();

            // 4. Lock
            // Thread thread1 = new Thread(PrintExclamationSync) { Priority = ThreadPriority.AboveNormal };
            // Thread thread2 = new Thread(PrintUnderscoreSync) { Priority = ThreadPriority.BelowNormal };
            // thread1.Start();
            // thread2.Start();
            // thread1.Join();
            // thread2.Join();

            // 5. Ping Pong
            // Thread thread1 = new Thread(Ping);
            // Thread thread2 = new Thread(Pong);
            // thread1.Start();
            // thread2.Start();
            // thread1.Join();
            // thread2.Join();

            // 6. Interlocked
            //Thread thread1 = new Thread(Increase);
            //Thread thread2 = new Thread(Increase);
            // replace with 
            //Thread thread1 = new Thread(IncreaseInterlocked);
            //Thread thread2 = new Thread(IncreaseInterlocked);
            //thread1.Start();
            //thread2.Start();
            //thread1.Join();
            //thread2.Join();
            //Console.Write(_i);

            // 7. Mutex
            //var writer = new PingPongMutex(count: 3);
            //writer.WritePingPongs();
        }

        #region 1 - 3
        static void PrintPoint()
        {
            for(var i = 0; i < 1000; i++)
            {
                Console.Write(".");
            }
        }

        static void PrintExclamation()
        {
            for(var i = 0; i < 1000; i++)
            {
                Console.Write("!");
            }
        }
        static void PrintUnderscore()
        {
            for(var i = 0; i < 1000; i++)
            {
                Console.Write("_");
            }
        }
        #endregion 1 - 3

        #region 4
        static object _locker = new Object();

        static void PrintExclamationSync()
        {
            lock(_locker)
            {
                for(var i = 0; i < 1000; i++)
                {
                    Console.Write("!");
                }
            }
            
        }

        static void PrintUnderscoreSync()
        {
            lock(_locker)
            {
                for(var i = 0; i < 1000; i++)
                {
                    Console.Write("_");
                }
            }
        }
        #endregion 4

        #region 5
        static bool _ping = true;

        static void Ping()
        {
            for(var i = 0; i < 10; i++)
            {
                lock (_locker)
                {
                    if (!_ping)
                    {
                        Monitor.Wait(_locker);
                    }
                    Console.Write("Ping ");
                    _ping = false;
                    Monitor.Pulse(_locker);
                }
            }
            
        }

        static void Pong()
        {
            for(var i = 0; i < 10; i++)
            {
                lock (_locker)
                {
                    if (_ping)
                    {
                        Monitor.Wait(_locker);
                    }
                    Console.Write("Pong ");
                    _ping = true;
                    Monitor.Pulse(_locker);
                }
            }
        }
        #endregion // 5

        #region 6

        static int _i = 0;
        static void Increase()
        {
            for(var i = 0; i < 10000; i++)
            {
                _i = _i + 1;
            }
        }
        static void IncreaseInterlocked()
        {
            for(var i = 0; i < 10000; i++)
            {
                Interlocked.Increment(ref _i);
            }
        }
        #endregion // 6
    }
}
