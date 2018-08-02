namespace FunctionsDonkey.Cli
{
	using System;
	using System.Linq;
	using Microsoft.Extensions.DependencyInjection;
	using Templating;

	public class Functions : IFunctionBuilder
    {	
		private readonly FunctionIntrospection _introspection = new FunctionIntrospection();
		
		public static FunctionIntrospection Generation(IApiDefinition apiDefinition)
		{
            var builder = new Functions();
			apiDefinition.Build(builder);

			builder._introspection.Namespace = apiDefinition.GetType().Namespace;

			return builder._introspection;
		}

        public IFunctionBuilder Compose(Action<IServiceCollection> compositionDelegate)
		{
			return this;
		}

		public IFunctionBuilder AddHttp<TCommand, THandler>(string route)
		{
			_introspection.TemplateFile = Template.HttpTrigger;
			_introspection.Name = route.Split("/").Last().FirstCharToUpper();
			_introspection.CommandTypeName = typeof(TCommand).FullName;

			return this;
		}
	}
}