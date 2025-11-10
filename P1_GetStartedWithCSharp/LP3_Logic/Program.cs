const int maxPets = 8;
const int propertyCount = 6;
const int idIndex = 0;
const int speciesIndex = 1;
const int ageIndex = 2;
const int nicknameIndex = 3;
const int physicalDescriptionIndex = 4;
const int personalityDescriptionIndex = 5;
const string pressEnterToContinue = "Press the Enter key to continue.";
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

#region> Helpers
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
bool IsPositionOpen(int index) =>
  string.IsNullOrWhiteSpace(ourAnimals[index, idIndex].Replace(indexedPropertyTags[idIndex], ""));
void EnforceProperty(
  int animalIndex,
  int propertyIndex,
  string whenUnknown,
  Func<string> prompt,
  Func<string, bool>? validate = null
)
{
  string prop = RemoveTag(ourAnimals[animalIndex, propertyIndex]);
  if (!string.IsNullOrWhiteSpace(prop) && (prop == whenUnknown || validate is null || validate(prop)))
    return;
  else
  {
    Console.WriteLine(
      $"Animal: [{ourAnimals[animalIndex, idIndex]}] has an incomplete {indexedPropertyNames[propertyIndex]} ({prop})"
    );
    ourAnimals[animalIndex, propertyIndex] = $"{indexedPropertyTags[propertyIndex]}{prompt()}";
  }
}
string RemoveTag(string input) => input[(input.IndexOf(':') + 1)..].Trim();
void WriteNewMenu(string title)
{
  Console.Clear();
  Console.WriteLine(title + "\n");
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
  ourAnimals[index, idIndex] = indexedPropertyTags[idIndex] + id;
  ourAnimals[index, speciesIndex] = indexedPropertyTags[speciesIndex] + species;
  ourAnimals[index, ageIndex] = indexedPropertyTags[ageIndex] + age;
  ourAnimals[index, nicknameIndex] = indexedPropertyTags[nicknameIndex] + nickname;
  ourAnimals[index, physicalDescriptionIndex] = indexedPropertyTags[physicalDescriptionIndex] + physicalDescription;
  ourAnimals[index, personalityDescriptionIndex] = indexedPropertyTags[personalityDescriptionIndex] + personality;
}
#endregion

#region> Prompts
string PromptSpecies() =>
  Prompt(
    requestedInput: "'dog' or 'cat' to begin a new entry",
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
#endregion

#region> Main Methods
void PrintAnimals()
{
  WriteNewMenu("Current Animals");
  TraverseAnimals(onSetId: i =>
  {
    Console.WriteLine();
    for (int j = 0; j < propertyCount; j++)
      Console.WriteLine(ourAnimals[i, j].ToString());
  });
  WriteThenRead("\n\r", true);
}
void AddAnimals()
{
  WriteNewMenu("Add Animal Interface");
  string anotherPet = "y";
  int petCount = 0;
  TraverseAnimals(onSetId: i => petCount++);

  if (petCount < maxPets)
    Console.WriteLine($"We currently have {petCount} pets that need homes. We can manage {maxPets - petCount} more.");

  while (anotherPet == "y" && petCount < maxPets)
  {
    string animalSpecies = PromptSpecies();
    SetAnimal(
      index: petCount,
      id: animalSpecies[..1] + (petCount + 1).ToString(),
      species: PromptSpecies(),
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
      WriteThenRead("We have reached our limit on the number of pets that we can manage.", true);
  }
  WriteThenRead("\n", true);
}
void EnforceAgesAndPhysicalDescriptions()
{
  WriteNewMenu("Checking Ages and Physical Descriptions");
  TraverseAnimals(onSetId: i =>
  {
    EnforceProperty(i, ageIndex, "?", PromptAge, (input) => int.TryParse(input, out int petAge));
    EnforceProperty(i, physicalDescriptionIndex, "tbd", PromptPhysicalDescription);
  });
}
void EnforceNicknamesAndPersonalities()
{
  WriteNewMenu("Checking Animal Nicknames and Personalities");
  TraverseAnimals(onSetId: i =>
  {
    EnforceProperty(i, nicknameIndex, "?", PromptNickname);
    EnforceProperty(i, personalityDescriptionIndex, "tbd", PromptPersonalityDescription);
  });
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


string? menuSelection;
do
{
  WriteNewMenu("Welcome to the Contoso PetFriends app. Your main menu options are:");
  Console.WriteLine(" 1. List all of our current pet information");
  Console.WriteLine(" 2. Add a new animal friend to the ourAnimals array");
  Console.WriteLine(" 3. Ensure animal ages and physical descriptions are complete");
  Console.WriteLine(" 4. Ensure animal nicknames and personality descriptions are complete");
  Console.WriteLine(" 5. Edit an animal’s age");
  Console.WriteLine(" 6. Edit an animal’s personality description");
  Console.WriteLine(" 7. Display all cats with a specified characteristic");
  Console.WriteLine(" 8. Display all dogs with a specified characteristic");
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
      EnforceAgesAndPhysicalDescriptions();
      break;
    case "4":
      EnforceNicknamesAndPersonalities();
      break;
    case "5":
    case "6":
    case "7":
    case "8":
      WriteThenRead("UNDER CONSTRUCTION - please check back next month to see progress.", true);
      break;

    default:
      break;
  }
} while (menuSelection != "exit");
