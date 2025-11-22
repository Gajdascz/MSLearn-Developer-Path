namespace M6_Create_A_Web_API_With_ASPNET_Core_Controllers;

public class PizzaService
{
  static List<Pizza> Pizzas { get; }
  static int nextId = 3;

  static PizzaService()
  {
    Pizzas =
    [
      new Pizza()
      {
        Id = 1,
        Name = "Classic Italian",
        IsGlutenFree = false,
      },
      new Pizza()
      {
        Id = 2,
        Name = "Veggie",
        IsGlutenFree = true,
      },
    ];
  }

  public static List<Pizza> GetAll() => Pizzas;

  public static Pizza? Get(int id) => Pizzas.FirstOrDefault(p => p.Id == id);

  public static void Add(Pizza pizza)
  {
    pizza.Id = nextId++;
    Pizzas.Add(pizza);
  }

  public static void Delete(int id)
  {
    if (Get(id) is Pizza pizza)
      Pizzas.Remove(pizza);
  }

  public static void Update(Pizza pizza)
  {
    var index = Pizzas.FindIndex(p => p.Id == pizza.Id);
    if (index == -1)
      return;
    Pizzas[index] = pizza;
  }

  public static bool Has(int id) => Get(id) is not null;
}
