using System;

namespace OnlineShopping {
  [Serializable]
  class Catalog {
    List<Listing> AllListings = new List<Listing>();

    public void AddListing(Listing x) {
      AllListings.Add(x);
    }

    public void RemoveListing(Listing x) {
      if (AllListings.Contains(x)) {
        AllListings.Remove(x);
      }
    }
    
    public void ViewCatalog(Person user) {
      if (AllListings.Count == 0) {
        Console.WriteLine("Catalog Empty");
        return; 
      }
      int len = AllListings.Count;
      string []a = new string [len + 1];
      for (int i = 0; i < len; ++i) {
        a[i] = AllListings[i].Name;
      }
      a[len] = "Back";
  Back:
      Interface.choiceScreen(a);
      int choice = Interface.getChoice(1, len + 1);
      if (choice == len + 1) {
        return;
      }
      AllListings[choice - 1].ViewListingInfo();
      if (user is Buyer) {
        Console.WriteLine("Add to cart ?");
        Interface.choiceScreen(new string[] {"Yes", "Back", "Exit To Main Page"});
        int wantToBuy = Interface.getChoice(1, 3);
        if (wantToBuy == 1) {
          Console.WriteLine("Enter the quantity");
          int quantity = Convert.ToInt32(Console.ReadLine());
          while (quantity > AllListings[choice - 1].NumberOfItems || quantity <= 0) {
            if (quantity <= 0) {
              Console.WriteLine("Invalid Input");
            } else {
              Console.WriteLine("Not enough quantity in stock only " + AllListings[choice - 1].NumberOfItems.ToString() + " remains");
            }
            Interface.choiceScreen(new string[] {"Enter new quantity", "Back"});
            int newChoice = Interface.getChoice(1, 2);
            if (newChoice == 1) {
              quantity = Convert.ToInt32(Console.ReadLine());
            } else {
              goto Back;
            }
          }
          Buyer curUser = (Buyer)user;
          curUser.AddToCart(AllListings[choice - 1], quantity);
          goto Back;
        } else if (wantToBuy == 2) {
          goto Back;
        } else {
          return;
        }
      } else {
        Interface.choiceScreen(new string[] {"Back", "Exit To Main Page"});
        int sellerChoice = Interface.getChoice(1, 2);
        if (sellerChoice == 1) {
          goto Back;
        } else {
          return;
        }
      }
    }
  }
}
