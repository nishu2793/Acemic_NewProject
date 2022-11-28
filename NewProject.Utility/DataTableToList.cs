using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Utility
{
    public class DataTableToList
    {
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name.ToUpper() == column.ColumnName.ToUpper())
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        public static List<T> ConvertToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>()
                    .Select(c => c.ColumnName.ToUpper())
                    .ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    //pro.Name.ToUpper() == column.ColumnName.ToUpper()
                    if (columnNames.Contains(pro.Name.ToUpper()))
                    {
                        PropertyInfo pI = objT.GetType().GetProperty(pro.Name);
                        var dbValue = row[pro.Name];
                        if (dbValue != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dbValue)))
                        {
                            if (Nullable.GetUnderlyingType(pro.PropertyType) != null)
                            {
                                pro.SetValue(objT, Convert.ChangeType(dbValue, Type.GetType(Nullable.GetUnderlyingType(pI.PropertyType).ToString())), null);
                            }
                            else
                            {
                                pro.SetValue(objT, Convert.ChangeType(dbValue, Type.GetType(pI.PropertyType.ToString())), null);
                            }
                        }

                        //pro.SetValue(objT, row[pro.Name] == DBNull.Value ? null : Convert.ChangeType(row[pro.Name], pI.PropertyType));
                    }
                }
                return objT;
            }).ToList();
        }
    }
}

