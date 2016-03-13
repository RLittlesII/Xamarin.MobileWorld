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

Setup(() => 
{
    Information("Hello Cake and Bitrise");
});

Teardown(() =>
{
    Information("Later Cake and Bitrise");
});

Task("Default");
RunTarget(target);