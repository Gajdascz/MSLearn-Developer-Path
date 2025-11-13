namespace Data_M2;

public interface IBankAccount
{
  int AccountNumber { get; }
  double Balance { get; }
  string AccountType { get; }
  public List<Transaction> Transactions { get; }
  public IBankCustomer Owner { get; }

  void Deposit(double amount);
  bool Withdraw(double amount);
  bool Transfer(IBankAccount targetAccount, double amount);
  void ApplyInterest(double years);
  void ApplyRefund(double refund);
  bool IssueCashiersCheck(double amount);
  string DisplayAccountInfo();
}
