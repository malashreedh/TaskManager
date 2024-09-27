using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TaskManagerApp.Models
{
    public class TaskService // Renamed from TaskManager to TaskService
    {
        private List<UserTask> tasks = new List<UserTask>(); // Update to UserTask
        private readonly string filePath;
        public TaskService()
        {
            filePath = Path.Combine(Directory.GetCurrentDirectory(), "tasks.json");
            LoadTasks();
        }

        public void AddTask(UserTask task) // Update to UserTask
        {
            tasks.Add(task);
            SaveTasks();
        }

        public void RemoveTask(UserTask task) // Update to UserTask
        {
            tasks.Remove(task);
            SaveTasks();
        }

        public List<UserTask> GetTasks() => tasks; // Update to UserTask

        public void MarkTaskComplete(UserTask task) // Update to UserTask
        {
            task.MarkComplete();
            SaveTasks();
        }

        private void SaveTasks()
        {
            var json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        private void LoadTasks()
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                tasks = JsonConvert.DeserializeObject<List<UserTask>>(json) ?? new List<UserTask>(); // Update to UserTask
            }
        }
    }
}