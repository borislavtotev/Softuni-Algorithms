using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_BestLecturesSchedule
{
    class Program
    {
        static void Main(string[] args)
        {
            var lectureNumberParams = Console.ReadLine().Split(' ');
            int n = int.Parse(lectureNumberParams[1]);
            Lecture[] lectures = new Lecture[n];

            for (int i = 0; i < n; i++)
            {
                var lectureParams = Console.ReadLine().Split(' ');
                var startTime = int.Parse(lectureParams[1]);
                var endTime = int.Parse(lectureParams[3]);
                var name = lectureParams[0];
                lectures[i] = new Lecture(startTime, endTime, name);
            }

            var choosenLectures = new List<Lecture>();
            var sortedLectures = lectures.OrderBy(l => l.EndTime).ToList();
            while (sortedLectures.Count() > 0)
            {
                var currentLecture = sortedLectures.First();
                choosenLectures.Add(currentLecture);
                var lecturesForRemove = sortedLectures.Where(l => l.StartTime <= currentLecture.EndTime).ToList();
                foreach (var lecture in lecturesForRemove)
                {
                    sortedLectures.Remove(lecture);
                }
            }

            Console.WriteLine("Lectures ({0})", choosenLectures.Count);
            foreach (var lecture in choosenLectures)
            {
                Console.WriteLine(lecture);
            }
        }
    }

    public class Lecture: IComparable<Lecture> 
    {
        public Lecture(int startTime, int endTime, string name)
        {
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Name = name;
        }

        public int EndTime { get; set; }

        public string Name { get; set; }

        public int StartTime { get; set; }

        public int CompareTo(Lecture other)
        {
            return this.EndTime.CompareTo(other.EndTime);
        }

        public override string ToString()
        {
            return string.Format("{0}-{1} -> {2}", this.StartTime, this.EndTime, this.Name);
        }
    }
}
