namespace FunctionsDonkey
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	internal static class TypeExtensions
	{
		private static IApiDefinition _foundDefinition;

		internal static IApiDefinition GetApiDefinition()
		{
			if (_foundDefinition != null)
			{
				return _foundDefinition;
			}

			foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
			{
				var foundType = asm.GetTypes().FirstOrDefault(x => typeof(IApiDefinition).IsAssignableFrom(x) && x.IsClass);
				if (foundType != null)
				{
					try
					{
						_foundDefinition = (IApiDefinition) Activator.CreateInstance(foundType);
						return _foundDefinition;
					}
					catch (Exception)
					{
						throw new InvalidOperationException($"Found {foundType.Name} as the API definiion, but it lacks empty constructor");
					}
				}
			}
			throw new InvalidOperationException("Unable to find any type implementing IApiDefinition");
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="givenType"></param>
        /// <param name="genericType"></param>
        /// <see cref="https://stackoverflow.com/questions/74616/how-to-detect-if-type-is-another-generic-type#1075059"/>
        /// <returns></returns>
        internal static bool IsAssignableToGenericType(this Type givenType, Type genericType)
		{
			var interfaceTypes = givenType.GetInterfaces();

			foreach (var it in interfaceTypes)
			{
				if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
					return true;
			}

			if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
				return true;

			Type baseType = givenType.BaseType;
			if (baseType == null) return false;

			return IsAssignableToGenericType(baseType, genericType);
		}

		internal static IEnumerable<Type> GetImplementedInterfaces(this Type givenType, Type genericType)
		{
			var interfaceTypes = givenType.GetInterfaces();
			return interfaceTypes.Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == genericType);
		}
    }
}