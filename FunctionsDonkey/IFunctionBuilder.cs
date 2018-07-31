namespace FunctionsDonkey
{
	using System;
	using Microsoft.Extensions.DependencyInjection;

	public interface IFunctionBuilder
	{
		IFunctionBuilder Compose(Action<IServiceCollection> action);
		IFunctionBuilder AddHttp<TCommand, THandler>(string route);
	}
}