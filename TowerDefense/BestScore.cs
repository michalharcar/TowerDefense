using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace TowerDefense {
    public class BestScore {
        private List<Score> bestScore = new List<Score>();
        private ScoreComparer comparer = new ScoreComparer();
        static string QUERY_SELECT = "SELECT name, level, time, towers, money from Score;";
        static string QUERY_INSERT = "INSERT INTO Score(name,level, time, towers, money) VALUES(@name, @level, @time, @towers, @money)";
        
        public void addScore(Score score) {
            bestScore.Add(score);
            bestScore.Sort(comparer);
            saveToDB(score);
        }
        
        private void readFromDB() {
            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=TowerDefense;Integrated Security=True;Pooling=False";
            sql.Open();
            SqlCommand com = new SqlCommand(QUERY_SELECT);
            com.Connection = sql;
            SqlDataReader reader = com.ExecuteReader(System.Data.CommandBehavior.Default);
            bestScore.Clear();
            
            while(reader.Read()) {
                Score score = new Score();
                score.LoadData(reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4));
                bestScore.Add(score);
           //     reader.NextResult();
            }
                reader.Close();
                sql.Close();
            bestScore.Sort(comparer);
        }

        private void saveToDB(Score score) {
            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=TowerDefense;Integrated Security=True;Pooling=False";
            sql.Open();
            SqlCommand com = new SqlCommand(QUERY_INSERT);
                com.Connection = sql;
                com.Parameters.Add(new SqlParameter("@name", score.Name));
                com.Parameters.Add(new SqlParameter("@level", score.LevelNumber));
                com.Parameters.Add(new SqlParameter("@time", score.PlayingTime));
                com.Parameters.Add(new SqlParameter("@towers", score.TowersCreated));
                com.Parameters.Add(new SqlParameter("@money", score.MoneySpent));
                com.ExecuteNonQuery();
            sql.Close();
        
        }

        public override string ToString() {
            readFromDB();
            StringBuilder sb = new StringBuilder();
            int i =1;
            foreach(Score sc in bestScore){
                sb.AppendFormat("{0}. {1}       {2}           {3}           {4}                 {5}{6}", i, sc.Name, sc.LevelNumber, sc.PlayingTime, sc.TowersCreated, sc.MoneySpent, Environment.NewLine);
                i++;
            }
            return sb.ToString();
        }

        public class Score {
            public int PlayingTime { get; private set; }
            public int TowersCreated { get; private set; }
            public int MoneySpent { get; private set; }
            public int LevelNumber { get; private set; }
            public string Name { get; private set; }
            private Level level;
            private Player player;

            public Score(Level level, Player player) {
                this.level = level;
                this.player = player;
            }

            public Score(){
            }

            public void GetData() {
                level.SetPlayingTime();
                PlayingTime = level.PlayingTime;
                LevelNumber = level.LvlNumber;
                TowersCreated = player.TowersCreated;
                MoneySpent = player.MoneySpent;
                Name = Environment.UserName;
            }

            public void LoadData(String name, int level, int time, int towers, int money){
                Name = name;
                LevelNumber = level;
                PlayingTime = time;
                TowersCreated = towers;
                MoneySpent = money;
            }

        }

        class ScoreComparer : IComparer<Score> {


            public int Compare(Score x, Score y) {
                if (x.LevelNumber==y.LevelNumber)
                return x.PlayingTime - y.PlayingTime;
                return x.LevelNumber - y.LevelNumber;
            }
        }
    }
}
