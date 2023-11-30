class Task
{
    string description;
    int priority;
    bool isCompleted;
    Date date;

    // Getters and setters for each field
    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public int Priority
    {
        get { return priority; }
        set { priority = value; }
    }

    public Date Date
    {
        get { return date; }
        set { date = value; }
    }

    public bool IsCompleted
    {
        get { return isCompleted; }
        set { isCompleted = value; }
    }

    // A 'Task' object holds a description of the task, its priority level (1-10), the date it's due, and its completion status (false by default)
    public Task(string desc, int priority, string date, bool isCompleted = false)
    {
        this.description = desc;
        this.priority = priority;
        this.isCompleted = isCompleted;

        // Assuming the user entered the date in dd/mm/yyyy format, the values can be retrieved between the '/' characters
        string[] date_values = date.Split('/');
        int day = int.Parse(date_values[0]);
        int month = int.Parse(date_values[1]);
        int year = int.Parse(date_values[2]);
        this.date = new Date(day, month, year); // Create a new Date object with the date-values and make it a field of the current Task object
    }


    public override string ToString()
    {
        // Convert the 'isCompleted' field into a string - if it's true, the string is set to 'Completed', otherwise it's set to 'Not completed'
        string status = (isCompleted) ? "Completed" : "Not completed";
        return $"---TASK---\nDescription: {description}\nPriority: {priority}\nStatus: {status}\nDate: {date}\n----------";
    }
}