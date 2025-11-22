using System.Text.Json;
using FruitWebApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace FruitWebApp.Components.Pages;

public partial class Home : ComponentBase
{
  // IHttpClientFactory set using dependency injection
  [Inject]
  public required IHttpClientFactory HttpClientFactory { get; set; }

  [Inject]
  private NavigationManager? NavigationManager { get; set; }

  /* Add the data model, enumerable since an array is expected as a response */
  private IEnumerable<FruitModel>? _fruitList;

  // Begin GET operation code
  protected override async Task OnInitializedAsync()
  {
    var httpClient = HttpClientFactory.CreateClient("FruitAPI");
    using HttpResponseMessage response = await httpClient.GetAsync("/fruits");
    if (response.IsSuccessStatusCode)
      _fruitList = await JsonSerializer.DeserializeAsync<IEnumerable<FruitModel>>(
        await response.Content.ReadAsStreamAsync()
      );
    else
      Console.WriteLine($"Failed to load fruit list. Status Code: {response.StatusCode}");
  }

  // End GET operation code

  private void DeleteButton(int id) => NavigationManager!.NavigateTo($"/delete/{id}");

  private void EditButton(int id) => NavigationManager!.NavigateTo($"/edit/{id}");
}
