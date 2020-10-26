using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            Assert.That(_uut.IsOpen, Is.True);
            Assert.That(_uut.IsLocked, Is.False);
        }

        [Test]
        public void LockDoor_ReturnCorrectAndAlterState()
        {
            // unlocked and closed
            _uut.IsLocked = false; _uut.IsOpen = false;
            Assert.That(_uut.LockDoor(), Is.True);
            Assert.That(_uut.IsLocked, Is.True); // memeber changes to locked

            // unlocked and open
            _uut.IsLocked = false; _uut.IsOpen = true;
            Assert.That(_uut.LockDoor(), Is.False);

            // locked and closed
            _uut.IsLocked = true; _uut.IsOpen = false;
            Assert.That(_uut.LockDoor(), Is.False);

            // locked and open
            _uut.IsLocked = true; _uut.IsOpen = true;
            Assert.That(_uut.LockDoor(), Is.False);
        }

        [Test]
        public void UnlockDoor_ReturnCorrectAndAlterState()
        {
            // unlocked and closed
            _uut.IsLocked = false; _uut.IsOpen = false;
            Assert.That(_uut.UnlockDoor(), Is.False);

            // unlocked and open
            _uut.IsLocked = false; _uut.IsOpen = true;
            Assert.That(_uut.UnlockDoor(), Is.False);

            // locked and closed
            _uut.IsLocked = true; _uut.IsOpen = false;
            Assert.That(_uut.UnlockDoor(), Is.True);
            Assert.That(_uut.IsLocked, Is.False); // member changes to unlocked

            // locked and open
            _uut.IsLocked = true; _uut.IsOpen = true;
            Assert.That(_uut.UnlockDoor(), Is.False);
        }

        [Test]
        public void OnToggleDoor_ReturnCorrectAndAlterState()
        {
            _uut.IsOpen = true; _uut.IsLocked = false;

            // toggle door when open and unlocked
            Assert.That(_uut.OnToggleDoor(), Is.True);
            Assert.That(_uut.IsOpen, Is.False);
            Assert.That(_uut.OnToggleDoor(), Is.True);
            Assert.That(_uut.IsOpen, Is.True);

            // toggle door when closed and locked
            _uut.OnToggleDoor(); // close door
            _uut.IsLocked = true; // lock it

            Assert.That(_uut.OnToggleDoor(), Is.False); // try to open
            Assert.That(_uut.IsOpen, Is.False);
        }
    }
}
