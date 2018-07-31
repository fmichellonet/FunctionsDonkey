namespace FunctionsDonkey
{
	using System;
	using System.Threading.Tasks;
	using MediatR;

	public class Runtime : IRuntime
	{
		private readonly Type _definitionType;

		public Runtime(Type definitionType)
		{
			_definitionType = definitionType;
		}

		private IServiceProvider GetServiceProvider()
		{
			return CompositionRoot.GetServiceProvider();
		}

		public Task<TResponse> HandleCommand<TResponse>(IRequest<TResponse> command)
		{
			IServiceProvider provider = GetServiceProvider();
			var mediator = (IMediator)provider.GetService(typeof(IMediator));
			return mediator.Send(command);
		}
	}
}