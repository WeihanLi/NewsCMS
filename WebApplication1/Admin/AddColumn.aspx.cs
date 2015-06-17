using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.Admin
{
    public partial class AddColumn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ControlsDataBind();
        }

        //private void ControlsDataBind()
        //{
        //    tvCategory.Nodes.Clear();
        //    List<Models.Category> types = CommonNews.Helper.OperateContext.Current.LoadNewsTypes();
        //    IEnumerable<Models.Category> rootNodes = types.Where(t => t.ParentId == 0);
        //    TreeNode node = null;
        //    foreach (Models.Category item in rootNodes)
        //    {
        //        node = new TreeNode(item.CategoryName, item.CategoryId.ToString());
        //        if (tvCategory.Nodes.Contains(node))
        //        {
        //            continue;
        //        }
        //        IEnumerable<Models.Category> ts = types.Where(t => t.ParentId == item.CategoryId);
        //        AddNodesToTree(ts, node, 0);
        //    }
        //}

        //private void AddNodesToTree(IEnumerable<Models.Category> category,TreeNode node,int level)
        //{
        //    TreeNode childNode = null;
        //    foreach (Models.Category c in category)
        //    {
        //        childNode = new TreeNode(c.CategoryName, c.CategoryId.ToString());
        //        if (tvCategory.Nodes.Contains(childNode))
        //        {
        //            continue;
        //        }
        //        node.ChildNodes.Add(childNode);
        //        AddNodesToTree(category.Where(t => t.CategoryId == c.ParentId),childNode,level+1);
        //    }
        //    tvCategory.Nodes.Add(node);
        //}

        //protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        //{
        //    if (String.IsNullOrEmpty(txtColumn.Text))
        //    {
        //        return;
        //    }
        //    //Todo:添加二级目录
        //    Models.Category c = new Models.Category() { CategoryName = txtColumn.Text,ParentId = int.Parse(tvCategory.SelectedValue) };
        //    if (CommonNews.Helper.OperateContext.Current.AddNewsType(c))
        //    {
        //        Response.Redirect("default.aspx");
        //    }
        //    else
        //    {
        //        Response.Write("<script>alert('添加失败，请稍后重试！')</script>");
        //    }
        //}

        //protected void tvCategory_SelectedNodeChanged(object sender, EventArgs e)
        //{
        //    string tip = "你选择的父节点是" + tvCategory.SelectedNode.Text;
        //    lblParentNode.Text = tip;
        //    tvCategory.SelectedNode.Checked = true;
        //}

        //protected void btnCancelParent_Click(object sender, EventArgs e)
        //{
        //    lblParentNode.Text = "";
        //}
    }
}