class Date
{
    // Holds months that are indexed from 0 to 11, e.g. if the month = 10, this corresponds to the element at index [10-1] which is October
    static string[] months = {"January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December" };

    int day, month, year;

    // A 'Date' object holds integer values for the day/month/year in dd/mm/yyyy format
    public Date(int day, int month, int year)
    {
        this.day = day;
        this.month = month;
        this.year = year;
    }

    // When a date is output, it is displayed in this format. The month value is converted into a string by looking it up in the 'months' array
    public override string ToString()
    {
        return $"{day} {months[month - 1]}, {year}";
    }

    public string SavedFormat()
    {
        return $"{day}/{month}/{year}";
    }
}