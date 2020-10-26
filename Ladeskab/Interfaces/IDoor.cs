using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab.Interfaces
{
    public interface IDoor
    {
        bool LockDoor();
        bool UnlockDoor();
        bool OnToggleDoor();
    }
}
