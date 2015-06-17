using System;
using System.ComponentModel.DataAnnotations;
using Models.ViewModel;

namespace Models
{
    /// <summary>
    /// 新闻表
    /// </summary>
    public class News
    {
        private int newsId;
        private int newsTypeId;
        private string newsTitle;
        private string newsContent;
        private string newsPath;
        private string newsPublisher;
        private string newsImagePath=null;
        private string newsAttachPath=null;
        private string newsExternalLink=null;
        private int newsVisitCount=0;
        private DateTime publishTime;
        private bool isTop = false;
        private bool isDel = false;

        /// <summary>
        /// 新闻id
        /// </summary>
        [Key]
        public int NewsId
        {
            get
            {
                return newsId;
            }

            set
            {
                newsId = value;
            }
        }

        /// <summary>
        /// 新闻类别id
        /// </summary>
        public int NewsTypeId
        {
            get
            {
                return newsTypeId;
            }

            set
            {
                newsTypeId = value;
            }
        }

        /// <summary>
        /// 新闻标题
        /// </summary>
        public string NewsTitle
        {
            get
            {
                return newsTitle;
            }

            set
            {
                newsTitle = value;
            }
        }

        /// <summary>
        /// 新闻内容
        /// </summary>
        public string NewsContent
        {
            get
            {
                return newsContent;
            }

            set
            {
                newsContent = value;
            }
        }

        /// <summary>
        /// 新闻内容路径
        /// </summary>
        public string NewsPath
        {
            get
            {
                return newsPath;
            }

            set
            {
                newsPath = value;
            }
        }

        /// <summary>
        /// 新闻发布人
        /// </summary>
        public string NewsPublisher
        {
            get
            {
                return newsPublisher;
            }

            set
            {
                newsPublisher = value;
            }
        }

        /// <summary>
        /// 新闻图片路径
        /// </summary>
        public string NewsImagePath
        {
            get
            {
                return newsImagePath;
            }

            set
            {
                newsImagePath = value;
            }
        }

        /// <summary>
        /// 新闻附件路径
        /// </summary>
        public string NewsAttachPath
        {
            get
            {
                return newsAttachPath;
            }

            set
            {
                newsAttachPath = value;
            }
        }

        /// <summary>
        /// 新闻外链
        /// </summary>
        public string NewsExternalLink
        {
            get
            {
                return newsExternalLink;
            }

            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    return;
                }
                if (value.StartsWith("http://") || value.StartsWith("https://"))
                {
                    newsExternalLink = value;
                }
                else
                {
                    newsExternalLink = "http://" + value;
                }
                //设置为newsPath
                newsPath = newsExternalLink;
            }
        }

        /// <summary>
        /// 新闻访问次数
        /// </summary>
        public int NewsVisitCount
        {
            get
            {
                return newsVisitCount;
            }

            set
            {
                newsVisitCount = value;
            }
        }

        /// <summary>
        /// 新闻发布时间
        /// </summary>
        public DateTime PublishTime
        {
            get
            {
                return publishTime;
            }

            set
            {
                publishTime = value;
            }
        }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTop
        {
            get
            {
                return isTop;
            }

            set
            {
                isTop = value;
            }
        }

        /// <summary>
        /// 是否被删除
        /// </summary>
        public bool IsDel
        {
            get
            {
                return isDel;
            }

            set
            {
                isDel = value;
            }
        }
    }
}
