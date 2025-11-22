namespace M1_GetStartedWithDatesTimesAndTimeZones;

public class Transaction(string transactionId, string type, decimal amount, decimal? fee = null, DateTime? date = null)
{
  public string TransactionId { get; set; } = transactionId;
  public DateTime Date { get; set; } = date ?? DateTime.Now;
  public string Type { get; set; } = type;
  public decimal Amount { get; set; } = amount;

  public decimal? Fee { get; set; } = fee == 0 ? null : fee;

  public override string ToString() =>
    $"[{TransactionId}] {Type} on {Date} for {Amount:C}{(Fee is not null ? $", (fee: {Fee:C})" : null)}";
}
