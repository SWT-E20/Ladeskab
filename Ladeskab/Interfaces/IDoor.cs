using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab.Interfaces
{
    public interface IDoor
    {
        bool IsLocked { get; set; }

        void LockDoor();
        void UnlockDoor();
    }
}
