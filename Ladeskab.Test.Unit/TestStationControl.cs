using System;
using System.Collections.Generic;
using System.Text;
using LadeSkab;
using NSubstitute;
using NUnit.Framework;
//using static LadeSkab.Door;

namespace Ladeskab.Test.Unit
{
    [TestFixture]
    public class TestStationControl
    {
        private StationControl _uut;

        private IDoor _door;
        private IRfidReader _rfidReader;
        private IDisplay _display;
        private IChargeControl _charger;
        private ILogFile _logfile;

        [SetUp]
        public void Setup()
        {
            _door = Substitute.For<IDoor>();
            _rfidReader = Substitute.For<IRfidReader>();
            _display = Substitute.For<IDisplay>();
            _charger = Substitute.For<IChargeControl>();
            _logfile = Substitute.For<ILogFile>();

            _uut = new StationControl(_door, _rfidReader, _display, _charger, _logfile);
        }
        [TestCase(true)]
        [TestCase(false)]
        public void DoorStateChanged_Test(bool state)
        {
            _door.DoorStatusChanged += Raise.EventWith(new DoorStateChangedEventArgs() {IsOpen = state });
            Assert.That(_uut.DoorState, Is.EqualTo(state));
        }
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(-2)]
        public void ReadRFID_With_Arguments_test(int tag)
        {
            _rfidReader.KeySwiped += Raise.EventWith(new KeySwipedEventArgs {  Id= tag });
            Assert.That(_uut.ReadRFIDTag, Is.EqualTo(tag));
        }
        [Test]
        public void ReadRFID_Display_Open()
        {
            _door.OnToggleDoor().Returns(true);
            _rfidReader.KeySwiped += Raise.EventWith(new KeySwipedEventArgs() { Id = 25 });
            _display.Received(1).Print("Please close door before swiping!");
        }
        [Test]
        public void HandleDoorStatusEvent_DoorClosedAndChargerConnected_PrintCorrectMessage()
        {
            _charger.Connected().Returns(true);
            _door.DoorStatusChanged += Raise.EventWith(new DoorStateChangedEventArgs {IsOpen = false});
            _display.Received(1).Print("Swipe key");
        }

        [Test]
        public void ReadRFID_LockDoorStatus()
        {
            _charger.Connected().Returns(true);
            _rfidReader.KeySwiped += Raise.EventWith(new KeySwipedEventArgs() { Id = 25 });
            _door.Received(1).LockDoor();
        }
        [Test]
        public void ReadRFID_Startcharg()
        {
            _charger.Connected().Returns(true);
            _rfidReader.KeySwiped += Raise.EventWith(new KeySwipedEventArgs() { Id = 25 });
            _charger.Received(1).StartCharge();
        }

        [Test]
        public void ReadRFID_Display()
        {
            _charger.Connected().Returns(true);
            _rfidReader.KeySwiped += Raise.EventWith(new KeySwipedEventArgs() { Id = 25 });
            _display.Received().Print("Door was locked using key.");
        }
      
        [Test]
        public void ReadRFID_Stopcharging()
        {
            _charger.Connected().Returns(true);
            _rfidReader.KeySwiped += Raise.EventWith(new KeySwipedEventArgs() { Id = 25 });
            _rfidReader.KeySwiped += Raise.EventWith(new KeySwipedEventArgs() { Id = 25 });
            _charger.Received(1).StopCharge();
        }
        [Test]
        public void ReadRFID_unlockdoor()
        {
            _charger.Connected().Returns(true);
            _rfidReader.KeySwiped += Raise.EventWith(new KeySwipedEventArgs() { Id = 25 });
            _rfidReader.KeySwiped += Raise.EventWith(new KeySwipedEventArgs() { Id = 25 });
            _door.Received().UnlockDoor();
        }
        [Test]
        public void ReadRFID_Wrong_ID()
        {
            _charger.Connected().Returns(true);
            _rfidReader.KeySwiped += Raise.EventWith(new KeySwipedEventArgs() { Id = 25 });
            _rfidReader.KeySwiped += Raise.EventWith(new KeySwipedEventArgs() { Id = 43 });
            _display.Received(1).Print(" is an incorrect key.");
        }
    }
}
