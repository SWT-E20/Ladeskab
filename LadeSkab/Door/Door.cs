using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeSkab
{
    public class Door : IDoor
    {
        public bool IsLocked { get; set; }
        public bool IsOpen { get; set; }

        public Door()
        {
            IsLocked = false;
            IsOpen = false;
        }

        public class DoorStateChangedEventArgs : EventArgs
        {
            public bool IsOpen { get; set; }
        }
        public event EventHandler<DoorStateChangedEventArgs> DoorStatusChanged;
        private void OnDoorStatusChange(DoorStateChangedEventArgs e)
        {
            DoorStatusChanged?.Invoke(this, e);
        }

        public bool LockDoor()
        {
            if (IsLocked) return false; // already locked?
            if (IsOpen) return false; // cant lock while open

            Console.WriteLine("Door locked.");
            IsLocked = true;
            return true;
        }

        public bool UnlockDoor()
        {
            if (!IsLocked) return false; // already unlocked?
            if (IsOpen) return false; // cant unlock while open

            Console.WriteLine("Door unlocked.");
            IsLocked = false;
            return true;
        }

        public bool OnToggleDoor()
        {
            // close door if open:
            if (IsOpen)
            {
                Console.WriteLine("Door closed.");

                IsOpen = false;
                OnDoorStatusChange(new DoorStateChangedEventArgs{IsOpen = false}); // send door's updated status
                return true;
            }

            // open door if closed and not locked:
            else if (!IsLocked)
            {
                Console.WriteLine("Door opened.");

                IsOpen = true;
                OnDoorStatusChange(new DoorStateChangedEventArgs { IsOpen = true }); // send door's updated status
                return true;
            }
            Console.WriteLine("Can't open - door is locked!");
            return false;
        }
    }
}
