using Models.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Category
    {
        private int categoryId;
        private string categoryName;
        private string indexName;
        private int parentId = 0;

        /// <summary>
        ///  目录id
        /// </summary>
        [Key]
        public int CategoryId
        {
            get
            {
                return categoryId;
            }

            set
            {
                categoryId = value;
            }
        }

        /// <summary>
        /// 目录名称
        /// </summary>
        public string CategoryName
        {
            get
            {
                return categoryName;
            }

            set
            {
                categoryName = value;
            }
        }

        /// <summary>
        /// 目录索引名称
        /// </summary>
        public string IndexName
        {
            get
            {
                return indexName;
            }

            set
            {
                indexName = value;
            }
        }

        /// <summary>
        /// 父节点id
        /// </summary>
        public int ParentId
        {
            get
            {
                return parentId;
            }

            set
            {
                parentId = value;
            }
        }
    }
}