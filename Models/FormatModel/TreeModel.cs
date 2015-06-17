using System;
using System.Collections.Generic;

namespace Models.FormatModel
{
    public class TreeModel
    {
        //节点id
        public int id { get; set; }

        //节点显示的文本
        public string text { get; set; }

        //open 、closed
        public string state { get { return "closed"; } }

        //子元素
        public List<TreeModel> children { get; set; }

        /// <summary>
        /// 是否有子节点
        /// </summary>
        public bool HasChildren
        {
            get
            {
                return children != null;
            }
        }

        /// <summary>
        /// 是否已经包含节点
        /// </summary>
        /// <param name="id">节点id</param>
        /// <returns></returns>
        public bool Contains(int id)
        {
            foreach (TreeModel item in children)
            {
                if (item.id==id)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 是否已经包含节点
        /// </summary>
        /// <param name="tree">节点树模型</param>
        /// <returns></returns>
        public bool Contains(TreeModel tree)
        {
            foreach (TreeModel item in children)
            {
                if (item.id == tree.id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
