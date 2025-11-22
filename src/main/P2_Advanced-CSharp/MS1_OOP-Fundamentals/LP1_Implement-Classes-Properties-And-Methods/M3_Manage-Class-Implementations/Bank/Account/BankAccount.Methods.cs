namespace M3_Classes;

public partial class BankAccount
{
  public bool CanWithdraw(decimal amount, bool isTransaction = false) =>
    amount > 0 && Balance >= (!isTransaction ? amount : amount + MaxTransactionFee);

  public void Deposit(decimal amount)
  {
    if (amount > 0)
      Balance += amount;
  }

  public bool Withdraw(decimal amount)
  {
    if (CanWithdraw(amount, false))
    {
      Balance -= amount;
      return true;
    }
    return false;
  }

  public bool Transfer(BankAccount targetAccount, decimal amount)
  {
    if (Withdraw(amount))
    {
      targetAccount.Deposit(amount);
      return true;
    }
    return false;
  }

  public void ApplyInterest(decimal years)
  {
    Balance += BankAccountCalculations.CompoundInterest(Balance, InterestRate, years);
  }

  public void ApplyRefund(decimal refund) => Balance += refund;

  public bool IssueCashiersCheck(decimal amount)
  {
    if (CanWithdraw(amount, true))
    {
      Balance -= amount;
      Balance -= BankAccountCalculations.TransactionFee(amount, TransactionRate, MaxTransactionFee);
      return true;
    }
    return false;
  }

  public string ReturnAccountInfo() =>
    $"AccountNumber: {AccountNumber}, Type: {AccountType}, Balance: {Balance}, Interest Rate: {InterestRate}, Customer ID: {CustomerId}";
}
