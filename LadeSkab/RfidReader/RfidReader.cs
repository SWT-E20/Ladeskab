using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeSkab
{
    public class RfidReader : IRfidReader
    {
        public event EventHandler<int> KeySwiped;

        public void OnKeySwiped(int id)
        {
            KeySwiped?.Invoke(this, id);
        }
    }
}
