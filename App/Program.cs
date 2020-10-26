using System;
using Ladeskab;
using Ladeskab.Interfaces;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            // Assemble your system here from all the classes
            IDoor door = new Door();

            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E, T, R: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'T':
                        door.OnToggleDoor();
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        //rfidReader.OnRfidRead(id);
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}

