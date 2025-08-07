using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Rec_rewild.console
{
    public class Loading 
    {
        private static readonly string[] SpinnerFrames = { "●    ", " ●   ", "  ●  ", "   ● ", "    ●", "   ● ", "  ●  ", " ●   " };



        private static int spinnerIndex = 0;
        private static bool isloading = false;
        private static Thread spinnerThread;

        public static void ShowLoading(string message = "Loading")
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            if (isloading)
                return;

            isloading = true;

            Console.Clear();
            Console.WriteLine(message); 

            spinnerThread = new Thread(() =>
            {
                int top = Console.CursorTop;
                while (isloading)
                {
                    Console.SetCursorPosition(0, top);
                    Console.Write($"[ {SpinnerFrames[spinnerIndex]} ]    ");
                    spinnerIndex = (spinnerIndex + 1) % SpinnerFrames.Length;
                    Thread.Sleep(100);
                }

            });

            spinnerThread.IsBackground = true;
            spinnerThread.Start();
        }


        public static void StopLoading()
        {
            isloading = false;
            spinnerThread?.Join();
        }
    }
}
