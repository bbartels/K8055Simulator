# K8055Simulator

### Synopsis

This project allows you to develop for the K8055 without the need of having the physical device with you.
This Simulator interfaces with your project the same way the regular K8055 does through unmanaged code calls. Making it very easy to change between the Simulator and the physical device. 

### Motivation

This project was initially created because of the enjoyment of developing with the K8055, but the lack of the actual device at home. So this Simulator allowed me to develop at home and use the same exact code on the actual device at school.

### Prerequisites

  - You have to have the [.NET Framework V4.5+](https://www.microsoft.com/en-us/download/details.aspx?id=30653) installed.
  - (Optional) If you want to build this Project from source yourself, you have to have [Visual Studio](https://www.visualstudio.com/downloads/) installed.

### Installation

I have provided an already compiled Dynamic Link Library here. 
But if you prefer to build the library yourself I provided instructions below:

Type the following in the Visual Studio Commandline (adjust paths accordingly):
```cmd
> cd PATH_TO_K8055Simulator
> msbuild K8055Simulator.sln
> cd K8055Simulator\bin\Debug
> copy K8055.dll PATH_TO_YOUR_SOLUTION
```
Alternatively you can just open the K8055Simulator Solution in Visual Studio and compile it from there.

### API Reference

The project has been built to mimic the library that has been provided by the K8055. So you can use [this API documentation](http://www.velleman.eu/downloads/0/user/usermanual_k8055_dll_uk.pdf) from the official Velleman site.


### Code Example

This code example is written in C#, but you can access this library as long as your preferred language has support for unmanaged C/C++ imports and you fulfill the Prerequisites.

The imports in the example below don't cover all the importable functions, please refer to here for a full view of the functions that can be imported!
Or open the Vellemann API Reference mentioned above to find the correct signature for the functions and import them the same way as shown in the code example below.
```C++
using System.Runtime.InteropServices;

namespace K8055Demo
{
    public static class K8055
    {
        [DllImport("K8055D.dll")]
        public static extern int OpenDevice(int CardAddress);
        [DllImport("K8055D.dll")]
        public static extern void CloseDevice();
        [DllImport("K8055D.dll")]
        public static extern void SetDigitalChannel(int Channel);
        [DllImport("K8055D.dll")]
        public static extern void ReadDigitalChannel(int Channel);
    }
    
    public class Program
    {
        public static void Main(string[] args)
        {
            K8055.OpenDevice(0); //Open communication with K8055 that has the device address 0
            K8055.SetDigitalChannel(1); //Sets digital output channel 1 to 'ON'
            K8055.SetDigitalChannel(6); //Sets digital output channel 6 to 'ON'
            Console.WriteLine(K8055.ReadDigitalChannel(2)); //Reads digital input channel and prints in console
            K8055.CloseDevice(); //Closes communication with the K8055
        }
    }
}
```
## Contributors

This project is in a very early stage in development, it is not guaranteed to be 1:1 compatible with the actual K8055.
And since I personally don't own the actual device I had to work with the official documentation to make sure this project is working as intended by Vellemann. 

So if you own a K8055 and you want to contribute to this project you can try to run some tests to ensure the project's integrity. If you find any issues with the code you can open up an issue or submit a pull-request. 

## License

This project is licensed under the MIT License, for further information please refer to the LICENSE file in the projects root directory.


