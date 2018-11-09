using System;
using KamiJal.CoupleMatch.Service;

namespace KamiJal.CoupleMatch
{
    internal class Program
    {
        public static readonly CoupleMatchManager CoupleMatchManager;

        static Program()
        {
            CoupleMatchManager = new CoupleMatchManager();
        }

        private static void Main(string[] args)
        {
            Console.WriteLine($"Program started at {DateTime.Now}");
            CoupleMatchManager.Start();

            Console.ReadLine();
            CoupleMatchManager.End();
        }
    }
}