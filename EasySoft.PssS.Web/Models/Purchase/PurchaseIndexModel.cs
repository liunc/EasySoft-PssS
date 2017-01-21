using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasySoft.PssS.Web.Models.Purchase
{
    public class PurchaseIndexModel
    {
        public string Category { get; set; }

        public string Title { get; set; }

        public List<PurchaseItemModel> PurchaseItems { get; set; }
    }

}