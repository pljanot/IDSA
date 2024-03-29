﻿using System.Collections.Generic;

namespace IDSA.OldServiceLocator
{
    /// <summary>
    /// ... OBSOLETE INTERFACE ...
    /// </summary>
    interface IServiceLocator
    {
        void Register<T>(T svc);
        T Resolve<T>() where T : class;
    }

    /// <summary>
    /// ... OBSOLETE CLASS ...
    /// </summary>
    class ServiceLocator : IServiceLocator
    {
        static ServiceLocator()
        {
            _instance = new ServiceLocator();
        }
        private static readonly IServiceLocator _instance;
        public static IServiceLocator Instance
        {
            get { return _instance; }
        }

        private readonly Dictionary<object, object> _services = new Dictionary<object, object>();

        public void Register<T>(T svc)
        {
            _services.Add(typeof(T), svc);
        }

        public T Resolve<T>() where T : class
        {
            return _services[typeof(T)] as T;
        }
    }
}
