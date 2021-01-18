using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Torpedo.Model;

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
        public List<Rank> getRankList()
        {
            string inJson = File.ReadAllText(@"..\..\..\results.json");
            List<Result> allResult = JsonConvert.DeserializeObject<List<Result>>(inJson);
            List<String> uniqueNames = allResult.Select(x => x.username).Distinct().ToList();
            List<Rank> ranking = new List<Rank>();
            
            for (int i = 0; i < uniqueNames.Count; i++)
            {
                int wins = 0;
                int loses = 0;
                for (int j = 0; j < allResult.Count; j++)
                {                    
                    if (allResult[j].username == uniqueNames[i])
                    {
                        if (allResult[j].result == "win")
                        {
                            wins++;
                        }
                        else loses++;
                    }
                    Rank rank = new Rank(allResult[j].username, wins, loses);
                    ranking.Add(rank);
                }                
            }
            return ranking;
        }


        //string inJson = File.ReadAllText(@"..\..\..\results.json");

        //List<Result> allResult = JsonConvert.DeserializeObject<List<Result>>(inJson);

        //foreach (Result item in allResult)
        //{
        //    name.Items.Add(item); // a name a listview neve amibe az adatokat toltjuk
        //}
    }
}
