namespace FunctionsDonkey.Templating
{
	using System.IO;
	using System.Threading.Tasks;
	using Mustache;

	public static class Generator
    {
		public static async Task GenerateAsync(TextWriter writer, IFunctionIntrospection introspection)
		{
			var content = EmbeddedRessourceReader.ReadFromRessource(introspection.TemplateFile);
			
			var compiler = new FormatCompiler
			{
				RemoveNewLines = false
			};

			var generator = compiler.Compile(content);
			var replaced = generator.Render(introspection);

			writer.Write(replaced);
			
			await writer.FlushAsync().ConfigureAwait(false);
		}		
	}
}