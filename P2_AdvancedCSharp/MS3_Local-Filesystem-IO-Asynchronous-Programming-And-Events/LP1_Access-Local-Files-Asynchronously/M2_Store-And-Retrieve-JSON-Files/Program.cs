using System.Text.Json;
using Files_M2;

Console.WriteLine(
  "Demonstrate JSON file storage and retrieval using BankCustomer, BankAccount, and Transaction classes"
);

Bank bank = new();

string firstName = "Niki";
string lastName = "Demetriou";
BankCustomer bankCustomer = new(firstName, lastName);

// Add Checking, Savings, and MoneyMarket accounts to bankCustomer
bankCustomer.AddAccount(new CheckingAccount(bankCustomer, bankCustomer.CustomerId, 5000));
bankCustomer.AddAccount(new SavingsAccount(bankCustomer, bankCustomer.CustomerId, 15000));
bankCustomer.AddAccount(new MoneyMarketAccount(bankCustomer, bankCustomer.CustomerId, 90000));

// Add the bank customer to the bank object
bank.AddCustomer(bankCustomer);
DateOnly startDate = new(2025, 2, 1);
DateOnly endDate = new(2025, 2, 28);
bankCustomer = SimulateDepositsWithdrawalsTransfers.SimulateActivityDateRange(startDate, endDate, bankCustomer);

string currentDirectory = Directory.GetCurrentDirectory();

string bankLogsDirectoryPath = Path.Combine(currentDirectory, "BankLogs");
if (Directory.Exists(bankLogsDirectoryPath))
  Directory.Delete(bankLogsDirectoryPath, true);

Directory.CreateDirectory(bankLogsDirectoryPath);
Console.WriteLine($"Created directory: {bankLogsDirectoryPath}");

string transactionsDirectory = Path.Combine(bankLogsDirectoryPath, "Transactions");
Directory.CreateDirectory(transactionsDirectory);

string accountsDirectoryPath = Path.Combine(bankLogsDirectoryPath, "Accounts");
Directory.CreateDirectory(accountsDirectoryPath);

/*
Transaction singleTransaction = bankCustomer.Accounts[0].Transactions.ElementAt(0);
string singleTransactionJson = JsonSerializer.Serialize(singleTransaction);
Console.WriteLine($"\nJSON string: {singleTransactionJson}");

if (JsonSerializer.Deserialize<Transaction>(singleTransactionJson) is Transaction deserializedTransaction)
{
  Console.WriteLine($"\nDeserialized transaction object: {deserializedTransaction.ReturnTransaction()}");
}
else
{
  Console.WriteLine(
    $"Deserialization failed. Check the Transaction class for public setters and a parameterless constructor"
  );
}

string transactionsJson = JsonSerializer.Serialize(bankCustomer.Accounts[0].Transactions);
Console.WriteLine($"\nbankCustomer.Accounts[0].Transactions serialized to JSON: \n{transactionsJson}");
string transactionsJsonFilePath = Path.Combine(
  bankLogsDirectoryPath,
  "Transactions",
  bankCustomer.Accounts[0].AccountNumber.ToString() + "-transactions" + ".json"
);

var directoryPath = Path.GetDirectoryName(transactionsJsonFilePath);
if (directoryPath is not null && !Directory.Exists(directoryPath))
  Directory.CreateDirectory(directoryPath);
File.WriteAllText(transactionsJsonFilePath, transactionsJson);
Console.WriteLine($"\nSerialized transactions saved to: {transactionsJsonFilePath}");

string transactionsJsonFromFile = File.ReadAllText(transactionsJsonFilePath);
if (
  JsonSerializer.Deserialize<IEnumerable<Transaction>>(transactionsJsonFromFile)
  is IEnumerable<Transaction> deserializedJsonFromFile
)
{
  Console.WriteLine($"\nDeserialized transactions:");
  foreach (var transaction in deserializedJsonFromFile)
  {
    Console.WriteLine(transaction.ReturnTransaction());
  }
}
else
{
  Console.WriteLine(
    "Deserialization failed. Check the Transaction class for public setters and a parameterless constructor."
  );
}
JsonSerializerOptions serializeAccountOptions = new()
{
  WriteIndented = true,
  ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
};
string accountJson = JsonSerializer.Serialize(bankCustomer.Accounts[0], serializeAccountOptions);
string accountFilePath = Path.Combine(
  bankLogsDirectoryPath,
  "Account",
  bankCustomer.Accounts[0].AccountNumber.ToString() + ".json"
);
if (Path.GetDirectoryName(accountFilePath) is string validAccountFilePath)
  Directory.CreateDirectory(validAccountFilePath);
File.WriteAllText(accountFilePath, accountJson);
Console.WriteLine($"Serialized account saved to: {accountFilePath}");

string accountJsonFromFile = File.ReadAllText(accountFilePath);
try
{
  if (JsonSerializer.Deserialize<BankAccount>(accountJsonFromFile) is BankAccount deserializedAccountFromJsonFile)
  {
    Console.WriteLine($"\nDeserialized BankAccount object: {deserializedAccountFromJsonFile.DisplayAccountInfo()}");
    Console.WriteLine($"Transactions for Account Number: {deserializedAccountFromJsonFile.AccountNumber}");
    foreach (var transaction in deserializedAccountFromJsonFile.Transactions)
    {
      Console.WriteLine(transaction.ReturnTransaction());
    }
  }
}
catch (Exception ex)
{
  string displayMessage = "Exception has occurred: " + ex.Message.Split('.')[0] + ".";
  displayMessage +=
    "\n\nConsider using Data Transfer Objects (DTOs) for serializing and deserializing complex and nested objects.";
  Console.WriteLine(displayMessage);
}




BankAccount customerAccount1 = (BankAccount)bankCustomer.Accounts[0];

string jsonAccountDTOFilePath = Path.Combine(
  accountsDirectoryPath,
  customerAccount1.AccountNumber.ToString() + ".json"
);

BankAccountDTO bankAccountDTO = BankAccountDTO.MapToDTO(customerAccount1);
string jsonAccountDto = JsonSerializer.Serialize(bankAccountDTO, serializeAccountOptions);
File.WriteAllText(jsonAccountDTOFilePath, jsonAccountDto);
Console.WriteLine($"Serialized account saved to: {jsonAccountDTOFilePath}");
string jsonTransactions = JsonSerializer.Serialize(customerAccount1.Transactions);
string jsonTransactionsFilePath = Path.Combine(
  transactionsDirectory,
  customerAccount1.AccountNumber.ToString() + ".json"
);
File.WriteAllText(jsonTransactionsFilePath, jsonTransactions);
Console.WriteLine($"Serialized account transactions saved to: {jsonTransactionsFilePath}");

jsonAccountDto = File.ReadAllText(jsonAccountDTOFilePath);
if (JsonSerializer.Deserialize<BankAccountDTO>(jsonAccountDto, serializeAccountOptions) is BankAccountDTO accountDTO)
{
  var recoveredBankAccount = new BankAccount(
    bankCustomer,
    bankCustomer.CustomerId,
    accountDTO.Balance,
    accountDTO.AccountType
  );
  jsonTransactions = File.ReadAllText(jsonTransactionsFilePath);
  if (
    JsonSerializer.Deserialize<IEnumerable<Transaction>>(jsonTransactions, serializeAccountOptions)
    is IEnumerable<Transaction> transactions
  )
  {
    foreach (var transaction in transactions)
    {
      recoveredBankAccount.AddTransaction(transaction);
    }
    Console.WriteLine($"\nRecovered BankAccount object: {recoveredBankAccount.DisplayAccountInfo()}");
    Console.WriteLine($"Transactions for Account Number: {recoveredBankAccount.AccountNumber}\n");
    foreach (var transaction in recoveredBankAccount.Transactions)
    {
      Console.WriteLine(transaction.ReturnTransaction());
    }
  }
  else
  {
    Console.WriteLine(
      "Deserialization failed. Check the Transaction class for public setters and a parameterless constructor."
    );
  }
}
else
{
  Console.WriteLine(
    "Deserialization failed. Check the BankAccountDTO class for public setters and a parameterless constructor."
  );
}
*/

BankAccount checkingAccount = (CheckingAccount)bankCustomer.Accounts[0];
JsonStorage.SaveBankAccount(checkingAccount, bankLogsDirectoryPath);

string retrieveAccountFilePath = JsonRetrieval.ReturnAccountFilePath(
  bankLogsDirectoryPath,
  checkingAccount.AccountNumber
);

BankAccount retrievedAccount = JsonRetrieval.LoadBankAccount(
  retrieveAccountFilePath,
  transactionsDirectory,
  bankCustomer
);

Console.WriteLine($"The owner of the retrieved account is: {retrievedAccount.Owner.ReturnFullName()}");
Console.WriteLine(
  $"{retrievedAccount.Owner.ReturnFullName()} has the following {retrievedAccount.Owner.Accounts.Count} accounts:"
);
foreach (var account in retrievedAccount.Owner.Accounts)
  Console.WriteLine($"Account number: {account.AccountNumber} is a {account.AccountType} account.");

Console.WriteLine($"\nRetrieved {retrievedAccount.AccountType} account info: {retrievedAccount.DisplayAccountInfo()}");

Console.WriteLine(
  $"The following transactions were retrieved for {retrievedAccount.Owner.ReturnFullName()}'s {retrievedAccount.AccountType} account: \n"
);

foreach (var transaction in retrievedAccount.Transactions)
  Console.WriteLine(transaction.ReturnTransaction());

string customersDirectoryPath = Path.Combine(bankLogsDirectoryPath, "Customers");
Directory.CreateDirectory(customersDirectoryPath);

JsonStorage.SaveBankCustomer(bankCustomer, bankLogsDirectoryPath);
Console.WriteLine($"\nBank customer information for {bankCustomer.ReturnFullName()} backed up to JSON files.");

bank.RemoveCustomer(bankCustomer);

string customerFilePath = Path.Combine(customersDirectoryPath, bankCustomer.CustomerId + ".json");

BankCustomer retrievedCustomer = JsonRetrieval.LoadBankCustomer(
  bank,
  customerFilePath,
  accountsDirectoryPath,
  transactionsDirectory
);

Console.WriteLine($"\nRetrieved customer information for {retrievedCustomer.ReturnFullName()}:");
Console.WriteLine($"Customer ID: {retrievedCustomer.CustomerId}");
Console.WriteLine($"First Name: {retrievedCustomer.FirstName}");
Console.WriteLine($"Last Name: {retrievedCustomer.LastName}");
Console.WriteLine($"Number of accounts: {retrievedCustomer.Accounts.Count}");

foreach (var account in retrievedCustomer.Accounts)
{
  Console.WriteLine($"\nAccount number: {account.AccountNumber} is a {account.AccountType} account.");
  Console.WriteLine($" - Balance: {account.Balance}");
  Console.WriteLine($" - Interest Rate: {account.InterestRate}");
  Console.WriteLine($" - Transactions:");
  foreach (var transaction in account.Transactions)
  {
    Console.WriteLine($"    {transaction.ReturnTransaction()}");
  }
}
