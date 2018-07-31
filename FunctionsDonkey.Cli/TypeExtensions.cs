namespace FunctionsDonkey.Cli
{
    using System;
    using System.Linq;
    using System.Reflection;

    internal static class TypeExtensions
    {
        internal static IApiDefinition GetApiDefinition(this Assembly asm)
        {
            var foundType = asm.GetTypes().FirstOrDefault(x => typeof(IApiDefinition).IsAssignableFrom(x) && x.IsClass);
            if (foundType != null)
            {
                try
                {
                    return (IApiDefinition)Activator.CreateInstance(foundType);
                }
                catch (Exception)
                {
                    throw new InvalidOperationException($"Found {foundType.Name} as the API definiion, but it lacks empty constructor");
                }
            }
            throw new InvalidOperationException("Unable to find any type implementing IApiDefinition");
        }
    }
}