public class StepFactory
{
    EDVerificationService _eDVerificationService;

    public StepFactory(EDVerificationService eDVerificationService) {
        _eDVerificationService = eDVerificationService;
    }

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