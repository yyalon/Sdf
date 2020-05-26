using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Core
{
    public interface IRegister
    {

        #region Transient
        void RegisterTransient<TService, TImpl>(string name = null) where TService : class where TImpl : class, TService;
        void RegisterTransient(Type serviceType, Type impType, string name = null);
        void RegisterTransient(Type impType, string name = null);
        void RegisterTransient<TImpl>(string name = null) where TImpl : class;
        void RegisterTransient<TImpl>(TImpl instance, string name = null) where TImpl : class;
        void RegisterTransient<TService, TImpl>(Func<IResolver, TImpl> func, string name = null) where TImpl : class;
        void RegisterTransient<TImpl>(Func<IResolver, TImpl> func, string name = null) where TImpl : class;
        void RegisterGenericTransient(Type serviceType, Type impType, string name = null);
        #endregion

        #region Singleton
        void RegisterSingleton<TService, TImpl>(string name = null) where TService : class where TImpl : class, TService;
        void RegisterSingleton(Type serviceType, Type impType, string name = null);
        void RegisterSingleton<TImpl>(string name = null) where TImpl : class;
        void RegisterSingleton<TImpl>(TImpl instance, string name = null) where TImpl : class;
        void RegisterSingleton(Type tImpl, string name = null);
        void RegisterSingleton<TService, TImpl>(Func<IResolver, TImpl> func, string name = null) where TImpl : class;
        void RegisterSingleton<TImpl>(Func<IResolver, TImpl> func, string name = null) where TImpl : class;
        void RegisterGenericSingleton(Type serviceType, Type impType, string name = null);
        #endregion

        #region PreWebRequest
        void RegisterPreWebRequest<TService, TImpl>(string name = null) where TService : class where TImpl : class, TService;
        void RegisterPreWebRequest<TImpl>(TImpl instance, string name = null) where TImpl : class;
        void RegisterPreWebRequest<TImpl>(string name = null) where TImpl : class;
        void RegisterPreWebRequest(Type serviceType, Type impType, string name = null);
        void RegisterPreWebRequest(Type impType, string name = null);
        void RegisterPreWebRequest<TService, TImpl>(Func<IResolver, TImpl> func, string name = null) where TImpl : class;
        void RegisterPreWebRequest<TImpl>(Func<IResolver, TImpl> func, string name = null) where TImpl : class;
        void RegisterGenericPreWebRequest(Type serviceType, Type impType, string name = null); 
        #endregion

        bool IsRegisted<TService>(string name = null) where TService : class;
    }
}
