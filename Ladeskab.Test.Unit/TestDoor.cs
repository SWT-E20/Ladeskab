﻿using LadeSkab;
using NUnit.Framework;

namespace Ladeskab.Test.Unit
{
    [TestFixture]
    public class TestDoor
    {
        private Door _uut;
        [SetUp]
        public void Setup()
        {
            _uut = new Door();
        }

        [Test]
        public void ctor_hasDefaultValues()
        {
            Assert.That(_uut.IsOpen, Is.False);
            Assert.That(_uut.IsLocked, Is.False);
        }

        [Test]
        public void IsOpen_UseGet_GetExpectedResult()
        {
            var _open = _uut.IsOpen;
            Assert.That(_open, Is.False);
        }

        [Test]
        public void OnDoorStatusChanged_GetStatus_IsOpenIsAsExpected()
        {
            bool _currentDoorStatus = false;
            _uut.DoorStatusChanged += (o, args) =>
            {
                _currentDoorStatus = args.IsOpen;
            };

            _uut.ToggleDoor();

            Assert.That(_currentDoorStatus, Is.EqualTo(true));
        }

        [TestCase(false, false, true, true)] // success: unlocked and closed
        [TestCase(false, true, false, false)] // fail: unlocked and open
        [TestCase(true, false, true, false)] // fail: locked and closed
        [TestCase(true, true, true, false)] // fail: locked and open
        public void LockDoor_ReturnCorrectAndAlterState(bool lockState, bool openState, bool lockStateResult, bool returnResult)
        {
            _uut.IsLocked = lockState; _uut.IsOpen = openState;

            Assert.That(_uut.LockDoor(), Is.EqualTo(returnResult));
            Assert.That(_uut.IsLocked, Is.EqualTo(lockStateResult)); // memeber changes to locked
        }

        [TestCase(false, false, false, false)] // fail: unlocked and closed
        [TestCase(false, true, false, false)] // fail: unlocked and open
        [TestCase(true, false, false, true)] // success: locked and closed
        [TestCase(true, true, true, false)] // fail: locked and open
        public void UnlockDoor_ReturnCorrectAndAlterState(bool lockState, bool openState, bool lockStateResult, bool returnResult)
        {
            _uut.IsLocked = lockState; _uut.IsOpen = openState;

            Assert.That(_uut.UnlockDoor(), Is.EqualTo(returnResult));
            Assert.That(_uut.IsLocked, Is.EqualTo(lockStateResult)); // memeber changes to locked
        }

        [Test]
        public void ToggleDoor_ReturnCorrectAndAlterState()
        {
            _uut.IsOpen = true; _uut.IsLocked = false;

            // toggle door when open and unlocked
            Assert.That(_uut.ToggleDoor(), Is.True);
            Assert.That(_uut.IsOpen, Is.False);
            Assert.That(_uut.ToggleDoor(), Is.True);
            Assert.That(_uut.IsOpen, Is.True);

            // toggle door when closed and locked
            _uut.ToggleDoor(); // close door
            _uut.IsLocked = true; // lock it

            Assert.That(_uut.ToggleDoor(), Is.False); // try to open
            Assert.That(_uut.IsOpen, Is.False);
        }
    }
}
