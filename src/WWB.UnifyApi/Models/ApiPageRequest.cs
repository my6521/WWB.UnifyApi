namespace WWB.UnifyApi.Models
{
    public class ApiPageRequest
    {
        private int _pageIndex;
        private int _pageSize = 10;

        /// <summary>
        /// 当前页数
        /// </summary>
        public int PageIndex
        {
            get => _pageIndex <= 0 ? 1 : _pageIndex;
            set => _pageIndex = value <= 0 ? 1 : value;
        }

        /// <summary>
        /// 每页显示调试
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (value >= 100)
                {
                    _pageSize = 100;
                }
                else if (value <= 0)
                {
                    _pageSize = 10;
                }
                else
                {
                    _pageSize = value;
                }
            }
        }

        public int Offset => (PageIndex - 1) * PageSize;
    }
}