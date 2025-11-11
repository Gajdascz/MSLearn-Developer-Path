namespace M1_GetStartedWithDatesTimesAndTimeZones;

public interface IBankCustomer : IReportableEntity
{
  string FirstName { get; set; }
  string LastName { get; set; }
  string CustomerId { get; }

  string ReturnFullName();
  void UpdateName(string firstName, string lastName);
  string ReturnCustomerInfo();
}
