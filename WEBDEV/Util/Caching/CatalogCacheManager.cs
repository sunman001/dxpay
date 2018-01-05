using JMP.TOOL;
using System.Collections.Generic;
using System.Linq;

namespace WEBDEV.Util.Caching
{
    /// <summary>
    /// 分类目录缓存管理类
    /// </summary>
    public class CatalogCacheManager
    {
        /// <summary>
        /// 缓存键
        /// </summary>
        private static readonly string CacheKey = "c_k_help_catalog_list";
        /// <summary>
        /// 查找所有可用的分类集合
        /// </summary>
        /// <returns></returns>
        public static List<JMP.MDL.jmp_Help_Classification> FindAllEnabledList()
        {
            List<JMP.MDL.jmp_Help_Classification> list;
            if (CacheHelper.IsCache(CacheKey))
            {
                list = CacheHelper.GetCaChe<List<JMP.MDL.jmp_Help_Classification>>(CacheKey);
            }
            else
            {
                list = LoadData();
                SetCache(list);
            }
            return list;
        }

        /// <summary>
        /// 查找所有可用的分类根节点集合
        /// </summary>
        /// <returns></returns>
        public static List<JMP.MDL.jmp_Help_Classification> FindAllEnabledRoots()
        {
            return FindAllEnabledList().Where(x => x.ParentID == 0).OrderBy(x=>x.Sort).ThenBy(x=>x.ID).ToList();
        }

        /// <summary>
        /// 根据ID查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static JMP.MDL.jmp_Help_Classification FindById(int id)
        {
            return FindAllEnabledList().FirstOrDefault(x=>x.ID==id);
        }

        /// <summary>
        /// 根据父ID查找所有可用的分类集合
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public static List<JMP.MDL.jmp_Help_Classification> FindListByParentId(int parentId)
        {
            return FindAllEnabledList().Where(x => x.ParentID == parentId).OrderBy(x => x.Sort).ThenBy(x => x.ID).ToList();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <returns></returns>
        private static List<JMP.MDL.jmp_Help_Classification> LoadData()
        {
            var list = new JMP.BLL.jmp_Help_Classification().FindAllEnabledHelpList().OrderBy(x => x.Sort).ThenBy(x => x.ID).ToList();
            return list;
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="list"></param>
        private static void SetCache(List<JMP.MDL.jmp_Help_Classification> list)
        {
            CacheHelper.UpdateCacheObjectLocak(list, CacheKey, 60);
        }

        /// <summary>
        /// 刷新缓存
        /// </summary>
        public static void Refresh()
        {
            SetCache(LoadData());
        }
    }
}