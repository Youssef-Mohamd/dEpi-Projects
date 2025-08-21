using System;
using System.Diagnostics.Metrics;
using System.Xml.Linq;

namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {


            //            Bank bank = new Bank("BANK Masr", "Banha ");
            //            Customer customer1 = new Customer(1, "Youssef", "200548438", new DateTime(2025, 08, 24), 0);

            //          Account acc1 = new Account();
            //            customer1.Accounts.Add(acc1);

            //            // Deposit 

            //            acc1.deposit(10000);
            //            Console.WriteLine($"Balance after deposit: {acc1.Balance}");

            //            //withdraw

            //            acc1.Withdraw(2000);
            //            Console.WriteLine($"Balance after withdraw: {acc1.Balance}");

            //            Account  acc2 =new Account();
            //            customer1.Accounts.Add(acc2);

            //            //transfer
            //            acc1.Transfer(4000, acc2);
            //            Console.WriteLine($"Balance acc1 After Transfer:{acc1.Balance}");
            //            Console.WriteLine($"Balance aac2 After Transfer:{acc2.Balance}");

            //            // TotalBalance

            //            Console.WriteLine($"Total Balance :{acc1.Balance + acc2.Balance}");

            ////--------------------------//
            //            SavingAccount saving = new SavingAccount(10);      //  فايده 10% 

            //            saving.deposit(10000);
            //            Console.WriteLine($"[Saving] Balance after deposit: {saving.Balance}");

            //            decimal savingInterest = saving.CalculateInterest();
            //            Console.WriteLine($"[Saving] Interest: {savingInterest}");

            //            saving.Withdraw(1000);

            //            Console.WriteLine($"[Saving] Balance after Withdraw: {saving.Balance}");

            //---------------------------------------------------------------------//


            //Console.Write("Enter Bank Name: ");
            //string BankName = Console.ReadLine();

            //Console.Write("Enter Branch Code: ");
            //string BranchCode = Console.ReadLine();

            //Bank bank = new Bank(BankName, BranchCode);
            //Console.WriteLine($"welcome at :{BankName}");



            //Console.WriteLine("\n MENU ");

            //Console.WriteLine("1: Add Customer");
            //Console.WriteLine("2: Open Account (Savings/Current)");
            //Console.WriteLine("3: Deposit");
            //Console.WriteLine("4: Withdraw");
            //Console.WriteLine("5: Transfer");
            //Console.WriteLine("6: Show Account Details + Transactions");
            //Console.WriteLine("7: Show Customer Total Balance");
            //Console.WriteLine("8: List All Customers & Accounts");
            //Console.WriteLine("9: Exit");
            //Console.Write("Choose: ");

            //string choose = Console.ReadLine();







            SavingAccount saving = new SavingAccount(5);      // Interest 10%
            CurrentAccount current = new CurrentAccount(500); // Overdraft limit 500

            while (true)
            {

                Console.WriteLine("\n____Bank Menu ____");
                Console.WriteLine("1. Add Customer");
                Console.WriteLine("2. Deposit to Saving Account");
                Console.WriteLine("3. Withdraw from Saving Account");
                Console.WriteLine("4. Deposit to Current Account");
                Console.WriteLine("5. Withdraw from Current Account");
                Console.WriteLine("6. Transfer from Saving to Current");
                Console.WriteLine("7. Show Balances");
                Console.WriteLine("0. Exit");
                Console.Write("Your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {

                    case "1":

                        Console.WriteLine("enter name of customer ");
                        string cusName= Console.ReadLine();
                       
                        Console.WriteLine("enter your national id ");
                        string cusNid = Console.ReadLine();
                        Customer newCustomer = new Customer(1, cusName, cusNid, new DateTime(2025, 08, 24), 0);


                        Console.WriteLine("Choose account type: (1) Saving, (2) Current");
                        string accType = Console.ReadLine();

                        if (accType == "1")
                        {
                            Console.Write("Enter interest rate (%): ");
                            decimal rate = decimal.Parse(Console.ReadLine());
                            newCustomer.Accounts.Add(new SavingAccount(rate));
                        }
                        else if (accType == "2")
                        {
                            Console.Write("Enter overdraft limit: ");
                            decimal limit = decimal.Parse(Console.ReadLine());
                            newCustomer.Accounts.Add(new CurrentAccount(limit));
                        }

                       // Customer.Add(newCustomer);
                        Console.WriteLine("Customer and account added successfully.");
                        break;

                    case "2":
                        Console.Write("Enter amount: ");
                        decimal depSav = decimal.Parse(Console.ReadLine());
                        saving.deposit(depSav);
                        Console.WriteLine($"Balance after deposit: {saving.Balance}");
                        Console.WriteLine("Deposit successful.");
                        break;

                    case "3":
                        Console.Write("Enter amount: ");
                        decimal withSav = decimal.Parse(Console.ReadLine());
                        saving.Withdraw(withSav);
                        break;

                    case "4":
                        Console.Write("Enter amount: ");
                        decimal depCur = decimal.Parse(Console.ReadLine());
                        current.deposit(depCur);
                        Console.WriteLine("Deposit successful.");
                        break;


                    case "5":
                        Console.Write("Enter amount: ");
                        decimal withCur = decimal.Parse(Console.ReadLine());
                        current.Withdraw(withCur);
                        break;

                    case "6":
                        Console.Write("Enter amount: ");
                        decimal transferAmount = decimal.Parse(Console.ReadLine());
                        saving.Transfer(transferAmount, current);
                        break;

                    case "7":
                        Console.WriteLine("\n--- Saving Account ---");
                        Console.WriteLine($"Balance: {saving.Balance}");
                        Console.WriteLine($"Interest: {saving.CalculateInterest()}");

                        Console.WriteLine("\n--- Current Account ---");
                        Console.WriteLine($"Balance: {current.Balance}");
                        Console.WriteLine($"Overdraft Limit: {current.OverdraftLimit}");
                        break;

                    case "0":
                        Console.WriteLine("Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice, try again.");
                        break;
                }

























            }
        }
    }
}

