namespace M3_DesignResilientCodeWithInterfaces;

public class Application(ILogger logger, IDataAccess dataAccess)
{
  // TASK 6: Implement ILogger and IDataAccess interfaces and
  // refactor to accept them as parameters.

  // *The Application class represents the main application logic, and
  // *is tightly coupled to ConsoleLogger and DatabaseAccess.
  // *Currently, the constructor directly instantiates the
  // *ConsoleLogger and DatabaseAccess classes, creating tight coupling.

  private readonly ILogger Logger = logger;
  private readonly IDataAccess DataAccess = dataAccess;

  // Runs the application logic.
  public void Run()
  {
    // Log the start of the application.
    Logger.Log("Application started.");

    // Connect to the database and retrieve data.
    DataAccess.Connect();
    var data = DataAccess.GetData();

    // Log the retrieved data.
    Logger.Log($"Data retrieved: {data}");

    // Log the end of the application.
    Logger.Log("Application finished.");
  }
}
