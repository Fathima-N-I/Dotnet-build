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
        expense.Id = expenses.Count > 0 ? expenses.Max(e => e.Id) + 1 : 1;
        expenses.Add(expense);
        SaveExpenses();
    }

    public bool ShowExpenses()
    {
        if(expenses.Count == 0)
        {
            Console.WriteLine("No expenses found.");
            return false;
        }

        foreach(var expense in expenses)
        {
            Console.WriteLine($"{expense.Id} - {expense.Category} - {expense.Amount}");
        }
        return true;
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
    public bool DeleteExpense(int id)
    {
        var expenseToDelete = expenses.FirstOrDefault(e => e.Id == id);
        if(expenseToDelete != null)
        {
            expenses.Remove(expenseToDelete);
            SaveExpenses();
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public bool UpdateExpense(int id, string? newCategory, decimal? newAmount)
    {
        var expenseToUpdate = expenses.FirstOrDefault(e => e.Id == id);
        if (expenseToUpdate != null)
        {
            if (!string.IsNullOrWhiteSpace(newCategory))
            {
                expenseToUpdate.Category = newCategory;
            }
            if (newAmount.HasValue) 
            {
                expenseToUpdate.Amount = newAmount.Value;
            }

            SaveExpenses();
            return true;
        }
        else
        {
            return false;
        }
    }

    public decimal GetTotalExpenses()
    {
        return expenses.Sum(e => e.Amount);
    }

    public List<Expense> GetExpensesSortedByAmount()
    {
        return expenses
            .OrderBy(e => e.Amount)
            .ToList();
    }

    public Expense? GetHighestExpense()
    {
        return expenses
            .OrderByDescending(e => e.Amount)
            .FirstOrDefault();
    }

    public List<Expense> SearchByCategory(string category)
    {
        return expenses
            .Where(e => e.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }


}
