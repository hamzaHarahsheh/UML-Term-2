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
        Console.WriteLine("Please enter your choice!");
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
      FileStream fs = new FileStream("Catalogs.txt", FileMode.Append, FileAccess.Write);
      BinaryFormatter bf = new BinaryFormatter();
      bf.Serialize(fs, cat);
      fs.Close();
    }

    public static List<Catalog> LoadCatalog() {
      if (!File.Exists("Catalogs.txt")) return new List<Catalog> {};
      FileStream fs = new FileStream("Catalogs.txt", FileMode.Open, FileAccess.Read);
      BinaryFormatter bf = new BinaryFormatter();
      List<Catalog> list = new List<Catalog>();
      while (fs.Position < fs.Length) {
        Catalog obj = (Catalog)bf.Deserialize(fs);
        list.Add(obj);
      }
      fs.Close();
      return list;
    }

    public static void Main() {
      choiceScreen(new string[] {"logIn", "SignUp"});
      int choice = getChoice(1, 2);
      if (choice == 1) {
        
      } else {

      }
    }
  }
}
