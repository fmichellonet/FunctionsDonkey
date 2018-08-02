namespace FunctionsDonkey.Templating
{
	using System.IO;
	using System.Reflection;
	using System.Text;

	public static class EmbeddedRessourceReader
	{
		public static string ReadFromRessource(string fullQualifiedName)
		{
			using (var stream = typeof(EmbeddedRessourceReader).GetTypeInfo().Assembly.GetManifestResourceStream(fullQualifiedName))
			{
				using (var reader = new StreamReader(stream, Encoding.UTF8))
				{
					return reader.ReadToEnd();
				}
			}
		}
	}
}