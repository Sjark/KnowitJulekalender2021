namespace KnowitJulekalender2021.Dag12;

public class Dag12 : ISolution
{
    public void Execute()
    {
        var tasks = File.ReadAllLines($"{AppContext.BaseDirectory}\\Dag12\\task.txt");

        Category? currentCategory = null;
        var currentIndentation = 0;
        var rootCategories = new List<Category>();

        foreach (var task in tasks)
        {
            var foundItem = false;

            while (!foundItem)
            {
                var paddedGift = "G".PadLeft(currentIndentation + 1, '-');
                var paddedCategory = "K".PadLeft(currentIndentation + 1, '-');

                if (task.StartsWith(paddedGift))
                {
                    currentCategory?.Gifts.Add(task[(currentIndentation + 2)..]);
                    foundItem = true;
                }
                else if (task.StartsWith(paddedCategory))
                {
                    if (currentCategory == null)
                    {
                        currentCategory = new Category(task[(currentIndentation + 2)..]);
                        rootCategories.Add(currentCategory);
                    }
                    else
                    {
                        var oldCategory = currentCategory;

                        currentCategory = new Category(task[(currentIndentation + 2)..]);
                        oldCategory.Categories.Add(currentCategory);
                        currentCategory.ParentCategory = oldCategory;
                    }

                    currentIndentation++;
                    foundItem = true;
                }
                else
                {
                    currentCategory = currentCategory?.ParentCategory;
                    currentIndentation--;
                }
            }
        }

        var flattenedCategories = new List<string>();
        foreach (var category in rootCategories)
        {
            category.RemoveEmptyCategories();

            if (!category.IsEmpty())
            {
                flattenedCategories.Add(category.Name);
                flattenedCategories.AddRange(category.FlattenCategories());
            }
        }

        Console.WriteLine(flattenedCategories.Count);
    }
}

public class Category
{
    public Category(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
    public Category? ParentCategory { get; set; }
    public List<Category> Categories { get; set; } = new List<Category>();
    public List<string> Gifts { get; set; } = new List<string>();

    public bool IsEmpty()
    {
        if (Gifts.Count > 0)
        {
            return false;
        }

        foreach (var category in Categories)
        {
            if (!category.IsEmpty())
            {
                return false;
            }
        }

        return true;
    }

    public void RemoveEmptyCategories()
    {
        foreach (var category in Categories.ToList())
        {
            if (category.IsEmpty())
            {
                Categories.Remove(category);
            }
            else
            {
                category.RemoveEmptyCategories();
            }
        }
    }

    public List<string> FlattenCategories()
    {
        var categories = new List<string>();

        foreach (var category in Categories)
        {
            categories.Add(category.Name);

            categories.AddRange(category.FlattenCategories());
        }

        return categories;
    }
}
