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
