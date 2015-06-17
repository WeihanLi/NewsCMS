using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User
    {
        private int userId;
        private string userName;
        private string userPassword;
        private bool isAdmin=false;
        private bool isDel = false;
        private DateTime addTime;

        /// <summary>
        /// 用户id
        /// </summary>
        [Key]
        public int UserId
        {
            get
            {
                return userId;
            }

            set
            {
                userId = value;
            }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPassword
        {
            get
            {
                return userPassword;
            }

            set
            {
                userPassword = value;
            }
        }

        /// <summary>
        /// 用户添加时间
        /// </summary>
        public DateTime AddTime
        {
            get
            {
                return addTime;
            }

            set
            {
                addTime = value;
            }
        }

        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        public bool IsAdmin
        {
            get
            {
                return isAdmin;
            }

            set
            {
                isAdmin = value;
            }
        }

        /// <summary>
        /// 用户账户是否被禁用
        /// </summary>
        public bool IsDel
        {
            get { return isDel; }
            set { isDel = value; }
        }
    }
}
