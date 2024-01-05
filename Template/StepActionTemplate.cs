
abstract class StepActionTemplate {

    public virtual bool CheckPreCondition()
    {
        return false;
    }

    public virtual StepActionPerformResultDto Perform() => throw new NotImplementedException();

    public virtual KycApplicationStatus? DetermineKycApplicationStatus(CurrentStepStatus status) => throw new NotImplementedException();

    public void CreateStepIfNecessary()
    {
        if (!CheckNewStep()) 
        {
            return;
        }

        CreateNewStep();
    }

    public virtual bool CheckNewStep() => false;

    public virtual void CreateNewStep() => throw new NotImplementedException();
}