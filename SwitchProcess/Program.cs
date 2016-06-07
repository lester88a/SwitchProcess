using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Timers;

namespace SwitchProcess
{
    class Program
    {
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private static Timer aTimer;

        private static int waitingTime1;
        private static string title1;
        private static string title2;
        private static string title3;
        private static bool isTwoWindow; 
        static void Main(string[] args)
        {
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("-please select the following options-------------");
            Console.WriteLine("-1. switch 2 windows-----------------------------");
            Console.WriteLine("-2. switch 3 windows-----------------------------");
            Console.WriteLine("-------------------------------------------------\n");
            int option = Convert.ToInt32(Console.ReadLine());
            if (option == 1)
            {
                isTwoWindow = true;
            }
            else if (option == 2)
            {
                isTwoWindow = false;
            }

            Console.WriteLine("Please enter your switching time(1-300 seconds): ");
            waitingTime1 = Convert.ToInt32(Console.ReadLine());
           
            if (waitingTime1 >0 && waitingTime1<=300)
            {
                Console.WriteLine("Please enter your first window title: ");
                title1 = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Please enter your second window title: ");
                title2 = Convert.ToString(Console.ReadLine());
                if (isTwoWindow)
                {
                    SwitchWindow(title1);

                    aTimer = new Timer();
                    aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent_1);
                    aTimer.Interval = (1 * waitingTime1 * 1000); // 5 seconds
                    aTimer.Enabled = true;
                    aTimer.Start();
                }
                else
                {
                    Console.WriteLine("Please enter your last window title: ");
                    title3 = Convert.ToString(Console.ReadLine());
                    SwitchWindow(title1);

                    aTimer = new Timer();
                    aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent_1);
                    aTimer.Interval = (1 * waitingTime1 * 1000); // 5 seconds
                    aTimer.Enabled = true;
                    aTimer.Start();
                }

               
            }

            Console.WriteLine("Press \'q\' to quit the windows switching application.");
            while (Console.Read() != 'q') ;
        }
        
        //switch windows function
        static void SwitchWindow(string mainWindowTitle)
        {
            try
            {
                //bool launched = false;

                Process[] processList = Process.GetProcesses();
                
                foreach (Process theProcess in processList)
                {
                    if (theProcess.MainWindowTitle.ToUpper().Contains(mainWindowTitle.ToUpper()))
                    {
                        ShowWindow(theProcess.MainWindowHandle, 3);
                        //launched = true;
                    }
                    else
                    {
                        ShowWindow(theProcess.MainWindowHandle, 2);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        //timer event
        private static void OnTimedEvent_1(object source, ElapsedEventArgs e)
        {
            SwitchWindow(title2);
            aTimer.Stop();
            aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent_2);
            aTimer.Interval = (1 * waitingTime1 * 1000); // 5 seconds
            aTimer.Enabled = true;
            aTimer.Start();
        }
        //timer event
        private static void OnTimedEvent_2(object source, ElapsedEventArgs e)
        {
            if (isTwoWindow)
            {
                SwitchWindow(title1);
                aTimer.Stop();
                aTimer = new Timer();
                aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent_1);
                aTimer.Interval = (1 * waitingTime1 * 1000); // 5 seconds
                aTimer.Enabled = true;
                aTimer.Start();
            }
            else
            {
                SwitchWindow(title3);
                aTimer.Stop();
                aTimer = new Timer();
                aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent_3);
                aTimer.Interval = (1 * waitingTime1 * 1000); // 5 seconds
                aTimer.Enabled = true;
                aTimer.Start();
            }
        }
        //timer event
        private static void OnTimedEvent_3(object source, ElapsedEventArgs e)
        {
            SwitchWindow(title1);
            aTimer.Stop();
            aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent_1);
            aTimer.Interval = (1 * waitingTime1 * 1000); // 5 seconds
            aTimer.Enabled = true;
            aTimer.Start();
        }
    }
}
