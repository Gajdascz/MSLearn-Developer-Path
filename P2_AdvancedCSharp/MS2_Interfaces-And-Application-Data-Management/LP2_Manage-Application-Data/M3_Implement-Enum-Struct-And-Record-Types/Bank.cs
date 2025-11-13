using System;
using System.Collections.Generic;

namespace BankApp
{
  // TASK 1: Define the AccountType enum
  public enum AccountType
  {
    Checking,
    Savings,
    Business,
  }

  // TASK 2: Define the Transaction struct
  public readonly struct Transaction(double amount, string description, DateTime? date = null)
  {
    public double Amount { get; } = amount;
    public DateTime Date { get; } = date ?? DateTime.Now;
    public string Description { get; } = description;

    public override string ToString() => $"{Date:d}: {Description} - {Amount:C}";
  }

  // TASK 3: Define the Customer record
  public record Customer(string Name, string CustomerId, string Address);

  // TASK 4: Implement the BankAccount class
  public class BankAccount(int accountNumber, AccountType type, Customer accountHolder, double initialBalance = 0)
  {
    public int AccountNumber { get; set; } = accountNumber;
    public AccountType Type { get; set; } = type;
    public Customer AccountHolder { get; set; } = accountHolder;
    public double Balance { get; set; } = initialBalance;
    private List<Transaction> Transactions = [];

    public void AddTransaction(double amount, string description)
    {
      Balance += amount;
      Transactions.Add(new Transaction(amount, description));
    }

    public override string ToString() =>
      $"Account Holder: {AccountHolder.Name}, Account Number: {AccountNumber}, Type: {Type}, Balance: {Balance:C}";

    public void DisplayTransactions()
    {
      Console.WriteLine("Transactions:");
      foreach (Transaction transaction in Transactions)
        Console.WriteLine(transaction);
    }
  }
}
