namespace M1_Inheritance;

public interface IBankAccount
{
  int AccountNumber { get; }
  string CustomerId { get; }
  decimal Balance { get; }
  string AccountType { get; }
  void Deposit(decimal amount);
  bool Withdraw(decimal amount);
  bool Transfer(IBankAccount targetAccount, decimal amount);
  void ApplyInterest(decimal years);
  void ApplyRefund(decimal refund);
  bool IssueCashiersCheck(decimal amount);
  string ReturnAccountInfo();
}
