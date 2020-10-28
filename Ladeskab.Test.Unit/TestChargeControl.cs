﻿using LadeSkab;
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

            _uut = new ChargeControl { Charger = _charger, Display = _display };
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void StartCharge_AllFunctionCallsReceived(int reqNumOfCalls) {
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
    }
}