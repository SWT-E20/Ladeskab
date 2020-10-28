using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeSkab
{
    public class KeySwipedEventArgs : EventArgs
    {
        public int Id { set; get; }
    }
    public interface IRfidReader
    {
        event EventHandler<KeySwipedEventArgs> KeySwiped;
        void OnKeySwiped(int id);
    }
}
