using System;
using LadeSkab;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.Test.Unit
{
    [TestFixture]
    class TestChargeControl
    {
        private ChargeControl _uut;
        private IDisplay _display;
        private IUsbCharger _charger;

        [SetUp]
        public void Setup()
        {
            _display = Substitute.For<IDisplay>();
            _charger = Substitute.For<IUsbCharger>();

            _uut = new ChargeControl(_display, _charger);

            //Fake eventhandler
            //_uut.CurrentChangedEvent +=
            //    (
            //    );
        }

        [Test]
        public void ctor_hasDefaultValues()
        {
            Assert.That(_uut._state, Is.EqualTo(ChargeControl.ChargeControlState.NoConnection));
            Assert.That(_uut._prevState, Is.EqualTo(ChargeControl.ChargeControlState.Undefined));
        }

        [Test]
        public void HandleCurrentChangedEvent_SetPrevStateAndState_PrevStateAndStateIsEqual()
        {
            _uut._prevState = ChargeControl.ChargeControlState.NoConnection;
            _uut._state = ChargeControl.ChargeControlState.NoConnection;

            _charger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs());

            Assert.That(_uut._prevState, Is.EqualTo(_uut._state));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void StartCharge_AllFunctionCallsReceived(int reqNumOfCalls)
        {
            for (int i = 0; i < reqNumOfCalls; i++)
            {
                _uut.StartCharge();
            }

            _charger.Received(reqNumOfCalls).StartCharge();
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void StopCharge_AllFunctionCallsReceived(int reqNumOfCalls)
        {
            for (int i = 0; i < reqNumOfCalls; i++)
            {
                _uut.StopCharge();
            }

            _charger.Received(reqNumOfCalls).StopCharge();
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Connected_CorrectReturnBasedOnChargerMember(bool c)
        {
            _charger.Connected = c;

            Assert.That(_uut.Connected(), Is.EqualTo(c));
        }

        [TestCase(0, ChargeControl.ChargeControlState.NoConnection)]
        [TestCase(2, ChargeControl.ChargeControlState.FullCharge)]
        [TestCase(6, ChargeControl.ChargeControlState.Charging)]
        [TestCase(600, ChargeControl.ChargeControlState.Overload)]
        public void ChangedEvent_stateChangesCorrectly(double charge, ChargeControl.ChargeControlState resultState)
        {
            // raise event using test charge as argument:
            _charger.CurrentValueEvent += Raise.EventWith(
                new CurrentEventArgs()
                {
                    Current = charge
                });

            Assert.That(_uut._state, Is.EqualTo(resultState));
        }


        //Rettelse til genaflevering



        [TestCase(1, "Phone at 100% charge.")]
        [TestCase(5, "Phone at 100% charge.")]
        [TestCase(6, "Phone is charging...")]
        [TestCase(500, "Phone is charging...")]
        [TestCase(501, "Critical error while charging phone!")]
        public void HandleCurrentChangedEvent_ChargerCurrentChanged_PrintCorrectMessage(int _current, string _message)
        {
            _charger.CurrentValueEvent += Raise.EventWith(
                new CurrentEventArgs()
                {
                    Current = _current
                });

            _display.Received(1).Print(_message);
        }
        [Test]
        public void BehaviourTest_OverloadEventTriggerd()
        {
            _charger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = 510 });
            _charger.Received(1).StopCharge();
        }

        [Test]
        public void BehaviourTest_StopChargeCalled()
        {
            _uut.StopCharge();
            _charger.Received(1).StopCharge();
        }

        [Test]
        public void BehaviourTest_StartChargeCalled()
        {
            _uut.StartCharge();
            _charger.Received(1).StartCharge();
        }

    }
}
