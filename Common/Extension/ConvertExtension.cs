using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Common.Extension
{
    /// <summary>
    /// 类型转化扩展
    /// </summary>
    public static class ConvertExtension
    {
        /// <summary>
        /// The convert helper.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="conversionType">
        /// The conversion type.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public static object ConvertHelper(object value, Type conversionType)
        {
            Type nullableType = Nullable.GetUnderlyingType(conversionType);

            // 判断当前类型是否可为 null
            if (nullableType != null)
            {
                if (value == DBNull.Value)
                {
                    return null;
                }

                // 若是枚举 则先转换为枚举
                if (nullableType.IsEnum)
                {
                    value = Enum.Parse(nullableType, value.ToString());
                }

                return Convert.ChangeType(value, nullableType);
            }

            if (conversionType.IsEnum)
            {
                return Enum.Parse(conversionType, value.ToString());
            }

            return Convert.ChangeType(value, conversionType);
        }

        /// <summary>
        /// The convert to decimal null.
        /// </summary>
        /// <param name="targetObj">
        /// The target obj.
        /// </param>
        /// <returns>
        /// The <see cref="decimal"/>.
        /// </returns>
        public static decimal? ConvertToDecimalNull(object targetObj)
        {
            if (targetObj == null || targetObj == DBNull.Value)
            {
                return null;
            }

            return Convert.ToDecimal(targetObj);
        }

        /// <summary>
        /// The convert to int null.
        /// </summary>
        /// <param name="targetObj">
        /// The target obj.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int? ConvertToIntNull(object targetObj)
        {
            if (targetObj == null || targetObj == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt32(targetObj);
        }

        /// <summary>
        /// The convert to string.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ConvertToString(object obj)
        {
            return obj == null ? string.Empty : obj.ToString();
        }

        /// <summary>
        /// 将泛类型集合List类转换成DataTable
        /// </summary>
        /// <param name="entitys">泛类型集合</param>
        /// <typeparam name="T">T</typeparam>
        /// <returns>DataTable</returns>
        public static DataTable ListToDataTable<T>(List<T> entitys)
        {
            // 检查实体集合不能为空
            if (entitys == null || entitys.Count < 1)
            {
                throw new System.Exception("需转换的集合为空");
            }

            // 取出第一个实体的所有Propertie
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            // 生成DataTable的structure
            // 生产代码中，应将生成的DataTable结构Cache起来，此处略
            DataTable dt = new DataTable();
            foreach (PropertyInfo t in entityProperties)
            {
                // dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);
                dt.Columns.Add(t.Name);
            }

            // 将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                // 检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new System.Exception("要转换的集合元素类型不一致");
                }

                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);
                }

                dt.Rows.Add(entityValues);
            }

            return dt;
        }
    }
}
