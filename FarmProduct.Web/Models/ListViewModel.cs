using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmProduct.Web.Models
{
    public class ListViewModel<T> : BaseModel
    {
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
        public List<T> Items { get; set; }
    }
}