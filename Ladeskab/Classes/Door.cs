using System;
using System.Collections.Generic;
using System.Text;
using Ladeskab.Interfaces;

namespace Ladeskab
{
    public class Door : IDoor
    {
        public bool IsLocked { get; set; }
        public bool IsOpen { get; set; }

        public Door()
        {
            IsLocked = false;
            IsOpen = true;
        }

        public bool LockDoor()
        {
            if (IsLocked) return false;
            if (IsOpen) return false;

            Console.WriteLine("Door locked.");
            IsLocked = true;
            return true;
        }

        public bool UnlockDoor()
        {
            if (!IsLocked) return false;
            if (IsOpen) return false;

            Console.WriteLine("Door unlocked.");
            IsLocked = false;
            return true;
        }
        
        public bool OnToggleDoor()
        {
            if (IsOpen)
            {
                IsOpen = false;
                Console.WriteLine("Door closed.");
                return true;
            }
            else if(!IsLocked)
            {
                IsOpen = true;
                Console.WriteLine("Door opened.");
                return true;
            }
            Console.WriteLine("Can't open - door is locked!");
            return false;
        }
    }
}
