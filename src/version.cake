#tool Mono.TextTransform

public class ApplicationVersion
{
	public string AssemblyVersion { get; set; }

	public static ApplicationVersion GetApplicationVersion(ICakeContext context, FilePath template)
	{
		if(context == null)
		{
			throw new ArgumentNullException("context");
		}

		context.TransformTemplate(template, new TextTransformSettings { OutputFile = "./src/MobileWorld/AssemblyVersion.cs" });

		var assembly = context.ParseAssemblyInfo(context.File("./src/MobileWorld/AssemblyVersion.cs"));

		return new ApplicationVersion
		{
			//AssemblyVersion = assembly.AssemblyVersion.ToString()
		};
	}
}