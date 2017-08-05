// ----------------------------------------------------------
// 系统名称：EasySoft Core
// 项目名称：工具类库
// 创 建 人：刘年超
// 创建时间：2017-01-12
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
using System.Configuration;

namespace EasySoft.Core.Util
{
    /// <summary>
    /// 常量定义类
    /// </summary>
    public class Config
    {
        #region 变量

        private static int pageSize = Constant.INT_ZERO;

        #endregion

        #region 

        /// <summary>
        /// 获取分页大小
        /// </summary>
        /// <returns>返回分页大小</returns>
        public static int GetPageSize()
        {
            if (pageSize <= Constant.INT_ZERO)
            {
                if (int.TryParse(ConfigurationManager.AppSettings["PageSize"], out pageSize))
                {
                    if (pageSize <= Constant.INT_ZERO)
                    {
                        pageSize = Constant.DEFAULT_PAGE;
                    }
                }
                else
                {
                    pageSize = Constant.DEFAULT_PAGE;
                }
            }
            return pageSize;
        }

        #endregion


    }
}
