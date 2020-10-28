using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeSkab
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // member variable
        private LadeskabState _state;

        private IChargeControl _charger;
        private IDisplay _display;
        private IDoor _door;
        private ILogFile _logfile;
        private IRfidReader _rfid;

        private int _oldId { get; set; }

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        // constructor
        public StationControl()
        {
            ChargeControl = new ChargeControl();
            Display = new Display();
            Door = new Door();
            Logfile = new LogFile(logFile);
            Rfid = new RfidReader();

            _state = LadeskabState.Available;
        }

        #region Door

        public IDoor Door
        {
            private get { return _door; }
            set
            {
                _door = value;
                _door.DoorStatusChanged += HandleDoorStatusEvent;
            }
        }

        private void HandleDoorStatusEvent(object sender, bool isOpen)
        {
            if (isOpen)
            {
                _state = LadeskabState.DoorOpen;
                if (!_charger.Connected())
                {
                    _display.Print("Connect phone");
                }
            }
            else
            {
                _state = LadeskabState.Available;
                if (_charger.Connected())
                {
                    _display.Print("Swipe key");
                }
            }
        }

        #endregion

        #region RFID
        public IRfidReader Rfid
        {
            set
            {
                _rfid = value;
                _rfid.KeySwipedEvent += HandleRfidDetectedEvent;
            }
        }
        private void HandleRfidDetectedEvent(object sender, KeySwipedEventArgs e)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected())
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = e.Id;
                        _logfile.Log($"{DateTime.Now.ToString()}: {e} locked the door.");

                        _display.Print("Door was locked using key.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        _display.Print("Connect your phone first!");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (e.Id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        _logfile.Log($"{DateTime.Now.ToString()}: {e} unlocked the door.");

                        _display.Print("Disconnect phone");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.Print($"{e} is an incorrect key.");
                    }

                    break;
            }
        }

        #endregion

        #region Charger
        public IChargeControl ChargeControl
        {
            private get { return _charger; }
            set
            {
                _charger = value;
            }
        }
        #endregion

        #region Display
        public IDisplay Display
        {
            private get { return _display; }
            set { _display = value; }
        }
        #endregion

        #region LogFile
        public ILogFile Logfile
        {
            private get { return _logfile; }
            set { _logfile = value; }
        }
        #endregion
        
    }
}
