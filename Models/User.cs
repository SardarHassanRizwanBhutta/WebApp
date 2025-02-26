namespace WebApp.Models;
// Model are classes that represents data which the app manages
public class User
{
    public long Id { get; set; } // serves as a unique key in relational database
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
}