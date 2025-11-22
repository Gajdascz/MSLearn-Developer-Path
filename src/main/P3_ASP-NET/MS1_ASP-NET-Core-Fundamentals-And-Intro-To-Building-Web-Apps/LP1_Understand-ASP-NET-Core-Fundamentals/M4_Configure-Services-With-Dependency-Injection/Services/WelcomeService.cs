namespace M4_Configure_Services_With_Dependency_Injection.Services;

using M4_Configure_Services_With_Dependency_Injection.Interfaces;

public class WelcomeService : IWelcomeService
{
  DateTime _serviceCreated = DateTime.Now;
  Guid _serviceId = Guid.NewGuid();

  public string GetWelcomeMessage() =>
    $"Welcome to Contoso! The current time is {_serviceCreated}. This service instance has an ID of {_serviceId}";
}
