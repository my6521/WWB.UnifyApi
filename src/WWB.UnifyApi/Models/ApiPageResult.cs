using System.Collections.Generic;

namespace WWB.UnifyApi.Models
{
    public class ApiPageResult<T> where T : class
    {
        public IList<T> List { get; set; }

        public int Total { get; set; }
    }

    public class ApiPageResult<T, S> : ApiPageResult<T>
        where S : class
        where T : class
    {
        public S Summary { get; set; }
    }
}