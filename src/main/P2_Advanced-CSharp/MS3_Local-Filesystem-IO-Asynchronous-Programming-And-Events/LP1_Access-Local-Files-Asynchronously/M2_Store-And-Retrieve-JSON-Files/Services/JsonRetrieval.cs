using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Files_M2;

public class JsonRetrieval
{
  private static readonly JsonSerializerOptions _options = new()
  {
    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
  };

  public static BankAccountDTO LoadBankAccountDTO(string filePath)
  {
    if (File.ReadAllText(filePath) is string json && !string.IsNullOrWhiteSpace(json))
    {
      if (JsonSerializer.Deserialize<BankAccountDTO>(json, _options) is BankAccountDTO accountDTO)
        return accountDTO;
      else
        throw new Exception("Account could not be deserialized.");
    }
    else
      throw new Exception("No account found.");
  }

  public static IEnumerable<Transaction> LoadAllTransactions(string filePath)
  {
    if (File.ReadAllText(filePath) is string jsonTransactions && !string.IsNullOrWhiteSpace(jsonTransactions))
    {
      if (
        JsonSerializer.Deserialize<IEnumerable<Transaction>>(jsonTransactions, _options)
        is IEnumerable<Transaction> transactions
      )
        return transactions;
      else
        throw new Exception("Transactions could not be deserialized.");
    }
    else
      throw new Exception("No transactions found.");
  }

  public static BankAccount LoadBankAccount(
    string accountFilePath,
    string transactionsDirectoryPath,
    BankCustomer customer
  )
  {
    IEnumerable<IBankAccount> existingCustomerAccounts = customer.GetAllAccounts();

    IEnumerable<Transaction> recoveredTransactions = [];
    var accountDTO = LoadBankAccountDTO(accountFilePath);
    IBankAccount? returnAccount = null;
    foreach (var existingAccount in existingCustomerAccounts)
    {
      if (existingAccount.AccountNumber == accountDTO.AccountNumber)
      {
        returnAccount = existingAccount;
        break;
      }
    }

    if (returnAccount is not null)
    {
      string transactionsFilePath = Path.Combine(
        transactionsDirectoryPath,
        $"{accountDTO.AccountNumber}-transactions.json"
      );

      if (File.Exists(transactionsFilePath))
        recoveredTransactions = LoadAllTransactions(transactionsFilePath);

      int finalExistingAccountTransaction = returnAccount.Transactions.Count - 1;
      int finalRecoveredTransaction = recoveredTransactions.Count() - 1;

      if (
        finalExistingAccountTransaction >= 0
        && finalRecoveredTransaction >= 0
        && returnAccount.Transactions.ElementAt(finalExistingAccountTransaction).TransactionDate
          < recoveredTransactions.ElementAt(finalRecoveredTransaction).TransactionDate
      )
      {
        foreach (var transaction in recoveredTransactions)
        {
          if (!returnAccount.Transactions.Contains(transaction))
          {
            returnAccount.AddTransaction(transaction);
          }
        }
      }

      return (BankAccount)returnAccount;
    }
    else
    {
      var recoveredBankAccount = new BankAccount(
        customer,
        customer.CustomerId,
        accountDTO.Balance,
        accountDTO.AccountType
      );

      string transactionsFilePath = Path.Combine(transactionsDirectoryPath, $"{accountDTO.AccountNumber}.json");

      recoveredTransactions = LoadAllTransactions(transactionsFilePath);

      foreach (var transaction in recoveredTransactions)
        recoveredBankAccount.AddTransaction(transaction);

      return recoveredBankAccount;
    }
  }

  public static string ReturnAccountFilePath(string directoryPath, int AccountNumber)
  {
    foreach (var filePath in Directory.GetFiles(Path.Combine(directoryPath, "Accounts"), "*.json"))
    {
      if (Path.GetFileNameWithoutExtension(filePath) == AccountNumber.ToString())
        return filePath;
    }
    throw new Exception("Account file not found.");
  }

  public static IEnumerable<BankAccount> LoadAllAccounts(
    string directoryPath,
    string transactionsDirectoryPath,
    BankCustomer customer
  )
  {
    List<BankAccount> accounts = [];
    foreach (var filePath in Directory.GetFiles(Path.Combine(directoryPath, "Accounts"), "*.json"))
      accounts.Add(LoadBankAccount(filePath, transactionsDirectoryPath, customer));
    return accounts;
  }

  public static BankCustomerDTO LoadBankCustomerDTO(string filePath)
  {
    if (File.ReadAllText(filePath) is string json && !string.IsNullOrEmpty(json))
    {
      if (JsonSerializer.Deserialize<BankCustomerDTO>(json, _options) is BankCustomerDTO deserializedJson)
        return deserializedJson;
      else
        throw new Exception("Customer could not be deserialized.");
    }
    else
      throw new Exception("No customer found.");
  }

  public static BankCustomer LoadBankCustomer(
    Bank bank,
    string filePath,
    string accountsDirectoryPath,
    string transactionsDirectoryPath
  )
  {
    var customerDTO = LoadBankCustomerDTO(filePath);
    var bankCustomer = bank.GetCustomerById(customerDTO.CustomerId);
    if (bankCustomer is null)
    {
      bankCustomer = new BankCustomer(customerDTO.FirstName, customerDTO.LastName, customerDTO.CustomerId, bank);
      bank.AddCustomer(bankCustomer);
    }

    foreach (var accountNumber in customerDTO.AccountNumbers)
    {
      var existingAccount = bankCustomer.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

      if (existingAccount == null)
      {
        var accountFilePath = Path.Combine(accountsDirectoryPath, $"{accountNumber}.json");
        var recoveredAccount = LoadBankAccount(accountFilePath, transactionsDirectoryPath, bankCustomer);

        if (recoveredAccount != null)
          bankCustomer.AddAccount(recoveredAccount);
      }
      else
        bankCustomer.AddAccount(existingAccount);
    }

    return bankCustomer;
  }

  public static IEnumerable<BankCustomer> LoadAllCustomers(
    Bank bank,
    string directoryPath,
    string accountsDirectoryPath,
    string transactionsDirectoryPath
  )
  {
    List<BankCustomer> customers = [];
    foreach (var filePath in Directory.GetFiles(Path.Combine(directoryPath, "Customers"), "*.json"))
      customers.Add(LoadBankCustomer(bank, filePath, accountsDirectoryPath, transactionsDirectoryPath));
    return customers;
  }
}
