# Installation steps for development tools and emulator
## Step 1 - Unity 2019
- [Download link](https://unity.com/releases/editor/archive)
### Components to check:
- Windows 10 Professional / Enterprise (Hyper-V)
- Unity
- Microsoft Visual Studio Community
- Universal Windows Platform
- Windows Build Support

## Step 2 - visual studio 2017 COMMUNITY v15.9
- [Download link](https://visualstudio.microsoft.com/thank-you-downloading-visual-studio/?sku=Community&rel=15)
### Components to check:
- Universal Windows Platform
- Game dev with Unity
- .NET 4.0
- desktop development with C++
- Change SDK version to 10.0.17763 in HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Microsoft SDKs\Windows\v10.0

## Step 3 - Windows Software Development kit
- [Download link](https://developer.microsoft.com/en-us/windows/downloads/windows-sdk/)

## Step 4 - HoloLens emulator
- [Download link](https://learn.microsoft.com/en-us/windows/mixed-reality/develop/advanced-concepts/hololens-emulator-archive)
(Link at the bottom of page for gen 1)
- [Direct link](https://go.microsoft.com/fwlink/?linkid=2065980)

## Step 5 - Enable developer mode (not always required)

![image](Assets/Readme_images/developer_mode.PNG)

# Build the project
## Step 1
Import and build the project with Unity.
- File -> Build settings
![image](Assets/Readme_images/build_settings.PNG)

## Step 2
Open the produced build folder in Visual Studio.

File -> Open -> Project/solution... 

and then select ```ReflexesTrainingAR.sln``` inside the build folder.

## Step 3
In the right panel, select ´´´ReflexesTrainingAR´´´
![image](Assets/Readme_images/right_panel_vscode.PNG)

And set it as startup project.
![image](Assets/Readme_images/right_panel_vscode_set_as.png)

## Step 4
Select ```Debug```, ```X86``` and ```HoloLens simulator``` as run options, then click on the green arrow to run the project.
![image](Assets/Readme_images/run_options.png)
