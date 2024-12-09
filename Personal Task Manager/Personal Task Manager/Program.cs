using System;
using System.Collections.Generic;
using System.IO;

namespace TaskManager
{
    // Task Class
    public class Task
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Priority { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public Task(string title, string description, string category, string priority, DateTime dueDate)
        {
            Title = title;
            Description = description;
            Category = category;
            Priority = priority;
            DueDate = dueDate;
            IsCompleted = false;
        }

        public override string ToString()
        {
            return $"{Title} | {Category} | {Priority} | Due: {DueDate.ToShortDateString()} | Completed: {IsCompleted}";
        }
    }

    // Task Manager System
    public class TaskManagerSystem
    {
        private List<Task> tasks;

        public TaskManagerSystem()
        {
            tasks = new List<Task>();
        }

        // Add a new task
        public void AddTask(string title, string description, string category, string priority, DateTime dueDate)
        {
            Task newTask = new Task(title, description, category, priority, dueDate);
            tasks.Add(newTask);
            Console.WriteLine("Task added successfully.");
        }

        // View all tasks
        public void ViewTasks()
        {
            Console.WriteLine("Tasks List:");
            foreach (var task in tasks)
            {
                Console.WriteLine(task);
            }
        }

        // Mark a task as completed
        public void MarkTaskAsCompleted(string title)
        {
            Task task = tasks.Find(t => t.Title == title);
            if (task != null)
            {
                task.IsCompleted = true;
                Console.WriteLine($"Task '{title}' marked as completed.");
            }
            else
            {
                Console.WriteLine($"Task '{title}' not found.");
            }
        }

        // View tasks by category
        public void ViewTasksByCategory(string category)
        {
            Console.WriteLine($"Tasks in '{category}' category:");
            foreach (var task in tasks)
            {
                if (task.Category == category)
                {
                    Console.WriteLine(task);
                }
            }
        }

        // View tasks by priority
        public void ViewTasksByPriority(string priority)
        {
            Console.WriteLine($"Tasks with '{priority}' priority:");
            foreach (var task in tasks)
            {
                if (task.Priority == priority)
                {
                    Console.WriteLine(task);
                }
            }
        }

        // View tasks that are overdue
        public void ViewOverdueTasks()
        {
            DateTime currentDate = DateTime.Now;
            Console.WriteLine("Overdue Tasks:");
            foreach (var task in tasks)
            {
                if (task.DueDate < currentDate && !task.IsCompleted)
                {
                    Console.WriteLine(task);
                }
            }
        }

        // Save tasks to a file
        public void SaveDataToFile()
        {
            using (StreamWriter writer = new StreamWriter("tasks_data.txt"))
            {
                foreach (var task in tasks)
                {
                    writer.WriteLine($"{task.Title}|{task.Description}|{task.Category}|{task.Priority}|{task.DueDate.ToShortDateString()}|{task.IsCompleted}");
                }
            }
            Console.WriteLine("Data saved to file.");
        }

        // Load tasks from a file
        public void LoadDataFromFile()
        {
            if (File.Exists("tasks_data.txt"))
            {
                string[] lines = File.ReadAllLines("tasks_data.txt");

                foreach (var line in lines)
                {
                    string[] parts = line.Split('|');
                    string title = parts[0];
                    string description = parts[1];
                    string category = parts[2];
                    string priority = parts[3];
                    DateTime dueDate = DateTime.Parse(parts[4]);
                    bool isCompleted = bool.Parse(parts[5]);

                    Task newTask = new Task(title, description, category, priority, dueDate)
                    {
                        IsCompleted = isCompleted
                    };
                    tasks.Add(newTask);
                }
                Console.WriteLine("Data loaded from file.");
            }
            else
            {
                Console.WriteLine("No existing task data found.");
            }
        }
    }

    // Program Class (Main entry point)
    class Program
    {
        static void Main(string[] args)
        {
            TaskManagerSystem taskManager = new TaskManagerSystem();
            taskManager.LoadDataFromFile();

            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("Personal Task Manager");
                Console.WriteLine("1. Add New Task");
                Console.WriteLine("2. View All Tasks");
                Console.WriteLine("3. Mark Task as Completed");
                Console.WriteLine("4. View Tasks by Category");
                Console.WriteLine("5. View Tasks by Priority");
                Console.WriteLine("6. View Overdue Tasks");
                Console.WriteLine("7. Save Data");
                Console.WriteLine("8. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // Add New Task
                        Console.Write("Enter Task Title: ");
                        string title = Console.ReadLine();
                        Console.Write("Enter Task Description: ");
                        string description = Console.ReadLine();
                        Console.Write("Enter Task Category: ");
                        string category = Console.ReadLine();
                        Console.Write("Enter Task Priority (High, Medium, Low): ");
                        string priority = Console.ReadLine();
                        Console.Write("Enter Task Due Date (MM/DD/YYYY): ");
                        DateTime dueDate = DateTime.Parse(Console.ReadLine());

                        taskManager.AddTask(title, description, category, priority, dueDate);
                        break;

                    case "2":
                        // View All Tasks
                        taskManager.ViewTasks();
                        break;

                    case "3":
                        // Mark Task as Completed
                        Console.Write("Enter Task Title to Mark as Completed: ");
                        string taskTitle = Console.ReadLine();
                        taskManager.MarkTaskAsCompleted(taskTitle);
                        break;

                    case "4":
                        // View Tasks by Category
                        Console.Write("Enter Category to View Tasks: ");
                        string taskCategory = Console.ReadLine();
                        taskManager.ViewTasksByCategory(taskCategory);
                        break;

                    case "5":
                        // View Tasks by Priority
                        Console.Write("Enter Priority to View Tasks (High, Medium, Low): ");
                        string taskPriority = Console.ReadLine();
                        taskManager.ViewTasksByPriority(taskPriority);
                        break;

                    case "6":
                        // View Overdue Tasks
                        taskManager.ViewOverdueTasks();
                        break;

                    case "7":
                        // Save Data
                        taskManager.SaveDataToFile();
                        break;

                    case "8":
                        // Exit
                        taskManager.SaveDataToFile();
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid Option! Please try again.");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }
}

