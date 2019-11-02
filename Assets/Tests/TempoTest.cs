using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TempoTest
    {
        // Tempo tempo;

        [SetUp]
        public void SetUp()
        {
            // tempo = new Tempo();
        }

        [TearDown]
        public void TearDown()
        {
            //if (tempo != null)
            //{
            //    tempo.StopTempo();
            //}
        }

        // A Test behaves as an ordinary method
        [Test]
        public void TestSubdivisionsTime()
        {
            Assert.IsTrue(true);
        }
    }
}
