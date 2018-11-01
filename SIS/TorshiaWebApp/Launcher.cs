using SIS.Framework;
using System;

namespace TorshiaWebApp
{
    public class Launcher
    {
        public static void Main(string[] args)
        {
            WebHost.Start(new StartUp());
        }
    }
}
