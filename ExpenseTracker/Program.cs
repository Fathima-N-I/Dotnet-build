using System.Data.Common;
using ExpenseTracker.Models;
using ExpenseTracker.Services;

ExpenseService service = new ExpenseService();

static void ShowExpenses(List<Expense> expenses)
{
    if(expenses.Count == 0)
    {
        Console.WriteLine("No expenses found.");
    }
    else
    {
        foreach (Expense expense in expenses)
        {
            Console.WriteLine($"{expense.Id} - {expense.Category} - {expense.Amount} - {expense.Date:d}");
        }
    } 
}

while (true)
{

    Console.WriteLine("Expense Tracker");
    Console.WriteLine("-----------------");
    Console.WriteLine("1. Add Expense");
    Console.WriteLine("2. Show Expenses");
    Console.WriteLine("3. Delete Expense");
    Console.WriteLine("4. Update Expense");
    Console.WriteLine("5. Search by Category");
    Console.WriteLine("6. Show Total Expenses");
    Console.WriteLine("7. SHows Highest Expense");
    Console.WriteLine("8. Sort Expenses by Amount");
    Console.WriteLine("9. Exit");
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
            Category = category,
            Amount = amount,
            Date =  DateTime.Now
        };

        service.AddExpense(expense);
        Console.WriteLine("Expense added");
    }

    else if ( choice == "2")
    {
        bool show = service.ShowExpenses();
        if(!show)
        {
            Console.WriteLine("No expenses to show.");
        }
    }

    else if( choice == "3" )
    {
        Console.Write("Enter Expense ID to delete:");
        int id = Convert.ToInt32(Console.ReadLine());
        bool deleted = service.DeleteExpense(id);
        if(deleted)
        {
            Console.WriteLine("Expense deleted successfully.");
        }
        else
        {
            Console.WriteLine("Expense not found.");
        }
    }

    else if (choice == "4")
    {
        Console.Write("Enter expense ID to update: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enter what to update: 1 for Category and 2 for Amount");
        int updateChoice = Convert.ToInt32(Console.ReadLine());
        bool updated = false;
        if (updateChoice == 1)
        {
            Console.WriteLine("Enter the new category:");
            string category = Console.ReadLine() ?? string.Empty; 
            
            updated = service.UpdateExpense(id, category, null);
        }
        else if (updateChoice == 2) 
        {
            Console.WriteLine("Enter the new amount:");
            decimal amount = Convert.ToDecimal(Console.ReadLine());
            
            updated = service.UpdateExpense(id, null, amount);
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }
        if (updated)
        {
            Console.WriteLine("Expense updated");
        }
        else
        {
            Console.WriteLine("Expense not found.");
        }
    }

    else if (choice == "5")
    {
        Console.Write("Enter Category to filter");
        string? category = Console.ReadLine();

        if(string.IsNullOrWhiteSpace(category))
        {
            Console.WriteLine("Category cannot be empty");
        }
        else
        {
            ShowExpenses(service.SearchByCategory(category));
        }
    }

    else if (choice == "6")
    {
        decimal total = service.GetTotalExpenses();

        Console.WriteLine($"Total Expenses: $ {total}");
    }

    else if (choice == "7")
    {
        Expense? highestExpense = service.GetHighestExpense();

        if (highestExpense == null)
        {
            Console.WriteLine("No expense found");
        }
        else
        {
            Console.WriteLine($"{highestExpense.Category} | {highestExpense.Amount} | {highestExpense.Date:d}");
        }
    }
    
    else if (choice == "8")
    {
        ShowExpenses(service.GetExpensesSortedByAmount());
    }

    else if( choice == "9" )
    {
        break;
    }
    else
    {
        Console.WriteLine("Invalid choice. Please try again.");
    }

}
