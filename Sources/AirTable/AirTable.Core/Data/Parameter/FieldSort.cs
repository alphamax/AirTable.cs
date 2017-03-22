using AirTable.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTable.Core.Data.Parameter
{
    /// <summary>
    /// Sort URL proxy
    /// </summary>
    public class FieldSort
    {
        /// <summary>
        /// Field to sort
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// Direction of sorting
        /// </summary>
        public SortDirection Direction { get; set; }

        public FieldSort(string name, SortDirection direction)
        {
            FieldName = name;
            Direction = direction;
        }

        public string ToURLFormat(int index)
        {
            return "sort%5B" + index + "%5D%5Bfield%5D=" + DataHelper.UrlEncode(FieldName) + "&sort%5B" + index + "%5D%5Bdirection%5D="
                + (Direction == SortDirection.Ascending ? "asc" : "desc"); 
        }
    }
}
