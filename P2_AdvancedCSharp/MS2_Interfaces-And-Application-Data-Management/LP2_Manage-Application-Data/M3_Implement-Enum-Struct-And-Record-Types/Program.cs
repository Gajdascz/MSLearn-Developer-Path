using System;

namespace BankApp
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Welcome to the Bank App!");
      (string name, string id, string address) customerInfo = ("Tim Shao", "C123456789", "123 Elm Street");
      Customer customer = new(customerInfo.name, customerInfo.id, customerInfo.address);

      // TASK 5: Display Basic Bank Account Information

      // Create a basic BankAccount
      BankAccount account = new(
        accountNumber: 123456789,
        type: AccountType.Checking,
        accountHolder: customer,
        initialBalance: 500
      );
      Console.WriteLine(account);

      // TASK 6: Perform Transactions
      account.AddTransaction(200, "Deposit");
      Console.WriteLine($"After deposit: {account}");

      // Perform a withdrawal
      account.AddTransaction(-50, "ATM Withdrawl");
      Console.WriteLine($"After withdrawal: {account}");

      // TASK 7: Display Transaction History
      account.DisplayTransactions();

      // TASK 8: Demonstrate Record Comparison
      Customer customer2 = new(customerInfo.name, customerInfo.id, customerInfo.address);
      Console.WriteLine($"Are customers equal? {customer == customer2}");

      // Task 9: Demonstrate Immutability of Structs
      Transaction transaction = new(100, "Test Transaction");
      Console.WriteLine($"Immutable Transaction: {transaction}");
    }
  }
}
