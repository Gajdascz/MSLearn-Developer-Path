namespace M1_GetStartedWithDatesTimesAndTimeZones;

public class ReportGenerator(IReportableEntity entity)
  : IMonthlyReportGenerator,
    IQuarterlyReportGenerator,
    IYearlyReportGenerator
{
  private IReportableEntity Entity { get; } = entity;

  public void GenerateMonthlyReport()
  {
    Console.WriteLine($"Generating monthly report for {Entity.GetReportableType()}: {Entity.GetReportableName()}");
  }

  public void GenerateCurrentMonthToDateReport()
  {
    Console.WriteLine(
      $"Generating current month-to-date report for {Entity.GetReportableType()}: {Entity.GetReportableName()}"
    );
  }

  public void GeneratePrevious30DayReport()
  {
    Console.WriteLine(
      $"Generating previous month report for {Entity.GetReportableType()}: {Entity.GetReportableName()}"
    );
  }

  public void GenerateQuarterlyReport()
  {
    Console.WriteLine($"Generating quarterly report for {Entity.GetReportableType()}: {Entity.GetReportableName()}");
  }

  public void GeneratePreviousYearReport()
  {
    Console.WriteLine(
      $"Generating previous year report for {Entity.GetReportableType()}: {Entity.GetReportableName()}"
    );
  }

  public void GenerateCurrentYearToDateReport()
  {
    Console.WriteLine(
      $"Generating current year-to-date report for {Entity.GetReportableType()}: {Entity.GetReportableName()}"
    );
  }

  public void GenerateLast365DaysReport()
  {
    Console.WriteLine(
      $"Generating last 365 days report for {Entity.GetReportableType()}: {Entity.GetReportableName()}"
    );
  }
}
