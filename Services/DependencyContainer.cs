using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.Interfaces;

namespace App1.Services
{

    public class DependencyContainer : IDependencyContainer
    {
        private readonly Dictionary<Type, Type> _registeredTypes = new Dictionary<Type, Type>();
        private readonly Dictionary<Type, object> _resolvedObjects = new Dictionary<Type, object>();
        private bool disposedValue;

        public void Register<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface
        {
            _registeredTypes[typeof(TInterface)] = typeof(TImplementation);
        }

        public TService Resolve<TService>()
        {
            return (TService)Resolve(typeof(TService));
        }

        public object Resolve(Type type)
        {
            if (_resolvedObjects.ContainsKey(type))
            {
                return _resolvedObjects[type];
            }

            if (!_registeredTypes.ContainsKey(type))
            {
                throw new InvalidOperationException($"Type {type.Name} is not registered");
            }

            var resolvedType = _registeredTypes[type];
            var constructor = resolvedType.GetConstructors()[0];
            var parameters = constructor.GetParameters()
                                    .Select(p => Resolve(p.ParameterType))
                                    .ToArray();

            var resolvedObject = constructor.Invoke(parameters);

            _resolvedObjects[type] = resolvedObject;

            return resolvedObject;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _resolvedObjects.Clear();
                    _registeredTypes.Clear();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
