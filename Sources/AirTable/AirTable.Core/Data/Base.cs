using AirTable.Core.Data.Parameter;
using AirTable.Core.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTable.Core
{
    /// <summary>
    /// Base/Table declaration.
    /// </summary>
    public class Base
    {
        /// <summary>
        /// Id of the base
        /// </summary>
        public string BaseId { get; set; }
        /// <summary>
        /// Name of the base
        /// </summary>
        public string BaseName { get; set; }
        /// <summary>
        /// Endpoint URL of the API
        /// </summary>
        public string EndPointUrl { get; set; }
        /// <summary>
        /// API secret token
        /// </summary>
        public string TokenKey { get; set; }
        /// <summary>
        /// Version of the API
        /// </summary>
        public string Version { get; set; }

        public Base(string baseId, string baseName, string endPointUrl, string version, string tokenKey)
        {
            BaseId = baseId;
            BaseName = baseName;
            EndPointUrl = endPointUrl;
            Version = version;
            TokenKey = tokenKey;
        }

        /// <summary>
        /// List all items of the base
        /// </summary>
        /// <returns></returns>
        public RecordList List()
        {
            return List(new ListParameter());
        }

        /// <summary>
        /// List items of the base folowing the given parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public RecordList List(ListParameter parameters)
        {
            var result = RestHelper.MakeRequest("GET", EndPointUrl + "/" + Version + "/" + BaseId + "/" + BaseName + parameters.toURLFormat(),
                new RequestParameter("Authorization", "Bearer " + TokenKey));
            var resultDeserialized = JsonConvert.DeserializeObject<JObject>(result);
            var allRecords = resultDeserialized["records"];

            List<Record> records = new List<Record>();
            foreach (var item in allRecords)
            {
                records.Add(new Record(item));
            }

            if (resultDeserialized["offset"] != null)
            {
                return new RecordList() { Records = records, Offset = resultDeserialized["offset"].Value<string>() };
            }
            else
            {
                return new RecordList() { Records = records};
            }
        }

        /// <summary>
        /// Retreive a record
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public async Task<Record> Retreive(string recordId)
        {
            var result = RestHelper.MakeRequest("GET", EndPointUrl + "/" + Version + "/" + BaseId + "/" + BaseName + "/" + recordId,
                new RequestParameter("Authorization", "Bearer " + TokenKey));
            var resultDeserialized = JsonConvert.DeserializeObject<JObject>(result);

            return new Record(resultDeserialized);
        }

        /// <summary>
        /// Update a record with the given record item.
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public async Task<Record> Update(Record record)
        {
            var result = RestHelper.MakeRequest("PATCH", EndPointUrl + "/" + Version + "/" + BaseId + "/" + BaseName + "/" + record.Id,
               record.ToJSONFormat(false),
               new RequestParameter("Authorization", "Bearer " + TokenKey));
            var resultDeserialized = JsonConvert.DeserializeObject<JObject>(result);
            return new Record(resultDeserialized);

        }

        /// <summary>
        /// Create a record
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public async Task<Record> Create(Record record)
        {
            var result = RestHelper.MakeRequest("POST", EndPointUrl + "/" + Version + "/" + BaseId + "/" + BaseName,
               record.ToJSONFormat(true),
               new RequestParameter("Authorization", "Bearer " + TokenKey));
            var resultDeserialized = JsonConvert.DeserializeObject<JObject>(result);
            return new Record(resultDeserialized);
        }

        /// <summary>
        /// Delete a record based on the Id
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public async Task<bool> Delete(string recordId)
        {
            var result = RestHelper.MakeRequest("DELETE", EndPointUrl + "/" + Version + "/" + BaseId + "/" + BaseName + "/" + recordId,
                new RequestParameter("Authorization", "Bearer " + TokenKey));
            var resultDeserialized = JsonConvert.DeserializeObject<JObject>(result);
            return resultDeserialized["deleted"].Value<bool>();
        }
    }
}
