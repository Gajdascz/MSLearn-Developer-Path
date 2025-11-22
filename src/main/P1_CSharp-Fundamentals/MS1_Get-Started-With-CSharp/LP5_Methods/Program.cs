#region> Data
Console.CursorVisible = false;
int height = Console.WindowHeight - 1;
int width = Console.WindowWidth - 5;
bool shouldExit = false;

const int sickIndex = 2;
const int fasterIndex = 1;
const int neutralIndex = 0;
#endregion

#region> Food State
((int x, int y) position, int current, string[] types) food = ((0, 0), 0, ["@@@@@", "$$$$$", "#####"]);
string CurrentFood() => food.types[food.current];

void UpdateFood()
{
  food.current = Random.Shared.Next(0, food.types.Length);
  food.position = (Random.Shared.Next(0, width - CurrentPlayerState().Length), Random.Shared.Next(0, height - 1));
  Console.SetCursorPosition(food.position.x, food.position.y);
  Console.Write(CurrentFood());
}
#endregion

#region> Player

((int x, int y) position, int current, string[] states) player = ((0, 0), 0, ["('-')", "(^-^)", "(X_X)"]);

bool PlayerIsSick() => player.current == sickIndex;
bool PlayerIsFaster() => player.current == fasterIndex;
string CurrentPlayerState() => player.states[player.current];

void FreezePlayer()
{
  Thread.Sleep(1000);
  player.current = 0;
}
void ClearPlayer((int x, int y) position)
{
  Console.SetCursorPosition(position.x, position.y);
  for (int i = 0; i < CurrentPlayerState().Length; i++)
    Console.Write(" ");
}
void DrawPlayer((int x, int y) position)
{
  Console.SetCursorPosition(position.x, position.y);
  Console.Write(CurrentPlayerState());
}
void UpdatePlayer()
{
  player.current = food.current;
  DrawPlayer(player.position);
}

#endregion


#region> Game Environment
(int x, int y) ClampInBounds((int x, int y) position) =>
  (
    (position.x < 0) ? 0 : (position.x >= width ? width : position.x),
    (position.y < 0) ? 0 : (position.y >= height ? height : position.y)
  );

bool PositionsAreEqual((int x, int y) pos1, (int x, int y) pos2) => pos1.x == pos2.x && pos1.y == pos2.y;

#endregion

#region> Game State
bool TerminalResized() => height != Console.WindowHeight - 1 || width != Console.WindowWidth - 5;

void EndGame(string reason)
{
  Console.Clear();
  Console.Write($"{reason}. Play Again (y/n)? ");
  string? playAgain = Console.ReadLine();
  if (playAgain == "y")
    InitializeGame();
  else
    Environment.Exit(0);
}
void Loop()
{
  while (true)
  {
    if (TerminalResized())
      EndGame("Console resized");
    else
    {
      if (PlayerIsFaster())
        Move(1, false);
      else if (PlayerIsSick())
        FreezePlayer();
      else
        Move(otherKeysExit: false);
      if (PositionsAreEqual(player.position, food.position))
      {
        UpdatePlayer();
        UpdateFood();
      }
    }
  }
}
void InitializeGame()
{
  Console.Clear();
  UpdateFood();
  UpdatePlayer();
  Loop();
}
#endregion

void Move(int speed = 1, bool otherKeysExit = false)
{
  var (lastX, lastY) = player.position;

  switch (Console.ReadKey(true).Key)
  {
    case ConsoleKey.UpArrow:
      player.position.y--;
      break;
    case ConsoleKey.DownArrow:
      player.position.y++;
      break;
    case ConsoleKey.LeftArrow:
      player.position.x -= speed;
      break;
    case ConsoleKey.RightArrow:
      player.position.x += speed;
      break;
    case ConsoleKey.Escape:
      EndGame("Escape key pressed");
      break;
    default:
      // Exit if any other keys are pressed
      if (otherKeysExit)
        EndGame("Exit on other key press enabled and a key other than a direction was pressed.");
      break;
  }

  // Clear the characters at the previous position
  ClearPlayer((lastX, lastY));

  // Keep player position within the bounds of the Terminal window
  var (newX, newY) = ClampInBounds(player.position);
  player.position.x = newX;
  player.position.y = newY;
  // Draw the player at the new location
  DrawPlayer(player.position);
}

InitializeGame();
