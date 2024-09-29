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
        private readonly NoteService noteService; // New NoteService

        public IndexModel()
        {
            string taskFilePath = "tasks.json"; 
            string noteFilePath = "notes.json"; //note file path
            taskService = new TaskService(); // Initialize TaskService
            noteService = new NoteService(); // Initialize NoteService
        }

        [BindProperty]

        public Note NewNote { get; set; } // New note to be added


        public List<Note> Notes { get; private set; } // Collection of existing notes
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

            // Load existing notes from database or any other source
            // This is just a placeholder. Replace with your actual code.
            Notes = noteService.GetNotes(); // Load notes using NoteService
        }

        public IActionResult OnPostAddNote()
        {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Add the new note to the database or any other source
        noteService.AddNote(NewNote); // Add note using NoteService

        // Clear the NewNote object so that the textarea will be cleared
        NewNote = new Note();

        return RedirectToPage();
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

public class Note
{
    public string Content { get; set; }
}