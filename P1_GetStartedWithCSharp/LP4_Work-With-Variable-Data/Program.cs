#region> Data
const int maxPets = 8;
const int propertyCount = 6;
const int idIndex = 0;
const int speciesIndex = 1;
const int ageIndex = 2;
const int nicknameIndex = 3;
const int physicalDescriptionIndex = 4;
const int personalityDescriptionIndex = 5;
const string pressEnterToContinue = "Press the Enter key to continue.";
string[] loadingIcons = ["◜", "◝", "◞", "◟"];
string[] indexedPropertyNames = ["id", "species", "age", "nickname", "physical description", "personality"];
string[] indexedPropertyTags =
[
  "ID #: ",
  "Species: ",
  "Age: ",
  "Nickname: ",
  "Physical description: ",
  "Personality: ",
];

string[,] ourAnimals = new string[maxPets, propertyCount];
#endregion

#region> Console Helpers
string? WriteThenRead(string message, bool enterToContinue = false)
{
  Console.WriteLine(message + (enterToContinue ? $"\n{pressEnterToContinue}" : null));
  return Console.ReadLine();
}
string Prompt(
  string requestedInput,
  string? whenUnknown = null,
  Func<string, bool>? validate = null,
  string? whenEmpty = null
)
{
  string? input = WriteThenRead(
    $"Enter {requestedInput}{(whenUnknown is null ? null : $" or {whenUnknown} if unknown")}"
  );
  if (string.IsNullOrEmpty(input))
  {
    if (whenEmpty is not null)
      return whenEmpty;
  }
  else
  {
    if (input == whenUnknown)
      return whenUnknown;
    else if (validate is null || validate(input))
      return input;
  }
  return Prompt(requestedInput, whenUnknown, validate);
}

void PrintLoadingIcon(string message)
{
  for (int j = 0; j < loadingIcons.Length; j++)
  {
    Console.Write($"\r{loadingIcons[j]} {message}");
    Thread.Sleep(100);
  }
  Console.Write($"\r{"".PadRight(Console.BufferWidth)}");
  Console.CursorVisible = true;
}
string? DoConsoleAction(string title, Action action, bool haltAtEnd = true)
{
  Console.Clear();
  Console.WriteLine(title + "\n");
  action();
  return haltAtEnd ? WriteThenRead("\r\n", true) : null;
}
#endregion

#region> Animals Array Helpers
bool IsPositionOpen(int index) =>
  string.IsNullOrWhiteSpace(ourAnimals[index, idIndex].Replace(indexedPropertyTags[idIndex], ""));
string RemoveTag(string input) => input[(input.IndexOf(':') + 1)..].Trim();
bool AnimalHasPropertyValue(int animalIndex, int propertyIndex, string value) =>
  RemoveTag(ourAnimals[animalIndex, propertyIndex]) == value;
void SetAnimalPropertyValue(int animalIndex, int propertyIndex, string value) =>
  ourAnimals[animalIndex, propertyIndex] = $"{indexedPropertyTags[propertyIndex]}{value}";

void TraverseAnimals(Action<int>? onSetId = null, Action<int>? onEmptyId = null)
{
  for (int i = 0; i < maxPets; i++)
  {
    if (IsPositionOpen(i))
    {
      if (onEmptyId is not null)
        onEmptyId(i);
      continue;
    }
    else if (onSetId is not null)
      onSetId(i);
  }
}
void SetAnimal(
  int index,
  string id,
  string species,
  string age,
  string nickname,
  string physicalDescription,
  string personality
)
{
  SetAnimalPropertyValue(index, idIndex, id);
  SetAnimalPropertyValue(index, speciesIndex, species);
  SetAnimalPropertyValue(index, ageIndex, age);
  SetAnimalPropertyValue(index, nicknameIndex, nickname);
  SetAnimalPropertyValue(index, physicalDescriptionIndex, physicalDescription);
  SetAnimalPropertyValue(index, personalityDescriptionIndex, personality);
}

int? FindAnimalIndex(string animalIdOrNickname, string animalSpecies)
{
  PrintLoadingIcon($"Looking for {animalIdOrNickname}");
  for (int i = 0; i < maxPets; i++)
  {
    if (
      AnimalHasPropertyValue(i, speciesIndex, animalSpecies)
      && (
        AnimalHasPropertyValue(i, idIndex, animalIdOrNickname)
        || AnimalHasPropertyValue(i, nicknameIndex, animalIdOrNickname)
      )
    )
      return i;
  }
  return null;
}
#endregion

#region> Prompts
string PromptSpecies() =>
  Prompt(
    requestedInput: "animal species ('dog' or 'cat')",
    validate: input =>
    {
      string lower = input.ToLower();
      return lower == "dog" || lower == "cat";
    }
  );
string PromptAge() =>
  Prompt(requestedInput: "pet's age", whenUnknown: "?", input => int.TryParse(input, out int petAge), whenEmpty: "?");
string PromptPhysicalDescription() =>
  Prompt(
    requestedInput: "pet's physical description of the pet (size, color, gender, weight, housebroken)",
    whenUnknown: "tbd",
    whenEmpty: "tbd"
  );
string PromptPersonalityDescription() =>
  Prompt(
    requestedInput: "description of the pet's personality (likes or dislikes, tricks, energy level)",
    whenEmpty: "tbd",
    whenUnknown: "tbd"
  );
string PromptNickname() => Prompt("pet's nickname", whenUnknown: "?", whenEmpty: "?");
string PromptInputAnotherAnimal() =>
  Prompt(
      "y if you'd like to enter another pet or n if you'd like to return to the menu.",
      validate: input =>
      {
        string lowered = input.ToLower();
        return lowered == "y" || lowered == "n";
      }
    )
    .ToLower();
string PromptPropertyType() =>
  Prompt(
    requestedInput: $"type of property ({string.Join(" ", indexedPropertyNames.SkipWhile(p => p == "id"))})",
    validate: i => indexedPropertyNames.SkipWhile(p => p == "id").Contains(i)
  );
string PromptId() =>
  Prompt(
    requestedInput: $"animal id",
    validate: (input) =>
    {
      for (int i = 0; i < maxPets; i++)
      {
        if (AnimalHasPropertyValue(i, idIndex, input))
          return true;
      }
      return false;
    }
  );
Func<string>[] indexedModifiablePropertyPrompts =
[
  PromptId,
  PromptSpecies,
  PromptAge,
  PromptNickname,
  PromptPhysicalDescription,
  PromptPersonalityDescription,
];
string[] PromptSearchTerms()
{
  string searchTermString = Prompt("comma separated list of terms to search for");
  string[] searchTerms = searchTermString.Split(',');
  for (int i = 0; i < searchTerms.Length; i++)
    searchTerms[i] = searchTerms[i].ToLower().Trim();
  Array.Sort(searchTerms);
  return searchTerms;
}
#endregion

#region> Initialize Animals
for (int i = 0; i < maxPets; i++)
{
  SetAnimal(i, species: "", id: "", age: "", physicalDescription: "", personality: "", nickname: "");
}
SetAnimal(
  0,
  id: "d1",
  species: "dog",
  age: "2",
  nickname: "lola",
  physicalDescription: "medium sized cream colored female golden retriever weighing about 65 pounds. housebroken.",
  personality: "loves to have her belly rubbed and likes to chase her tail. gives lots of kisses."
);
SetAnimal(
  1,
  id: "d2",
  species: "dog",
  age: "9",
  physicalDescription: "large reddish-brown male golden retriever weighing about 85 pounds. housebroken.",
  personality: "loves to have his ears rubbed when he greets you at the door, or at any time! loves to lean-in and give doggy hugs.",
  nickname: "loki"
);
SetAnimal(
  2,
  id: "c3",
  species: "cat",
  age: "1",
  physicalDescription: "small white female weighing about 8 pounds. litter box trained.",
  personality: "friendly",
  nickname: "Puss"
);
SetAnimal(3, species: "cat", id: "c4", age: "?", physicalDescription: "", personality: "", nickname: "");
#endregion

#region> Main Methods
void PrintAnimals() =>
  DoConsoleAction(
    "Current Animals",
    () =>
    {
      TraverseAnimals(onSetId: i =>
      {
        Console.WriteLine();
        for (int j = 0; j < propertyCount; j++)
          Console.WriteLine(ourAnimals[i, j].ToString());
      });
    }
  );
void AddAnimals() =>
  DoConsoleAction(
    "Add Animals",
    () =>
    {
      string anotherPet = "y";
      int petCount = 0;
      TraverseAnimals(onSetId: i => petCount++);

      while (anotherPet == "y" && petCount < maxPets)
      {
        Console.Clear();
        if (petCount < maxPets)
          Console.WriteLine(
            $"We currently have {petCount} pets that need homes. We can manage {maxPets - petCount} more.\n"
          );
        string species = PromptSpecies();
        SetAnimal(
          index: petCount,
          id: species[..1] + (petCount + 1).ToString(),
          species: species,
          age: PromptAge(),
          nickname: PromptNickname(),
          physicalDescription: PromptPhysicalDescription(),
          personality: PromptPersonalityDescription()
        );
        petCount++;

        // check maxPet limit
        if (petCount < maxPets)
          anotherPet = PromptInputAnotherAnimal();

        if (petCount >= maxPets)
          Console.WriteLine("We have reached our limit on the number of pets that we can manage.");
      }
    }
  );
void EnforceProperty(int animalIndex, int propertyIndex, string whenUnknown, Func<string, bool>? validate = null)
{
  string prop = RemoveTag(ourAnimals[animalIndex, propertyIndex]);
  if (!string.IsNullOrWhiteSpace(prop) && (prop == whenUnknown || validate is null || validate(prop)))
    return;
  else
  {
    Console.WriteLine(
      $"Animal: [{ourAnimals[animalIndex, idIndex]}] has an incomplete {indexedPropertyNames[propertyIndex]}"
    );
    SetAnimalPropertyValue(animalIndex, propertyIndex, indexedModifiablePropertyPrompts[propertyIndex]());
  }
}
void EnforceCompleteAnimalProperties() =>
  DoConsoleAction(
    "Checking All Animal Data For Invalid Entries",
    () =>
    {
      TraverseAnimals(onSetId: i =>
      {
        EnforceProperty(i, nicknameIndex, "?");
        EnforceProperty(i, personalityDescriptionIndex, "tbd");
        EnforceProperty(i, ageIndex, "?", (input) => int.TryParse(input, out int petAge));
        EnforceProperty(i, physicalDescriptionIndex, "tbd");
      });
      Console.WriteLine("All animals have complete data.");
    }
  );
void DisplayAnimalsWithCharacteristics() =>
  DoConsoleAction(
    "View Animals By Characteristics",
    () =>
    {
      string cleanSpecies = PromptSpecies().Trim().ToLower();
      string[] searchTerms = PromptSearchTerms();
      Console.WriteLine("\n");
      bool anyMatchesFound = false;
      bool currentAnimalMatches = false;
      TraverseAnimals(onSetId: i =>
      {
        if (ourAnimals[i, speciesIndex].Trim().ToLower().Contains(cleanSpecies))
        {
          string fullDescription = (
            ourAnimals[i, nicknameIndex]
            + "\n"
            + ourAnimals[i, ageIndex]
            + "\n"
            + ourAnimals[i, physicalDescriptionIndex]
            + "\n"
            + ourAnimals[i, personalityDescriptionIndex]
          )
            .Trim()
            .ToLower();
          string animalId = RemoveTag(ourAnimals[i, idIndex]);
          string animalName = RemoveTag(ourAnimals[i, nicknameIndex]);
          if (string.IsNullOrWhiteSpace(animalName))
            animalName = animalId;
          foreach (string term in searchTerms)
          {
            if (!string.IsNullOrWhiteSpace(term))
            {
              Console.CursorVisible = false;
              PrintLoadingIcon($"Checking if {animalName} matches '{term}'");
              if (fullDescription.Contains(term))
              {
                Console.WriteLine($"\r{animalName} matches {term}");
                currentAnimalMatches = true;
                anyMatchesFound = true;
              }
            }
          }
          if (currentAnimalMatches)
          {
            Console.WriteLine($"\r\n{animalName}{(animalId == animalName ? "" : $" ({animalId})")}\n{fullDescription}");
            currentAnimalMatches = false;
          }
        }
      });
      if (!anyMatchesFound)
        Console.WriteLine($"None of our animals found with the characteristics: {string.Join(" ", searchTerms)}");
    }
  );
void SetAnimalProperty()
{
  string animalIdOrNickname = Prompt("Animal Id or Nickname");
  string species = PromptSpecies();

  string propertyName = PromptPropertyType();
  int propertyIndex = Array.IndexOf(indexedPropertyNames, propertyName);
  Func<string> prompt = indexedModifiablePropertyPrompts[propertyIndex];

  DoConsoleAction(
    $"Setting {animalIdOrNickname}'s {propertyName}",
    () =>
    {
      if (FindAnimalIndex(animalIdOrNickname, species) is int index)
      {
        SetAnimalPropertyValue(index, propertyIndex, indexedModifiablePropertyPrompts[propertyIndex]());
        Console.WriteLine($"{animalIdOrNickname}'s {propertyName} successfully set");
      }
      else
        Console.WriteLine(
          $"Failed to set {animalIdOrNickname}'s {propertyName} property. No animal with id or nickname {animalIdOrNickname} was found."
        );
    }
  );
}
#endregion


string? menuSelection = null;
do
{
  Console.WriteLine("Welcome to the Contoso PetFriends app. Your main menu options are:");
  Console.WriteLine(" 1. List all of our current pet information");
  Console.WriteLine(" 2. Add a new animal friend to the ourAnimals array");
  Console.WriteLine(" 3. Ensure all animals have complete data");
  Console.WriteLine(" 4. Edit an animal’s property");
  Console.WriteLine(" 5. Search animals");
  Console.WriteLine();
  Console.WriteLine("Enter your selection number (or type Exit to exit the program)");
  menuSelection = Console.ReadLine();
  if (menuSelection != null)
    menuSelection = menuSelection.ToLower();

  switch (menuSelection)
  {
    case "1":
      PrintAnimals();
      break;
    case "2":
      AddAnimals();
      break;
    case "3":
      EnforceCompleteAnimalProperties();
      break;
    case "4":
      SetAnimalProperty();
      break;
    case "5":
      DisplayAnimalsWithCharacteristics();
      break;

    default:
      break;
  }
} while (menuSelection != "exit");
