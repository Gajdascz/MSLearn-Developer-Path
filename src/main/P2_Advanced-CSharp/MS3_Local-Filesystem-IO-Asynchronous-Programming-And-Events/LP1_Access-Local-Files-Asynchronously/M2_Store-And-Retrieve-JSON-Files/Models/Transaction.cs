using System;

namespace Files_M2;

// Represents a financial transaction with details such as date, time, amount, source and target accounts, type, and description.
public class Transaction(
  DateOnly date,
  TimeOnly time,
  double balance,
  double amount,
  int sourceAccountNum,
  int targetAccountNum,
  string typeOfTransaction,
  string descriptionMessage = ""
)
{
  public Guid TransactionId { get; set; } = Guid.NewGuid();
  public string TransactionType { get; set; } = typeOfTransaction;
  public DateOnly TransactionDate { get; set; } = date;
  public TimeOnly TransactionTime { get; set; } = time;
  public double PriorBalance { get; set; } = balance;
  public double TransactionAmount { get; set; } = amount;
  public int SourceAccountNumber { get; set; } = sourceAccountNum;
  public int TargetAccountNumber { get; set; } = targetAccountNum;
  public string Description { get; set; } = descriptionMessage;

  public Transaction()
    : this(
      date: default,
      time: default,
      amount: default,
      balance: default,
      sourceAccountNum: default,
      targetAccountNum: default,
      typeOfTransaction: "",
      descriptionMessage: ""
    ) { }

  // Determines whether the transaction is valid based on its type and details.
  public bool IsValidTransaction() =>
    (TransactionType, TransactionAmount, SourceAccountNumber == TargetAccountNumber) switch
    {
      ("Withdraw", <= 0, true) => true,
      ("Deposit", > 0, true) => true,
      ("Transfer", > 0, false) => true,
      ("Bank Fee", < 0, true) => true,
      ("Bank Refund", > 0, true) => true,
      _ => false,
    };

  // Returns a formatted string with transaction details for logging.
  public string ReturnTransaction()
  {
    return $"Transaction ID: {TransactionId}, Type: {TransactionType}, Date: {TransactionDate}, Time: {TransactionTime}, Prior Balance: {PriorBalance:C} Amount: {TransactionAmount:C}, Source Account: {SourceAccountNumber}, Target Account: {TargetAccountNumber}, Description: {Description}";
  }
}
