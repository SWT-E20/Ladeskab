using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeSkab
{
    public class Display : IDisplay
    {
        public void Print(string msg)
        {
            Console.WriteLine("Display: " +  msg);
        }
    }
}
