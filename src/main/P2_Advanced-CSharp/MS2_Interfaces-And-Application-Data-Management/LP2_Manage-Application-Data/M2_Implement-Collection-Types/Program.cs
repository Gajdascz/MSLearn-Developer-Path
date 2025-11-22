using System.Collections.Generic; // TASK 7: Step 1 - Add System.Collections.Generic namespace
using System.Globalization;
using Data_M2;

#region> Task 1: Create and manage objects
// Step 1 - Create a Bank object
Bank myBank = new("MyBank");

// TASK 1: Step 2 - Create BankCustomer and BankAccount objects
BankCustomer customer1 = new("Alice", "Smith");
BankAccount account1 = new CheckingAccount(customer1, 1000);

// TASK 1: Step 3 - Add accounts to customers and customers to the bank
myBank.AddCustomer(customer1);
customer1.AddAccount(account1);

// TASK 1: Step 4 - Simulate deposits, withdrawals, and transfers
SimulateDepositWithdrawTransfer simulator = new();
SimulateDepositWithdrawTransfer.SimulateDeposit(account1, 500);
SimulateDepositWithdrawTransfer.SimulateWithdrawal(account1, 300);

// TASK 1: Step 5 - Use a HashSet to ensure unique transactions
HashSet<Transaction> uniqueTransactions = [];

// TASK 1: Step 6 - Generate a report of transactions
Console.WriteLine("\nTransaction Report:");
foreach (Transaction transaction in uniqueTransactions)
  Console.WriteLine(
    $"Transaction ID: {transaction.TransactionId}, type: {transaction.Type}, amount: {transaction.Amount:C}, Date: {transaction.Date}"
  );
#endregion

#region> Task 8: Generate transaction reports using a Dictionary
// TASK 8: Step 1 - Create a Dictionary to group transactions by account
Dictionary<string, List<Transaction>> transactionReports = [];

// TASK 8: Step 2 - Populate the Dictionary with transactions
foreach (BankCustomer customer in myBank.Customers)
{
  foreach (BankAccount account in customer.Accounts)
    transactionReports[account.AccountNumber.ToString()] = account.Transactions;
}

// TASK 8: Step 3 - Generate a report for a specific account
Console.WriteLine($"\nTransaction Report for Account {account1.AccountNumber}:");
if (transactionReports.TryGetValue(account1.AccountNumber.ToString(), out List<Transaction>? value))
{
  foreach (Transaction transaction in value)
    Console.WriteLine(
      $"Transaction ID: {transaction.TransactionId}, Type: {transaction.Type}, Amount: {transaction.Amount:C}, Date: {transaction.Date}"
    );
}
else
  Console.WriteLine("No transactions found for Account 12345.");
#endregion

#region> Task 9: Generate a report of transactions within a date range
// TASK 9: Step 1 - Prompt user for a date range
Console.WriteLine("\nEnter the start date (MM/DD/YYYY):");
string? startDateInput = Console.ReadLine();
DateTime startDate = DateTime.Parse(
  string.IsNullOrWhiteSpace(startDateInput) ? DateTime.Now.AddDays(-1).ToString() : startDateInput
);

Console.WriteLine("Enter the end date (MM/DD/YYYY):");
string? endDateInput = Console.ReadLine();
DateTime endDate = DateTime.Parse(string.IsNullOrWhiteSpace(endDateInput) ? DateTime.Now.ToString() : endDateInput);

// TASK 9: Step 2 - Filter transactions by date range
Console.WriteLine("\nTransactions within the specified date range:");
foreach (BankCustomer customer in myBank.Customers)
{
  foreach (BankAccount account in customer.Accounts)
  {
    foreach (Transaction transaction in account.Transactions)
    {
      if (transaction.Date >= startDate && transaction.Date <= endDate)
      {
        Console.WriteLine(
          $"Transaction ID: {transaction.TransactionId}, Type: {transaction.Type}, Amount: {transaction.Amount:C}, Date: {transaction.Date}"
        );
      }
    }
  }
}
#endregion

#region> Task 10: Generate a summary report of all transactions
// TASK 10: Step 1 - Calculate the total number of transactions and total amount
int totalTransactions = 0;
double totalAmount = 0;

foreach (var customer in myBank.Customers)
{
  foreach (var account in customer.Accounts)
  {
    totalTransactions += account.Transactions.Count;
    totalAmount += account.Transactions.Sum(t => t.Amount);
  }
}

// TASK 10: Step 2 - Display summary report
Console.WriteLine("\nSummary Report:");
Console.WriteLine($"Total Transactions: {totalTransactions}");
Console.WriteLine($"Total Amount: {totalAmount:C}");

#endregion
