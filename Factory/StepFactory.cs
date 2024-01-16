public class StepFactory
{
    EDVerificationService _eDVerificationService;

    public StepFactory(EDVerificationService eDVerificationService) {
        _eDVerificationService = eDVerificationService;
    }

    /* 
    Algo asi es lo que probablemente tengamos en nuestro código,
    aunque pareciera que no haria falta nesariamente implementar este patrón.
    Para adaptarlo a que use el factory, en este caso lo que haria es que
    este metodo devuelva el factory a utilizar.
    Algo asi como GetFactoryByStepAction().
    Y ese factory se lo pasamos al objeto que necesite crear esta instancia.
    (hoy lo tenemos en el KycApplicationService)
    */
    public static StepActionTemplate? CreateStepAction(StepAction action) {
        switch (action) 
        {
            case StepAction.START_EDV:
                return new StartEdvStepAction(_eDVerificationService);
            case StepAction.START_IDV:
                return new StartIdvStepAction(_iDVerificationService);
            default:
                return null;
        }
    }
}