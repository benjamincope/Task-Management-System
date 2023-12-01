// All methods of class are static since no objects of TaskManagementSystem need to be created
using System.Threading.Tasks;

class TaskManagementSystem
{
    static List<Task> tasks = new List<Task>(); // Hold the list of the user's tasks as Task objects while the program is active
    static string file_name = "tasks.txt";
    


    // Convert a date entered as a string into a Date object. Method is public because it's needed when initialising a Task object
    public static Date ConvertStringToDate(string date) {
        try
        {
            // Assuming the user entered the date in dd/mm/yyyy format, the values can be retrieved between the '/' characters
            string[] date_values = date.Split('/');
            int day = int.Parse(date_values[0]);
            int month = int.Parse(date_values[1]);
            int year = int.Parse(date_values[2]);
            return new Date(day, month, year);
        } 
        catch (Exception)
        {
            Console.WriteLine($"Invalid date entered - the current date '{DateTime.Now.ToString("dd/MM/yyyy")}' will be used.");
            // If the user didn't enter the date in dd/mm/yyyy format, the date will be set to today's current date
            return new Date(int.Parse(DateTime.Now.Day.ToString()), int.Parse(DateTime.Now.Month.ToString()), int.Parse(DateTime.Now.Year.ToString()));
        }
    }



    // Create a new Task object and add it to the list of tasks
    static void AddTask(string desc, int priority, string date, bool isCompleted=false)
    {
        Task task = new Task(desc, priority, date, isCompleted);
        tasks.Add(task);
    }



    // Update the user's specified task with the new values they have inputted
    static void UpdateTask(int position, string desc, int priority, string date)
    {
        Task task = tasks[position - 1];
        task.Description = desc;
        task.Priority = priority;

        // Only change the value of the task's date if the inputted value is different, otherwise an identical Date object is created (memory inefficient)
        if (task.Date.ToString() != date)
        {
            task.Date = ConvertStringToDate(date);
        }
        Console.WriteLine("Task has been updated.");
        Console.ReadLine();
    }



    // Mark the user's specified task as completed
    static void CompleteTask(int position)
    {
        Task task = tasks[position - 1];
        task.IsCompleted = true;
        Console.WriteLine("Task has been marked as completed.");
        Console.ReadLine();
    }



    // Delete the user's specified task
    static void DeleteTask(int position)
    {
        tasks.Remove(tasks[position - 1]); // Delete the task at the index of the position, e.g. the task at position 2 would be at index 1
        Console.WriteLine("Task has been deleted.");
        Console.ReadLine();
    }



    // Display the user's uncompleted tasks
    static void ViewUncompletedTasks()
    {
        Console.WriteLine("--------------------");
        // Create a list of uncompleted tasks by going through each task in 'tasks' and only selecting the tasks that have their 'isCompleted' field set to false
        var uncompleted = from task in tasks 
                          where task.IsCompleted == false 
                          select task;
        foreach (Task task in uncompleted)
        {
            Console.WriteLine(task);
        }
        Console.WriteLine("--------------------");
        Console.ReadLine();
    }



    // Display all of the user's tasks
    static void ViewAllTasks()
    {
        Console.WriteLine("--------------------");
        int position = 1; // This position is used to give each task an ID to help the user
        foreach (Task task in tasks)
        {
            Console.WriteLine($"ID: {position}\n{task}");
            position ++;
        }
        Console.WriteLine("--------------------");
        Console.ReadLine();
    }



    // Save/rewrite the user's tasks to the 'tasks' text file
    static void SaveTasks()
    {
        using (StreamWriter writer = new StreamWriter(file_name)) // Open the text file in write-mode
        {
            foreach (Task task in tasks)
            {
                // Write every task to the file in the format of 'description|priority-level|date|completion-status'
                writer.WriteLine($"{task.Description}|{task.Priority}|{task.Date.SavedFormat()}|{task.IsCompleted}");
            }
        }
        Console.WriteLine("Tasks have been saved to file.");
        Console.ReadLine();
    }



    // Load the user's tasks from the 'tasks' text file (incase the user has used the program before)
    static void LoadTasks()
    {
        using (StreamReader reader = new StreamReader(file_name)) // Open the text file in read-mode
        {
            while (!reader.EndOfStream) // Read through the text file until the end of the file is reached
            {
                string task = reader.ReadLine();
                string[] task_values = task.Split('|'); // In the text file, the tasks are written in the format: 'description|priority|date|completion-status'
                AddTask(task_values[0], int.Parse(task_values[1]), task_values[2], bool.Parse(task_values[3]));
            }
        }
    }


    // Main menu
    static void Menu()
    {
        string input;
        do {
            Console.WriteLine("--------------------");
            Console.WriteLine("---MAIN MENU---");
            Console.WriteLine("1 - View all tasks");
            Console.WriteLine("2 - View uncompleted tasks");
            Console.WriteLine("3 - Add task");
            Console.WriteLine("4 - Update task info");
            Console.WriteLine("5 - Mark task as completed");
            Console.WriteLine("6 - Delete task");
            Console.WriteLine("7 - Save tasks");
            Console.WriteLine("8 - Exit");
            Console.WriteLine("--------------------");

            input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    ViewAllTasks();
                    break;

                case "2":
                    ViewUncompletedTasks();
                    break;

                case "3":
                    Console.WriteLine("Enter the following fields:");
                    Console.Write("Task description: ");
                    string desc = Console.ReadLine();
                    Console.Write("Priority level (1-10, 1=lowest, 10=highest): ");
                    int priority = int.Parse(Console.ReadLine());
                    Console.Write("Date due (dd/mm/yyyy): ");
                    string date = Console.ReadLine();

                    // Add the new task. Don't supply parameter for 'isCompleted' since new tasks should start off uncompleted
                    AddTask(desc, priority, date);
                    Console.WriteLine("Task added");
                    Console.ReadLine();
                    break;

                case "4":
                    Console.WriteLine("Enter the following fields:");
                    Console.Write("Position of task: "); // Requires the user to enter the position in the list that the task is at
                    int position = int.Parse(Console.ReadLine());
                    Console.Write("New task description: ");
                    string new_desc = Console.ReadLine();
                    Console.Write("New priority level: ");
                    int new_priority = int.Parse(Console.ReadLine());
                    Console.Write("New due date (dd/mm/yyyy): ");
                    string new_date = Console.ReadLine();

                    UpdateTask(position, new_desc, new_priority, new_date);
                    break;

                case "5":
                    Console.Write("Enter the position of the task you have completed: ");
                    int pos = int.Parse(Console.ReadLine());
                    CompleteTask(pos);
                    break;

                case "6":
                    Console.Write("Enter the position of the task you want to delete: ");
                    int task_pos = int.Parse(Console.ReadLine());
                    DeleteTask(task_pos);
                    break;

                case "7":
                    SaveTasks();
                    break;

                case "8":
                    Console.WriteLine("Exiting..");
                    SaveTasks(); // Save the tasks to the text file incase the user forgot to do so
                    break;

                default:
                    Console.WriteLine("Invalid input, please enter a number from 1-8.");
                    break;
            }


        } while (input != "8"); // Repeat this main-loop until the user enters '8'
    }



    public static void Main(string[] args)
    {
        Console.WriteLine("Main method");
        LoadTasks();
        Menu();
    }
}
