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
    public class StationControl_UnitTest
    {
        private StationControl _uut;

        private IDoor _door;
        private IRfidReader _rfidReader;
        private IDisplay _display;
        private IChargeControl _charger;

        [SetUp]
        public void Setup()
        {
            _door = Substitute.For<IDoor>();
            _rfidReader = Substitute.For<IRfidReader>();
            _display = Substitute.For<IDisplay>();
            _charger = Substitute.For<IChargeControl>();

            _uut = new StationControl(_door, _rfidReader, _display, _charger);
        }
        //[TestCase(true)]
        [TestCase(false)]
        public void DoorStateChanged_Test(bool state)
        {
            _door.DoorStatusChanged += Raise.EventWith(new DoorStateChangedEventArgs() {IsOpen = state });
            Assert.That(_uut.DoorState, Is.EqualTo(state));
        }

       // [TestCase(1)]
        [TestCase(0)]
        public void ReadRFID_With_Arguments_test(int tag)
        {
            _rfidReader.KeySwiped += Raise.EventWith(new KeySwipedEventArgs {  Id= tag });
            Assert.That(_uut.ReadRFIDTag, Is.EqualTo(tag));
        }
        //[Test]
        //public void ReadRFID_LockDoorCalled_InAvailable()
        //{
        //    _charger.Connected().Returns(true);
        //    _rfidReader.KeySwiped += Raise.EventWith(new KeySwipedEventArgs() { Id = 32 });
        //    _door.Received(1).LockDoor();
        //}


    }
}
