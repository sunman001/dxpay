using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Configuration;

namespace DxPay.Factory
{
    /// <summary>
    /// 实例工厂类
    /// </summary>
    public class InstanceFactory
    {

        /// <summary>
        /// 保存仓库层和服务层中各实体类型的数据字典
        /// </summary>
        private static readonly Dictionary<string, Type> DictType = new Dictionary<string, Type>();

        /// <summary>
        /// 保存仓库层和服务层中各实例对象的数据字典
        /// </summary>
        private static readonly Dictionary<string, object> DictInstances = new Dictionary<string, object>();

        static InstanceFactory()
        {
            Init();
        }

        private static void Init()
        {
            //new List<string> { "DxPay.Services.dll", "DxPay.Repositories.dll" };
            var includeDlls = WebConfigurationManager.AppSettings["IncludeInstanceFactoryDll"].Split(',').ToList(); 
            
            var asms = AssemblyLocator.GetBinFolderAssemblies().Where(x => x.Modules.Any(m => includeDlls.Exists(i => i == m.Name))).ToList();
            var cls = asms
                .SelectMany(t => t.GetTypes())
                .Where(t => { return t.Namespace != null && !t.IsAbstract && !t.IsInterface && (t.IsClass); }).ToList();

            cls.ForEach(x =>
            {
                if (!DictType.ContainsKey(x.Name))
                {
                    DictType[x.Name] = x;
                }
            });
        }


        public static TService GetServiceInstance<TService>()
        {
            return CreateServiceInstance<TService>();
        }

        private static TService CreateServiceInstance<TService>()
        {
            var serviceType = typeof(TService);
            var serviceDictKey = serviceType.Name;

            if (DictInstances.ContainsKey(serviceDictKey))
            {
                return (TService)DictInstances[serviceDictKey];
            }
            var key = Regex.Replace(serviceDictKey, "Service$", "Repository");
            var repoType = DictType[key];
            var repo = CreateRepositoryInstance(repoType);

            var service = (TService)Activator.CreateInstance(typeof(TService), repo);
            DictInstances[serviceDictKey] = service;
            return service;
        }

        private static object CreateRepositoryInstance(Type type)
        {
            var key = type.Name;
            if (DictInstances.ContainsKey(key))
            {
                return DictInstances[key];
            }
            var repo = Activator.CreateInstance(type);
            DictInstances[key] = repo;
            return repo;
        }
    }
}