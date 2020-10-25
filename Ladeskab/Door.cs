using System;
using System.Collections.Generic;
using System.Text;
using Ladeskab.Interfaces;

namespace Ladeskab
{
    public class Door : IDoor
    {
        public bool IsLocked {get; set; }

        public void LockDoor()
        {
            Console.WriteLine("Door locked.");
            IsLocked = true;
        }

        public void UnlockDoor()
        {
            Console.WriteLine("Door unlocked.");
            IsLocked = false;
        }
    }
}
