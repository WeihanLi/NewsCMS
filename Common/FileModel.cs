using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class FileModel
    {
        private string fileName;
        private string fileNameWithoutExtension;
        private string filePath;
        private DateTime createTime;
        private long fileSize;
        private string fileExtension;

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName
        {
            get
            {
                return fileName;
            }

            set
            {
                fileName = value;
            }
        }

        /// <summary>
        /// 文件创建或最后一次修改时间
        /// </summary>
        public DateTime CreateTime
        {
            get
            {
                return createTime;
            }

            set
            {
                createTime = value;
            }
        }

        /// <summary>
        /// 文件大小(KB)
        /// </summary>
        public long FileSize
        {
            get
            {
                return fileSize;
            }

            set
            {
                fileSize = value;
            }
        }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string FileExtension
        {
            get
            {
                return fileExtension;
            }

            set
            {
                fileExtension = value;
            }
        }
        /// <summary>
        /// 文件名不带后缀
        /// </summary>
        public string FileNameWithoutExtension
        {
            get
            {
                return System.IO.Path.GetFileNameWithoutExtension(filePath);
            }
        }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath
        {
            get
            {
                return filePath;
            }

            set
            {
                filePath = value;
            }
        }
    }

    public enum FileType
    {
        css=0,js=1,content=2,category=3
    }
}
