/*
El strategy al principio lo pensabamos para todo el comportamiento de cada step (EDV/IDV).
Ahora creo que aplica más el template para eso.
Pero si lo podriamos usar para los distintos comportamientos que puede tener un step especifico.
Por ej, el EDV Step podria tener 2 strategies (AristotleStrategy y DataHubStrategy) 
que se utilizan en el "perform", el metodo que se conecta a un provider, le manda info 
y segun la respuesta define un estado nuevo.
*/

public interface IStepStrategy
{
    void ExecuteStep();
}

public class EDVStepStrategy : IStepStrategy
{
    public void ExecuteStep()
    {
        Console.WriteLine("Ejecutando paso EDV (Estrategia específica para EDV)");
    }
}

public class IDVStepStrategy : IStepStrategy
{
    public void ExecuteStep()
    {
        Console.WriteLine("Ejecutando paso IDV (Estrategia específica para IDV)");
    }
}

public class Step
{
    private IStepStrategy strategy;

    public Step(IStepStrategy strategy)
    {
        this.strategy = strategy;
    }

    public void Execute()
    {
        if (strategy != null)
        {
            strategy.ExecuteStep();
        }
        else
        {
            Console.WriteLine("Estrategia no definida");
        }
    }
}

public class StepFactory
{
    public Step CreateStep(string action)
    {
        Step step;
        if (action == "START_EDV")
        {
            step = new Step(new EDVStepStrategy());
        }
        else if (action == "START_IDV")
        {
            step = new Step(new IDVStepStrategy());
        }
        else
        {
            throw new ArgumentException("Acción no válida para crear un paso");
        }

        return step;
    }
}

class Program
{
        StepFactory stepFactory = new StepFactory();

        string action1 = "START_EDV";
        Step step1 = stepFactory.CreateStep(action1);
        step1.Execute(); 

        string action2 = "START_IDV";
        Step step2 = stepFactory.CreateStep(action2);
        step2.Execute(); 
}


