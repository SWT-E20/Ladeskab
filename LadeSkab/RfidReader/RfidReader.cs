using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeSkab
{
    public class RfidReader : IRfidReader
    {

        public event EventHandler<KeySwipedEventArgs> KeySwipedEvent;

        private int _oldId;
        public void SetId(int newId)
        {
            if (newId != _oldId)
            {
                OnKeySwiped(new KeySwipedEventArgs() { Id = newId });
                _oldId = newId;
            }
        }

        public void OnKeySwiped(KeySwipedEventArgs e)
        {
            KeySwipedEvent?.Invoke(this, e);
        }

        public void OnKeySwiped(int id)
        {
            throw new NotImplementedException();
        }
    }
}
