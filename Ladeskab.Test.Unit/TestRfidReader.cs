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
        private int _testId;

        [SetUp]
        public void SetUp()
        {
            _uut = new RfidReader();
        }

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public void OnKeySwiped_InputID_IDIsSet(int id, int result)
        {
            _uut.KeySwiped += (o, args) =>
            {
                _testId = args.Id;
            };

            _uut.OnKeySwiped(id);
            Assert.That(_testId, Is.EqualTo(result));
        }

        

    }


}

