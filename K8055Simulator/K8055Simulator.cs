/*
 * This file is licensed under the MIT License.
 * Check the LICENSE file in the projects root for more information.
 */
using System;
using System.Runtime.InteropServices;
using RGiesecke.DllExport;

namespace K8055Simulator
{
    public static class K8055DllExport
    {
        #region K8055Simulator Operations

        [DllExport("OpenDevice", CallingConvention = CallingConvention.StdCall)]
        public static int OpenDevice(int CardAdress)
        {
            return K8055Sim.OpenDevice(CardAdress);
        }

        [DllExport("CloseDevice", CallingConvention = CallingConvention.StdCall)]
        public static void CloseDevice()
        {
            K8055Sim.CloseDevice(true);
        }

        [DllExport("ReadAnalogChannel", CallingConvention = CallingConvention.StdCall)]
        public static int ReadAnalogChannel(int Channel)
        {
            return K8055Sim.ReadAnalogChannel(Channel);
        }

        [DllExport("ReadAllAnalog", CallingConvention = CallingConvention.StdCall)]
        public static void ReadAllAnalog(ref int Data1, ref int Data2)
        {
            K8055Sim.ReadAllAnalog(ref Data1, ref Data2);
        }


        [DllExport("ClearAnalogChannel", CallingConvention = CallingConvention.StdCall)]
        public static void ClearAnalogChannel(int Channel)
        {
            K8055Sim.ClearAnalogChannel(Channel);
        }

        [DllExport("ClearAllAnalog", CallingConvention = CallingConvention.StdCall)]
        public static void ClearAllAnalog()
        {
            K8055Sim.ClearAllAnalog();
        }

        [DllExport("OutputAnalogChannel", CallingConvention = CallingConvention.StdCall)]
        public static void OutputAnalogChannel(int Channel, int Data)
        {
            K8055Sim.OutputAnalogChannel(Channel, Data);
        }

        [DllExport("OutputAllAnalog", CallingConvention = CallingConvention.StdCall)]
        public static void OutputAllAnalog(int Data1, int Data2)
        {
            K8055Sim.OutputAllAnalog(Data1, Data2);
        }

        [DllExport("SetAnalogChannel", CallingConvention = CallingConvention.StdCall)]
        public static void SetAnalogChannel(int Channel)
        {
            K8055Sim.SetAnalogChannel(Channel);
        }

        [DllExport("SetAllAnalog", CallingConvention = CallingConvention.StdCall)]
        public static void SetAllAnalog()
        {
            K8055Sim.SetAllAnalog();
        }

        [DllExport("ClearAllDigital", CallingConvention = CallingConvention.StdCall)]
        public static void ClearAllDigital()
        {
            K8055Sim.ClearAllDigital();
        }

        [DllExport("ClearDigitalChannel", CallingConvention = CallingConvention.StdCall)]
        public static void ClearDigitalChannel(int Channel)
        {
            K8055Sim.ClearDigitalChannel(Channel);
        }

        [DllExport("SetAllDigital", CallingConvention = CallingConvention.StdCall)]
        public static void SetAllDigital()
        {
            K8055Sim.SetAllDigital();
        }

        [DllExport("SetDigitalChannel", CallingConvention = CallingConvention.StdCall)]
        public static void SetDigitalChannel(int Channel)
        {
            K8055Sim.SetDigitalChannel(Channel);
        }

        [DllExport("WriteAllDigital", CallingConvention = CallingConvention.StdCall)]
        public static void WriteAllDigital(int Data)
        {
            K8055Sim.WriteAllDigital(Data);
        }

        [DllExport("ReadDigitalChannel", CallingConvention = CallingConvention.StdCall)]
        public static bool ReadDigitalChannel(int Channel)
        {
            return K8055Sim.ReadDigitalChannel(Channel);
        }

        [DllExport("ReadAllDigital", CallingConvention = CallingConvention.StdCall)]
        public static int ReadAllDigital()
        {
            return K8055Sim.ReadAllDigital();
        }

        [DllExport("ReadCounter", CallingConvention = CallingConvention.StdCall)]
        public static int ReadCounter(int CounterNr)
        {
            return K8055Sim.ReadCounter(CounterNr);
        }

        [DllExport("ResetCounter", CallingConvention = CallingConvention.StdCall)]
        public static void ResetCounter(int CounterNr)
        {
            K8055Sim.ResetCounter(CounterNr);
        }

        [DllExport("SetCounterDebounceTime", CallingConvention = CallingConvention.StdCall)]
        public static void SetCounterDebounceTime(int CounterNr, int DebounceTime)
        {
            K8055Sim.SetCounterDebounceTime(CounterNr, DebounceTime);
        }

        #endregion

        //The operations below are not available in the actual K8055Simulator, though can be used for simulation purposes
        #region K8055SimulationOperations

        [DllExport("SetDigitalInputChannel", CallingConvention = CallingConvention.StdCall)]
        public static void SetDigitalInputChannel(int Channel, bool Status)
        {
            K8055Sim.SetDigitalInputChannel(Channel, Status);
        }

        [DllExport("ReadDigitalOutputChannel", CallingConvention = CallingConvention.StdCall)]
        public static bool ReadDigitalOutputChannel(int Channel)
        {
            return K8055Sim.ReadDigitalOutputChannel(Channel);
        }

        [DllExport("SetAnalogInputChannel", CallingConvention = CallingConvention.StdCall)]
        public static void SetAnalogInputChannel(int Channel, int Data)
        {
            K8055Sim.SetAnalogInputChannel(Channel, Data);
        }

        [DllExport("ReadAnalogOutputChannel", CallingConvention = CallingConvention.StdCall)]
        public static int ReadAnalogOutputChannel(int Channel)
        {
            return K8055Sim.ReadAnalogOutputChannel(Channel);
        }

        #endregion
    }

    public static class K8055Sim
    {
        private static K8055Window _window;
        private static K8055D _k8055D;
        private static readonly K8055D[] Cards = { new K8055D(), new K8055D(), new K8055D(), new K8055D() };

        #region CommunicationOperations

        /// <summary>
        /// Opens communication with the selected K8055D card.
        /// </summary>
        /// <param name="cardAddress">
        /// Value between 0 and 3 to select the K8055D card to be connected.
        /// This is used to distinguish multiple K8055D cards. 
        /// </param>
        /// <returns>If the communication succeeds the passed cardAddress will be returned, otherwise -1.</returns>
        public static int OpenDevice(int cardAddress)
        {
            if (cardAddress < 0 || 3 < cardAddress) return -1;
            _window?.Close();
            _k8055D = Cards[cardAddress];
            _k8055D.Connected = true;

            _window = new K8055Window(_k8055D);
            _window.Show();

            return cardAddress;
        }

        /// <summary>
        /// Closes communication with the K8055D card.
        /// </summary>
        public static void CloseDevice(bool closeWindow)
        {
            if(closeWindow) _window?.Close();
            if (_k8055D == null) return;
            _k8055D.Connected = false;
            _k8055D = null;
        }

        #endregion

        #region AnalogInputChannelOperations

        /// <summary>
        /// The input voltage of the selected ADC channel is returned as linearly distributed values between (0 - 255).
        /// </summary>
        /// <param name="channel">The input channel of which the voltage should be returned.</param>
        /// <returns>A linearly distributed value between (0 - 255) (0V - 5V) to represent the voltage of the selected channel.</returns>
        public static int ReadAnalogChannel(int channel)
        {
            if (_k8055D == null || !_k8055D.Connected || channel < 1 || channel > 2)
            {
                throw new ArgumentException("Communication with K8055 was either already closed or channel number is invalid!");
            }

            double voltage = channel == 1 ? _k8055D.AnalogInputChannel[0] : _k8055D.AnalogInputChannel[1];

            return (int)(voltage / 5 * 255);
        }

        /// <summary>
        /// Reads both analog channels and stores the values in the respective pointer which were passed as Arguments.
        /// </summary>
        /// <param name="data1">Int pointer for value of analog channel 1.</param>
        /// <param name="data2">Int pointer for value of analog channel 2.</param>
        public static void ReadAllAnalog(ref int data1, ref int data2)
        {
            data1 = ReadAnalogChannel(0);
            data2 = ReadAnalogChannel(1);
        }

        #endregion

        #region AnalogOutputChannelOperations
        /// <summary>
        /// The selected analog output will be set to 0V.
        /// </summary>
        /// <param name="channel">Value between 1 and 2 which correspond to the analog channels.</param>
        public static void ClearAnalogChannel(int channel)
        {
            OutputAnalogChannel(channel, 0);
        }

        /// <summary>
        /// All analog outputs get set to 0V.
        /// </summary>
        public static void ClearAllAnalog()
        {
            OutputAllAnalog(0, 0);
        }

        /// <summary>
        /// The analog output will be set to the passed voltage in data.
        /// The value 0 represents the minial output voltage of 0V.
        /// The value 255 represents the maximal output voltage of 5V.
        /// </summary>
        /// <param name="channel">The analog output which should be set to passed voltage.</param>
        /// <param name="data">A value between 0 and 255 which maps to the output voltage (0 - 255).</param>
        public static void OutputAnalogChannel(int channel, int data)
        {
            if (_k8055D == null || !_k8055D.Connected || data < 0 || data > 255) return;
            if (channel < 1 || channel > 2) return;
            _k8055D.AnalogOutputChannel[channel - 1] = (double)data / 255 * 5;
        }

        /// <summary>
        /// Both of the analog outputs are set to the values in data1 and data2.
        /// The value 0 represents the minial output voltage of 0V.
        /// The value 255 represents the maximal output voltage of 5V.
        /// </summary>
        /// <param name="data1">A value between 0 and 255 which maps to the output voltage in channel0 (0 - 255).</param>
        /// <param name="data2">A value between 0 and 255 which maps to the output voltage in channel0 (0 - 255).</param>
        public static void OutputAllAnalog(int data1, int data2)
        {
            if (_k8055D == null || !_k8055D.Connected || data1 < 0 || 255 < data1 || 
                                     data2 < 0 || 255 < data2) return;

            _k8055D.AnalogOutputChannel[0] = (double)data1 / 255 * 5;
            _k8055D.AnalogOutputChannel[1] = (double)data2 / 255 * 5;
        }

        /// <summary>
        /// The selected analog output channel will be set to the maxmimum output voltage of 5V.
        /// </summary>
        /// <param name="channel">The channel which should be set to the max. voltage.</param>
        public static void SetAnalogChannel(int channel)
        {
            OutputAnalogChannel(channel, 255);
        }

        /// <summary>
        /// All analog outputs are set to the maxmimum output voltage of 5V.
        /// </summary>
        public static void SetAllAnalog()
        {
            OutputAllAnalog(255, 255);
        }

        #endregion

        #region DigitalOutputChannelOperations

        /// <summary>
        /// All digital outputs are set to 'OFF'.
        /// </summary>
        public static void ClearAllDigital()
        {
            if (_k8055D == null || !_k8055D.Connected) return;
            for (int i = 0; i < 8; i++)
            {
                _k8055D.DigitalOutputChannel[i] = false;
            }
        }

        /// <summary>
        /// The selected digial output is set to 'OFF'.
        /// </summary>
        /// <param name="channel">The digital channel which should be set to OFF.</param>
        public static void ClearDigitalChannel(int channel)
        {
            if (_k8055D == null || !_k8055D.Connected || channel < 1 || 8 < channel) return;
            _k8055D.DigitalOutputChannel[channel - 1] = false;
        }

        /// <summary>
        /// All digital outputs are set to 'ON'.
        /// </summary>
        public static void SetAllDigital()
        {
            if (_k8055D == null || !_k8055D.Connected) return;
            for (int i = 1; i < 9; i++)
            {
                _k8055D.DigitalOutputChannel[i] = true;
            }
        }

        /// <summary>
        /// The selcted digital output channel is set to 'ON'.
        /// </summary>
        /// <param name="channel">The digital channel which should be set to 'ON'</param>
        public static void SetDigitalChannel(int channel)
        {
            if (_k8055D == null || !_k8055D.Connected || channel < 1 || 8 < channel) return;
            _k8055D.DigitalOutputChannel[channel - 1] = true;
        }
        
        /// <summary>
        /// The first 8 bits starting from the least significant bit are used to toggle the digital outputs according to their value.
        /// i.e. 00000000000000000000000010101010 sets dig. outputs 2, 4, 6 and 8 to 'ON' and 1, 3, 5, 7 to 'OFF'.
        /// </summary>
        /// <param name="data">The Integer which should be used to toggle the digital outputs.</param>
        public static void WriteAllDigital(int data)
        {
            if (_k8055D == null || !_k8055D.Connected || data < 0 || data > 255) return;

            for (int i = 0; i < 8; i++)
            {
                _k8055D.DigitalOutputChannel[i] = (data & 1) == 1;
                data >>= 1;
            }
        }
        #endregion

        #region DigitalInputChannelOperations
        /// <summary>
        /// The state of the selected digital channel is read. 
        /// </summary>
        /// <param name="channel">Value between 1 and 5 whcih corresponds to the digital input to be read.</param>
        /// <returns>Whether the digital is 'ON'(true) or 'OFF'(false).</returns>
        public static bool ReadDigitalChannel(int channel)
        {
            if (_k8055D == null || !_k8055D.Connected || channel < 1 || channel > 5) return false;
            return _k8055D.DigitalInputChannel[channel - 1];
        }

        /// <summary>
        /// Reads and returns the state of all digital inputs.
        /// </summary>
        /// <returns>The first 5 bits starting from the least significant bit correspond to the state of the input channels</returns>
        public static int ReadAllDigital()
        {
            if (_k8055D == null || !_k8055D.Connected) return -1;
            int counter = 0;
            for (int i = 0; i < 5; i++)
            {
                counter += _k8055D.DigitalInputChannel[i] ? Convert.ToInt32(1 * Math.Pow(2, i)) : 0;
            }
            return counter;
        }
        #endregion

        #region CounterOperations

        /// <summary>
        /// Reads and returns the state of the selected counter.
        /// </summary>
        /// <param name="counterNr">The counter whose value should be read.</param>
        /// <returns>A value between 0 and 65536 to correspond to the number of presses.</returns>
        public static int ReadCounter(int counterNr)
        {
            if (_k8055D == null || !_k8055D.Connected || counterNr < 1 || 2 < counterNr)
            {
                throw new ArgumentException("Communication with K8055 was either already closed or CounterNr is invalid!");
            }
            return _k8055D.Counter[counterNr - 1];
        }

        /// <summary>
        /// The selected counter is reset to 0.
        /// </summary>
        /// <param name="counterNr">The counter whose state should be reset.</param>
        public static void ResetCounter(int counterNr)
        {
            if (_k8055D == null || !_k8055D.Connected || counterNr < 1 || counterNr > 2) return;
            _k8055D.Counter[counterNr - 1] = 0;
        }

        /// <summary>
        /// The debounce time of the selected counter is set to the passed value.
        /// To prevent false triggering of the inputs, the counters are debounced, 
        /// which means that the input has to be 'ON' for the time of the current debounce time
        /// to register it as a valid press.
        /// </summary>
        /// <param name="counterNr">The counter whose debounce time should be set.</param>
        /// <param name="debounceTime">
        /// The value to be set to the debounce time (in milliseconds) can range from 0 to 5000.
        /// </param>
        public static void SetCounterDebounceTime(int counterNr, int debounceTime)
        {
            if (_k8055D == null || !_k8055D.Connected || counterNr < 1 || debounceTime < 0   ||
                                                         2 < counterNr || 5000 < debounceTime) return;
            _k8055D.DebounceTime[counterNr - 1] = debounceTime;
        }

        #endregion

        #region SimulationOperations
        //These methods are not available in the standard K8055D library and are only used for simulation purposes.

        /// <summary>
        /// The selected digital input is set to passed state.
        /// This method does not work with an actual K8055D board!
        /// </summary>
        /// <param name="channel">The digital input whose state should be modified.</param>
        /// <param name="status">The state to which the digital input should be set.</param>
        public static void SetDigitalInputChannel(int channel, bool status)
        {
            if (_k8055D == null || !_k8055D.Connected || channel < 1 || channel > 5) return;
            _k8055D.DigitalInputChannel[channel - 1] = status;
        }

        /// <summary>
        /// The selected digital output channel is read.
        /// This method does not work with an actual K8055D board!
        /// </summary>
        /// <param name="channel">The digital output to be read.</param>
        /// <returns>The state of the selected digital output.</returns>
        public static bool ReadDigitalOutputChannel(int channel)
        {
            if (_k8055D == null || !_k8055D.Connected || channel < 1 || 8 < channel) return false;
            return _k8055D.DigitalOutputChannel[channel - 1];
        }

        /// <summary>
        /// The voltage of the selected analog input is set to the passed argument.
        /// This method does not work with an actual K8055D board!
        /// </summary>
        /// <param name="channel">The analog input to be set.</param>
        /// <param name="data">A value between 0 and 255 to represent the voltage (0 - 5V)</param>
        public static void SetAnalogInputChannel(int channel, int data)
        {
            if (_k8055D == null || !_k8055D.Connected || channel < 1 || 2 < channel) return;
            _k8055D.AnalogInputChannel[channel - 1] = (double)data * 5 / 255;
        }

        /// <summary>
        /// Der Analoge Ausgang wird gelesen und zurückgegeben.
        /// </summary>
        /// Funktioniert nicht mit dem echten K8055D!!!
        /// <param name="channel"></param>
        /// <returns>Ein Wert zwischen 0 und 255 der die Spannung im Bereich von 0V - 5V darstellt.
        ///  Bei -1 ist der K8055D nicht verbunden oder dieser Ausgang existiert nicht.</returns>
        public static int ReadAnalogOutputChannel(int channel)
        {
            if (_k8055D == null || !_k8055D.Connected || channel < 1 || channel > 2) return -1;
            return (int)(_k8055D.AnalogOutputChannel[channel - 1] / 5 * 255);
        }

        /// <summary>
        /// Der Zähler wird um 1 erhöht.
        /// Funktioniert nicht mit dem echten K8055D!!!
        /// </summary>
        /// <param name="counter">Der Zähler der erhöht werden soll.</param>
        public static void IncreaseCounter(int counter)
        {
            _k8055D.Counter[counter - 1]++;
        }
        #endregion

        internal class K8055D
        {
            internal bool Connected { get; set; }

            #region Input channel Properties

            internal double[] AnalogInputChannel { get; } = new double[2];
            internal bool[] DigitalInputChannel { get; } = new bool[5];

            #endregion

            #region Output channel Properties

            internal double[] AnalogOutputChannel { get; } = new double[2];
            internal bool[] DigitalOutputChannel { get; } = new bool[8];

            #endregion

            #region Counter Properties

            internal ushort[] Counter { get; } = new ushort[2];
            internal int[] DebounceTime { get; } = new int[2];
     
            #endregion

            /// <summary>
            /// The default debounce time of 2ms is set.
            /// </summary>
            internal K8055D()
            {
                DebounceTime[0] = 2;
                DebounceTime[1] = 2;
            }
        }
    }
}