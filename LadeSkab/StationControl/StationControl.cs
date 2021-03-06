﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LadeSkab.Door;

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
        public bool DoorState { get; set; }
        public int ReadRFIDTag { get; set; }
        private LadeskabState _state;

        private IChargeControl _charger;
        private IDisplay _display;
        private IDoor _door;
        private ILogFile _logfile;
        private IRfidReader _rfid;

        private int _oldId { get; set; }

        // constructor
        public StationControl(IDoor door, IRfidReader rfidReader, IDisplay display, IChargeControl charger, ILogFile logfile)
        {
            _door = door;
            _display = display;
            _charger = charger;
            _rfid = rfidReader;
            _logfile = logfile;

            _door.DoorStatusChanged += HandleDoorStatusEvent;
            _rfid.KeySwiped += HandleRfidDetectedEvent;

            _state = LadeskabState.Available;
        }

        #region Door

        private void HandleDoorStatusEvent(object sender, DoorStateChangedEventArgs e)
        {
            DoorState = e.IsOpen;
            if (DoorState)
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

        private void HandleRfidDetectedEvent(object sender, KeySwipedEventArgs e)
        {
            ReadRFIDTag = e.Id;
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected())
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = ReadRFIDTag;
                        _logfile.Log($"{DateTime.Now.ToString()}: {ReadRFIDTag} locked the door.");

                        _display.Print("Door was locked using key.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        _display.Print("Connect your phone first!");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    _display.Print("Please close door before swiping!");
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (ReadRFIDTag == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        _logfile.Log($"{DateTime.Now.ToString()}: {ReadRFIDTag} unlocked the door.");

                        _display.Print("Disconnect phone");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.Print(" is an incorrect key.");
                    }

                    break;
            }
        }

        #endregion  
    }
}
