public class MyDependency: IMyDependency {
    
    public void LogMessage(string message) {
        Console.WriteLine($"Message:  {message}");
    }
}