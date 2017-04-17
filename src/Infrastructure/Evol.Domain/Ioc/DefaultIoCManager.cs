﻿using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Evol.Domain.Ioc
{
    public class DefaultIoCManager : IIoCManager, IDisposable
    {
        public IServiceCollection Container { get; }

        public IServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceScope != null)
                    return _serviceScope.ServiceProvider;
                return _serviceProviderthunk.Invoke();
            }
        }

        private Func<IServiceProvider> _serviceProviderthunk;

        public DefaultIoCManager(IServiceCollection serviceCollection, Func<IServiceProvider> serviceProviderthunk)
        {
            Container = serviceCollection;
            _serviceProviderthunk = serviceProviderthunk;
        }

        /// <summary>
        /// Resolve dependency injection service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetService<T>()
        {
            return ServiceProvider.GetService<T>();
        }

        public IEnumerable<T> GetServices<T>()
        {
            return ServiceProvider.GetServices<T>();
        }

        private IServiceScope _serviceScope;

        public void Dispose()
        {
            if(_serviceScope != null)
            _serviceScope.Dispose();
        }
    }
}
