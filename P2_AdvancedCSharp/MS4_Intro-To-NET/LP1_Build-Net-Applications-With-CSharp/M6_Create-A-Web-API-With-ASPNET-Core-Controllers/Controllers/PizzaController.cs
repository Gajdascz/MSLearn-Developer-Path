using Microsoft.AspNetCore.Mvc;

namespace M6_Create_A_Web_API_With_ASPNET_Core_Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
  public PizzaController() { }

  [HttpGet]
  public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll();

  [HttpGet("{id}")]
  public ActionResult<Pizza> Get(int id) => PizzaService.Get(id) is Pizza pizza ? pizza : NotFound();

  [HttpPost]
  public ActionResult Create(Pizza pizza)
  {
    PizzaService.Add(pizza);
    return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
  }

  [HttpPut("{id}")]
  public ActionResult Update(int id, Pizza pizza)
  {
    if (id != pizza.Id)
      return BadRequest();

    if (PizzaService.Has(id))
    {
      PizzaService.Update(pizza);
      return NoContent();
    }
    else
      return NotFound();
  }

  [HttpDelete("{id}")]
  public IActionResult Delete(int id)
  {
    if (PizzaService.Has(id))
    {
      PizzaService.Delete(id);
      return NoContent();
    }
    else
      return NotFound();
  }
}
