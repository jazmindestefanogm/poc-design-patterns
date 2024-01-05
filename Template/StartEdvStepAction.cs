
class StartEdvStepAction : StepActionTemplate {

    string action = "START_EDV";
    EDVerificationService _edverificationService;

    public StartEdvStepAction(EDVerificationService eDVerificationService) => _edverificationService = eDVerificationService;

    public override bool CheckPreCondition() 
    {
        // return current step && kyc status == null;
        return false;
    }

    public override StepActionPerformResultDto Perform()
    {
       CurrentStepStatus status = _edverificationService.Verify("providerDeLaConfig");

       StepAction nextAction = status == CurrentStepStatus.EDV_NEED_IDV ? StepAction.START_IDV : StepAction.CANCEL;

       KycApplicationStatus? kycStatus = DetermineKycApplicationStatus(status);

        return new StepActionPerformResultDto 
        {
            stepStatus = status,
            Status = kycStatus ?? KycApplicationStatus.EDV_EVAL,
            kycApplicationNextAction = nextAction,
        };
    }

    public override KycApplicationStatus? DetermineKycApplicationStatus(CurrentStepStatus status)
    {
        switch (status) {
            case CurrentStepStatus.EDV_APPROVED: 
            case CurrentStepStatus.EDV_RISKY:
                return KycApplicationStatus.RISK_EVAL;
            case CurrentStepStatus.EDV_REJECTED:
                return KycApplicationStatus.REJECTED;
            case CurrentStepStatus.EDV_NEED_IDV:
                return KycApplicationStatus.IDV_EVAL;
            default:
                return null;    
        }
    }

    public override bool CheckNewStep()
    {
        return true; 
    }

    public override void CreateNewStep() => new EDVerificationStep();


}

internal class EDVerificationStep
{
    public EDVerificationStep()
    {
    }
}

internal class EDVerificationService
{
    internal CurrentStepStatus Verify(string v)
    {
        throw new NotImplementedException();
    }
}