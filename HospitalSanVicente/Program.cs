using HospitalSanVicente.Services;
using Microsoft.Extensions.Configuration;

// Build configuration
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Configure EmailService
EmailService.Configure(configuration);

// Run the main application
MainMenu.Run();
