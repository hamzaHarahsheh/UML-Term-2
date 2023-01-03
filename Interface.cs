using System;
using System.Runtime.Serialization.Formatters.Binary;

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
        Console.WriteLine("#Please enter your choice!");
        choice = Convert.ToInt32(Console.ReadLine()); 
      }
      return choice;
    }

    public static void SaveUser(Person person) {
      FileStream fs = new FileStream("SystemUsers.txt", FileMode.Append, FileAccess.Write);
      BinaryFormatter bf = new BinaryFormatter();
      bf.Serialize(fs, person);
      fs.Close();
    }
    
    public static List<Person> LoadUsers() {
      if (!File.Exists("SystemUsers.txt")) return new List<Person> {};
      FileStream fs = new FileStream("SystemUsers.txt", FileMode.Open, FileAccess.Read);
      BinaryFormatter bf = new BinaryFormatter();
      List<Person> list = new List<Person>();
      while(fs.Position < fs.Length) {
        Person obj = (Person)bf.Deserialize(fs);
        list.Add(obj);
      }
      fs.Close();
      return list;
    }

    public List<Seller> LoadSellers() {
      List<Person> Users = LoadUsers();
      List<Seller> curSellers = new List<Seller> ();
      foreach(Person user in Users) {
        if (user is Seller) {
          curSellers.Add((Seller)user);
        }
      }
      return curSellers;
    }

    public static Person GetUser(string email, string pass) {
      List<Person> Users = LoadUsers();
      foreach(Person user in Users) {
        if (user is Buyer) {
          Buyer cur = (Buyer)user;
          if (cur.MyAccount.Email == email && cur.MyAccount.Password == pass) {
            return cur;
          }
        } else {
          Seller cur = (Seller) user;
          if (cur.MyAccount.Email == email && cur.MyAccount.Password == pass) {
            return cur;
          }
        }
      }
      return new Person("NULL");
    }

    public static void SaveCatalog(Catalog cat) {
      FileStream fs = new FileStream("Catalogs.txt", FileMode.Create, FileAccess.Write);
      BinaryFormatter bf = new BinaryFormatter();
      bf.Serialize(fs, cat);
      fs.Close();
    }

    public static Catalog LoadCatalog() {
      if (!File.Exists("Catalogs.txt")) return new Catalog();
      FileStream fs = new FileStream("Catalogs.txt", FileMode.Open, FileAccess.Read);
      BinaryFormatter bf = new BinaryFormatter();
      Catalog cat = new Catalog();
      while (fs.Position < fs.Length) {
        cat = (Catalog)bf.Deserialize(fs);
      }
      fs.Close();
      return cat;
    }

    public static void LogInPage(Catalog cat) {
      int numberOfTries = 0;
      Person user = new Person("NULL");
      while (numberOfTries < 3) {
        Console.WriteLine("#Enter Your Email!");
        string email = Console.ReadLine();
        Console.WriteLine("#Enter Your Password!");
        string pass = Console.ReadLine();
        user = GetUser(email, pass);
        if (user.Name != "NULL") {
          break;
        }
        Console.WriteLine("You have " + (3 - ++numberOfTries).ToString() + " Tries Left");
      }
      if (user.Name == "NULL") {
        Console.WriteLine("Try again in 10 minutes");
        return;
      }
      
      if (user is Seller) {
        SellerHomePage((Seller)user, cat);
      } else {
        BuyerHomePage((Buyer)user, cat);
      }
    }
    public static void SignUpPage(Catalog cat) {
      choiceScreen(new string[] { "Signup as a Seller", "Signup as a Buyer" });
      int choice = getChoice(1, 2);
      Console.WriteLine("#Enter your name:");
      string name = Console.ReadLine();
      Console.WriteLine("#Enter your email:");
      string email = Console.ReadLine();
      Console.WriteLine("#Create your password:");
      string password = Console.ReadLine();
      Console.WriteLine("#Enter your phone number:");
      string phoneNumber = Console.ReadLine();
      Console.WriteLine("#Enter your address details (country, city, street, postalCode)");
      string country = Console.ReadLine();
      string city = Console.ReadLine();
      string street = Console.ReadLine();
      string postalCode = Console.ReadLine();

      if (choice == 1) {
        Console.WriteLine("#Enter your store name:");
        string storeName = Console.ReadLine();
        Seller user = new Seller(storeName, name, email, phoneNumber, password, "Seller", street, city, postalCode, country);
        SaveUser(user);
        SellerHomePage(user, cat);
      }
      else {
        Console.WriteLine("#Enter your card number:");
        string cardNumber = Console.ReadLine();
        Console.WriteLine("#Enter your pin code:");
        string pinCode = Console.ReadLine();
        const double balance = 100;
        Buyer user = new Buyer(name, email, phoneNumber, password, "Buyer", street, city, postalCode, country, cardNumber, pinCode);
        SaveUser(user);
        BuyerHomePage(user, cat);
      }
    }

    public static void BuyerHomePage(Buyer user, Catalog cat) {
      Console.WriteLine("HI BUYER: " + user.Name);
      while(true) {
        choiceScreen(new string[] {
        "View all listings",
        "View choosen listing info",
        "View Cart",
        "Checkout",
        "Change account info",
        "Search for listing",
        "Exit"
        });

        int choice = getChoice(1, 7);
        if(choice == 7) { 
          break; 
        }
        switch (choice) {
          case 1:
            user.ViewAllListing(cat);
            break;
          case 2:
            //TODO
            break;
          case 3:
            user.ViewMyCart();
            break;
          case 4:
            //TODO
            break;
          case 5:
            user.UpdateAccountInfo(cat);
            break;
          case 6:
            //TODO
            break;
        }
      }
    }

    public static void SellerHomePage(Seller user, Catalog cat) {
      Console.WriteLine("HI Seller: " + user.Name);
      while(true){
        choiceScreen(new string[]{
        "Add new listing",
        "Delete existing listing",
        "Update listing",
        "View my listings",
        "View all listings", 
        "View sold listings info",
        "Change account info",
        "Logout"
        });

        int choice = getChoice(1, 8);
        if(choice == 8){
          break;
        }
        switch(choice){
          case 1 : 
            user.AddListing(cat);
            break;
          case 2 :
            user.RemoveListing(cat);
            break;
          case 3 : 
            user.UpdateListingInfo(cat);
            break;
          case 4 : 
            user.ViewSellerListings();
            break;
          case 5 : 
            user.ViewAllListing(cat);
            break;
          case 6 : 
            //TODO
            break;
          case 7 : 
            user.UpdateAccountInfo(cat);
            break;
        }
      }
    }

    public static void Main() {
      Catalog cat = new Catalog();
      cat = LoadCatalog();
      choiceScreen(new string[] {"logIn", "SignUp"});
      int choice = getChoice(1, 2);
      if (choice == 1) {
        LogInPage(cat);
      } else {
        SignUpPage(cat);
      }
    }
  }
}
