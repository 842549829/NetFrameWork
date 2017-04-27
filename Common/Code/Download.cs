using System.Data;
using System.IO;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace Common.Code
{
    /// <summary>
    /// 下载
    /// </summary>
    public class Download
    {
        #region File

        /// <summary>
        /// 生成Excel
        /// </summary>
        /// <param name="table">DataTable</param>
        /// <returns>MemoryStream</returns>
        public static MemoryStream BuildToExcel(DataTable table)
        {
            MemoryStream ms = new MemoryStream();

            using (table)
            {
                using (IWorkbook workbook = new HSSFWorkbook())
                {
                    using (ISheet sheet = workbook.CreateSheet())
                    {
                        IRow headerRow = sheet.CreateRow(0);

                        // handling header.
                        foreach (DataColumn column in table.Columns)
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue(column.Caption);
                        }

                        // handling value.
                        int rowIndex = 1;

                        foreach (DataRow row in table.Rows)
                        {
                            IRow dataRow = sheet.CreateRow(rowIndex);

                            foreach (DataColumn column in table.Columns)
                            {
                                dataRow.CreateCell(column.Ordinal, CellType.STRING).SetCellValue(row[column].ToString());
                            }

                            rowIndex++;
                        }

                        AutoSizeColumns(sheet);
                        workbook.Write(ms);
                        ms.Flush();
                        ms.Position = 0;
                    }
                }
            }

            return ms;
        }

        /// <summary>
        /// 生成CSV
        /// </summary>
        /// <param name="table">DataTable</param>
        /// <returns>MemoryStream</returns>
        public static MemoryStream BuildToCsv(DataTable table)
        {
            Encoding encode = Encoding.GetEncoding("gb2312");
            StringBuilder str = new StringBuilder();
            if (table != null && table.Columns.Count > 0 && table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    str.Append(table.Columns[i].ColumnName.Replace("\"", "\"\""));
                    if (i < table.Columns.Count - 1)
                    {
                        str.Append(",");
                    }
                }

                foreach (DataRow item in table.Rows)
                {
                    str.Append("\r\n");
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        if (item[i] != null)
                        {
                            str.Append("'").Append(item[i].ToString().Replace("\"", "\"\""));
                        }

                        if (i < table.Columns.Count - 1)
                        {
                            str.Append(",");
                        }
                    }
                }
            }

            MemoryStream stream = new MemoryStream(encode.GetBytes(str.ToString()));
            return stream;
        }

        /// <summary>
        /// 自动设置Excel列宽
        /// </summary>
        /// <param name="sheet">Excel表</param>
        private static void AutoSizeColumns(ISheet sheet)
        {
            if (sheet.PhysicalNumberOfRows > 0)
            {
                IRow headerRow = sheet.GetRow(0);

                for (int i = 0, l = headerRow.LastCellNum; i < l; i++)
                {
                    sheet.AutoSizeColumn(i);
                }
            }
        }

        #endregion
    }
}
