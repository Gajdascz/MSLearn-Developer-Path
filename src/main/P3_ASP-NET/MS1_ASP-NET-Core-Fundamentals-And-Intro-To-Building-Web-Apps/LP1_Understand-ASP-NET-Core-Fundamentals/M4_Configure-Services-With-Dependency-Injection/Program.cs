using M4_Configure_Services_With_Dependency_Injection.Interfaces;
using M4_Configure_Services_With_Dependency_Injection.Services;

var builder = WebApplication.CreateBuilder(args);

// Created once and stays alive for the lifetime of the application
// builder.Services.AddSingleton<IWelcomeService, WelcomeService>();

// Created once when a request is received and disposed when the request is complete (the end of the request's middleware block terminates)
// builder.Services.AddScoped<IWelcomeService, WelcomeService>();

// Created each time they're requested (multiple instances) and disposed of when the request is complete
builder.Services.AddTransient<IWelcomeService, WelcomeService>();

var app = builder.Build();

app.MapGet(
  "/",
  (IWelcomeService welcomeService, IWelcomeService welcomeService2) =>
  {
    string msg1 = welcomeService.GetWelcomeMessage();
    string msg2 = welcomeService2.GetWelcomeMessage();
    return $"{msg1}\n{msg2}";
  }
);

app.Run();
