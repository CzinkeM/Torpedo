using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Torpedo
{
    class DataHandling
    {
        public void AddResultToJson(Result newResult)
        {
            if (File.Exists(@"..\..\..\results.json"))
            {
                string inJson = File.ReadAllText(@"..\..\..\results.json");
                List<Result> results = JsonConvert.DeserializeObject<List<Result>>(inJson);

                results.Add(newResult);

                string outJson = JsonConvert.SerializeObject(results.ToArray());

                File.WriteAllText(@"..\..\..\results.json", outJson);
            }
            else
            {
                List<Result> results = new List<Result>();

                results.Add(newResult);

                string outJson = JsonConvert.SerializeObject(results.ToArray());

                File.WriteAllText(@"..\..\..\results.json", outJson);
            }
        }


        //string inJson = File.ReadAllText(@"..\..\..\results.json");

        //List<Result> allResult = JsonConvert.DeserializeObject<List<Result>>(inJson);

        //foreach (Result item in allResult)
        //{
        //    name.Items.Add(item); // a name a listview neve amibe az adatokat toltjuk
        //}
    }
}
