using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _03_MessageSharing
{
    class Program
    {
        static string[] people;

        static void Main(string[] args)
        {
            var peopleString = Console.ReadLine();
            peopleString = peopleString.Replace("People: ", "");
            people = Regex.Split(peopleString, ", ");

            var connectionString = Console.ReadLine();
            connectionString = connectionString.Replace("Connections: ", "");
            string[] connections = Regex.Split(connectionString, ", ");

            var peopleToInformString = Console.ReadLine();
            peopleToInformString = peopleToInformString.Replace("Start: ", "");
            string[] peopleToInformWithNames = Regex.Split(peopleToInformString, ", ");

            HashSet<int>[] peopleWithFriends = new HashSet<int>[people.Length];
            Dictionary<string, int> peopleNamesIds = new Dictionary<string, int>();
            for (int i = 0; i < people.Length; i++)
            {
                peopleWithFriends[i] = new HashSet<int>();
                peopleNamesIds.Add(people[i], i);
            }

            foreach (var connection in connections)
            {
                var peopleToConnect = Regex.Split(connection, " - ");
                var people1Id = peopleNamesIds[peopleToConnect[0]];
                var people2Id = peopleNamesIds[peopleToConnect[1]];
                peopleWithFriends[people1Id].Add(people2Id);
                peopleWithFriends[people2Id].Add(people1Id);
            }

            HashSet<int> peopleToInform = new HashSet<int>();
            foreach (var peopleName in peopleToInformWithNames)
            {
                peopleToInform.Add(peopleNamesIds[peopleName]);
            }

            bool[] informedPeople = new bool[people.Length];
            int informedCount = 0;
            int steps = -1;
            HashSet<int> lastInformedPeople = new HashSet<int>();

            while (informedCount < people.Length && peopleToInform.Count > 0)
            {
                var newPeopleToInform = new HashSet<int>();
                foreach (var peopleId in peopleToInform)
                {
                    informedPeople[peopleId] = true;
                    informedCount++;
                    foreach (var friend in peopleWithFriends[peopleId])
                    {
                        if (!informedPeople[friend] && !peopleToInform.Contains(friend))
                        {
                            newPeopleToInform.Add(friend);
                        }
                    }
                }

                lastInformedPeople = peopleToInform;
                peopleToInform = newPeopleToInform;
                steps++;
            }

            if (informedCount == people.Length)
            {
                Console.WriteLine("All people reached in {0} steps", steps);
                var results = PrintPeople(lastInformedPeople);
                Console.WriteLine("People at last step: " + string.Join(", ", results));
            }
            else
            {
                List<string> results = new List<string>();
                for (int i = 0; i < informedPeople.Length; i++)
                {
                    if (!informedPeople[i])
                    {
                        results.Add(people[i]);
                    }
                }

                results.Sort();
                Console.WriteLine("Cannot reach: " + string.Join(", ", results));
            }
        }

        private static List<string> PrintPeople(HashSet<int> peopleByIds)
        {
            List<string> results = new List<string>(peopleByIds.Count);
            foreach (var peopleId in peopleByIds)
            {
                results.Add(people[peopleId]);
            }

            results.Sort();
            return results;
        }
    }
}
