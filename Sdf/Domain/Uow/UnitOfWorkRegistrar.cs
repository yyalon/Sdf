using Autofac.Builder;
using Autofac.Extras.DynamicProxy;
using Autofac.Features.Scanning;
using Sdf.Common;
using Sdf.Core;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Sdf.Domain.Uow
{
    public class UnitOfWorkRegistrar
    {
        public static void Initialize(List<RegistrationStore> registrations)
        {

            foreach (var registrationStore in registrations)
            {

                var registrationType = registrationStore.Registration.GetType();
                var genericArguments = registrationType.GetGenericArguments();
                if (genericArguments.Length == 3)
                {
                    var activatorDataType = genericArguments[1];
                    if (activatorDataType.IsSubclassOfOrInherit(typeof(ConcreteReflectionActivatorData)))
                    {
                        var registration = registrationStore.Registration as IRegistrationBuilder<object, ConcreteReflectionActivatorData, object>;
                        if (IsUnitOfWorkType(registrationStore.TImp.GetTypeInfo()))
                        {
                            if (registrationStore.IsCanClassInterceptors)
                            {
                                registration.EnableClassInterceptors().InterceptedBy(typeof(UowInterceptor));
                            }
                            else
                            {
                                registration.EnableInterfaceInterceptors().InterceptedBy(typeof(UowInterceptor));
                            }
                        }
                    }
                    else if (activatorDataType.IsSubclassOfOrInherit(typeof(ScanningActivatorData)))
                    {
                        var registration = registrationStore.Registration as IRegistrationBuilder<object, ScanningActivatorData, object>;
                        if (IsUnitOfWorkType(registrationStore.TImp.GetTypeInfo()))
                        {
                            if (registrationStore.IsCanClassInterceptors)
                            {
                                registration.EnableClassInterceptors().InterceptedBy(typeof(UowInterceptor));
                            }
                            else
                            {
                                registration.EnableInterfaceInterceptors().InterceptedBy(typeof(UowInterceptor));
                            }
                        }
                    }
                    else
                    {
                        var registration = registrationStore.Registration as IRegistrationBuilder<object, object, object>;
                        if (IsUnitOfWorkType(registrationStore.TImp.GetTypeInfo()))
                        {
                            registration.EnableInterfaceInterceptors().InterceptedBy(typeof(UowInterceptor));
                        }
                    }
                }
            }
        }
        private static bool IsUnitOfWorkType(TypeInfo implementationType)
        {
            var uowAttribute = implementationType.GetCustomAttribute<UowAttribute>();
            if (uowAttribute != null)
            {
                return uowAttribute.Enabled;
            }
            return IsImplIUowProxy(implementationType);

        }
        private static bool IsImplIUowProxy(TypeInfo implementationType)
        {
            var interFaces = implementationType.ImplementedInterfaces;
            foreach (var item in interFaces)
            {
                if (item.AssemblyQualifiedName == typeof(IUowProxy).AssemblyQualifiedName)
                    return true;
            }
            return false;
        }
    }
}
