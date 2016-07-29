// Load Tools
#tool Mono.TextTransform

//Load cake build configuration
#load ./build/version.cake
#load ./build/endpoint.cake



// using Cake.Common.Tools.WiX.Heat;

// ARGUMENTS
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");
var api = Argument("api", "Test");

// ENVIRONMENT
var isRunningOnUnix = IsRunningOnUnix();

// FILE SYSTEM
var solution = File("../Xamarin.MobileWorld.sln");
var xamarinSolution = File("../Xamarin.MobileWorld.sln");
var androidProject = File("../Droid/MobileWorld.Droid.csproj");
var androidPackage = File(string.Format("../Droid/bin/{0}/com.xamarin.mobileworld.apk", configuration));
var iOSPackage = File(string.Format("../iOS/bin/iPhone/{0}/xamarin.mobileworld.ipa", configuration));

var assemblyVersion = ApplicationVersion.GetApplicationVersion(Context, File("./src/MobileWorld/AssemblyVersion.tt"));

Setup(() => 
{
    Information("Hello Cake");
    Information("Assembly Version: {0}", assemblyVersion.AssemblyVersion);

    Endpoint.GetBaseAddress(Context, File("./src/MobileWorld/Endpoint.tt"), api);  
    //Information("Is Travis CI Build: {0}", TravisCI.IsRunningOnTravisCI);
});

Teardown(() =>
{
    Information("Later Cake");
});

Task("Nuget-Restore")
.Does(() =>
{
    NuGetRestore("./src/MobileWorld.sln");
});
 
Task("Fold")
.WithCriteria(TravisCI.IsRunningOnTravisCI)
.Does(() => 
{ 
  if(TravisCI.IsRunningOnTravisCI) 
  { 
    TravisCI.WriteStartFold("name"); 
    Information("Folded"); 
    TravisCI.WriteEndFold("name"); 
  } 
}); 
 
Task("Text-Transform") 
.Does(() => 
{ 
  var transform = File("./src/MobileWorld/AssemblyVersion.tt"); 
  TransformTemplate(transform, new TextTransformSettings { OutputFile="./src/MobileWorld/AssemblyVersion.cs" }); 
});

Task("WiX-Directory")
.IsDependentOn("Nuget-Restore")
.Does(() =>
{
    var harvestDirectorySource = Directory("./src");
    Information(harvestDirectorySource.GetType().Name);
    var harvestDirectory = Directory("./");
    Information(harvestDirectory.GetType().Name);
    var filePath = File("Wix.Directory.wxs");
    Information(MakeAbsolute(harvestDirectory).FullPath);
    //WiXHeat(harvestDirectory, filePath, WiXHarvestType.Dir);
});

Task("WiX-Heat-No-Settings")
.IsDependentOn("Nuget-Restore")
.Does(() =>
{
    var harvestFiles =  Directory("./");
    var filePath = File("cake.wxs");
    WiXHeat(harvestFiles, filePath);
});

Task("WiX-File")
.IsDependentOn("Nuget-Restore")
.Does(() =>
{
    var harvestFile = File("./tools/Cake/Cake.Core.dll");
    var filePath = File("Wix.File.wxs");
    //WiXHeat(harvestFile, filePath, WiXHarvestType.File);
});

Task("WiX-Files")
.IsDependentOn("Nuget-Restore")
.Does(() =>
{
    var filePath = File("Wix.Files.wxs");
    var harvestFiles = GetFiles("./tools/Cake/*.dll");
    //WiXHeat(harvestFiles, filePath, WiXHarvestType.File);
});

Task("WiX-Website")
.Does(() =>
{
    var filePath = File("WiX.Website.wxs");
    //WiXHeat("Default Web Site", filePath, WiXHarvestType.Website,  new HeatSettings { NoLogo = true });
});

Task("Default")
.IsDependentOn("Nuget-Restore")
.IsDependentOn("Text-Transform")
.IsDependentOn("WiX-Heat-No-Settings");

RunTarget(target);