using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Torpedo.View
{
    /// <summary>
    /// Interaction logic for Ranking.xaml
    /// </summary>
    public partial class Ranking : Window
    {
        private List<Rank> allTimeRank = new List<Rank>();
        private DataHandling dataHandling = new DataHandling();
        public Ranking()
        {
            InitializeComponent();
            allTimeRank.Add(new Rank() {Name="Marci", Loses=1, Wins=2 });
            allTimeRank.Add(new Rank() {Name="Fanni", Loses=0, Wins=10 });
            allTimeRank.Add(new Rank() {Name="Dani", Loses=12, Wins=2 });

            allTimeRank = getRankList();

            listRank.ItemsSource = allTimeRank;

        }
        private List<Rank> getRankList()
        {
            string inJson = File.ReadAllText(@"..\..\..\results.json");
            List<Result> allResult = JsonConvert.DeserializeObject<List<Result>>(inJson);
            List<String> uniqueNames = allResult.Select(x => x.username).Distinct().ToList();
            List<Rank> ranking = new List<Rank>();

            for (int i = 0; i < uniqueNames.Count; i++)
            {
                int wins = allResult.Where(name => name.username == uniqueNames[i]).Where(result => result.result == "win").Count();
                int defeat = allResult.Where(name => name.username == uniqueNames[i]).Where(result => result.result == "defeat").Count();
                Rank rank = new Rank() { Name = uniqueNames[i], Wins = wins, Loses = defeat };
                ranking.Add(rank);
            }
            File.WriteAllLines(@"C:\Users\czink\Documents\WriteLines.txt", uniqueNames);
            return ranking;
        }
        public class Rank
        {

            public string Name { get;set; }
            public int Loses { get;set; }
            public int Wins { get;set; }
        }

        private void clickBackButton(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Close();
        }
    }
}
