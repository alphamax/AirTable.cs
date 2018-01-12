using AirTable.Core;
using AirTable.Core.Data.Parameter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AirTable.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            AirTableConnector connector = new AirTableConnector("YourPrivateKey");
            var baseAirTable = connector.ExtractBase("BaseId", "BaseName");
            Task.Factory.StartNew(
                async () =>
                {
                    try
                    {
                        //Pure sample based on Agile template.
                        var top3Stories = await baseAirTable.List(new ListParameter() { PageSize = 2 });
                        var story1 = await baseAirTable.Retreive(top3Stories.Records.First().Id);
                        //var deleted = await baseAirTable.Delete(top3Stories.First().Id);
                        story1.ExtractStringField("User Want").FieldValue = "Modified !!! " + DateTime.Now.ToLongTimeString();
                        story1 = await baseAirTable.Update(story1);

                        var newRecord = new Record();
                        newRecord.ExtractStringField("User Want").FieldValue = "Created ! " + DateTime.Now.ToLongTimeString();
                        story1 = await baseAirTable.Create(newRecord);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                });
            Console.ReadLine();
        }
    }
}
