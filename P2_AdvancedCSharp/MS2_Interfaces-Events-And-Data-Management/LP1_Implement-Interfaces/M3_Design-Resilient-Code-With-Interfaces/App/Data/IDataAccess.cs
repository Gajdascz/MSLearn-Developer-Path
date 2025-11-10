namespace M3_DesignResilientCodeWithInterfaces;

// TASK 2: Create an interface ILogger with a method Log(string message).

public interface IDataAccess
{
  void Connect(); // Connects to the data source.
  string GetData(); // Retrieves data from the data source.
}
