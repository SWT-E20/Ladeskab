﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeSkab
{
    public interface IRfidReader
    {
        event EventHandler<int> KeySwiped;
        void OnKeySwiped(int id);
    }
}
