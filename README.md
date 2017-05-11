# K8055Simulator
<p align="center">
  <img src="http://i.imgur.com/Bnu7URb.png" width="591"/>
</p>
<p align="center" style="font-size:12px">K8055 CAD Render Image by Nemanja Petkov <a href="https://grabcad.com/3d.cnc-1">here</a>.</p>

### Synopsis

This project allows you to develop for the K8055 without the need of having the physical device with you.
This Simulator interfaces with your project the same way the regular K8055 does through unmanaged code calls, making it very easy to change between the Simulator and the physical device. 

### Motivation

This project was initially created because of my enjoyment developing with the K8055, but the lack of the actual device at home. So this Simulator allowed me to develop at home and use the same exact code on the actual device at school.

### Prerequisites

  - You have to have the [.NET Framework V4.5+](https://www.microsoft.com/en-us/download/details.aspx?id=30653) installed.
  - (Optional) If you want to build this Project from source yourself, you have to have [Visual Studio](https://www.visualstudio.com/downloads/) installed.

### Installation

I have provided an already compiled Dynamic Link Library [here](https://github.com/bbartels/K8055Simulator/releases/tag/1.0). 
But if you prefer to build the library yourself I provided instructions below:

Type the following in the Visual Studio Commandline (may need to adjust path to nuget cli tool):
```cmd
> git clone https://github.com/bbartels/K8055Simulator.git
> msbuild K8055Simulator\K8055Simulator.sln /t:restore
> msbuild K8055Simulator\K8055Simulator.sln /p:Configuration=Release
> cd K8055Simulator\K8055Simulator\bin\Release
```
Alternatively you can open the K8055Simulator.sln in Visual Studio and compile there.

### API Reference

The project has been built to mimic the library that has been provided by the K8055. So you can use [this API documentation](http://www.velleman.eu/downloads/0/user/usermanual_k8055_dll_uk.pdf) from the official Velleman site.

### Demo

You can download an already compiled demo project on the releases tab [here](https://github.com/bbartels/K8055Simulator/releases/tag/1.0) and the testing program [here](https://github.com/bbartels/K8055Test/releases/tag/1.0) make sure to put both binaries in the same directory and run the executable.

However if you prefer to compile it yourself, follow the instructions below.

Type the following in the Visual Studio Commandline:
```cmd
> git clone https://github.com/bbartels/K8055Test.git
> msbuild.exe K8055Test\K8055Test.sln /p:Configuration=Release
> git clone https://github.com/bbartels/K8055Simulator.git
> msbuild K8055Simulator\K8055Simulator.sln /t:restore
> msbuild K8055Simulator\K8055Simulator.sln /p:Configuration=Release
> move K8055Simulator\K8055Simulator\bin\Release\K8055D.dll K8055Test\K8055Test\bin\Release
> K8055Test\K8055Test\bin\Release\K8055Test.exe
```

You could also compile both solutions using via Visual Studio directly. Make sure to copy the K8055D.dll that is created from the K8055Simulator solution in the same path as the K8055Test.exe.

### Code Example

This code example is written in C#, but you can access this library as long as your preferred language has support for unmanaged C/C++ imports and you fulfill the Prerequisites.

The imports in the example below don't cover all the importable functions, please refer to here for a full view of the functions that can be imported!
Or open the Vellemann API Reference mentioned above to find the correct signature for the functions and import them the same way as shown in the code example below.
```C#
using System;
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

### Future

While I have nothing concrete planned for the future I was thinking to maybe porting this to .NET Core and a third party cross platform UI library to make this project work with Mac and Linux. 

Though I am holding off of this for now for multiple reasons, first I want to wait until the next version of .NET Core ships with .NET Standard 2.0 to make this project a little more future proof and second I am not sure if there is even any interest in having cross platform compatibility. 

If you have any concrete feature requests you can submit an Issue on this Github page and I'll look into potentially implementing it.

## License

This project is licensed under the MIT License, for further information please refer to the LICENSE file in the projects root directory.
