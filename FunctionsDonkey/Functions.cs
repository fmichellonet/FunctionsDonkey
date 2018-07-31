namespace FunctionsDonkey
{
	using System;
	using MediatR;
	using Microsoft.Extensions.DependencyInjection;

	public class Functions : IFunctionBuilder
	{

		private static IRuntime _runtime;

		public static IRuntime Runtime()
		{
			if (_runtime == null)
			{
				var definition = TypeExtensions.GetApiDefinition();
				var builder = new Functions();
				definition.Build(builder);
				_runtime = new Runtime(definition.GetType());
            }
			
			return _runtime;
		}

		public IFunctionBuilder Compose(Action<IServiceCollection> compositionDelegate)
		{
			compositionDelegate(CompositionRoot.ServiceCollection);
			return this;
		}

		public IFunctionBuilder AddHttp<TCommand, THandler>(string route)
		{
			var handlerType = typeof(THandler);
			var reqHandlerType = typeof(IRequestHandler<,>);

			foreach (var interfaceHandledType in handlerType.GetImplementedInterfaces(reqHandlerType))
			{
				CompositionRoot.ServiceCollection.AddTransient(interfaceHandledType, typeof(THandler));
			}
			
            return this;
		}
    }
}