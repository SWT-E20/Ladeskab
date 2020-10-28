using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using LadeSkab;
using NUnit.Framework;

namespace Ladeskab.Test.Unit
{
    [TestFixture]
    public class TestRfidReader
    {
        private RfidReader _uut;
        private KeySwipedEventArgs _onKeySwipe;

        [SetUp]
        public void SetUp()
        {
            _onKeySwipe = null;

            _uut = new RfidReader();

            _uut.KeySwipedEvent +=
                (o, args) => { _onKeySwipe = args; };
        }

        [Test]
        public void SetId_IdSetToNewValue_EventFired()
        {
            _uut.SetId(25);
            Assert.That(_onKeySwipe, Is.Not.Null);
        }

        [Test]
        public void SetId_IdSetToNewValue_CorrectValueReceived()
        {
            _uut.SetId(666);
            Assert.That(_onKeySwipe.Id, Is.EqualTo(666));
        }

    }


}

