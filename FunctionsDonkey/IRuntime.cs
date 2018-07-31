namespace FunctionsDonkey
{
	using System.Threading.Tasks;
	using MediatR;

	public interface IRuntime
	{
		Task<TResponse> HandleCommand<TResponse>(IRequest<TResponse> command);
	}
}