using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LadeSkab.Door;

namespace LadeSkab
{
    public class DoorStateChangedEventArgs : EventArgs
    {
        public bool IsOpen { get; set; }
    }
    public interface IDoor
    {
        event EventHandler<DoorStateChangedEventArgs> DoorStatusChanged;
        bool LockDoor();
        bool UnlockDoor();
        bool OnToggleDoor();
    }
}
