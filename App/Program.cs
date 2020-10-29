using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeSkab;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            IDoor door = new Door();
            IRfidReader rfid = new RfidReader();
            IDisplay display = new Display();
            ILogFile logfile = new LogFile("logfile.txt");

            UsbCharger charger = new UsbCharger();
            IChargeControl chargeControl = new ChargeControl {Charger = charger, Display = display};

            StationControl ladeSkab = new StationControl(door, rfid, display, chargeControl, logfile);

            Console.WriteLine("e - Close program.");
            Console.WriteLine("o - Open/close door.");
            Console.WriteLine("r - Scan RFID.");
            Console.WriteLine("c - Phone connected.");
            Console.WriteLine("d - Phone disconnected.\n");

            bool finish = false;
            do
            {
                string input;
                Console.Write("> ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'e':
                        finish = true;
                        break;

                    case 'o':
                        door.OnToggleDoor();
                        break;

                    case 'r':
                        Console.WriteLine("Enter RFID id: ");
                        string inputId = Console.ReadLine();
                        int id = Convert.ToInt32(inputId);

                        rfid.OnKeySwiped(id);
                        break;

                    case 'c':
                        Console.WriteLine("Phone connected.");
                        charger.SimulateConnected(true);
                        break;

                    case 'd':
                        Console.WriteLine("Phone disconnected.");
                        charger.SimulateConnected(false);
                        break;

                    default:
                        Console.WriteLine("!INVALID INPUT!");
                        break;
                }
                Console.WriteLine();

            } while (!finish);
        }
    }
}
