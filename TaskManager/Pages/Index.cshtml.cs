using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagerApp.Models;
using System.Linq; // For FirstOrDefault
using System.Collections.Generic; // For List

namespace TaskManagerApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly TaskService taskService; // Updated to TaskService

        public IndexModel()
        {
            taskService = new TaskService(); // Initialize TaskService
        }

        [BindProperty]
        public UserTask NewTask { get; set; } // Update to UserTask

        public List<UserTask> Tasks { get; set; } // Update to UserTask

        // Properties to track overdue and due-soon tasks
        public int OverdueCount { get; set; }
        public int DueSoonCount { get; set; }

        public void OnGet()
        {
            Tasks = taskService.GetTasks(); // Update to UserTask

            // Calculate overdue and due-soon tasks
            OverdueCount = Tasks.Count(t => t.Deadline < System.DateTime.Now && !t.IsComplete); // Update to UserTask
            DueSoonCount = Tasks.Count(t => t.Deadline >= System.DateTime.Now && t.Deadline <= System.DateTime.Now.AddDays(7) && !t.IsComplete); // Update to UserTask
        }

        public IActionResult OnPostAddTask()
        {
            if (ModelState.IsValid)
            {
                taskService.AddTask(NewTask); // Update to UserTask
                return RedirectToPage(); // Refresh the page
            }
            return Page();
        }

        public IActionResult OnPostCompleteTask(string title)
        {
            var task = taskService.GetTasks().FirstOrDefault(t => t.Title == title); // Update to UserTask
            if (task != null)
            {
                taskService.MarkTaskComplete(task); // Update to UserTask
            }
            return RedirectToPage(); // Refresh the page
        }

        public IActionResult OnPostRemoveTask(string title)
        {
            var task = taskService.GetTasks().FirstOrDefault(t => t.Title == title); // Update to UserTask
            if (task != null)
            {
                taskService.RemoveTask(task); // Update to UserTask
            }
            return RedirectToPage(); // Refresh the page
        }
    }
}
