using System;
using System.Collections.Generic;

namespace OnlineShopping
{
  [Serializable]
  class CheckOut {
    int id;
    static int cnt = 0;
    DateTime d;
    double totalAmount;
    public CheckOut() {
      id = ++cnt;
      totalAmount = 0;
    }

    public void PrintReceipt () {
      d = DateTime.Now;
      Console.WriteLine("Transactino ID: " + id.ToString());
      Console.WriteLine("Total Amount: " + totalAmount.ToString());
      Console.WriteLine("Date: " + d.Day + "/" + d.Month + "/" + d.Year);
      Console.WriteLine("Time: " + d.Hour + ":" + d.Minute);
    }

    public void DoCheckOut(Buyer b, Catalog cat) {
      if (b.MyCart.curCart.Count == 0) {
        Console.WriteLine("Cart is Empty");
        return;
      }
            Console.WriteLine("#Shipping Address: ");
            Interface.choiceScreen(new String[] { "Default shipping address", "Add new address" });
            int choice = Interface.getChoice(1,2);
            if (choice == 2)
            {
                Console.WriteLine("#Add new address: (Country, City, Street, PostalCode)");
                Interface.ReadString();
                Interface.ReadString();
                Interface.ReadString();
                Interface.ReadString();
            }
            Console.WriteLine("#Enter your payment info: (CardNumber, PinCode)");
            string cardNumber = Interface.ReadString();
            string pinCode = Interface.ReadString();
            if(b.PaymentInfo.CardNumber != cardNumber || b.PaymentInfo.PinCode != pinCode)
            {
                Console.WriteLine("Incorret cardNumber or pinCode");
                return;
            } 

        if (b.PaymentInfo.Balance < b.MyCart.TotalPrice) {
        Console.WriteLine("You don't have enough money");
        return;
      }
      
      totalAmount = b.MyCart.TotalPrice;
      b.PaymentInfo.Balance -= totalAmount;
      int cnt = b.MyCart.curCart.Count;
      for (int i = 0; i < cnt; ++i) {
        b.MyCart.TotalPrice -= b.MyCart.curCart[0].Price * b.MyCart.Frequancy[b.MyCart.curCart[0]];
        b.MyCart.curCart[0].NumberOfItems -= b.MyCart.Frequancy[b.MyCart.curCart[0]];
        if (b.MyCart.curCart[0].CurSeller.Sold.ContainsKey(b)) {
          bool flag = false;
          for (int j = 0; j < b.MyCart.curCart[0].CurSeller.Sold[b].Count; ++j) {
            if (b.MyCart.curCart[0].CurSeller.Sold[b][j].Item1.Id == b.MyCart.curCart[0].Id) {
              flag = true;
              int x = b.MyCart.curCart[0].CurSeller.Sold[b][j].Item2 + b.MyCart.Frequancy[b.MyCart.curCart[0]];
              Tuple<Listing, int> newT = new Tuple<Listing, int> (b.MyCart.curCart[0], x);
              b.MyCart.curCart[0].CurSeller.Sold[b][j] = newT;
            }
          }
          if (!flag) {
            Tuple<Listing, int> t = new Tuple<Listing, int>(b.MyCart.curCart[0], b.MyCart.Frequancy[b.MyCart.curCart[0]]);
            b.MyCart.curCart[0].CurSeller.Sold[b].Add(t);
          }
        } else {
          List<Tuple<Listing, int>> firstListing = new List<Tuple<Listing, int>> ();
          Tuple<Listing, int> t = new Tuple<Listing, int> (b.MyCart.curCart[0], b.MyCart.Frequancy[b.MyCart.curCart[0]]);
          firstListing.Add(t);
          b.MyCart.curCart[0].CurSeller.Sold.Add(b, firstListing);
        }
        b.MyCart.Frequancy[b.MyCart.curCart[0]] = 0;
        if (b.MyCart.curCart[0].NumberOfItems == 0) {
          b.MyCart.curCart[0].CurSeller.MyListings.Remove(b.MyCart.curCart[0]);
          cat.RemoveListing(b.MyCart.curCart[0]);
        }
        Interface.UpdateUserFile(b.MyCart.curCart[0].CurSeller);
        b.RemoveFromCart(b.MyCart.curCart[0]);
      }
      Interface.SaveCatalog(cat);
      PrintReceipt();
      Console.WriteLine("Transaction Done!!!!!");
    }
  }
}
