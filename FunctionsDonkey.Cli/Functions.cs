namespace FunctionsDonkey.Cli
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Microsoft.Extensions.DependencyInjection;
	using Templating;

	public class Functions : IFunctionBuilder
    {
		private readonly string _namespace;

		private Functions(string @namespace)
		{
			_namespace = @namespace;
		}

		private readonly Dictionary<string, FunctionIntrospection> _introspection = new Dictionary<string, FunctionIntrospection>();
		
		public static IDictionary<string, FunctionIntrospection> Generation(IApiDefinition apiDefinition)
		{
            var builder = new Functions(apiDefinition.GetType().Namespace);
			apiDefinition.Build(builder);
			
			return builder._introspection;
		}

        public IFunctionBuilder Compose(Action<IServiceCollection> compositionDelegate)
		{
			return this;
		}

		public IFunctionBuilder AddHttp<TCommand, THandler>(string route)
		{
			var httpIntrospection = new FunctionIntrospection
			{
				Namespace = _namespace,
				TemplateFile = Template.HttpTrigger,
				Name = route.Split("/").Last().FirstCharToUpper(),
				CommandTypeName = typeof(TCommand).FullName
			};

			_introspection.Add(route, httpIntrospection);

			return this;
		}
	}
}