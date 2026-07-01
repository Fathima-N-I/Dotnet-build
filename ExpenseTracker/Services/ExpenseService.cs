using ExpenseTracker.Models;
using System.Text.Json;

namespace ExpenseTracker.Services;

public class ExpenseService
{
    private List<Expense> expenses = new List<Expense>();

    private String filePath = "Data/expenses.json";

    public ExpenseService()
    {
        loadExpense();
    }

    public void loadExpense()
    {
        if(File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            expenses = JsonSerializer.Deserialize<List<Expense>>(json)?? new List<Expense>();
        }
    }

    public void AddExpense(Expense expense)
    {
        expenses.Add(expense);
        SaveExpenses();
    }

    public void ShowExpenses()
    {
        if(expenses.Count == 0)
        {
            Console.WriteLine("No expenses found.");
            return;
        }

        foreach(var expense in expenses)
        {
            Console.WriteLine($"{expense.Id} - {expense.Category} - {expense.Amount}");
        }
    }

    public void SaveExpenses()
    {
        string json = JsonSerializer.Serialize(
            expenses,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });
        File.WriteAllText(filePath,json);
    }
    public void DeleteExpense(int id)
    {
        var expenseToDelete = expenses.FirstOrDefault(e => e.Id == id);
        if(expenseToDelete != null)
        {
            expenses.Remove(expenseToDelete);
            SaveExpenses();
            Console.WriteLine($"Expense with ID {id} deleted.");
        }
        else
        {
            Console.WriteLine($"Expense with ID {id} not found.");
        }
    }
    
    public void UpdateExpense(int id, string newCategory, decimal? newAmount)
    {
        var expenseToUpdate = expenses.FirstOrDefault(e => e.Id == id);
        if (expenseToUpdate != null)
        {
            // Only update category if a new one is provided
            if (newCategory != null)
            {
                expenseToUpdate.Category = newCategory;
            }

            // Only update amount if a new one is provided
            if (newAmount.HasValue) // Or: newAmount != null
            {
                expenseToUpdate.Amount = newAmount.Value;
            }

            SaveExpenses();
            Console.WriteLine($"Expense with ID {id} updated.");
        }
        else
        {
            Console.WriteLine($"Expense with ID {id} not found.");
        }
    }
}
