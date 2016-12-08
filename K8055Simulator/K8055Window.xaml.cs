/*
 * This file is licensed under the MIT License.
 * Check the LICENSE file in the projects root for more information.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace K8055Simulator
{
    public partial class K8055Window
    {
        internal readonly K8055Sim.K8055D K8055D;

        private readonly DispatcherTimer _updateValues = new DispatcherTimer();

        private readonly DateTime[] _counterTimeStamp = new DateTime[2];

        private readonly List<Image> _digitalOutputList = new List<Image>();
        private readonly List<ComboBox> _digitalInputList = new List<ComboBox>();
        private readonly bool[] _digitalInputState = new bool[5];

        /// <summary>
        /// Gets the reference to the currently active K8055Simulator board.
        /// </summary>
        internal K8055Window(K8055Sim.K8055D k8055D)
        {
            InitializeComponent();
            InitializeLists();
            InitializeTimers();
            _updateValues.Start();
            K8055D = k8055D;
        }

        /// <summary>
        /// Lists are initialized with controls for easy iteration.
        /// </summary>
        private void InitializeLists()
        {
            foreach (Image image in K8055DigitalOutputCanvas.Children.OfType<Image>())
            {
                _digitalOutputList.Add(image);
            }
            _digitalOutputList.Reverse();

            foreach (ComboBox comboBox in K8055DigitalInputCanvas.Children.OfType<ComboBox>())
            {
                comboBox.Items.Add(new ListBoxItem() {Content = "True"});
                comboBox.Items.Add(new ListBoxItem() { Content = "False" });
                _digitalInputList.Add(comboBox);
            }

            _counterTimeStamp[0] = new DateTime();
            _counterTimeStamp[1] = new DateTime();
        }

        /// <summary>
        /// Initializes DispatcherTimer
        /// </summary>
        private void InitializeTimers()
        {
            _updateValues.Tick += DispatcherTimerTick;
            _updateValues.Interval = new TimeSpan(0, 0, 0, 0, 25);
        }

        /// <summary>
        /// Updates WPF-Controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DispatcherTimerTick(object sender, EventArgs e)
        {
            //updates power LED.
            SetOpacityUIElement(K8055PowerLedImage, K8055D.Connected);

            //Updates analog output image opacity.
            SetOpacityUIElement(K8055AnalogOutput1Image, K8055D.AnalogOutputChannel[0] > 0);
            SetOpacityUIElement(K8055AnalogOutput2Image, K8055D.AnalogOutputChannel[1] > 0);
            //Updates analog output voltages.
            K8055AnalogOutput1Label.Content = $"{Math.Round(K8055D.AnalogOutputChannel[0], 3)} V";
            K8055AnalogOutput2Label.Content = $"{Math.Round(K8055D.AnalogOutputChannel[1], 3)} V";

            //Updates digital outputs.
            for (int i = 0; i < _digitalOutputList.Count; i++)
            {
                SetOpacityUIElement(_digitalOutputList[i], K8055D.DigitalOutputChannel[i]);
            }
        }

        /// <summary>
        /// Saves the current time to make sure once the button is release that the debounce has been exceeded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void K8055DigitalInputMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int index = int.Parse(((Button)sender).Name[17].ToString()) - 1;

            K8055D.DigitalInputChannel[index] = true;
            if (index < 2) _counterTimeStamp[index] = DateTime.Now;
        }

        /// <summary>
        /// Increases appropriate counter if the debouncetime for said counter has been exceeded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void K8055DigitalInputMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int index = int.Parse(((Button)sender).Name[17].ToString()) - 1;
            if (!_digitalInputState[index]) K8055D.DigitalInputChannel[index] = false;

            if (index < 2 && DateTime.Now - _counterTimeStamp[index] >= TimeSpan.FromMilliseconds(K8055D.DebounceTime[index]))
            {
                K8055Sim.IncreaseCounter(++index);
            }
        }

        /// <summary>
        /// Updates the K8055Simulator with the user chosen digital output values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void K8055DigitalInputSetClick(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)sender).Name[17].ToString()) - 1;
            bool inputOn = _digitalInputList[index].SelectedIndex == 0;

            K8055D.DigitalInputChannel[index] = inputOn;
            _digitalInputState[index] = inputOn;
        }

        /// <summary>
        /// Updates the K8055Simulator analog output voltages with the user chosen values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void K8055AnalogInputSetClick(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)sender).Name[16].ToString());
            try
            {
                K8055D.AnalogInputChannel[index - 1] = index == 1
                    ? double.Parse(K8055AnalogInput1TextBox.Text)
                    : double.Parse(K8055AnalogInput2TextBox.Text);
            }
            catch
            {
                MessageBox.Show("Please make sure your input is a decimal value.");
            }
        }

        /// <summary>
        /// The passed UIElement's opacity is set to the desired state.
        /// </summary>
        /// <param name="uiElement">UIElement to change the opactiy of.</param>
        /// <param name="visible">The opacity state.</param>
        private void SetOpacityUIElement(UIElement uiElement, bool visible)
        {
            uiElement.Opacity = visible ? 100 : 0;
        }

        private void K8055WindowClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Application.Current.Shutdown();
        }
    }
}
