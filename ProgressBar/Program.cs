using System;
using System.Windows;

namespace ProgressBar
{
    public static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        public static void Main()
        {
            new Application().Run(new ProgressBarMain());
        }
    }
}
