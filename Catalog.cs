using System;

namespace OnlineShopping {
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

    public void ViewCatalog () {
      for (int i = 1; i <= AllListings.Count; i++) {
        Console.WriteLine(i.ToString() + "- " + AllListings[i - 1].Name);
      }
    }

  }
}