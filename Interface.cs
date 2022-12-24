using System;
namespace OnlineShopping {
  class Interface {
    public static void choiceScreen(string []choices) {
      Console.WriteLine("**************************************************************");
      for (int i = 1; i <= choices.Length; ++i) {
        Console.WriteLine(i.ToString() + "- " + choices[i - 1]);
      }
      Console.WriteLine("**************************************************************");
      Console.WriteLine("\n");
    }

    public static int getChoice(int left, int right) {
      int choice = -1;
      while (choice < left || choice > right) { // while the number entered is not one of the choices
        Console.WriteLine("Please enter your choice!");
        choice = Convert.ToInt32(Console.ReadLine()); 
      }
      return choice;
    }

    public static void BuyerHomePage() {

    }

    public static void SellerHomePage() {

    }

    public static void Main() {
      choiceScreen(new string[] {"logIn", "SignUp", "UpdateInfo"});
      int choice = getChoice(1, 3);
      Console.WriteLine(choice);
    }
  }
}
