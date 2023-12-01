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
        this.date = TaskManagementSystem.ConvertStringToDate(date);
    }


    public override string ToString()
    {
        // Convert the 'isCompleted' field into a string - if it's true, the string is set to 'Completed', otherwise it's set to 'Not completed'
        string status = (isCompleted) ? "Completed" : "Not completed";
        return $"---TASK---\nDescription: {description}\nPriority: {priority}\nStatus: {status}\nDate: {date}\n----------";
    }
}