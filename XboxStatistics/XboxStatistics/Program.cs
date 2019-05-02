using System;
using System.Linq;

namespace XboxStatistics
{
    class Program
    {
        private static readonly MyXboxOneGames Xbox = new MyXboxOneGames();

        static void Main(string[] args)
        {
            Question("How many games do I have?", HowManyGamesDoIHave);
            Question("How many games have I completed?", HowManyGamesHaveICompleted);
            Question("How much Gamerscore do I have?", HowMuchGamescoreDoIHave);
            Question("How many days did I play?", HowManyDaysDidIPlay);
            Question("Which game have I spent the most hours playing?", WhichGameHaveISpentTheMostHoursPlaying);
            Question("In which game did I unlock my latest achievement?", InWhichGameDidIUnlockMyLatestAchievement);
            Question("List all of my statistics in Binding of Isaac:", ListAllOfMyStatisticsInBindingOfIsaac);
            Question("How many achievements did I earn per year?", HowManyAchievementsDidIEarnPerYear);
            Question("List all of my games where I have earned a rare achievement", ListAllOfMyGamesWhereIHaveEarnedARareAchievement);
            Question("List the top 3 games where I have earned the most rare achievements", ListTheTop3GamesWhereIHaveEarnedTheMostRareAchievements);
            Question("Which is my rarest achievement?", WhichIsMyRarestAchievement);
            Console.ReadLine();
        }

        static void Question(string question, Func<string> answer)
        {
            Console.WriteLine($"Q: {question}");
            Console.WriteLine($"A: {answer()}");
            Console.WriteLine();
        }
        static string HowManyGamesDoIHave()
        {
            return Xbox.MyGames.Count().ToString();
        }
        static string HowManyGamesHaveICompleted()
        {
            //HINT: you need to count the games where I reached the maximum Gamerscore
            return Xbox.MyGames.Count(g => g.MaxGamerscore == g.CurrentGamerscore).ToString();
        }
        static string HowMuchGamescoreDoIHave()
        {
            return Xbox.MyGames.Sum(g => g.CurrentGamerscore).ToString();
        }
        static string HowManyDaysDidIPlay()
        {
            //HINT: there's a game stat property called MinutesPlayed, and as the name suggests it stored total minutes
            var minutes = Xbox.GameStats.Sum(game => game.Value.Where(v => v.Name == "MinutesPlayed").Select(v => v.Value == null ? 0 : int.Parse(v.Value)).FirstOrDefault());
            return (minutes / (60 * 24)).ToString();
        }
        static string WhichGameHaveISpentTheMostHoursPlaying()
        {
            //HINT: there's a game stat property called MinutesPlayed, and as the name suggests it stored total minutes
            var q1 = Xbox.GameStats.OrderByDescending(game => game.Value.Where(v => v.Name == "MinutesPlayed").Select(v => v.Value == null ? 0 : int.Parse(v.Value)).FirstOrDefault()).First();
            return Xbox.MyGames.Where(g => g.TitleId == q1.Key).Select(g => g.Name).First() + $" => {int.Parse((q1.Value.Where(x => x.Name == "MinutesPlayed").Select(x => x.Value)).FirstOrDefault())/60} hours";
        }
        static string InWhichGameDidIUnlockMyLatestAchievement()
        {
            var maxValue = Xbox.MyGames.Max(g => g.LastUnlock);
            return Xbox.MyGames.Where(g => g.LastUnlock == maxValue).Select(g => g.Name).First() + $" on {maxValue.ToString("yyyy-MM-dd HH:mm")}";
        }
        static string ListAllOfMyStatisticsInBindingOfIsaac()
        {
            const string gameName = "Binding of Isaac";
            var titleID = Xbox.MyGames.Where(g => g.Name.Contains(gameName)).Select(x => x.TitleId).FirstOrDefault();
            return string.Join(Environment.NewLine, Xbox.GameStats[titleID].Select(x => x.Name + " = " + x.Value));
        }
        static string HowManyAchievementsDidIEarnPerYear()
        {
            //HINT: unlocked achievements have an "Achieved" progress state
            var q1 = from achievment
                     in Xbox.Achievements.Values.SelectMany(a => a.Where(aa => aa.ProgressState == "Achieved"))
                     group achievment by achievment.Progression.TimeUnlocked.Year into g
                     orderby g.Key
                     select new { Year = g.Key, Items = g.ToList() };

            return string.Join(Environment.NewLine, q1.Select(x => x.Year + ": " + x.Items.Count));
        }
        static string ListAllOfMyGamesWhereIHaveEarnedARareAchievement()
        {
            //HINT: rare achievements have a rarity category called "Rare"
            var q1 = Xbox.Achievements.Select(a => a.Value.Where(aa => aa.Rarity.CurrentCategory == "Rare" && aa.ProgressState == "Achieved").Select(aa => a.Key).FirstOrDefault()).Distinct();
            return string.Join(Environment.NewLine, q1.Select(key => Xbox.MyGames.Where(game => game.TitleId == key).Select(k => k.Name).FirstOrDefault()));
        }
        static string ListTheTop3GamesWhereIHaveEarnedTheMostRareAchievements()
        {
            var q1 = Xbox.Achievements.Select(ach => new {  Sum = ach.Value.Count(aa => aa.Rarity.CurrentCategory == "Rare" && aa.ProgressState == "Achieved"), Name = Xbox.MyGames.First(g => g.TitleId == ach.Key).Name });
            return string.Join(Environment.NewLine, q1.OrderByDescending(o => o.Sum).Take(3).Select(x => x.Name + $" ({x.Sum})"));
        }
        static string WhichIsMyRarestAchievement()
        {
            var q1 = Xbox.Achievements.SelectMany(ach => ach.Value.Where(a => a.Rarity.CurrentCategory == "Rare" && a.ProgressState == "Achieved").Select(a => new { Percentage = a.Rarity.CurrentPercentage, AchName = a.Name, GameName = Xbox.MyGames.First(g => g.TitleId == ach.Key).Name})).OrderBy(o => o.Percentage).First();
            return String.Format($"You are among the {q1.Percentage} of gamers who earned the '{q1.AchName}' achievement in {q1.GameName}");
        }
    }
}