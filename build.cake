// ARGUMENTS
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");

// ENVIRONMENT
var isRunningOnUnix = IsRunningOnUnix();

// FILE SYSTEM
var solution = File("../ShipFly.sln");
var xamarinSolution = File("../src/UI/Xamarin/ShipFly.Forms/ShipFly.Forms.sln");
var androidProject = File("../src/UI/Xamarin/ShipFly.Forms/Droid/ShipFly.Forms.Droid.csproj");
var androidPackage = File(string.Format("../src/UI/Xamarin/ShipFly.Forms/Droid/bin/{0}/com.shipfly.shipfly.apk", configuration));
var iOSPackage = File(string.Format("../src/UI/Xamarin/ShipFly.Forms/iOS/bin/iPhone/{0}/ShipFly.ipa", configuration));
var travisci = TravisCI;

Setup(() => 
{
    Information("Hello Cake");
    Information("Is Travis CI Build: {0}", TravisCI.IsRunningOnTravisCI);
});

Teardown(() =>
{
    Information("Later Cake");
});

Task("Nuget-Restore")
.Does(() =>
{
	    NuGetRestore("./src/MobileWorld.sln", new NuGetRestoreSettings {
        Source = new List<string> {
            "https://www.myget.org/F/rlittlesii-cake/api/v2"
        }
    });
});

Task("Travis-Test")
.Does(() =>
{
	if(TravisCI.IsRunningOnTravisCI)
	{
		Information("{0}: {1}", "CI" , TravisCI.Environment.CI);
		Information("{0}: {1}", "Home" , TravisCI.Environment.Home);
//		Information("{0}: {1}", "" , TravisCI.);
//		Information("{0}: {1}", "", TravisCI.);
//		Information("{0}: {1}", "", TravisCI.);
//		Information("{0}: {1}", "", TravisCI.);
//		Information("{0}: {1}", "", TravisCI.);
//		Information("{0}: {1}", "", TravisCI.);
//		Information("{0}: {1}", "", TravisCI.);
//		Information("{0}: {1}", "", TravisCI.);
//		Information("{0}: {1}", "", TravisCI.);
//		Information("{0}: {1}", "", TravisCI.);
//		Information("{0}: {1}", "", TravisCI.);
//		Information("{0}: {1}", "", TravisCI.);
//		Information("{0}: {1}", "", TravisCI.);
//		Information("{0}: {1}", "", TravisCI.);
//		Information("{0}: {1}", "", TravisCI.);
//		Information("{0}: {1}", "", TravisCI.);
	}
});

Task("Fold")
.IsDependentOn("Travis-Test")
.Does(() =>
{
	if(TravisCI.IsRunningOnTravisCI)
	{
		TravisCI.WriteStartFold("name");
		Information("Folded");
		TravisCI.WriteEndFold("name");
	}
});


Task("Default")
.IsDependentOn("Fold");

RunTarget(target);