using JMP.TOOL;

namespace TOOL.Extensions
{
    /// <summary>
    /// 对象复制类
    /// </summary>
    public static class ObjectCopierExtensions
    {
        /// <summary>
        /// 深拷贝一个实体对象
        /// </summary>
        /// <typeparam name="T">需要拷贝的对象类型</typeparam>
        /// <param name="source">需要拷贝的原实体对象</param>
        /// <returns>拷贝的实体副本</returns>
        public static T Clone<T>(this T source)
        {
            //if (!typeof(T).IsSerializable)
            //{
            //    throw new ArgumentException("The type must be serializable.", "source");
            //}

            if (ReferenceEquals(source, null))
            {
                return default(T);
            }

            //IFormatter formatter = new BinaryFormatter();
            //Stream stream = new MemoryStream();
            //using (stream)
            //{
            //    formatter.Serialize(stream, source);
            //    stream.Seek(0, SeekOrigin.Begin);
            //    return (T)formatter.Deserialize(stream);
            //}

            var serilize = JsonHelper.Serialize(source);
            return JsonHelper.Deserialize<T>(serilize);
        }
    }
}