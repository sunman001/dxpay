namespace TOOL.Sql
{
    public static class WhereBuilder
    {
        /// <summary>
        /// 创建一个储存查询条件集合的容器
        /// </summary>
        /// <returns></returns>
        public static IWhereContainer CreateContainer()
        {
            return new WhereContainer();
        }
    }
}
