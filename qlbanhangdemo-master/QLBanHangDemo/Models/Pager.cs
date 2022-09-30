using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLBanHangDemo.Models
{
    public class Pager
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        //Constructor
        public Pager()
        {
        }
        public Pager(int totalItem, int Page, int pageSize = 10)
        {
            int totalPage = (int)Math.Ceiling((decimal)totalItem/(decimal)pageSize);
            int curruentPage = Page;
            int startPage = curruentPage - 5;
            int endPage = curruentPage + 4;
            if (startPage <= 0)
            {
                startPage = 1;
                endPage = endPage - (startPage - 1); 
            }
            if (endPage > totalPage)
            {
                endPage = totalPage;
                if(endPage >10)
                {
                    startPage = endPage - 9;
                }    
            }
            this.CurrentPage = curruentPage;
            this.EndPage = endPage;
            this.TotalItems = totalItem;
            this.TotalPages = totalPage;
            this.PageSize = pageSize;
            this.StartPage = startPage;
        }

    }
}
