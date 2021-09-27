using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.ResourceParameters
{
    public class BaseParametersGetAllViewModel
    {
        const int maxPagesSize = 20;

        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPagesSize) ? maxPagesSize : value;
        }

    }
}
