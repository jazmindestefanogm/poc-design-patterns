public interface IVerificationService
{
    // Metodos a definir
}

public class EDVerificationService : IVerificationService
{
    // Implementación del servicio EDVerificationService
}

public class IDVerificationService : IVerificationService
{
    // Implementación del servicio IDVerificationService
}

public interface IStepAction
{
    void Execute();
}

public class StartEdvStepAction : IStepAction
{
    private readonly IVerificationService _verificationService;

    public StartEdvStepAction(IVerificationService verificationService)
    {
        _verificationService = verificationService;
    }

    public void Execute()
    {
        // Implementación de la acción EDV
    }
}

public class StartIdvStepAction : IStepAction
{
    private readonly IVerificationService _verificationService;

    public StartIdvStepAction(IVerificationService verificationService)
    {
        _verificationService = verificationService;
    }

    public void Execute()
    {
        // Implementación de la acción IDV
    }
}
