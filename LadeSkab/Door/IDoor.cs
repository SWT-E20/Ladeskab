using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeSkab
{
    public interface IDoor
    {
        event EventHandler<bool> DoorStatusChanged;
        bool LockDoor();
        bool UnlockDoor();
        bool OnToggleDoor();
    }
}
