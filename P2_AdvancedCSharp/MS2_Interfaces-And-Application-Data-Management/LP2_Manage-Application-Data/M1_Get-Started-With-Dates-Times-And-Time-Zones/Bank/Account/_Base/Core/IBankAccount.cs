namespace M1_GetStartedWithDatesTimesAndTimeZones;

public interface IBankAccount : IReportableEntity
{
  int AccountNumber { get; }
  string CustomerId { get; }
  decimal Balance { get; }
  string AccountType { get; }
  string AccountTypeIdentifier { get; }
  public List<Transaction> Transactions { get; }
  bool Deposit(decimal amount);
  bool Withdraw(decimal amount);
  bool Transfer(IBankAccount targetAccount, decimal amount, bool hasFee, int delayMs);
  bool ApplyInterest(decimal years);
  bool ApplyRefund(decimal refund);
  bool IssueCashiersCheck(decimal amount);
  string ReturnAccountInfo();

  // This would typically be a private method but for demonstration purposes its made public
  bool ProcessTransaction(
    decimal amount,
    string type,
    bool hasFee = false,
    Func<bool>? confirmApplyToBalance = null,
    DateTime? date = null
  );
}
