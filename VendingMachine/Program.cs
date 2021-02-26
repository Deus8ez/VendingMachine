using System;
using System.Collections.Generic;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            Machine machine = new Machine();
            VendingService vender = new VendingService(machine);
            vender.StartVending();
        }
    }
}
