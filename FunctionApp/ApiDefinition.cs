namespace FunctionApp
{
	using Commands;
	using FunctionsDonkey;
	using Handlers;
	using MediatR;
	using Microsoft.Extensions.DependencyInjection;

	public class ApiDefinition : IApiDefinition
	{
		public void Build(IFunctionBuilder builder)
		{
			builder.Compose(services =>
				   {
					   //services.AddTransient<IRequestHandler<ItemAddedToCart, string>, ItemAddedToCartHandler>();
				   })
                   .AddHttp<ItemAddedToCart, ItemAddedToCartHandler>("addToCart")
				   .AddHttp<ItemAddedToCart, ItemAddedToCartHandler>("addToCart2");
		}
	}
}