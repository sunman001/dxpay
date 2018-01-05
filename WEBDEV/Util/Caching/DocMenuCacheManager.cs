using JMP.Model.Query.Doc;
using JMP.TOOL;
using System.Collections.Generic;
using System.Linq;

namespace WEBDEV.Util.Caching
{
    /// <summary>
    /// 开发者中心缓存管理类
    /// </summary>
    public class DocMenuCacheManager
    {
        /// <summary>
        /// 缓存键
        /// </summary>
        private static readonly string CacheKey = "c_k_doc_menu_list";

        /// <summary>
        /// 查找所有可用的数据集合
        /// </summary>
        /// <returns></returns>
        public static List<DocMenuQueryModel> FindAllEnabledList()
        {
            List<DocMenuQueryModel> list;
            if (CacheHelper.IsCache(CacheKey))
            {
                list = CacheHelper.GetCaChe<List<DocMenuQueryModel>>(CacheKey);
            }
            else
            {
                list = LoadData();
                SetCache(list);
            }
            return list;
        }

        /// <summary>
        /// 查找所有可用的根节点集合
        /// </summary>
        /// <returns></returns>
        public static List<DocMenuQueryModel> FindAllEnabledRoots()
        {
            return FindAllEnabledList().Where(x => x.ParentID == 0).OrderBy(x => x.Sort).ThenBy(x => x.ID).ToList();
        }

        /// <summary>
        /// 查找所有的可用的二级节点集合
        /// </summary>
        /// <returns></returns>
        public static List<DocMenuQueryModel> FindAllEnabledSecondChildList()
        {
            return FindAllEnabledList().Where(x => x.ParentID > 0 && x.ArticleId == 0).OrderBy(x => x.Sort).ThenBy(x => x.ID).ToList();
        }

        /// <summary>
        /// 根据ID查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DocMenuQueryModel FindById(int id)
        {
            return FindAllEnabledList().FirstOrDefault(x => x.ID == id);
        }

        /// <summary>
        /// 根据父ID查找所有可用的分类集合
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public static List<DocMenuQueryModel> FindListByParentId(int parentId)
        {
            return FindAllEnabledList().Where(x => x.ParentID == parentId).OrderBy(x => x.Sort).ThenBy(x => x.ID).ToList();
        }

        /// <summary>
        /// 根据ClassId查找所有集合
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public static List<DocMenuQueryModel> FindListByClassId(int classId)
        {
            return FindAllEnabledList().Where(x => x.ClassId == classId).OrderBy(x => x.Sort).ThenBy(x => x.ID).ToList();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <returns></returns>
        private static List<DocMenuQueryModel> LoadData()
        {
            var list = new JMP.BLL.jmp_Help_Classification().FindAllEnabledDocList().OrderBy(x => x.Sort).ThenBy(x => x.ID).ToList();
            return list;
        }

        /// <summary>
        /// 设置缓存数据
        /// </summary>
        /// <param name="list"></param>
        private static void SetCache(List<DocMenuQueryModel> list)
        {
            CacheHelper.UpdateCacheObjectLocak(list, CacheKey, 60);
        }

        /// <summary>
        /// 刷新缓存数据
        /// </summary>
        public static void Refresh()
        {
            SetCache(LoadData());
        }
    }
}