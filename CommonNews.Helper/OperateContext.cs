using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web;

namespace CommonNews.Helper
{
    public class OperateContext
    {
        #region const 常量
        
        /// <summary>
        /// 用户Session变量值
        /// </summary>
        public const string UserSESSION = "User";
        /// <summary>
        /// 管理员Session变量值
        /// </summary>
        public const string AdminSESSION = "Admin";

       
        //virtual directoryName
        /// <summary>
        /// css目录  ----虚拟路径
        /// </summary>
        public const string CSSDIR = "styles/";
        /// <summary>
        /// js文件目录  ----虚拟路径
        /// </summary>
        public const string JSDIR = "Scripts/";
        /// <summary>
        /// 内容模板目录  ----虚拟路径
        /// </summary>
        public const string ContentTemplateDIR = "templates/contentTemplate/";
        /// <summary>
        /// 栏目页模板路径 ----虚拟路径
        /// </summary>
        public const string CategoryTemplateDIR = "templates/categoryTemplate/";

        #endregion

        private static OperateContext current = null;
        private static Common.LogHelper logger = new Common.LogHelper(typeof(OperateContext));

        //栏目页模板路径---(physical path) Full Path
        private static string categoryTemplatePath = Common.PathHelper.MapPath("templates/categoryTemplate/template.html");
        //内容页模板路径---(physical path) Full Path
        private static string contentTemplatePath = Common.PathHelper.MapPath("templates/contentTemplate/template.html");
        //面包屑各个链接之间的分隔符
        private static string separator = Common.ConfigurationHelper.AppSetting("separator");
        //静态化线程
        //private static Thread threadStatic = null;

        private OperateContext()
        {
            BLLSession = new BLLSessionFactory().GetBLLSession();
            //Spring.Net 获取
            //BLLSession = DI.SpringHelper.GetObject<IBLL.IBLLSession>("BLLSession");
        }

        public IBLL.IBLLSession BLLSession;

        public static OperateContext Current
        {
            get
            {
                if (current == null)
                {
                    current = CallContext.GetData(typeof(OperateContext).Name) as OperateContext;
                    if (current == null)
                    {
                        current = new OperateContext();
                        CallContext.SetData(typeof(OperateContext).Name, current);
                    }
                }
                return current;
            }
        }

        /// <summary>
        /// get the params the the POST input stream
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T RequestParams<T>()
        {
            string postString = null;
            //从请求的数据流中获取请求信息
            using (System.IO.Stream stream = HttpContext.Current.Request.InputStream)
            {
                //创建 byte数组以接受从流中获取到的消息
                byte[] postBytes = new byte[stream.Length];
                //将POST请求中的数据流读入 准备好的 byte数组中
                stream.Read(postBytes, 0, (int)stream.Length);
                //从数据流中获取到字符串
                postString = System.Text.Encoding.UTF8.GetString(postBytes);
                //logger.Debug("接收到的POST字符串："+postString);
            }
            return Common.ConverterHelper.JsonToObject<T>(postString);
        }

        #region 用户相关

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Login(Models.ViewModel.LoginViewModel model)
        {
            //抽象工厂实现方式
            //BLLFactory.BLLAbsFactory fac = BLLFactory.BLLAbsFactory.GetBLLFactory();
            //IBLL.IUser userBLL = fac.GetUser();

            //依赖注入
            //IBLL.IUser userBLL = Current.BLLSession.IUser;
            
            Models.User u = SelectUser(model.UserName);
            if (u==null||u.IsDel)
            {
                return false;
            }
            //if (u.AuthCode != null)
            //{
            //    return Model.LoginState.RequireVerification;
            //}
            //判断 model.RememberMe 的状态
            //Model.LoginState state = BLLSession.IUser.Login(model);
            //判断是否登录成功
            //if (state == Model.LoginState.Success)
            if (BLLSession.IUser.Login(model))
            {
                try
                {
                    //isAdmin condition~~~
                    if (u != null && u.IsAdmin)
                    {
                        HttpContext.Current.Session["Admin"] = u.UserName;
                    }
                    //登录成功，设置Session
                    HttpContext.Current.Session["User"] = u;
                    return true;
                }
                catch (HttpException ex)
                {
                    logger.Error(ex);
                    return false;
                }
                catch (NullReferenceException ex)
                {
                    logger.Error(ex);
                    return false;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return false;
                }
                
                //登录成功并且选中 “记住我” 复选框，则将用户名、密码（需要加密）保存到cookie
                //if (model.RememberMe)
                //{
                //    //创建cookie
                //    HttpCookie cookieUid = new HttpCookie("uid", Common.Helper.EncryptUserInfo(model.Email));
                //    HttpCookie cookiePwd = new HttpCookie("pwd", Common.Helper.EncryptUserInfo(model.Password));
                //    //设置过期时间
                //    cookieUid.Expires = DateTime.Now.AddDays(1);
                //    cookiePwd.Expires = DateTime.Now.AddDays(1);
                //    //将cookie输出到浏览器
                //    HttpContext.Current.Response.Cookies.Add(cookieUid);
                //    HttpContext.Current.Response.Cookies.Add(cookiePwd);
                //}
            }
            //返回登录状态
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddUser(Models.User u)
        {
            //Models.User u = new Models.User() { UserName=model.Name,UserPassword=Common.SecurityHelper.SHA1_Encrypt(model.Password) };
            if (BLLSession.IUser.Add(u)==1)
            {
                //提示激活账户，或添加Session 以登录
                //Model.User u = SelectMember(model.Email);
                //添加 Session
                //HttpContext.Current.Session["Member"] = u;
                //if (SendRegistMail(u))
                //{
                //    return true;
                //}
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断userName是否存在
        /// </summary>
        /// <param name="userName">userName</param>
        /// <returns></returns>
        public bool ExistUserName(string userName)
        {
            return Current.BLLSession.IUser.Exist(m => m.UserName.ToLower() == userName.ToLower());
        }

        /// <summary>
        /// 根据userName查询用户
        /// </summary>
        /// <param name="userName">userName</param>
        /// <returns>用户实体</returns>
        public Models.User SelectUser(string userName)
        {
            return BLLSession.IUser.GetListBy(m => m.UserName == userName).FirstOrDefault();
        }

        /// <summary>
        /// 获取所有用户列表
        /// </summary>
        /// <returns></returns>
        public List<Models.User> SelectUser()
        {
            return BLLSession.IUser.GetList();
        }

        /// <summary>
        /// 获取用户列表分页数据
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex">页码索引</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="whereLambda"></param>
        /// <param name="orderBy"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public List<Models.User> SelectUser<TKey>(int pageIndex, int pageSize, Expression<Func<Models.User, bool>> whereLambda, Expression<Func<Models.User, TKey>> orderBy, bool isAsc)
        {
            return BLLSession.IUser.GetPagedList(pageIndex, pageSize, whereLambda, orderBy, isAsc);
        }

        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="u">用户实体</param>
        /// <returns>是否成功成功</returns>
        public bool UpdateUserPwd(Models.User u)
        {
            //加密
            u.UserPassword = Common.SecurityHelper.SHA1_Encrypt(u.UserPassword);
            return BLLSession.IUser.Modify(u, "UserPassword")==1;
        }

        /// <summary>
        /// 禁用用户
        /// </summary>
        /// <param name="u">用户实体</param>
        /// <returns></returns>
        public bool DisableUser(Models.User u)
        {
            u.IsDel = true;
            return BLLSession.IUser.Modify(u, "IsDel") == 1;
        }

        /// <summary>
        /// 禁用用户
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public bool DisableUser(int userId)
        {
            Models.User u = new Models.User() { UserId = userId,IsDel=true };
            return BLLSession.IUser.Modify(u, "IsDel") == 1;
        }

        /// <summary>
        /// 启用用户
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public bool EnableUser(int userId)
        {
            Models.User u = new Models.User() { UserId = userId, IsDel = false };
            return BLLSession.IUser.Modify(u, "IsDel") == 1;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteUser(Models.User u)
        {
            return BLLSession.IUser.Del(u)==1;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public bool DeleteUser(int userId)
        {
            return BLLSession.IUser.Del(new Models.User() { UserId =userId}) == 1;
        }
        #endregion

        #region 新闻管理

        #region News

        /// <summary>
        /// 获取新闻浏览次数
        /// </summary>
        /// <param name="newsId">新闻id</param>
        /// <returns></returns>
        public int GetNewsVisitCount(int newsId)
        {
            UpdateNewsVisitCount(newsId);
            return BLLSession.INews.GetListBy(m => m.NewsId == newsId).FirstOrDefault().NewsVisitCount;
        }

        /// <summary>
        /// 更新新闻的浏览次数
        /// </summary>
        /// <param name="newsId">新闻id</param>
        /// <returns></returns>
        public bool UpdateNewsVisitCount(int newsId)
        {
            Models.News news = SelectNews(newsId);
            news.NewsVisitCount += 1;
            if (BLLSession.INews.Modify(news, "NewsVisitCount") == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据新闻id获取新闻实体
        /// </summary>
        /// <param name="id">新闻id</param>
        /// <returns></returns>
        public Models.News SelectNews(int id)
        {
            return BLLSession.INews.GetListBy(m => m.NewsId == id).FirstOrDefault();
        }

        /// <summary>
        /// 加载新闻带置顶新闻
        /// </summary>
        /// <returns></returns>
        public List<Models.News> SelectNewsWithTopped()
        {
            //加载新闻带置顶
            List<Models.News> news = BLLSession.INews.GetListBy(n => n.IsTop == true&&n.IsDel==false,n=>n.PublishTime,false);
            news.AddRange(BLLSession.INews.GetListBy(n => n.IsTop == false && n.IsDel == false, n => n.PublishTime, false));
            return news;
        }

        /// <summary>
        /// 加载新闻带置顶新闻
        /// </summary>
        /// <param name="typeId">新闻类别</param>
        /// <returns></returns>
        public List<Models.News> SelectNewsWithTopped(int typeId)
        {
            //加载新闻带置顶
            List<Models.News> news = BLLSession.INews.GetListBy(n => n.IsTop == true&&n.NewsTypeId == typeId && n.IsDel == false, n => n.PublishTime, false);
            news.AddRange(BLLSession.INews.GetListBy(n => n.IsTop == false && n.IsDel == false && n.NewsTypeId == typeId, n => n.PublishTime, false));
            return news;
        }

        public List<Models.News> LoadNewsWithTopped(int pageIndex, int pageSize, int typeId, out int rowsCount)
        {
            List<Models.News> news = typeId > 0 ? SelectNewsWithTopped(typeId) : SelectNewsWithTopped();
            rowsCount = news.Count;
            List<Models.News> data = news.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return data;
        }

        /// <summary>
        /// 获取所有新闻
        /// </summary>
        /// <returns></returns>
        public List<Models.News> SelectNews()
        {
            return BLLSession.INews.GetList();
        }

        /// <summary>
        /// 加载所有分类到树
        /// </summary>
        /// <returns>树模型</returns>
        public Models.FormatModel.TreeModel LoadAllTypesTreeNode()
        {
            List<Models.Category> types = LoadNewsTypes();
            Models.FormatModel.TreeModel treeNodes = null;
            foreach (Models.Category item in (types.Where(t=>t.ParentId==0)))
            {
                if (treeNodes.Contains(item.CategoryId))
                {
                    continue;
                }
                Models.FormatModel.TreeModel tree = new Models.FormatModel.TreeModel() { id = item.CategoryId, text = item.CategoryName };
                AddTypeToTree(types.Where(t => t.ParentId == item.CategoryId), tree);
            }
            return treeNodes;
        }

        /// <summary>
        /// 将某个分类添加到tree的节点中
        /// </summary>
        /// <param name="cs">符合条件的分类信息</param>
        /// <param name="tree">树信息</param>
        private void AddTypeToTree(IEnumerable<Models.Category> cs,Models.FormatModel.TreeModel tree)
        {
            foreach (Models.Category item in cs)
            {
                if (tree.Contains(item.CategoryId))
                {
                    continue;
                }
                Models.FormatModel.TreeModel t= new Models.FormatModel.TreeModel() { id = item.CategoryId, text = item.CategoryName };
                AddTypeToTree(cs.Where(type => type.ParentId == item.CategoryId), t);
            }
        }

        /// <summary>
        /// 添加新闻
        /// </summary>
        /// <param name="news"></param>
        /// <param name="u"></param>
        /// <returns></returns>
        public bool AddNews(Models.News news)
        {
            //设置新闻发布时间--已经在Handler处设置
            //news.PublishTime = DateTime.Now;
            //新闻发布人
            news.NewsPublisher = (HttpContext.Current.Session["User"] as Models.User).UserName;
            try
            {
                if (String.IsNullOrEmpty(news.NewsExternalLink))
                {
                    //news 静态化，设置news 的NewsPath
                    //获取当前时间字符串，构建新闻路径
                    string timeString = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    news.NewsPath = "/News/" + news.NewsTypeId.ToString() + "/" + timeString.Substring(0, 6) + "/" + timeString + ".html";
                    //添加新闻并获取新闻id，统计浏览次数
                    news.NewsId = BLLSession.INews.AddDataWithIdReturn(news);
                    //创建静态文件
                    CreateNewsFile(news);
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return false;
            }
            
        }
        
        /// <summary>
        /// 生成新闻的静态文件
        /// </summary>
        /// <param name="news">新闻实体</param>
        private static void CreateNewsFile(Models.News news)
        {
            try
            {
                //注意防止并发操作
                //写入id，统计浏览次数
                string destPath = Common.PathHelper.MapPath(news.NewsPath);
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("{@@NewsId}", news.NewsId.ToString());
                dic.Add("{@@NewsTitle}", news.NewsTitle);
                dic.Add("{@@NewsContent}", news.NewsContent);
                //Todo: 地址间隔符，可以直接写在配置文件中
                dic.Add("{@@Location}", GetLocation(news.NewsTypeId,separator));
                dic.Add("{@@NewsPublisher}", news.NewsPublisher);
                dic.Add("{@@PublishTime}", news.PublishTime.ToString("yyyy-MM-dd"));
                dic.Add("{@@NewsAttachPath}", news.NewsAttachPath);
                StaticHelper.GenerateStaticContent(contentTemplatePath, dic, destPath);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        /// <summary>
        /// 生成栏目页静态页
        /// </summary>
        /// <param name="type">栏目实体</param>
        private static void CreateListFile(Models.Category type)
        {
            string destPath = Common.PathHelper.MapPath("News/"+type.CategoryId.ToString()+"/index.html");
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("{@@CategoryId}", type.CategoryId.ToString());
            dic.Add("{@@CategoryName}", type.CategoryName);
            dic.Add("{@@Location}", GetLocation(type.CategoryId, separator));
            StaticHelper.GenerateStaticContent(categoryTemplatePath, dic, destPath);
        }

        /// <summary>
        /// 重新静态化，重新生成所有内容（修改内容模板页时一定要执行）
        /// </summary>
        public static void StaticAllContent()
        {
            try
            {
                List<Models.News> newsList = Current.BLLSession.INews.GetListBy(n => n.NewsExternalLink == null&&n.NewsPath!=null);
                foreach (Models.News item in newsList)
                {
                    CreateNewsFile(item);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        /// <summary>
        /// 一键静态化，生成所有内容（修改列表模板页之后一定要执行）
        /// </summary>
        public static void StaticAllCategory()
        {
            try
            {
                List<Models.Category> types = Current.LoadNewsTypes();
                foreach (Models.Category type in types)
                {
                    CreateListFile(type);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        /// <summary>
        /// 全站静态化、（Todo:首页）
        /// </summary>
        public static void StaticAll()
        {
            StaticAllContent();
            StaticAllCategory();
        }

        /// <summary>
        /// 获取站点地图位置
        /// </summary>
        /// <param name="typeId">新闻类别idtypeId</param>
        /// <returns></returns>
        public static string GetLocation(int typeId,string separator)
        {
            StringBuilder sbTemp = new StringBuilder("<a href='/'>首页</a>");
            if (typeId<=0)
            {
                return "<a href='/'>首页</a>";
            }
            Models.Category c = Current.SelectNewsType(typeId);
            List<string> locations = new List<string>();
            locations.Add("<a href='/news/" + c.CategoryId.ToString() + "/index.html'>" + c.CategoryName + "</a>");
            while (c.ParentId!=0)
            {
                c = Current.SelectNewsType(c.ParentId);
                //路径
                locations.Add("<a href='/news/"+c.CategoryId.ToString()+"/index.html'>" + c.CategoryName + "</a>");
                ////构建栏目路径
                //sbTemp.Append("<a href='/news/"+c.CategoryId.ToString()+"/index.html'>" + c.CategoryName + "</a>");
            }
            for (int i = locations.Count-1 ; i >= 0; i--)
            {
                sbTemp.Append(separator);
                sbTemp.Append(locations[i]);
            }
            return sbTemp.ToString();
        }

        /// <summary>
        /// 将新闻移动到回收站
        /// </summary>
        /// <param name="id">新闻id</param>
        /// <returns>是否操作成功</returns>
        public bool MoveToRecycleBin(int id)
        {
            Models.News d = new Models.News() { NewsId = id, IsDel = true };
            try
            {
                return BLLSession.INews.Modify(d, "IsDel") == 1;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// 将新闻移动到回收站
        /// </summary>
        /// <param name="news">新闻实体</param>
        /// <returns></returns>
        public bool MoveToRecycleBin(Models.News news)
        {
            news.IsDel = true;
            try
            {
                return BLLSession.INews.Modify(news, "IsDel") == 1;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// 将移除到回收站的内容恢复
        /// </summary>
        /// <param name="id">内容id</param>
        /// <returns></returns>
        public bool RestoreFromRecycleBin(int id)
        {
            Models.News d = new Models.News() { NewsId = id, IsDel = false };
            try
            {
                return BLLSession.INews.Modify(d, "IsDel") == 1;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// 彻底删除新闻
        /// </summary>
        /// <param name="id">新闻id</param>
        /// <returns></returns>
        public bool DelNewsCompletely(int id)
        {
            try
            {
                BLLSession.INews.Del(new Models.News() { NewsId = id });
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// 清空回收站中的新闻
        /// </summary>
        /// <returns></returns>
        public bool EmptyRecycleBin()
        {
            try
            {
                BLLSession.INews.DelBy(n => n.IsDel == true);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// 转换新闻的置顶状态，未置顶设置为置顶新闻、已置顶撤销置顶
        /// </summary>
        /// <param name="id">新闻id</param>
        /// <returns></returns>
        public bool TransferTopState(Models.News news)
        {
            //置顶状态取反
            news.IsTop = !news.IsTop;
            try
            {
                return BLLSession.INews.Modify(news, "IsTop") == 1;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// 设置置顶
        /// </summary>
        /// <param name="id">新闻id</param>
        /// <returns></returns>
        public bool SetTop(int id)
        {
            Models.News d = new Models.News() { NewsId = id, IsTop = true };
            try
            {
                return BLLSession.INews.Modify(d, "IsTop") == 1;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// 取消设置为置顶，撤销置顶
        /// </summary>
        /// <param name="id">新闻id</param>
        /// <returns></returns>
        public bool CancelTop(int id)
        {
            Models.News d = new Models.News() { NewsId = id, IsTop = false };
            try
            {
                return BLLSession.INews.Modify(d, "IsTop") == 1;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// 更新新闻
        /// </summary>
        /// <param name="news">新闻ViewModel实体</param>
        /// <returns></returns>
        public bool UpdateNews(Models.News news)
        {
            news.NewsPublisher = (HttpContext.Current.Session["User"] as Models.User).UserName;
            try
            {
                //判断新闻有指向外链
                if (String.IsNullOrEmpty(news.NewsExternalLink))
                {
                    //news 静态化，设置news 的 NewsId，统计浏览次数
                    //获取新闻路径
                    if (String.IsNullOrEmpty(news.NewsPath))
                    {
                        news.NewsPath = SelectNews(news.NewsId).NewsPath;
                    }
                    //创建静态文件
                    CreateNewsFile(news);
                }
                //设置修改项
                return BLLSession.INews.Modify(news, "NewsTypeId", "NewsTitle","NewsContent","NewsPublisher","NewsPath","NewsImagePath","NewsAttachPath","NewsExternalLink","IsTop") == 1;
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                return false;
            }
        }
        
        /// <summary>
        /// query news count
        /// </summary>
        /// <returns></returns>
        public int QueryNewsCount()
        {
            return BLLSession.INews.GetList().Count;
        }

        /// <summary>
        /// 加载回收站中的新闻，按id排序分页数据
        /// </summary>
        /// <param name="pageIndex">页码索引</param>
        /// <param name="pageSize">页码容量</param>
        /// <param name="isAsc">是否正序排列</param>
        /// <returns></returns>
        public List<Models.News> LoadRecycleBinNews(int pageIndex, int pageSize,int typeId, bool isAsc)
        {
            if (typeId <= 0)
            {
                return BLLSession.INews.GetPagedList(pageIndex, pageSize, m => m.IsDel == true, s => s.NewsId, isAsc);
            }
            else
            {
                return BLLSession.INews.GetPagedList(pageIndex, pageSize, m => m.IsDel == true&&m.NewsTypeId==typeId, s => s.NewsId, isAsc);
            }
        }

        /// <summary>
        /// 加载未删除的新闻，按id排序分页数据
        /// </summary>
        /// <param name="pageIndex">页码索引</param>
        /// <param name="pageSize">页码容量</param>
        /// <param name="isAsc">是否正序排列</param>
        /// <returns></returns>
        public List<Models.News> LoadNews(int pageIndex, int pageSize,int typeId, bool isAsc)
        {
            if (typeId <= 0)
            {
                return BLLSession.INews.GetPagedList(pageIndex, pageSize, m => m.IsDel == false, s => s.NewsId, isAsc);
            }
            else
            {
                return BLLSession.INews.GetPagedList(pageIndex, pageSize, m => m.IsDel == false&&m.NewsTypeId==typeId, s => s.NewsId, isAsc);
            }
        }

        /// <summary>
        /// 加载News列表
        /// </summary>
        /// <param name="pageIndex">页码索引</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="whereLambda">条件lambda表达式</param>
        /// <param name="orderBy">排序lambda表达式</param>
        /// <returns>News列表</returns>
        public List<Models.News> LoadNews<TKey>(int pageIndex, int pageSize, Expression<Func<Models.News, bool>> whereLambda, Expression<Func<Models.News, TKey>> orderBy, bool isAsc)
        {
            return  BLLSession.INews.GetPagedList(pageIndex, pageSize, whereLambda, orderBy, isAsc);
        }
        
        #endregion

        #region NewsType

        /// <summary>
        /// 加载新闻类别、栏目
        /// </summary>
        /// <returns></returns>
        public List<Models.Category> LoadNewsTypes()
        {
            return BLLSession.ICategory.GetList();
        }

        /// <summary>
        /// 加载顶级目录、一级目录
        /// </summary>
        /// <returns></returns>
        public List<Models.Category> LoadTopCategory()
        {
            try
            {
                return BLLSession.ICategory.GetListBy(c => c.ParentId == 0);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// 加载非顶级、次级目录
        /// </summary>
        /// <param name="parentId">父节点id</param>
        /// <returns>父节点为id的目录</returns>
        public List<Models.Category> LoadSecondaryCategory(int parentId)
        {
            return BLLSession.ICategory.GetListBy(c => c.ParentId == parentId);
        }

        /// <summary>
        /// 根据id查询新闻类别实体
        /// </summary>
        /// <param name="id">新闻类别id</param>
        /// <returns>新闻类别实体</returns>
        public Models.Category SelectNewsType(int id)
        {
            return BLLSession.ICategory.GetListBy(m => m.CategoryId == id).FirstOrDefault();
        }

        /// <summary>
        /// 添加新闻类别
        /// </summary>
        /// <param name="newsTypeName">新闻类别名称</param>
        /// <returns></returns>
        public bool AddNewsType(Models.Category t)
        {
            try
            {
                return BLLSession.ICategory.Add(t) == 1;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// 删除新闻类别
        /// </summary>
        /// <param name="id">新闻类别id</param>
        /// <returns></returns>
        public bool DelNewsType(int id)
        {
            try
            {
                return BLLSession.ICategory.Del(new Models.Category() { CategoryId = id })==1;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// 更新新闻类别
        /// </summary>
        /// <param name="newsType">新闻类别实体</param>
        /// <returns></returns>
        public bool UpdateNewsType(Models.Category newsType)
        {
            try
            {
                return BLLSession.ICategory.Modify(newsType, "CategoryName") == 1;
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                return false;
            }
        }

        #endregion

        #endregion

        #region 网站资源管理、模板管理 css+js + html模板(内容页模板、列表页模板)

        public List<Common.FileModel> GetCssFiles()
        {
            return Common.PathHelper.GetFileList(Common.PathHelper.MapPath(CSSDIR));
        }

        public List<Common.FileModel> GetJavaScriptFiles()
        {
            return Common.PathHelper.GetFileList(Common.PathHelper.MapPath(JSDIR));
        }
        
        public List<Common.FileModel> GetContentTemplates()
        {
            return Common.PathHelper.GetFileList(Common.PathHelper.MapPath(ContentTemplateDIR));
        }

        public List<Common.FileModel> GetCategoryTemplates()
        {
            return Common.PathHelper.GetFileList(Common.PathHelper.MapPath(CategoryTemplateDIR));
        }
        
        /// <summary>
        /// 获取文件内容
        /// </summary>
        /// <param name="fileType">文件类型</param>
        /// <param name="fileName">文件名</param>
        /// <returns>文件内容</returns>
        public string GetFileContent(Common.FileType fileType,string fileName)
        {
            string tmpText = String.Empty;
            try
            {
                switch (fileType)
                {
                    case Common.FileType.css:
                        fileName += ".css";
                        tmpText = System.IO.File.ReadAllText(Common.PathHelper.MapPath(CSSDIR + fileName));
                        break;
                    case Common.FileType.js:
                        fileName += ".js";
                        tmpText = System.IO.File.ReadAllText(Common.PathHelper.MapPath(JSDIR + fileName));
                        break;
                    case Common.FileType.content:
                        fileName += ".html";
                        tmpText = System.IO.File.ReadAllText(Common.PathHelper.MapPath(ContentTemplateDIR + fileName));
                        break;
                    case Common.FileType.category:
                        fileName += ".html";
                        tmpText = System.IO.File.ReadAllText(Common.PathHelper.MapPath(CategoryTemplateDIR + fileName));
                        break;
                    default:
                        tmpText = "error！该文件不能编辑！";
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            return tmpText;
        }

        #region 保存文件更改
        //Todo:保存文件时，重新全部静态化,重新生成静态页

        /// <summary>
        /// 将更改保存到文件
        /// </summary>
        /// <param name="contents">文件内容</param>
        /// <param name="fileType">文件类型</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public bool SaveFile(string contents, Common.FileType fileType, string fileName)
        {
            try
            {
                switch (fileType)
                {
                    case Common.FileType.css:
                        fileName += ".css";
                        System.IO.File.WriteAllText(Common.PathHelper.MapPath(CSSDIR + fileName),contents);
                        break;
                    case Common.FileType.js:
                        fileName += ".js";
                        System.IO.File.WriteAllText(Common.PathHelper.MapPath(JSDIR + fileName),contents);
                        break;
                    case Common.FileType.content:
                        fileName += ".html";
                        System.IO.File.WriteAllText(Common.PathHelper.MapPath(ContentTemplateDIR + fileName),contents);
                        break;
                    case Common.FileType.category:
                        fileName += ".html";
                        System.IO.File.WriteAllText(Common.PathHelper.MapPath(CategoryTemplateDIR + fileName),contents);
                        break;
                    default:
                        return false;
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            return false;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="contents">文件内容</param>
        /// <param name="filePath">文件绝对路径</param>
        /// <returns></returns>
        public bool SaveFile(string contents, string filePath)
        {
            try
            {
                System.IO.File.WriteAllText(filePath, contents);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            return false;
        }
        
        #endregion

        #endregion
    }
}