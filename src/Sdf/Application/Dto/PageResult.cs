using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Application.Dto
{
    public class PageResult<T>
    {
        public int Total { get; set; }
        public List<T> Items { get; set; }
        public object Other { get; set; }
        private int pageSize;
        public int MaxPage
        {
            get
            {
                if (pageSize == 0)
                    return 0;
                if (Total % pageSize == 0)
                    return (int)(Total / pageSize);
                else
                    return (int)(Total / pageSize) + 1;
            }
        }
        public PageResult(int total, List<T> items)
        {
            this.Total = total;
            this.Items = items;

        }
        public PageResult(int total, List<T> items, int pageSize)
        {
            this.Total = total;
            this.Items = items;
            this.pageSize = pageSize;
        }
        public PageResult(int total, List<T> items, object other)
        {
            this.Total = total;
            this.Items = items;
            this.Other = other;
        }
        public PageResult(int total, List<T> items, int pageSize, object other)
        {
            this.Total = total;
            this.Items = items;
            this.Other = other;
            this.pageSize = pageSize;
        }
        public PageResult()
        { }
        public static PageResult<T> CreateEmpty()
        {
            return new PageResult<T>(0, null);
        }
    }
}
