using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Files_M3;

public class ApprovedCustomersLoader
{
  /*
    // The .csproj file needs to include the following ItemGroup element to copy the Config folder to the output directory
  
    <ItemGroup>
    <!-- Include all files in the Config folder and copy them to the output directory -->
    <Content Include="Config\**\*">
        <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    </ItemGroup>
  */

  private class ApprovedCustomersConfig
  {
    public List<ApprovedCustomer> ApprovedNames { get; set; } = [];
  }

  public class ApprovedCustomer
  {
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
  }

  private static readonly string ConfigFilePath = Path.Combine(
    Directory.GetCurrentDirectory(),
    "Config",
    "ApprovedCustomers.json"
  );

  public static List<ApprovedCustomer> LoadApprovedNames()
  {
    if (!File.Exists(ConfigFilePath))
      throw new FileNotFoundException($"Configuration file not found: {ConfigFilePath}");

    string json = File.ReadAllText(ConfigFilePath);
    return JsonSerializer.Deserialize<ApprovedCustomersConfig>(json)?.ApprovedNames ?? [];
  }
}
