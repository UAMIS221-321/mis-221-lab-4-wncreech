using System;

class Program {
  public static void Main (string[] args) {
    Console.WriteLine ("Welcome to Crimson Crust!\n");
    string input;
    while (true) {
      DisplayMenu();
      input = Console.ReadLine();
      if (input == "-1") return;
      switch (input) {
      case "1":
        DisplayPlainPizza();
        break;
      case "2":
        DisplayCheesePizza();
        break;
      case "3":
        DisplayPepperoniPizza();
        break;
      default:
        Console.WriteLine("Invalid input. Please try again.");
        break;
      }
    }
  }

  static int getRandomPizzaSize() {
    Random rnd = new Random();
    int number = rnd.Next(5); //random number 0-3
    return (number + 8); //random number 8-12
  }

  static int pepperoniRandom(){
    Random rnd = new Random();
    int number = rnd.Next(10); //random number 0-9
    return number; //random number 0-9
  }

  static void DisplayMenu() {
    Console.WriteLine("Enter a number from the options below or ''-1' to exit.");
    Console.WriteLine("1. Display plain pizza slice");
    Console.WriteLine("2. Display cheese pizza slice");
    Console.WriteLine("3. Display pepperoni pizza slice");
  }

  static void DisplayPlainPizza() {
    int size = getRandomPizzaSize();
    for (int i = size; i > 0; i--) {
      for (int j = 0; j < i; j++) {
      Console.Write("* ");
    }
    Console.WriteLine();
  }
}

  static void DisplayCheesePizza() {
    int size = getRandomPizzaSize();
    for (int i = size; i > 0; i--) {
      for (int j = 0; j < i; j++) {
      if (j == 0 || j == i - 1 || i == size) Console.Write("* ");
      else Console.Write("# ");
    }
    Console.WriteLine();
  }
  }

  static void DisplayPepperoniPizza() {
    int size = getRandomPizzaSize();
    int pepperoni = pepperoniRandom();
    for (int i = size; i > 0; i--) {
      for (int j = 0; j < i; j++) {
        if (j == 0 || j == i - 1 || i == size) Console.Write("* ");
        else if (pepperoni == 7) Console.Write("[] "); //1/10 chance of pepperoni on any cell of cheese
        else Console.Write("# ");
        pepperoni = pepperoniRandom();
    }
    Console.WriteLine();
  }
  }


}
