namespace FunctionApp.Handlers
{
	using System.Threading;
	using System.Threading.Tasks;
	using Commands;
	using MediatR;

	public class ItemAddedToCartHandler : IRequestHandler<ItemAddedToCart, string>
	{
		Task<string> IRequestHandler<ItemAddedToCart, string>.Handle(ItemAddedToCart request,
			CancellationToken cancellationToken)
		{
			return Task.FromResult("Mah!");
		}
	}
}