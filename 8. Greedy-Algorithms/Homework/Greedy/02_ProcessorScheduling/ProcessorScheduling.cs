using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_ProcessorScheduling
{
    class Program
    {
        static void Main(string[] args)
        {
            var taskNumberParams = Console.ReadLine().Split(' ');
            var taskCount = int.Parse(taskNumberParams[1]);
            Task[] tasks = new Task[taskCount];

            for (int i = 0; i < taskCount; i++)
            {
                var taskParams = Console.ReadLine().Split(' ');
                int value = int.Parse(taskParams[0]);
                int deadline = int.Parse(taskParams[2]);
                tasks[i] = new Task(i+ 1, value, deadline);
            }

            var sortedTasks = tasks.OrderByDescending(t => t.Value).ToList();
            var maxDeadline = tasks.Max(t => t.Deadline);
            var choosedTasks = new List<Task>();
            var index = 0;
            var totalValue = 0;
            var maxAddedDeadline = 0;
            while (choosedTasks.Count() < maxDeadline && index < taskCount)
            {
                var currentTask = sortedTasks[index];
                index++;

                var futureMaxDeadLine = Math.Max(currentTask.Deadline, maxAddedDeadline);
                if (currentTask.Deadline <= choosedTasks.Count() && futureMaxDeadLine < choosedTasks.Count() + 1)
                {
                    continue;
                }

                choosedTasks.Add(currentTask);
                maxAddedDeadline = futureMaxDeadLine;

                totalValue += currentTask.Value;
            }

            choosedTasks.Sort();
            Console.Write("Optimal schedule: ");
            Console.WriteLine(string.Join(" -> ", choosedTasks));
            Console.WriteLine("Total value: " + totalValue);
        }
    }

    class Task: IComparable<Task>
    {
        public Task(int id, int value, int deadline)
        {
            this.Id = id;
            this.Value = value;
            this.Deadline = deadline;
        }

        public int Deadline { get; private set; }
        public int Id { get; private set; }
        public int Value { get; private set; }

        public int CompareTo(Task other)
        {
            if (this.Deadline == other.Deadline)
            {
                return other.Value.CompareTo(this.Value);
            }

            return this.Deadline.CompareTo(other.Deadline);
        }

        public override string ToString()
        {
            return this.Id.ToString();
        }
    }
}
