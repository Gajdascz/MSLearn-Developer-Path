// TASK 3: Refactor the code to use an IDataAccess interface.

// This class is responsible for connecting to a database and retrieving data.
// It is currently tightly coupled to the Application class.
namespace M3_DesignResilientCodeWithInterfaces;

public class DatabaseAccess : IDataAccess
{
  // Simulates connecting to a database.
  public void Connect()
  {
    Console.WriteLine("Database connected.");
  }

  // Simulates retrieving data from the database.
  public string GetData()
  {
    return "Sample Data from Database";
  }
}
