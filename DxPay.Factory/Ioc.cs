using System;
using System.Collections.Generic;
using System.Linq;

namespace DxPay.Factory
{
    public static class IoC
    {
        //https://www.codeproject.com/Articles/347651/Define-Your-Own-IoC-Container?msg=4191797
        private static readonly IDictionary<Type,Type> Types = new Dictionary<Type, Type>();
        private static readonly IDictionary<Type,object> TypeInstances = new Dictionary<Type, object>();
        public static void Register<TContract, TImplementation>()
        {
            Types[typeof(TContract)] = typeof(TImplementation);
        }
        public static void Register<TContract, TImplementation>(TImplementation instance)
        {
            TypeInstances[typeof(TContract)] = instance;
        }
        public static T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }
        public static object Resolve(Type contract)
        {
            if (TypeInstances.ContainsKey(contract))
            {
                return TypeInstances[contract];
            }
            var implementation = Types[contract];
            var constructor = implementation.GetConstructors()[0];
            var constructorParameters = constructor.GetParameters();
            if (constructorParameters.Length == 0)
            {
                return Activator.CreateInstance(implementation);
            }
            var parameters = new List<object>(constructorParameters.Length);
            parameters.AddRange(constructorParameters.Select(parameterInfo => Resolve(parameterInfo.ParameterType)));
            return constructor.Invoke(parameters.ToArray());
        }
    }
}