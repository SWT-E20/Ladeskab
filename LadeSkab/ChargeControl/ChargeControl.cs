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
        private ChargeControlState _prevState;

        private IDisplay _display;
        private IUsbCharger _charger;

        public ChargeControl()
        {
            _state = ChargeControlState.NoConnection;
            _prevState = ChargeControlState.Undefined;

            Display = new Display();
            Charger = new UsbCharger();
        }

        public void StartCharge()
        {
            Charger.StartCharge();
        }

        public void StopCharge()
        {
            Charger.StopCharge();
        }

        public bool Connected()
        {
            return Charger.Connected;
        }

        #region Charger
        public IUsbCharger Charger
        {
            get { return _charger; }
            set
            {
                _charger = value;
                _charger.CurrentValueEvent += HandleCurrentChangedEvent;
            }
        }
        #endregion

        #region Display
        public IDisplay Display
        {
            get { return _display; }
            set { 
                _display = value;
            }
        }
        #endregion

        void HandleCurrentChangedEvent(object sender, CurrentEventArgs e)
        {
            if (_prevState == _state) return;

            if(e.Current == 0)
            {
                _state = ChargeControlState.NoConnection;
            }
            else if (0 < e.Current && e.Current <= 5)
            {
                Display.Print("Phone at 100% charge.");
                _state = ChargeControlState.FullCharge;
            }
            else if (5 < e.Current && e.Current <= 500)
            {
                Display.Print("Phone is charging...");
                _state = ChargeControlState.Charging;
            }
            else if (500 < e.Current)
            {
                Display.Print("Critical error while charging phone!");
                _state = ChargeControlState.Overload;
                StopCharge();
            }

            _prevState = _state;
        }
    }
}
