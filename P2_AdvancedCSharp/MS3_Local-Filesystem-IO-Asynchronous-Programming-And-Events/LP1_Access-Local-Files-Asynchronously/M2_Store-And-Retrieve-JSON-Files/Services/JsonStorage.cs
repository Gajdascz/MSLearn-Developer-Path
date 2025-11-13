using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Files_M2;

public class JsonStorage
{
  private static readonly JsonSerializerOptions _options = new()
  {
    WriteIndented = true,
    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
  };

  public static void SaveBankAccount(BankAccount account, string directoryPath)
  {
    var accountDTO = new BankAccountDTO()
    {
      AccountNumber = account.AccountNumber,
      CustomerId = account.CustomerId,
      Balance = account.Balance,
      AccountType = account.AccountType,
      InterestRate = account.InterestRate,
    };
    string accountFilePath = Path.Combine(directoryPath, "Accounts", $"{account.AccountNumber}.json");
    var accountDirectoryPath = Path.GetDirectoryName(accountFilePath);
    if (Path.GetDirectoryName(accountFilePath) is string validPath && !Directory.Exists(validPath))
    {
      Directory.CreateDirectory(validPath);
    }
    string json = JsonSerializer.Serialize(accountDTO, _options);
    File.WriteAllText(accountFilePath, json);
    SaveAllTransactions(account.Transactions, directoryPath, account.AccountNumber);
  }

  public static void SaveAllTransactions(IEnumerable<Transaction> transactions, string directoryPath, int AccountNumber)
  {
    string transactionsFilePath = Path.Combine(directoryPath, "Transactions", $"{AccountNumber}.json");
    if (Path.GetDirectoryName(transactionsFilePath) is string validPath && !Directory.Exists(validPath))
    {
      Directory.CreateDirectory(validPath);
    }
    string json = JsonSerializer.Serialize(transactions, _options);
    File.WriteAllText(transactionsFilePath, json);
  }

  public static void SaveAllAccounts(IEnumerable<IBankAccount> accounts, string directoryPath)
  {
    foreach (var account in accounts)
      SaveBankAccount((BankAccount)account, directoryPath);
  }

  public static void SaveBankCustomer(BankCustomer customer, string directoryPath)
  {
    BankCustomerDTO customerDTO = new()
    {
      CustomerId = customer.CustomerId,
      FirstName = customer.FirstName,
      LastName = customer.LastName,
      AccountNumbers = [],
    };
    foreach (var account in customer.Accounts)
      customerDTO.AccountNumbers.Add(account.AccountNumber);

    string customerFilePath = Path.Combine(directoryPath, "Customers", $"{customer.CustomerId}.json");
    if (Path.GetDirectoryName(customerFilePath) is string validPath && !Directory.Exists(validPath))
      Directory.CreateDirectory(validPath);

    string json = JsonSerializer.Serialize(customerDTO, _options);
    File.WriteAllText(customerFilePath, json);
    SaveAllAccounts(customer.Accounts, directoryPath);
  }

  public static void SaveAllCustomers(IEnumerable<BankCustomer> customers, string directoryPath)
  {
    foreach (var customer in customers)
      SaveBankCustomer(customer, directoryPath);
  }
}
