using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using LadeSkab;
using NSubstitute;
using System.IO;
using Microsoft.VisualBasic;

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
            if (!File.Exists(_uut.Path))
            {
                string text = File.ReadAllText(_uut.Path);
                Console.WriteLine("Content: {0}", text);
            }
           
        }

        //[Test]
        //public void Log_Test_Val()
        //{
        //    var sw = new StringWriter();
        //    Console.SetOut(sw);
        //    _uut.Log("right");
        //    string output = sw.ToString();

        //    Assert.That(output, Is.EqualTo("right\r\n"));

        //}
    }
}
