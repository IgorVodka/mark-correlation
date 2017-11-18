using System;
using MarkCorrelation.Requests;

namespace MarkCorrelation
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            LoginRequest lr = new LoginRequest("viea16u008", "dkcrwhat");
            lr.Perform();

            EUMainPageRequest mpr = new EUMainPageRequest(lr.Client);
            mpr.Perform();

            Console.ReadKey(true);
        }
    }
}
