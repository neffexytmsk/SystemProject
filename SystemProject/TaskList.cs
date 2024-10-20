using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemProject
{
    public class Task
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int AssignedTo { get; set; }
    }
    public class TaskList
    {
        private string PATH = "task.json";
        private List<Task> tasks;

        public TaskList()
        {
            LoadTask();
        }

        private void LoadTask()
        {
            if (File.Exists(PATH))
            {
                var json = File.ReadAllText(PATH);
                tasks = JsonConvert.DeserializeObject<List<Task>>(json) ?? new List<Task>();
            }
            else
            {
                tasks = new List<Task>();
            }
        }
        public void SaveTasks()
        {
            var json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
            File.WriteAllText(PATH, json);
        }

        public void CreateTask(Task task)
        {
            task.ID = tasks.Count > 0 ? tasks.Max(x => x.ID) + 1 : 1;
            tasks.Add(task);
            SaveTasks();
        }

        public List<Task> GetTasksByUser(int userID)
        {
            return tasks.Where(x => x.AssignedTo == userID).ToList();
        }

        public void UpdateTaskStatus(int taskProjectID, string status)
        {
            var task = tasks.FirstOrDefault(x => x.ID == taskProjectID);
            if (task != null)
            {
                task.Status = status;
                SaveTasks();
                Console.WriteLine("Статус задачи обновлен.");
            }
            else
            {
                Console.WriteLine("Задача не найдена");
            }
        }
    }
}
