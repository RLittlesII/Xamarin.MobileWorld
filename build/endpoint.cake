#tool Mono.TextTransform

public class Endpoint
{
	public static void GetBaseAddress(ICakeContext context, FilePath assemblyTemplate, string environment)
	{
		if(context == null)
		{
			throw new ArgumentNullException("context");
		}
        
        if( assemblyTemplate == null)
        {
            throw new ArgumentNullException("assemblyTemplate");
        }

		var target = context.Argument("target", "Default");
		
        context.Information("assemblyTemplate: {0}", assemblyTemplate);
		context.Information("Environment: {0}", environment);
        
		context.TransformTemplate(assemblyTemplate, new TextTransformSettings { OutputFile = "./src/MobileWorld/Endpoint.cs", ArgumentCustomization = args => args.Append(string.Format("-a=environment!{0}", environment)) });
	}
}