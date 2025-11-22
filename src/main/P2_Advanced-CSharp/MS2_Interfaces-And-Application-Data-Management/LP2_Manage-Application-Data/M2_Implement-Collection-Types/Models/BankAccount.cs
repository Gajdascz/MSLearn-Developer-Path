using System;

namespace Data_M2;

// TASK 4: Update BankAccount Class

public class BankAccount : IBankAccount
{
  private static int s_nextAccountNumber;

  // Public read-only static properties
  public static double TransactionRate { get; private set; }
  public static double MaxTransactionFee { get; private set; }
  public static double OverdraftRate { get; private set; }
  public static double MaxOverdraftFee { get; private set; }

  public int AccountNumber { get; }
  public double Balance { get; internal set; } = 0;
  public string AccountType { get; set; } = "Checking";
  public virtual double InterestRate { get; protected set; } // Virtual property to allow overriding in derived classes

  // TASK 4: Step 1 - Add Owner property to reference BankCustomer
  public IBankCustomer Owner { get; set; }

  // TASK 4: Step 2 - Add List<Transaction> property to track transactions
  public List<Transaction> Transactions { get; set; } = [];

  static BankAccount()
  {
    Random random = new Random();
    s_nextAccountNumber = random.Next(10000000, 20000000);
    TransactionRate = 0.01; // Default transaction rate for wire transfers and cashier's checks
    MaxTransactionFee = 10; // Maximum transaction fee for wire transfers and cashier's checks
    OverdraftRate = 0.05; // Default penalty rate for an overdrawn checking account (negative balance)
    MaxOverdraftFee = 10; // Maximum overdraft fee for an overdrawn checking account
  }

  public BankAccount(IBankCustomer customer, double balance = 200, string accountType = "Checking")
  {
    AccountNumber = s_nextAccountNumber++;
    Owner = customer;
    Balance = balance;
    AccountType = accountType;
  }

  // Copy constructor for BankAccount
  public BankAccount(BankAccount existingAccount)
  {
    AccountNumber = s_nextAccountNumber++;
    Balance = existingAccount.Balance;
    AccountType = existingAccount.AccountType;
    Owner = existingAccount.Owner;
  }

  // TASK 4: Step 4 - Add method to store transaction (e.g., AddTransaction)
  public void AddTransaction(Transaction[] transactions) => Transactions.AddRange(transactions);

  public void AddTransaction(Transaction transaction) => Transactions.Add(transaction);

  // Method to deposit money into the account
  public void Deposit(double amount)
  {
    if (amount > 0)
    {
      Balance += amount;
      // TASK 4: Step 4a - Add logic to log the deposit transaction
      // Placeholder for logging the deposit transaction
    }
  }

  // Method to withdraw money from the account
  public virtual bool Withdraw(double amount)
  {
    if (amount > 0 && Balance >= amount)
    {
      Balance -= amount;
      // TASK 4: Step 4b - Add logic to log the withdrawal transaction
      // Placeholder for logging the withdrawal transaction
      return true;
    }
    return false;
  }

  // Method to transfer money to another account
  public bool Transfer(IBankAccount targetAccount, double amount)
  {
    if (Withdraw(amount))
    {
      targetAccount.Deposit(amount);
      // TASK 4: Step 4c - Add logic to log the transfer transaction
      // Placeholder for logging the transfer transaction

      return true;
    }
    return false;
  }

  // Method to apply interest
  public void ApplyInterest(double years)
  {
    double interest = AccountCalculations.CalculateCompoundInterest(Balance, InterestRate, years);
    Balance += interest;
    // TASK 4: Step 4d - Add logic to log the interest transaction
    // Placeholder for logging the interest transaction
  }

  // Method to apply refund
  public void ApplyRefund(double refund)
  {
    Balance += refund;
    // TASK 4: Step 4e - Add logic to log the refund transaction
    // Placeholder for logging the refund transaction
  }

  // Method to issue a cashier's check
  public bool IssueCashiersCheck(double amount)
  {
    if (amount > 0 && Balance >= amount + MaxTransactionFee)
    {
      Balance -= amount;
      double fee = AccountCalculations.CalculateTransactionFee(amount, TransactionRate, MaxTransactionFee);
      Balance -= fee;
      // TASK 4: Step 4f - Add logic to log the cashier's check transaction
      // Placeholder for logging the cashier's check and fee transactions
      return true;
    }
    return false;
  }

  // Method to display account information
  public virtual string DisplayAccountInfo()
  {
    return $"Account Number: {AccountNumber}, Type: {AccountType}, Balance: {Balance:C}, Interest Rate: {InterestRate:P}, Customer ID: {Owner.CustomerId}";
  }
}
