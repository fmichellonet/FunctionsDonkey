namespace FunctionsDonkey
{
	using System;
	using MediatR;
	using Microsoft.Extensions.DependencyInjection;

	public static class CompositionRoot
	{

        internal static ServiceCollection ServiceCollection { get; }

        static CompositionRoot()
		{
			ServiceCollection = new ServiceCollection();
        }

        private static readonly Lazy<IServiceProvider> ServiceProvider = new Lazy<IServiceProvider>(BuildServiceProvider);

        public static IServiceProvider GetServiceProvider()
        {
            return ServiceProvider.Value;
        }

        private static IServiceProvider BuildServiceProvider()
        {
			// Mediatr
			ServiceCollection.AddTransient<IMediator, Mediator>();
			ServiceCollection.AddTransient<ServiceFactory>(provider => provider.GetService);
			
            return ServiceCollection.BuildServiceProvider();
        }
    }
}