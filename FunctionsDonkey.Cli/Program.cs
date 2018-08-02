namespace FunctionsDonkey.Cli
{
	using System.IO;
	using System.Runtime.Loader;
	using System.Threading.Tasks;
	using CommandLine;
	using Templating;

	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var parsed = Parser.Default.ParseArguments<Options>(args)
							   .WithParsed(async opts =>
								   await RunOptionsAndReturnExitCode(opts).ConfigureAwait(false));
		}

		private static async Task RunOptionsAndReturnExitCode(Options opts)
		{
			var pointedAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(opts.AssemblyFilePath);
			var definition = pointedAssembly.GetApiDefinition();
			var functionIntrospection = Functions.Generation(definition);

			using (var stream = File.OpenWrite(Path.Combine(opts.OutputDirectory,
				$"{functionIntrospection.Name.FirstCharToUpper()}.cs")))
			{
				using (var writer = new StreamWriter(stream))
				{
					await Generator.GenerateAsync(writer, functionIntrospection).ConfigureAwait(false);
				}
			}
		}

		internal class Options
		{
			[Option('a', "assembly", Required = true, HelpText = "Assembly file path containing your api definition.")]
			public string AssemblyFilePath { get; set; }

			[Option('o', "outputDir", Required = true, HelpText = "Generated files output directory.")]
			public string OutputDirectory { get; set; }
		}
	}
}