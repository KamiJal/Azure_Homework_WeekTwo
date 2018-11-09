using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KamiJal.CoupleMatch.Api;
using KamiJal.CoupleMatch.Service;

namespace KamiJal.CoupleMatch
{
    class Program
    {
        public static readonly CoupleMatchManager CoupleMatchManager;

        static Program()
        {
            CoupleMatchManager = new CoupleMatchManager();
        }

        static void Main(string[] args)
        {
            Console.WriteLine($"Program started at {DateTime.Now}");
            CoupleMatchManager.Start();

            Console.ReadLine();
            CoupleMatchManager.End();
        }
    }
}
