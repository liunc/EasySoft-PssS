using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasySoft.PssS.Web.Models
{
    public class PagingModel<T>
    {
        public int TotalCount { get; set; }

        public List<T> Data { get; set; }

        public int PageSize { get; set; }

        public PagingModel()
        {
            this.PageSize = ParameterHelper.GetPageSize();
        }
    }
}