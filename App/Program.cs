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

            Console.WriteLine("E - close program.");
            Console.WriteLine("O - open/close door.");
            Console.WriteLine("R - Scan RFID.\n\n");

            bool finish = false;
            do
            {
                string input;
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                    case 'e':
                        finish = true;
                        break;

                    case 'O':
                    case 'o':
                        door.OnToggleDoor();
                        break;

                    case 'R':
                    case 'r':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        //rfidReader.OnRfidRead(id);
                        break;

                    /////////section for testing///////////
                    case 'L':
                    case 'l':
                        door.LockDoor();
                        break;

                    case 'U':
                    case 'u':
                        door.UnlockDoor();
                        break;
                    /////////////////////////////////////

                    default:
                        Console.WriteLine("!INVALID INPUT!");
                        break;
                }
                Console.WriteLine();

            } while (!finish);
        }
    }
}

