using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoPizza.Pages
{
  public class PizzaListModel(PizzaService service) : PageModel
  {
    private readonly PizzaService _service = service;
    public IList<Pizza> PizzaList { get; set; } = service.GetPizzas();

    public void OnGet() => PizzaList = _service.GetPizzas();

    [BindProperty]
    public Pizza NewPizza { get; set; } = default!;

    public IActionResult OnPost()
    {
      if (!ModelState.IsValid || NewPizza is null)
        return Page();
      _service.AddPizza(NewPizza);
      return RedirectToAction("Get");
    }

    public IActionResult OnPostDelete(int id)
    {
      _service.DeletePizza(id);
      return RedirectToAction("Get");
    }
  }
}
