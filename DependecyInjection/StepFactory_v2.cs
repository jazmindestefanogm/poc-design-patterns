public class StepFactory
{
    private readonly IServiceProvider _serviceProvider;

    public StepFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IStepAction? CreateStepAction(StepAction action)
    {
        switch (action)
        {
            case StepAction.START_EDV:
                return _serviceProvider.GetService<IStepAction>();
            case StepAction.START_IDV:
                return _serviceProvider.GetService<IStepAction>();
            default:
                return null;
        }
    }
}
