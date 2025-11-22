namespace M1_GetStartedWithDatesTimesAndTimeZones;

public abstract partial class BankAccount : IBankAccount
{
  public bool CanWithdraw(decimal amount) => amount > 0 && Balance >= amount;

  private string GenerateNextTransactionId() =>
    $"{CustomerId}-{AccountTypeIdentifier}-{$"{Transactions.Count + 1}".PadLeft(5, '0')}";

  public bool ProcessTransaction(
    decimal amount,
    string type,
    bool hasFee = false,
    Func<bool>? confirmApplyToBalance = null,
    DateTime? date = null
  )
  {
    if (amount == 0)
      return false;

    decimal fee = Math.Abs(
      hasFee ? BankAccountCalculations.TransactionFee(amount, TransactionRate, MaxTransactionFee) : 0
    );

    decimal finalAmount;
    if (amount < 0)
    {
      decimal totalDeduction = Math.Abs(amount) + fee;
      if (CanWithdraw(totalDeduction))
        finalAmount = -totalDeduction;
      else
        return false;
    }
    else
      finalAmount = amount - fee;

    if (!(confirmApplyToBalance is null || confirmApplyToBalance()))
      return false;

    Balance += finalAmount;
    Transactions.Add(new Transaction(GenerateNextTransactionId(), type, finalAmount, fee, date));

    return true;
  }

  public bool Deposit(decimal amount) => ProcessTransaction(amount, "deposit", false);

  public virtual bool Withdraw(decimal amount) => ProcessTransaction(amount, "withdraw", false);

  public bool ApplyRefund(decimal refund) => ProcessTransaction(refund, "refund", false);

  public bool Transfer(IBankAccount targetAccount, decimal amount, bool hasFee = false, int delayMs = 0) =>
    ProcessTransaction(
      amount,
      $"transfer-to:{targetAccount.CustomerId}-{targetAccount.AccountNumber}",
      hasFee,
      () =>
      {
        if (delayMs > 0)
          Thread.Sleep(delayMs);
        return targetAccount.Deposit(amount);
      }
    );

  public bool ApplyInterest(decimal years) =>
    ProcessTransaction(BankAccountCalculations.CompoundInterest(Balance, InterestRate, years), "interest-accrual");

  public bool IssueCashiersCheck(decimal amount) => ProcessTransaction(amount, "cashiers-check", true);

  public virtual string ReturnAccountInfo() =>
    $"AccountNumber: {AccountNumber}, Type: {AccountType}, Balance: {Balance:C}, Interest Rate: {InterestRate:P}, Customer ID: {CustomerId}, Transactions: {Transactions.Count}";

  public virtual string GetReportableName() => $"Account #: {AccountNumber}";

  public virtual string GetReportableType() => "Bank Account";
}
