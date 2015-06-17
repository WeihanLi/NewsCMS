using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.CustomControls
{
    public partial class ImageSlider : System.Web.UI.UserControl
    {
        private IEnumerable dataSource;

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ControlsDataBind();
            }
        }

        private void LoadData()
        {
            dataSource = CommonNews.Helper.OperateContext.Current.LoadNews(1,7,n=>n.NewsImagePath!=null,n=>n.NewsId,false);
        }

        private void ControlsDataBind()
        {
            if (dataSource == null)
            {
                LoadData();
            }
            repTexts.DataSource = dataSource;
            repTexts.DataBind();
            repImages.DataSource = dataSource;
            repImages.DataBind();
        }
    }
}