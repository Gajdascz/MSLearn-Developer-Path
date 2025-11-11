namespace M1_GetStartedWithDatesTimesAndTimeZones;

public abstract partial class BankAccount : IBankAccount
{
  public string AccountType { get; set; }
  public string AccountTypeIdentifier { get; protected set; }

  public decimal Balance { get; protected set; } = 0;

  public int AccountNumber { get; } = Random.Shared.Next(Constants.IdRange.low, Constants.IdRange.high);
  public string CustomerId { get; }
  public virtual decimal InterestRate { get; protected set; }
  public static decimal TransactionRate { get; protected set; } = 0.01m;
  public static decimal MaxTransactionFee { get; protected set; } = 10;
  public static decimal OverdraftRate { get; protected set; } = 0.05m;
  public static decimal MaxOverdraftFee { get; protected set; } = 10;

  public List<Transaction> Transactions { get; set; } = [];

  private static int s_nextAccountNumber = 1;

  public BankAccount(BankAccount existingAccount)
  {
    AccountNumber = s_nextAccountNumber++;
    CustomerId = existingAccount.CustomerId;
    Balance = existingAccount.Balance;
    AccountType = existingAccount.AccountType;
    Transactions = existingAccount.Transactions;
    AccountTypeIdentifier = existingAccount.AccountTypeIdentifier;
  }

  public BankAccount(string customerId, string accountType, string accountTypeIdentifier)
  {
    AccountNumber = s_nextAccountNumber++;
    CustomerId = customerId;
    AccountType = accountType;
    AccountTypeIdentifier = accountTypeIdentifier;
  }

  public BankAccount(
    string customerId,
    decimal balance = 200,
    string accountType = "Checking",
    string accountTypeIdentifier = "CHK"
  )
  {
    AccountNumber = s_nextAccountNumber++;
    CustomerId = customerId;
    Balance = balance;
    AccountType = accountType;
    AccountTypeIdentifier = accountTypeIdentifier;
  }
}
