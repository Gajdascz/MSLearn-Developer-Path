using System.Text;
using Files_M1;

#region> Use the Path, Directory, and File classes to create and enumerate directories and files
/*
// 1. Set path to temporary directory
string directoryPath = @"./Temp";
Console.WriteLine($"Directory path: {directoryPath}");

// 2. Define two subdirectories in temporary directory
string subDirectoryPath1 = Path.Combine(directoryPath, "SubDirectory1");
string subDirectoryPath2 = Path.Combine(directoryPath, "SubDirectory2");

// 3. Create four file paths to text files in the directory
string filePath = Path.Combine(directoryPath, "sample.txt");
string appendFilePath = Path.Combine(directoryPath, "append.txt");
string copyFilePath = Path.Combine(directoryPath, "copy.txt");
string moveFilePath = Path.Combine(directoryPath, "moved.txt");

Console.WriteLine(
  $"Text file paths ... Sample file path: {filePath}, Append file path: {appendFilePath}, Copy file path: {copyFilePath}, Move file path: {moveFilePath}"
);

// 5. Create directory if it does not exist
if (!Directory.Exists(directoryPath))
{
  Directory.CreateDirectory(directoryPath);
  Console.WriteLine($"Created directory at: {Path.GetFullPath(directoryPath)}");
}

// 6. Create subdirectories if they do not exist
if (!Directory.Exists(subDirectoryPath1))
{
  Directory.CreateDirectory(subDirectoryPath1);
  Console.WriteLine($"Created directory at: {Path.GetFullPath(subDirectoryPath1)}");
}
if (!Directory.Exists(subDirectoryPath2))
{
  Directory.CreateDirectory(subDirectoryPath2);
  Console.WriteLine($"Created directory at: {Path.GetFullPath(subDirectoryPath2)}");
}

// 7. Create and write text files to the directories
File.WriteAllText(filePath, "This is a sample file.");
File.WriteAllText(Path.Combine(subDirectoryPath1, "file1.txt"), "Content of file1 in SubDirectory 1");
File.WriteAllText(Path.Combine(subDirectoryPath2, "file2.txt"), "Content of file1 in SubDirectory 2");

// 8. Enumerate directories and files
Console.WriteLine("\nEnumerating directories and files ...\n");

foreach (var file in Directory.GetFiles(directoryPath))
{
  Console.WriteLine($"File: {file}");
}

foreach (var dir in Directory.GetDirectories(directoryPath))
{
  Console.WriteLine($"Directory: {dir}");
}

foreach (var subDir in Directory.GetDirectories(directoryPath))
{
  foreach (var file in Directory.GetFiles(subDir))
  {
    Console.WriteLine($"File: {file}");
  }
}
*/
#endregion

#region> Use the File class to perform file input/output operations
/*
Console.WriteLine("\nUse the File class to write and read CSV-formatted text files.");

// 1. Setup data
string label = "deposits";
double[,] depositValues =
{
  { 100.50, 200.75, 300.25 },
  { 150.00, 250.50, 350.75 },
  { 175.25, 275.00, 375.50 },
};

// 2. Create CSV string using StringBuilder (more performant as it works on a single string instance)
StringBuilder sb = new();
for (int i = 0; i < depositValues.GetLength(0); i++)
{
  sb.AppendLine($"{label}: {depositValues[i, 0]}, {depositValues[i, 1]}, {depositValues[i, 2]}");
}
string csvString = sb.ToString();

// 3. Split the CSV string by the environments newline character and remove empty entries
string[] csvLines = csvString.Split([Environment.NewLine], StringSplitOptions.RemoveEmptyEntries);
foreach (var line in csvLines)
{
  Console.WriteLine(line);
}

// 4. Write the csvString to a file
File.WriteAllText(filePath, csvString);

// 5. Read the file's content and display in console
string[] readLines = File.ReadAllLines(filePath);
Console.WriteLine($"\nContent read from the {filePath} file:");
foreach (var line in readLines)
{
  Console.WriteLine(line);
}

// 6. Append a new CSV entry to the file
File.AppendAllText(filePath, "deposits: 215.25, 417, 111.5\r\n");

// 7. Read the updated file and display content in console
string[] readUpdatedLines = File.ReadAllLines(filePath);
Console.WriteLine($"\nContent read from updated file at {filePath}:");
foreach (var line in readUpdatedLines)
{
  Console.WriteLine(line);
}

// 8. Extract label and value components from the CSV formatted string that was read from the file
string readLabel = readUpdatedLines[0].Split(':')[0];
double[,] readDepositValues = new double[readUpdatedLines.Length, 3];
for (int i = 0; i < readUpdatedLines.Length; i++)
{
  string[] parts = readUpdatedLines[i].Split(':');
  string[] values = parts[1].Split(',');
  for (int j = 0; j < values.Length; j++)
  {
    readDepositValues[i, j] = double.Parse(values[j]);
  }
}

// 9. Display parsed file-read CSV content to console
Console.WriteLine($"\nLabel: {readLabel}");
Console.WriteLine($"Deposit values:");
for (int i = 0; i < readDepositValues.GetLength(0); i++)
{
  Console.WriteLine($"{readDepositValues[i, 0]:C}, {readDepositValues[i, 1]:C}, {readDepositValues[i, 2]:C}");
}
*/
#endregion

#region> Use the File class to perform file management operations
/*
// 1. Check if file exists and print result
if (File.Exists(appendFilePath))
{
  Console.WriteLine($"The {appendFilePath} file exists.");
}
else
{
  Console.WriteLine($"The {appendFilePath} file does not exist.");
}

// 2. Copy sample.txt file to appendFilePath
File.Copy(filePath, copyFilePath, true);
Console.WriteLine($"Copied {filePath} to {copyFilePath}");

// 3. Move copy.txt file to moveFilePath
File.Move(copyFilePath, moveFilePath, true);
Console.WriteLine($"Moved {copyFilePath} to {moveFilePath}");

// 4. Delete append.txt file
if (File.Exists(moveFilePath))
{
  File.Delete(moveFilePath);
  Console.Write($"Deleted File: {moveFilePath}");
}
*/
#endregion


#region> Use the StreamWriter and StreamReader classes to read and write CSV files
Console.WriteLine("\nUse the StreamWriter and StreamReader classes.\n");

// 1. Create a TransactionLogs directory
string currentDirectory = Directory.GetCurrentDirectory();
Console.WriteLine($"Current directory: {currentDirectory}");

// Path will be in bin/Debug/netX.X because GetCurrentDirectory retrieves the directory of the executable program
string transactionDirectoryPath = Path.Combine(currentDirectory, "TransactionLogs");
if (!Directory.Exists(transactionDirectoryPath))
{
  Directory.CreateDirectory(transactionDirectoryPath);
  Console.WriteLine($"Created Directory: {transactionDirectoryPath}");
}

// 2. Create a path to a csv file to hold transaction data
string csvFilePath = Path.Combine(transactionDirectoryPath, "transactions.csv");

// 3. Generate simulated transaction data
string firstName = "Niki";
string lastName = "Demetriou";
BankCustomer customer = new BankCustomer(firstName, lastName);
customer.AddAccount(new CheckingAccount(customer, customer.CustomerId, 5000));
customer.AddAccount(new SavingsAccount(customer, customer.CustomerId, 15000));
customer.AddAccount(new MoneyMarketAccount(customer, customer.CustomerId, 90000));
DateOnly startDate = new DateOnly(2025, 3, 1);
DateOnly endDate = new DateOnly(2025, 3, 31);
customer = SimulateDepositsWithdrawalsTransfers.SimulateActivityDateRange(startDate, endDate, customer);

// 4. Use StreamWriter to write the data to the file
using (StreamWriter sw = new(csvFilePath))
{
  sw.WriteLine(
    "TransactionId,TransactionType,TransactionDate,TransactionTime,PriorBalance,TransactionAmount,SourceAccountNumber,TargetAccountNumber,Description"
  );
  Console.WriteLine("\nSimulated transactions:\n");
  foreach (var account in customer.Accounts)
  {
    foreach (var transaction in account.Transactions)
    {
      Console.WriteLine(
        $"{transaction.TransactionId},{transaction.TransactionType},{transaction.TransactionDate},{transaction.TransactionTime},{transaction.PriorBalance:F2},{transaction.TransactionAmount:F2},{transaction.SourceAccountNumber},{transaction.TargetAccountNumber},{transaction.Description}"
      );
      sw.WriteLine(
        $"{transaction.TransactionId},{transaction.TransactionType},{transaction.TransactionDate},{transaction.TransactionTime},{transaction.PriorBalance:F2},{transaction.TransactionAmount:F2},{transaction.SourceAccountNumber},{transaction.TargetAccountNumber},{transaction.Description}"
      );
    }
  }
}

// 5. Use StreamReader to read transaction data
using (StreamReader sr = new(csvFilePath))
{
  string headerLine = sr.ReadLine() ?? "NULL";
  Console.WriteLine("\nTransaction data read from the CSV file:\n");
  string? line;
  while ((line = sr.ReadLine()) != null)
  {
    Console.WriteLine(line);
  }
}
#endregion

#region> Use FileStream class to perform low-level file I/O operations
Console.WriteLine($"\n\nUse the FileStream class to perform file I/O operations.");

// 1. Create path to new filestream.txt file
string fileStreamPath = Path.Combine(currentDirectory, "filestream.txt");

// 2. Prepare and write transaction data
StringBuilder sb = new();
sb.AppendLine(
  "TransactionId,TransactionType,TransactionDate,TransactionTime,PriorBalance,TransactionAmount,SourceAccountNumber,TargetAccountNumber,Description"
);
foreach (var account in customer.Accounts)
{
  foreach (var transaction in account.Transactions)
  {
    sb.AppendLine(
      $"{transaction.TransactionId},{transaction.TransactionType},{transaction.TransactionDate},{transaction.TransactionTime},{transaction.PriorBalance:F2},{transaction.TransactionAmount:F2},{transaction.SourceAccountNumber},{transaction.TargetAccountNumber},{transaction.Description}"
    );
  }
}

// 3. Write data using FileStream
using (FileStream fileStream = new(fileStreamPath, FileMode.Create, FileAccess.Write))
{
  byte[] transactionDataBytes = new UTF8Encoding(true).GetBytes(sb.ToString());
  fileStream.Write(transactionDataBytes, 0, transactionDataBytes.Length);
  Console.WriteLine($"\nFile length after write: {fileStream.Length} bytes");
  fileStream.Flush();
}
Console.WriteLine($"\nTransaction data written using FileStream. File: {fileStreamPath}");

// 4. Read data using FileStream
using (FileStream fileStream = new(fileStreamPath, FileMode.Open, FileAccess.Read))
{
  byte[] readBuffer = new byte[1024];
  UTF8Encoding utf8Decoder = new(true);
  int bytesRead;

  Console.WriteLine("\nUsing FileStream to read/display transaction data.\n");

  while ((bytesRead = fileStream.Read(readBuffer, 0, readBuffer.Length)) > 0)
  {
    Console.WriteLine($"bytes read: {utf8Decoder.GetString(readBuffer, 0, bytesRead)}\n");
  }
  Console.WriteLine($"File length: {fileStream.Length} bytes");
  Console.WriteLine($"Current position: {fileStream.Position} bytes");

  fileStream.Seek(0, SeekOrigin.Begin);
  Console.WriteLine($"Position after seek: {fileStream.Position} bytes");

  bytesRead = fileStream.Read(readBuffer, 0, 100);
  Console.WriteLine($"Read first 100 bytes: {utf8Decoder.GetString(readBuffer, 0, bytesRead)}");
}
#endregion

#region> Use the BinaryWriter and BinaryReader classes to create and read binary files
// 1. Write data using BinaryWriter
string binaryFilePath = Path.Combine(currentDirectory, "binary.dat");
using (BinaryWriter binaryWriter = new(File.Open(binaryFilePath, FileMode.Create)))
{
  binaryWriter.Write(1.25);
  binaryWriter.Write("Hello");
  binaryWriter.Write(true);
}

// 2. Read data using BinaryReader
Console.WriteLine($"\n\nBinary data written to: {binaryFilePath}");
using (BinaryReader binaryReader = new(File.Open(binaryFilePath, FileMode.Open)))
{
  double a = binaryReader.ReadDouble();
  string b = binaryReader.ReadString();
  bool c = binaryReader.ReadBoolean();
  Console.WriteLine($"Binary data read from {binaryFilePath}: {a}, {b}, {c}");
}
#endregion
