using App1.Interfaces;
using App1.Services;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Managers
{
    public class ServiceManager : IServiceManager
    {
        private readonly IDependencyContainer _container;
        private static ServiceManager? _instance;
        private bool disposedValue;

        public static ServiceManager Instance
        {
            get
            {
                _instance ??= new ServiceManager(new DependencyContainer());
                return _instance;
            }
        }

        private ServiceManager(IDependencyContainer container)
        {
            _container = container;
        }

        public static void AddService<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface
        {
            var container = Instance._container;
            container.Register<TInterface, TImplementation>();
        }

        public static TService GetService<TService>()
        {
            return (TService)GetService(typeof(TService));
        }

        public static object GetService(Type serviceType)
        {
            var container = Instance._container;
            return container.Resolve(serviceType);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _container.Dispose();
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
