using AirTable.Core.Data;
using AirTable.Core.Data.Field;
using AirTable.Core.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTable.Core
{
    /// <summary>
    /// Record that represent a line of the base/table
    /// </summary>
    public class RecordList
    {
        /// <summary>
        /// List of records
        /// </summary>
        public IEnumerable<Record> Records { get; set; }

        /// <summary>
        /// Offset that must be set to the next request
        /// Null or Empty value mean no more page.
        /// </summary>
        public string Offset { get; set; }

    }
}
