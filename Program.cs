/*
Write a C# class to execute a number of pieces of work (actions) in the background (i.e.
without blocking the program’s execution).
• The actions must be executed sequentially – one at a time
• The actions must be executed in the order that they were added to the class
• The actions are not necessarily added all at the same time
• The actions are not necessarily all executed on the same thread
 */
using System;
using System.Collections.Generic;
using System.Threading;

namespace BackgroundActions
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Work> works = new List<Work>() 
            { 
                new Work(1), 
                new Work(2), 
                new Work(3) 
            };

            foreach (Work work in works)
            {
                Thread currentThread = new Thread(work.Start);
                currentThread.IsBackground = true;
                currentThread.Priority = ThreadPriority.Normal;
                currentThread.Start();
            }

            for(int action =1; action <=5; action ++)
            {
                // main thread action
                int currentActionValue= action*2;
                Console.WriteLine("******************");
                Console.WriteLine("Action: " + action  + " Current Value :"+ currentActionValue);

                //works
                foreach(Work work in works)
                {
                    Console.WriteLine( "Work ID: " + work.WorkId);
                    Console.WriteLine(work.Message);
                }

                Console.WriteLine("******************");
                Console.WriteLine("main thread: " + Thread.CurrentThread.ManagedThreadId + " is running");

            }


            // Complete Tasks
            foreach (Work work in works)
            {
                work.isThreadRunning = false;

                Console.WriteLine("TaskId: " + work.WorkId);
                Console.WriteLine("Message: " + work.Message);
            }
          

            Console.ReadLine();
        }
    }

    public class Work
    {

        public bool isThreadRunning;
        public int WorkId { get; set; }
        public string Message;
        public Work(int workId)
        {
            WorkId = workId;
            Message = "Work " + workId + " is initiated";
        }

        public void Start()
        {
            isThreadRunning = true;
            while (isThreadRunning)
                Message = "Thread is running";

            Message = "Thread is completed";
        }
    }
}
