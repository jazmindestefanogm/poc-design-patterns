# Design Patterns

This repository is implements solutions for Kuady 6 Pay Retailers Team, examples below just explain a bit more about Desing Patterns.

# Factory

- The basic Factory Pattern is a design pattern that generally provides a way to create objects without specifying the exact class of the object that will be created. It involves a single method that returns a new instance of a class, often based on input parameters.
- This pattern is typically used when the creation logic of the objects is not simple, but the types of objects are not numerous and do not form a hierarchy. Otherwise, this variation of the design pattern may not hold up as well.

## Factory Pattern

Example → You’re trying to create a report generator and you create a factory called `ReportGeneratorFactory` that employs an interface called `IReportGenerator`. Thus, all report generator classes must implement the `IReportGenerator` interface and set up any specific types of constructors required.

In this example, the `ReportGeneratorFactory` class provides different report generators based on the client’s input, where objects are generated through the `GetReportGenerator` method.

```csharp
public interface IReportGenerator
{
    string GenerateReport();
}

public class XmlReportGenerator : IReportGenerator
{
    public string GenerateReport()
    {
        return "This is an generated Xml Report.";
    }
}

public class CsvReportGenerator : IReportGenerator
{
    public string GenerateReport()
    {
        return "This is an generated Csv Report.";
    }
}

public class ReportGeneratorFactory
{
    public IReportGenerator GetReportGenerator(string format)
    {
        switch (format.ToLower())
        {
            case "xml":
                return new XmlReportGenerator();
            case "csv":
                return new CsvReportGenerator();
            default:
                throw new ApplicationException("Report format not supported.");
        }
    }
}
```

## Abstract Factory Pattern

Here are the steps to building a complex Factory pattern:

1. Create an abstract `Factory` class that all factories at the application level will implement.
2. In the abstract `Factory` class, you can define a method that creates application-specific factories.
3. Each application-specific factory will have its interface that will be used to create each class.

```csharp
public abstract class Factory
{
    public abstract ILiteratureFactory MakeLiteratureFactory();
}

public interface IBook
{
    string GetGenre();
}

public class FictionBook : IBook
{
    public string GetGenre()
    {
        return "Fiction Book.";
    }
}

public class NonFictionBook : IBook
{
    public string GetGenre()
    {
        return "Non-Fiction Book.";
    }
}

public interface ILiteratureFactory
{
    IBook CreateBook();
}

public class FictionFactory : ILiteratureFactory
{
    public IBook CreateBook()
    {
        return new FictionBook();
    }
}

public class NonFictionFactory : ILiteratureFactory
{
    public IBook CreateBook()
    {
        return new NonFictionBook();
    }
}

public enum FactoryEnum
{
	FICTION,
	NON_FICTION
}

public class LiteratureAcademy: Factory
{
    public override ILiteratureFactory MakeLiteratureFactory(FactoryEnum factory)
    {
				switch (factory)
				{
					case factory.FICTION:
								return new FictionFactory();
					case factory.NON_FICTION:
								return new NonFictionFactory();
				}

    }
}
```

### Best Practices When Using the Factory Pattern in C

When using the Factory pattern in C#, there are some best practices you should keep in mind. Here are some examples:

- Use the Factory class to prevent objects from being created directly by construction.
- Always define an interface or abstract class to be implemented by the object classes.
- Use an enumeration or a configuration file to define the Factory input parameters. Try using enums!

## Template Pattern

- The Template Method Pattern is a behavioral design pattern that defines the program skeleton of an algorithm in an algorithm class but delays some steps to subclasses. It allows subclasses to redefine certain steps of an algorithm without changing the algorithm’s structures.

### Template-Method Pattern: When To Use

1. The problem resolved by the **Template-Method** Pattern, is the use of an algorithm which has different variations. You would need to split your algorithm in different steps, implemented in the abstract class when in common between the different implementations. In other hand, the steps which are different will be implemented in the concrete classes.
2. Another interesting case where you would detect the need of this pattern, is when you have copy/paste code (private functions) between different classes.
3. Finally you may to use this pattern when most of your classes have related behaviours.

### Template-Method Pattern: Some Advantages

- It’s **fairly easy to create concrete implementations of an algorithm** because you’re removing common parts of the problem domain by the use of an abstract class.
- **Clean code** because you avoid duplicate code.
- **Ever cleaner code** because you separate the algorithm into private methods/functions, which are simpler and easier to test.

### Example

Suppose we’re building a document processing application that can save documents in different formats (e.g., PDF, Word, and Text). We’ll use the Template Method Pattern to define a common saving algorithm while allowing each document format to implement its specific saving logic.

- **Step 1: Define the Template Class**

First, we create an abstract class `Document` representing the template for saving documents:

```csharp
public abstract class Document
{
    public void Save()
    {
        Open();
        WriteContent();
        Close();
    }

    protected abstract void Open();
    protected abstract void WriteContent();
    protected abstract void Close();
}
```

In this template class, `Save` is the template method that defines the common algorithm. The `Open`, `WriteContent`, and `Close` methods are abstract and need to be implemented by concrete subclasses.

- \***\*Step 2: Create Concrete Document Classes\*\***

```csharp
public class PdfDocument : Document
{
    protected override void Open()
    {
        Console.WriteLine("Opening PDF document...");
    }

    protected override void WriteContent()
    {
        Console.WriteLine("Writing PDF content...");
    }

    protected override void Close()
    {
        Console.WriteLine("Closing PDF document...");
    }
}

// Similar classes for WordDocument and TextDocument
```

- \***\*Step 3: Using the Template Method\*\***

```csharp
static void Main(string[] args)
{
    Document pdfDocument = new PdfDocument();
    pdfDocument.Save();

    Document wordDocument = new WordDocument();
    wordDocument.Save();

    Document textDocument = new TextDocument();
    textDocument.Save();
}
```

## Strategy Pattern

- The strategy pattern gives you the flexibility to choose the right strategy for a task, just like selecting the most suitable tool from a toolbox. This approach makes your code more modular and adaptable, as you can plug in different strategies without making major changes to your overall program.

### Problem

Imagine you're a chef and you have different ways to cook a dish. You can bake, fry, or steam the food. Each cooking method represents a strategy. Now, instead of hard-coding a specific cooking method in your recipe, you can use the strategy pattern. You define an interface called "CookingStrategy" with a common method, let's say "Cook()", and create separate classes for each cooking method that implement this interface (e.g., "BakingStrategy", "FryingStrategy", "SteamingStrategy").

### Example

- To start, we need to create an interface called ICookingStrategy, which will act as our Strategy interface. This interface will declare a common method Cook() that represents the cooking action. It serves as a contract that all cooking strategies must adhere to.

```csharp
// Define the CookingStrategy interface
public interface ICookingStrategy
{
    void Cook();
}
```

- The next step is to create multiple classes that implement the ICookingStrategy interface. Each implementation will provide specific instructions for a particular cooking method within its Cook() method.

```csharp
// Implement the BakingStrategy
public class BakingStrategy : ICookingStrategy
{
    public void Cook()
    {
        Console.WriteLine("Baking the dish...");
        // Add specific instructions for baking
    }
}

// Implement the FryingStrategy
public class FryingStrategy : ICookingStrategy
{
    public void Cook()
    {
        Console.WriteLine("Frying the dish...");
        // Add specific instructions for frying
    }
}

// Implement the SteamingStrategy
public class SteamingStrategy : ICookingStrategy
{
    public void Cook()
    {
        Console.WriteLine("Steaming the dish...");
        // Add specific instructions for steaming
    }
}
```

- Next, we'll introduce a Chef class that makes use of the strategy pattern.

```csharp
// Chef class that utilizes the strategy pattern
public class Chef
{
    private ICookingStrategy _cookingStrategy;

    // Set the cooking strategy
    public void SetCookingStrategy(ICookingStrategy cookingStrategy)
    {
        _cookingStrategy = cookingStrategy;
    }

    // Cook the dish using the selected strategy
    public void CookDish()
    {
        Console.WriteLine("Preparing the dish...");
        _cookingStrategy.Cook();
        // Additional steps for dish preparation
        Console.WriteLine("Dish is ready!");
    }
}
```

- Finally, we can utilize the pattern in the following manner

```csharp
class Program
{
    static void Main(string[] args)
    {
        // Create a Chef instance
        Chef chef = new Chef();

        // Create different cooking strategies
        ICookingStrategy bakingStrategy = new BakingStrategy();
        ICookingStrategy fryingStrategy = new FryingStrategy();
        ICookingStrategy steamingStrategy = new SteamingStrategy();

        // Set the baking strategy
        chef.SetCookingStrategy(bakingStrategy);
        chef.CookDish(); // The dish will be baked

        // Set the frying strategy
        chef.SetCookingStrategy(fryingStrategy);
        chef.CookDish(); // The dish will be fried

        // Set the steaming strategy
        chef.SetCookingStrategy(steamingStrategy);
        chef.CookDish(); // The dish will be steamed
    }
}
```

### When to use the strategy pattern

Strategy pattern is a good choice when you need flexibility and modularity in choosing and swapping out different algorithms or behaviors at runtime.

### Pros and cons of using the strategy pattern

✔️ Improved code organization

✔️ Flexibility and extensibility

✔️ Code reusability

✔️ Separation of concerns

❌ Increases the complexity of the code

❌ Potential performance overhead

❌ Increased number of classes
