using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DI_04.Interface
{
    /// <summary>
    /// 文件管理服务
    /// </summary>
    public interface IFileManager
    {
        /// <summary>
        /// 显示结构
        /// </summary>
        /// <param name="render">缩进的层级和目录/文件的名称</param>
        void ShowStructure(Action<int, string> render);

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<string> ReadAllTestAsync(string path);
    }
}
