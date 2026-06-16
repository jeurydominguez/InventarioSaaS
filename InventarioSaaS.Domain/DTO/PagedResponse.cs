using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.DTO
{
    public class PagedResponse<T>
    {
        public List<T> Data { get; set; } = [];

        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalRecords { get; set; }

        public int TotalPages { get; set; }

        public bool HasNextPage => Page < TotalPages;

        public bool HasPreviousPage => Page > 1;
    }
}
