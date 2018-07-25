#load nuget:https://www.myget.org/F/xamarin-recipe/api/v2?package=Rocket.Surgery.Xamarin.Recipe&version=0.1.0-beta0017
#addin nuget:?package=Cake.Incubator&version=2.0.1

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context,
                                buildSystem: BuildSystem,
                                sourceDirectoryPath: "./src",
                                testDirectoryPath: "./tests",
                                solutionFilePath: "./MobileWorld.sln",
                                title: "Mobile World",
                                androidProjectPath: "./src/MobileWorld.Android/GreenLights.Android.csproj",
                                androidManifest: "./src/MobileWorld.Android/Properties/AndroidManifest.xml",
                                iosProjectPath: "./src/MobileWorld.iOS/GreenLights.iOS.csproj",
                                plistFilePath: "./src/MobileWorld.iOS/Info.plist",
                                platform: "iPhoneSimulator");

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context);

Build.RunDotNetCore();