using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Linq;

namespace BlogProject.Service.Models
{
    public sealed class DependencyResolver : IDependencyResolver
    {
        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
            // When BeginScope returns 'this', the Dispose method must be a no-op.
        }

        public object GetService(Type serviceType)
        {
            try
            {
                var baseType = typeof(ApiController);
                if (baseType.IsAssignableFrom(serviceType))
                {
                    var repositoryAssembly = Assembly.Load(new AssemblyName("BlogProject.Repository"));
                    var ctorInfos = serviceType.GetConstructors();
                    var paramsInfos = ctorInfos.FirstOrDefault().GetParameters();
                    var objs = new List<object>();
                    foreach (var item in paramsInfos)
                    {
                        var paramRealType = repositoryAssembly.ExportedTypes.FirstOrDefault(t => item.ParameterType.IsAssignableFrom(t));

                        objs.Add(Activator.CreateInstance(paramRealType));
                    }
                    return ctorInfos.FirstOrDefault().Invoke(objs.ToArray());
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new List<object>();
        }
    }
}