using System;
using System.Collections.Generic;
using System.Text;
using LadeSkab;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using NUnit.Framework;

namespace Ladeskab.Test.Unit
{
    [TestFixture]
    public class TestDisplay
    {
        private Display _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new Display();
        }

        [Test]
        public void Print_TestInputValid()
        {
            
        }
    }
}
