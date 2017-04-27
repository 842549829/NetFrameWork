using System.Data;
using System.IO;
using System.Web.Mvc;

namespace Mvc.Extension
{
    /// <summary>
    /// 下载
    /// </summary>
    public class Download
    {
        #region 下载
        /// <summary>
        /// 下载excel文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="dataTable">数据源</param>
        /// <returns>FileResult</returns>
        protected FileResult DownloadExcel(string fileName, DataTable dataTable)
        {
            MemoryStream stream = Common.Code.Download.BuildToExcel(dataTable);
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "application/vnd.ms-excel") { FileDownloadName = fileName };
        }

        /// <summary>
        /// 下载csv文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="dataTable">数据源</param>
        /// <returns>FileResult</returns>
        protected FileResult DownloadCsv(string fileName, DataTable dataTable)
        {
            MemoryStream stream = Common.Code.Download.BuildToCsv(dataTable);
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "application/vnd.ms-excel") { FileDownloadName = fileName };
        }
        #endregion
    }
}
