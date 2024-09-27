using System;

namespace TaskManagerApp.Models
{
    public class UserTask // Renamed from Task to UserTask
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsComplete { get; private set; }

        public UserTask()
        {
            IsComplete = false; // Default to not complete
        }

        public void MarkComplete()
        {
            IsComplete = true; // Mark the task as complete
        }
    }
}
