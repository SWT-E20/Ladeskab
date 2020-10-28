using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeSkab
{

    public class KeySwipedEventArgs : EventArgs
    {
        public int Id { get; set; }
    }
    public interface IRfidReader
    {
        event EventHandler<KeySwipedEventArgs> KeySwipedEvent;
        void OnKeySwiped(KeySwipedEventArgs id);
        void OnKeySwiped(int id);
    }
}
