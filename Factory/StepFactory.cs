
public class StepFactory
{
    public static Step? CreateStep(StepAction action) {
        switch (action) 
        {
            case StepAction.START_EDV:
                return new EDVStep();
            case StepAction.START_IDV:
                return new IDVStep();
            default:
                return null;
        }
    }
}