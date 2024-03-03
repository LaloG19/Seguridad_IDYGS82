﻿namespace ApplicationCore.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }

        public PagedResponse(T data, int pageNumber, int pageSize, int total = 0)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Total = total;
            this.Result = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }
    }
}
