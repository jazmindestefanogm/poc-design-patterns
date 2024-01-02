class StepActionPerformResult {
    StepStatus stepStatus;
    KycApplicationNextAction kycApplicationNextAction;
    KycApplicationStatus Status;
}

public enum StepAction
{
    START_EDV,
    START_IDV,
    CANCEL,
    REVIEW_IDV,
    COMFIRM_PII,
    APPROVE,
}

class abstract StepActionTemplate {

    public virtual bool CheckPreCondition()
    {
        return false;
    };

    public virtual StepActionPerformResult Perform()
    {
        throw not implemented exception;
    }

    public virtual KycApplicationStatus DetermineKycApplicationStatus()
    {
        throw not implemented exception;
    }

    public void CreateStepIfNecessary()
    {
        if (!CheckNewStep()) 
        {
            return;
        }

        CreateNewStep();
    }

    private virtual bool CheckNewStep()
    {
        return false;
    }

    private virtual void CreateNewStep()
    {
        throw not implemented exception;
    }
}

class StartEdvStepAction : StepActionTemplate {

    string action = START_EDV;

    EdvService edvService;

    public override bool CheckPreCondition()
    {
       return current step && kyc status == null;
    }

    public override StepActionPerformResult Perform()
    {
       StepStatus status = edvService.Verify(provider de la config);

       KycApplicationNextAction kycApplicationNextAction = status == EDV_NEED_IDV ? START_IDV : null;

       KycApplicationStatus kycStatus = DetermineKycApplicationStatus(status);

        return status && kycApplicationNextAction && kycStatus;
    }

    public override KycApplicationStatus DetermineKycApplicationStatus(StepStatus status)
    {
        switch (status) {
            case EDV_APPROVED: 
            case EDV_RISKY:
                return RISK_EVAL;
            case EDV_REJECTED:
                return REJECTED;
            case EDV_NEED_IDV:
                reutrn IDV_EVAL;
            default:
                return;    
        }
    }

    private override bool CheckNewStep()
    {
        return 
    }

    private override void CreateNewStep()
    {
        new EdvStep()
    }
}