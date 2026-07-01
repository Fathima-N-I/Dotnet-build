using System.Data.Common;
using ExpenseTracker.Models;
using ExpenseTracker.Services;

ExpenseService service = new ExpenseService();

while (true)
{
    Console.WriteLine("Expense Tracker");
    Console.WriteLine("-----------------");
    Console.WriteLine("1. Add Expense");
    Console.WriteLine("2. Show Expenses");
    Console.WriteLine("3. Delete Expense");
    Console.WriteLine("4. Update Expense");
    Console.WriteLine("5. Exit");

    Console.WriteLine("Enter your choice:");

    string? choice = Console.ReadLine();

    if( choice == "1" )
    {
        Console.Write("Enter Category:");
        string category = Console.ReadLine() ?? string.Empty;

        Console.Write("Enter Amount:");
        decimal amount = Convert.ToDecimal(Console.ReadLine());

        Expense expense = new Expense
        {
            Id = new Random().Next(1,1000),
            Category = category,
            Amount = amount,
            Date =  DateTime.Now
        };

        service.AddExpense(expense);
        Console.WriteLine("Expense added");
    }

    else if ( choice == "2")
    {
        service.ShowExpenses();
    }

    else if( choice == "3" )
    {
        Console.Write("Enter Expense ID to delete:");
        int id = Convert.ToInt32(Console.ReadLine());
        service.DeleteExpense(id);
        Console.WriteLine("Expense deleted");
    }

    else if (choice == "4")
    {
        Console.Write("Enter expense ID to update: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enter what to update: 1 for Category and 2 for Amount");
        int updateChoice = Convert.ToInt32(Console.ReadLine());

        if (updateChoice == 1)
        {
            Console.WriteLine("Enter the new category:");
            string category = Console.ReadLine() ?? string.Empty; 
            
            // Pass the new category, and null for the amount
            service.UpdateExpense(id, category, null);
        }
        else if (updateChoice == 2) // Changed to 'else if' for cleaner logic
        {
            Console.WriteLine("Enter the new amount:");
            decimal amount = Convert.ToDecimal(Console.ReadLine());
            
            // Pass null for the category, and the new amount
            service.UpdateExpense(id, null, amount);
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }
    }

    else if( choice == "5" )
    {
        break;
    }
    else
    {
        Console.WriteLine("Invalid choice. Please try again.");
    }

}
