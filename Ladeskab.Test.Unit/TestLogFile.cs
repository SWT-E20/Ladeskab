using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using LadeSkab;
using NSubstitute;
using System.IO;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;

namespace Ladeskab.Test.Unit
{
    [TestFixture]
    public class TestLogFile
    {
        private LogFile _uut;
        StringWriter stringResult;

        [SetUp]
        public void Setup()
        {
            stringResult = new StringWriter();
            Console.SetOut(stringResult);
            _uut = new LogFile("TestFile.txt");
        }


        [Test]
        public void ctor_Log_Created()
        {
            Assert.That(File.Exists(_uut.Path), Is.True);
        }

        [Test]
        public void TestLogContent()
        {
            _uut.Log("test");
            
            string[] lines = File.ReadAllLines(_uut.Path);
            foreach(string line in lines)
                Assert.That(line, Is.EqualTo("test"));
        }
    }
}
