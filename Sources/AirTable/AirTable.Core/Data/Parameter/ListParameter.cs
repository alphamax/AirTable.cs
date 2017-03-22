using AirTable.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTable.Core.Data.Parameter
{
    /// <summary>
    /// Parameter when listing item
    /// </summary>
    public class ListParameter
    {
        /// <summary>
        /// Fields that may be returned by API
        /// </summary>
        public IEnumerable<string> FieldNames { get; set; }
        /// <summary>
        /// Formula to determine what is nedded
        /// </summary>
        public string FilterByFormula { get; set; }
        /// <summary>
        /// Max record needed
        /// </summary>
        public int MaxRecord { get; set; }
        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Sorting needed
        /// </summary>
        public IEnumerable<FieldSort> FieldSorts { get; set; }
        /// <summary>
        /// View requested
        /// </summary>
        public string View { get; set; }

        //No mandatory parameters
        public ListParameter()
        {
            FieldNames = Enumerable.Empty<string>();
            FieldSorts = Enumerable.Empty<FieldSort>();
        }

        public string toURLFormat()
        {
            List<string> parameters = new List<string>();
            foreach (var item in FieldNames)
            {
                parameters.Add("fields%5B%5D=" + DataHelper.UrlEncode(item));
            }

            if (!string.IsNullOrWhiteSpace(FilterByFormula))
            {
                parameters.Add("filterByFormula=" + DataHelper.UrlEncode(FilterByFormula));
            }

            if (MaxRecord != 0)
            {
                parameters.Add("maxRecords=" + MaxRecord);
            }

            if (PageSize != 0)
            {
                parameters.Add("pageSize=" + PageSize);
            }

            for (int i = 0; i < FieldSorts.Count(); i++)
            {
                parameters.Add(FieldSorts.ElementAt(i).ToURLFormat(i));
            }

            if (!string.IsNullOrWhiteSpace(View))
            {
                parameters.Add("view=" + DataHelper.UrlEncode(View));
            }

            if (parameters.Count > 0)
            {
                return "?" + string.Join("&", parameters);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
