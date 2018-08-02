﻿namespace FunctionsDonkey.Cli
{
	using Templating;

	public class FunctionIntrospection : IFunctionIntrospection
	{
		public string Name { get; set; }
		public string Namespace { get; set; }
		public string CommandTypeName { get; set; }
		public string TemplateFile { get; set; }
	}
}