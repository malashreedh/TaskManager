using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagerApp.Models;
using System.Linq; // For FirstOrDefault
using System.Collections.Generic; // For List

//for Note class
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace TaskManagerApp.Pages
{
    //Initialize NewTask, Tasks, NewNote, and Notes in the constructor.
    public class IndexModel : PageModel
    {
       
        private readonly TaskService taskService; // Updated to TaskService

        public IndexModel()
        {
            taskService = new TaskService(); // Initialize TaskService
             NewTask = new UserTask();
            Tasks = new List<UserTask>();
            NewNote = new Note();
            Notes = new List<Note>();
        }

        [BindProperty]
        public UserTask NewTask { get; set; } // Update to UserTask

        public List<UserTask> Tasks { get; set; } // Update to UserTask



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


        //FOR NOTE CLASS


        
        [BindProperty]
        public Note NewNote { get; set; }
        public List<Note> Notes { get; set; }

        public async Task<IActionResult> OnPostAddNoteAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Read existing notes from notes.json
            var notes = new List<Note>();
            if (System.IO.File.Exists("notes.json"))
            {
                var json = await System.IO.File.ReadAllTextAsync("notes.json");
                notes = JsonSerializer.Deserialize<List<Note>>(json);
            }

            // Add the new note
            notes.Add(NewNote);

            // Write the updated list of notes back to notes.json
            var updatedJson = JsonSerializer.Serialize(notes);
            await System.IO.File.WriteAllTextAsync("notes.json", updatedJson);

            return RedirectToPage();
        }



        //for the following,
        //make sure that the Notes list is being properly
        //populated in the OnGetAsync method.
        public async Task OnGetAsync()
        {
            // Initialize Tasks to an empty list
            Tasks = taskService.GetTasks();
            if (System.IO.File.Exists("notes.json"))
            {
                var json = await System.IO.File.ReadAllTextAsync("notes.json"); // Declare the json variable here

                if (!string.IsNullOrEmpty(json))
                {
                    Notes = JsonSerializer.Deserialize<List<Note>>(json);
                }
                else
                {
                    Notes = new List<Note>();
                }
            }
            else
            {
                Notes = new List<Note>();
            }
        }

    }
}
