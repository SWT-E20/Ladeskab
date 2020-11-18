using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeSkab
{
    public class ChargeControl : IChargeControl
    {
        public enum ChargeControlState
        {
            Undefined,
            NoConnection,
            FullCharge,
            Charging,
            Overload
        };

        public ChargeControlState _state;
        public ChargeControlState _prevState;

        private IDisplay _display;
        private IUsbCharger _charger;

        public ChargeControl(IDisplay display, IUsbCharger charger)
        {
            _state = ChargeControlState.NoConnection;
            _prevState = ChargeControlState.Undefined;

            _display = display;
            _charger = charger;

            _charger.CurrentValueEvent += HandleCurrentChangedEvent;
        }

        public void StartCharge()
        {
            _charger.StartCharge();
        }

        public void StopCharge()
        {
            _charger.StopCharge();
        }

        public bool Connected()
        {
            return _charger.Connected;
        }

        //Event handler til genaflevering
        public event EventHandler<CurrentEventArgs> CurrentChangedEvent;
        protected virtual void OnCurrentChangedEvent(CurrentEventArgs e)
        {
            CurrentChangedEvent?.Invoke(this, e);
        }



        void HandleCurrentChangedEvent(object sender, CurrentEventArgs e)
        {
            if (_prevState == _state) return;

            if(e.Current == 0)
            {
                _state = ChargeControlState.NoConnection;
            }
            else if (0 < e.Current && e.Current <= 5)
            {
                _display.Print("Phone at 100% charge.");
                _state = ChargeControlState.FullCharge;
            }
            else if (5 < e.Current && e.Current <= 500)
            {
                _display.Print("Phone is charging...");
                _state = ChargeControlState.Charging;
            }
            else if (500 < e.Current)
            {
                
                _display.Print("Critical error while charging phone!");
                _state = ChargeControlState.Overload;
                StopCharge();
            }

            _prevState = _state;
        }

    }
}
