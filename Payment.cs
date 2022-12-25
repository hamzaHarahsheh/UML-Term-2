using System;
namespace OnlineShopping {
  class Payment {
    string cardNumber, pinCode;
    double balance;
    Address billingAddress;
    public Payment(string cardNumber, string pinCode) {
      this.cardNumber = cardNumber;
      this.pinCode = pinCode;
      billingAddress = new Address("king abdulla street", "Irbid", "174560", "Palastine");
    }

    public double Balance {
      set {
        balance = value;
      }
      get {
        return balance;
      }
    }
    public string CardNumber {
      get {
        return cardNumber;
      }
    }

    public string PinCode {
      get {
        return pinCode;
      }
    }

    public Address BillingAddress {
      set {
        billingAddress = value;
      }
      get {
        return billingAddress;
      }
    }
  }
}