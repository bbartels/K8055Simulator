using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using K8055Simulator;

namespace K8055Simulator.Tests
{
    [TestClass]
    class K8055GeneralOperationsTests
    {
        [TestMethod]
        public void OpenDevice_ReturnsCardAddress_ValidCardAddress()
        {
            Assert.AreEqual(K8055Sim.OpenDevice(0, false), 0);
            K8055DllExport.CloseDevice();
            Assert.AreEqual(K8055Sim.OpenDevice(1, false), 1);
            K8055DllExport.CloseDevice();
            Assert.AreEqual(K8055Sim.OpenDevice(2, false), 2);
            K8055DllExport.CloseDevice();
            Assert.AreEqual(K8055Sim.OpenDevice(3, false), 3);
            K8055DllExport.CloseDevice();
        }

        [TestMethod]
        public void OpenDevice_FailsToOpen_InvalidCardAddress()
        {
            Assert.AreEqual(K8055Sim.OpenDevice(-1, false), -1);
            Assert.AreEqual(K8055Sim.OpenDevice(4, false), -1);
        }

        [TestMethod]
        public void CloseDevice_ClosesDevice_NormalBehaviour()
        {
            K8055Sim.OpenDevice(0, false);
            K8055DllExport.SetDigitalInputChannel(0, true);
            Assert.AreEqual(K8055DllExport.ReadDigitalChannel(0), true);
            K8055DllExport.CloseDevice();
            Assert.AreEqual(K8055DllExport.ReadDigitalChannel(0), false);
        }
    }
}
