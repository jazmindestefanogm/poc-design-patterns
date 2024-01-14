# Design Patterns

## Factory Pattern

- The basic Factory Pattern is a design pattern that generally provides a way to create objects without specifying the exact class of the object that will be created. It involves a single method that returns a new instance of a class, often based on input parameters.
- This pattern is typically used when the creation logic of the objects is not simple, but the types of objects are not numerous and do not form a hierarchy. Otherwise, this variation of the design pattern may not hold up as well.

## Factory Example

Example → You’re trying to create a report generator and you create a factory called `ReportGeneratorFactory` that employs an interface called `IReportGenerator`. Thus, all report generator classes must implement the `IReportGenerator` interface and set up any specific types of constructors required.

In this example, the `ReportGeneratorFactory` class provides different report generators based on the client’s input, where objects are generated through the `GetReportGenerator` method.

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
