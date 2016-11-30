using System;
using System.Diagnostics;
using System.Threading;

namespace  CodedUI_Temp.ATProMethods
{
    public class Wait
    {
        public static void UntilTrue(Func<bool> p, string err = "exseeded waiting time", int timeout = 15000, int interval = 1000)
        {
            var timer = new Stopwatch();
            timer.Start();
            while (true)
            {
                if (p()) return;
                //Console.WriteLine(timer.ElapsedMilliseconds+" passed, result is false");
                //Log.Warn(timer.ElapsedMilliseconds + " ms past: result false");
                if (timer.ElapsedMilliseconds > timeout) throw new TimeoutException("Timeout Error: " + err);
            }
        }

        public static void Sleep(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        public static T UntilNoException<T>(Func<T> f, int timeout = 15000, int interval = 1000)
        {
            var timer = new Stopwatch();
            timer.Start();
            while (true)
            {
                try
                {
                    return f();
                }
                catch (Exception ex)
                {
                    if (timer.ElapsedMilliseconds > timeout) throw (ex);
                    Sleep(interval);
                }
            }
        }
        public static void UntilNoException(Action f, int timeout = 15000, int interval = 1000)
        {
            var timer = new Stopwatch();
            timer.Start();
            while (true)
            {
                try
                {
                    f();
                    return;
                }
                catch (Exception ex)
                {
                    if (timer.ElapsedMilliseconds > timeout) throw (ex);
                    Sleep(interval);
                }
            }
        }
        
    }
}
