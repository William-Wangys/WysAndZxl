using DI_04.Interface;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DI_04.Service
{
    /// <summary>
    /// 文件管理服务
    /// </summary>
    public class FileManager : IFileManager
    {
        /// <summary>
        /// 文件提供程序
        /// </summary>
        private readonly IFileProvider _fileProvider;

        public FileManager(IFileProvider fileProvider) => _fileProvider = fileProvider;

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string> ReadAllTestAsync(string path)
        {
            byte[] buffer;
            using (var strtam = _fileProvider.GetFileInfo(path).CreateReadStream())
            {
                buffer = new byte[strtam.Length];
                await strtam.ReadAsync(buffer, 0, buffer.Length);
            }
            return Encoding.Default.GetString(buffer);
        }

        /// <summary>
        /// 显示结构
        /// </summary>
        /// <param name="render">缩进的层级和目录/文件的名称</param>
        public void ShowStructure(Action<int, string> render)
        {
            int indent = -1;
            Render("");
            void Render(string subPath)
            {
                indent++;
                foreach (var fileInfo in _fileProvider.GetDirectoryContents(subPath))
                {
                    render.Invoke(indent, fileInfo.Name);
                    if (fileInfo.IsDirectory)
                    {
                        Render($@"{subPath}\{fileInfo.Name}".TrimStart('\\'));
                    }
                }
            }
        }
    }
}
