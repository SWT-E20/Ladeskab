using System;
using System.Collections.Generic;
using System.IO;
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
            var sw = new StringWriter();
            Console.SetOut(sw);

            _uut.Print("Hello world");

            string output = sw.ToString();

            Assert.That(output, Is.EqualTo("DISPLAY: Hello world\r\n"));
        }
    }
}
