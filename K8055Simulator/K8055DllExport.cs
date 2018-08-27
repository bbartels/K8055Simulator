/*
 * This file is licensed under the MIT License.
 * Check the LICENSE file in the projects root for more information.
 */
using System.Runtime.InteropServices;

namespace K8055Simulator
{
    public static class K8055DllExport
    {
        #region K8055Simulator Operations

        [DllExport]
        public static int OpenDevice(int CardAdress)
        {
            return K8055Sim.OpenDevice(CardAdress, true);
        }

        [DllExport]
        public static void CloseDevice()
        {
            K8055Sim.CloseDevice(true);
        }

        [DllExport]
        public static int ReadAnalogChannel(int Channel)
        {
            return K8055Sim.ReadAnalogChannel(Channel);
        }

        public static void ReadAllAnalog(ref int Data1, ref int Data2)
        {
            K8055Sim.ReadAllAnalog(ref Data1, ref Data2);
        }

        [DllExport]
        public static void ClearAnalogChannel(int Channel)
        {
            K8055Sim.ClearAnalogChannel(Channel);
        }

        [DllExport]
        public static void ClearAllAnalog()
        {
            K8055Sim.ClearAllAnalog();
        }

        [DllExport]
        public static void OutputAnalogChannel(int Channel, int Data)
        {
            K8055Sim.OutputAnalogChannel(Channel, Data);
        }

        [DllExport]
        public static void OutputAllAnalog(int Data1, int Data2)
        {
            K8055Sim.OutputAllAnalog(Data1, Data2);
        }

        [DllExport]
        public static void SetAnalogChannel(int Channel)
        {
            K8055Sim.SetAnalogChannel(Channel);
        }

        [DllExport]
        public static void SetAllAnalog()
        {
            K8055Sim.SetAllAnalog();
        }

        [DllExport]
        public static void ClearAllDigital()
        {
            K8055Sim.ClearAllDigital();
        }

        [DllExport]
        public static void ClearDigitalChannel(int Channel)
        {
            K8055Sim.ClearDigitalChannel(Channel);
        }

        [DllExport]
        public static void SetAllDigital()
        {
            K8055Sim.SetAllDigital();
        }

        [DllExport]
        public static void SetDigitalChannel(int Channel)
        {
            K8055Sim.SetDigitalChannel(Channel);
        }

        [DllExport]
        public static void WriteAllDigital(int Data)
        {
            K8055Sim.WriteAllDigital(Data);
        }

        [DllExport]
        public static bool ReadDigitalChannel(int Channel)
        {
            return K8055Sim.ReadDigitalChannel(Channel);
        }

        [DllExport]
        public static int ReadAllDigital()
        {
            return K8055Sim.ReadAllDigital();
        }

        [DllExport]
        public static int ReadCounter(int CounterNr)
        {
            return K8055Sim.ReadCounter(CounterNr);
        }

        [DllExport]
        public static void ResetCounter(int CounterNr)
        {
            K8055Sim.ResetCounter(CounterNr);
        }

        [DllExport]
        public static void SetCounterDebounceTime(int CounterNr, int DebounceTime)
        {
            K8055Sim.SetCounterDebounceTime(CounterNr, DebounceTime);
        }

        #endregion

        //The following operations are not available in the actual K8055, though can be used for simulation purposes.
        #region K8055SimulationOperations

        [DllExport]
        public static void SetDigitalInputChannel(int Channel, bool Status)
        {
            K8055Sim.SetDigitalInputChannel(Channel, Status);
        }

        [DllExport]
        public static bool ReadDigitalOutputChannel(int Channel)
        {
            return K8055Sim.ReadDigitalOutputChannel(Channel);
        }

        [DllExport]
        public static void SetAnalogInputChannel(int Channel, int Data)
        {
            K8055Sim.SetAnalogInputChannel(Channel, Data);
        }

        [DllExport]
        public static int ReadAnalogOutputChannel(int Channel)
        {
            return K8055Sim.ReadAnalogOutputChannel(Channel);
        }

        #endregion
    }
}
