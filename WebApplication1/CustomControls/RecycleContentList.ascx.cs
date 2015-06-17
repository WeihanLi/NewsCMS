using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.CustomControls
{
    public partial class RecycleContentList : System.Web.UI.UserControl
    {
        private IEnumerable dataSource;
        private static int pageIndex;
        private int pageSize = 10;

        public IEnumerable DataSource
        {
            get
            {
                return dataSource;
            }

            set
            {
                dataSource = value;
            }
        }

        public static int PageIndex
        {
            get
            {
                return pageIndex;
            }

            set
            {
                pageIndex = value;
            }
        }

        public int PageSize
        {
            get
            {
                return pageSize;
            }

            set
            {
                pageSize = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                pageIndex = 1;
                ControlsDataBind();
            }
        }

        private void LoadData()
        {
            dataSource = CommonNews.Helper.OperateContext.Current.LoadNews(pageIndex, pageSize, n=>n.IsDel==true,s=>s.NewsId, false);
        }

        private void ControlsDataBind()
        {
            PagedDataSource pds = new PagedDataSource();
            if (dataSource == null)
            {
                LoadData();
            }
            pds.DataSource = dataSource;
            if (pds.Count > 0)
            {
                pds.AllowPaging = true;
                pds.PageSize = 10;
                pds.CurrentPageIndex = pageIndex - 1;
                lblCount.Text = pds.PageCount.ToString();
                lblIndex.Text = pageIndex.ToString();
                if (pageIndex == 1)
                {
                    btnUp.Enabled = false;
                }
                else
                {
                    btnUp.Enabled = true;
                }
                if (pageIndex == pds.PageCount)
                {
                    btnNext.Enabled = false;
                }
                else
                {
                    btnNext.Enabled = true;
                }
                repDataList.DataSource = pds;
                repDataList.DataBind();
            }
            else
            {
                lblTip.Text = "暂无数据 -_-";
                lblTip.Visible = true;
            }
        }

        protected void btnUp_Click(object sender, EventArgs e)
        {
            pageIndex -= 1;
            ControlsDataBind();
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            pageIndex += 1;
            ControlsDataBind();
        }
    }
}