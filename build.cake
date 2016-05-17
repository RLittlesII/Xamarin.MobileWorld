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
    DirectoryPath harvestDirectory = Directory("./");
    var filePath = File("Wix.Directory.wxs");
    var settings = new HeatSettings { HarvestType = WiXHarvestType.Dir };
    Information(MakeAbsolute(harvestDirectory).FullPath);
    WiXHeat(harvestDirectory, filePath, settings);
});

Task("WiX-File")
.IsDependentOn("Nuget-Restore")
.Does(() =>
{
    var harvestFile = File("../website/src/website/bin/website.dll");
    var filePath = File("Wix.File.wxs");
    var settings = new HeatSettings { HarvestType = WiXHarvestType.File };
    var harvestFiles = GetFiles("../website/src/website/bin/website.dll");
    WiXHeat(harvestFiles, filePath, settings);
});

Task("Default")
.IsDependentOn("Nuget-Restore")
.IsDependentOn("Text-Transform");

RunTarget(target);