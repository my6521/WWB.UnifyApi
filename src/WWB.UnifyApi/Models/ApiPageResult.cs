using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWB.UnifyApi.Models
{
    public class ApiPageResult<T> where T : class
    {
        public IList<T> List { get; set; }

        public int Total { get; set; }
    }
}