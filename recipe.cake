#load nuget:https://www.myget.org/F/xamarin-recipe/api/v2?package=Rocket.Surgery.Xamarin.Recipe&version=0.3.0-fastlane-deliver0025 
#addin nuget:?package=Cake.Incubator&version=2.0.1

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context,
                                buildSystem: BuildSystem,
                                sourceDirectoryPath: "./src",
                                testDirectoryPath: "./tests",
                                solutionFilePath: "./Mobile.sln",
                                title: "Mobile",
                                androidProjectPath: "./src/Mobile.World.Android/Mobile.World.Android.csproj",
                                androidManifest: "./src/Mobile.World.Android/Properties/AndroidManifest.xml",
                                iosProjectPath: "./src/MobileWorld.iOS/GreenLights.iOS.csproj",
                                plistFilePath: "./src/MobileWorld.iOS/Info.plist",
                                platform: "iPhoneSimulator",
                                nugetConfig: "./NuGet.config");

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context);

Build.RunAndroid();