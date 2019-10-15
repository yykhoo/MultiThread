using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace MultiThread
{
    class Program
    {
        static int i = 0;
        
        static void Main(string[] args)
        {

            Console.WriteLine("i value: " + IncrementStaticVariable());
            Console.WriteLine("i value: " + IncrementStaticVariable());
            /*
            Console.WriteLine("j value: " + IncrementLocalVariable(1) );
            Console.WriteLine("j value: " + IncrementLocalVariable(1));
            */
            /*
            threadcalling tc = new threadcalling();
            tc.MyThreadMethod();
            */
            threadcalling.MyThreadMethod();
        }
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static int IncrementStaticVariable()
        {
            return ++i;
        }

        public static int IncrementLocalVariable(int j)
        {
            return ++j;
        }

        // shared memory variable between the two threads  
        // used to indicate which thread we are in  
        class threadcalling
        {
            private static string _threadOutput = "";
            static bool _stopThreads = false;
            private static int val = 0;

            [MethodImpl(MethodImplOptions.Synchronized)]
            public static void MyThreadMethod()
            {
                // construct two threads for our demonstration;  
                Thread thread1 = new Thread(new ThreadStart(IncrementStaticVariable));
                Thread thread2 = new Thread(new ThreadStart(IncrementStaticVariable2));

                // start them  
                thread1.Start();
                thread2.Start();
            }
            /// <summary>  
            /// Thread 1: Loop continuously,  
            /// Thread 1: Displays that we are in thread 1  
            /// </summary>  

            [MethodImpl(MethodImplOptions.Synchronized)]
            public static void DisplayThread1()
            {
                while (_stopThreads == false)
                {
                    Console.WriteLine("Display Thread 1");

                    // Assign the shared memory to a message about thread #1  
                    _threadOutput = "Hello Thread1";


                    Thread.Sleep(1000);  // simulate a lot of processing   

                    // tell the user what thread we are in thread #1, and display shared memory  
                    Console.WriteLine("Thread 1 Output --> {0}", _threadOutput);

                }
            }

            /// <summary>  
            /// Thread 2: Loop continuously,  
            /// Thread 2: Displays that we are in thread 2  
            /// </summary>  
            /// 
            [MethodImpl(MethodImplOptions.Synchronized)]
            public static void DisplayThread2()
            {
                while (_stopThreads == false)
                {
                    Console.WriteLine("Display Thread 2");


                    // Assign the shared memory to a message about thread #2  
                    _threadOutput = "Hello Thread2";


                    Thread.Sleep(1000);  // simulate a lot of processing  

                    // tell the user we are in thread #2  
                    Console.WriteLine("Thread 2 Output --> {0}", _threadOutput);

                }
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public static void IncrementStaticVariable()
            {
                while (_stopThreads == false)
                {
                    ++val;

                    Console.WriteLine("val: " + val);

                    if (val == 100)
                        _stopThreads = true;
                }
                    
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public static void IncrementStaticVariable2()
            {
                _stopThreads = false;
                while (_stopThreads == false)
                {
                    ++val;
                    Console.WriteLine("val2: " + val);
                }
            }
        }
    
    }
}
