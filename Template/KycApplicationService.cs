using System.Net;

class KycApplicationService 
{
    public async Task<ServiceResponse<KycApplicationDto>> StartKycApplication(string customerId)
    {

        var serviceResponse = new ServiceResponse<KycApplicationDto>();

        if (KycApplicationDto.HasAlreadyAnActiveApplication(customerId))
        {
            return serviceResponse;
        }

        var kycApp = CreateKycApplication(customerId);

        var firstAction = StepAction.START_EDV; // START_EDV

        var stepAction = StepFactory.CreateStepAction(firstAction); // new StartEdvStepAction();

        var result = ExecuteStepAction(stepAction, kycApp);

        if (result is null) // step action START_EDV
        {
            return serviceResponse.addError("kuady error");
        } // EDV

        kycApp.UpdateStatus(result.Status);
        kycApp.UpdateCurrentStepStatus(result.stepStatus);
        kycApp.UpdateNextAction(result.kycApplicationNextAction);

        object value = await _kycApplicationRepository.AddAsync(kycApp, "cancellationToken");

        serviceResponse.Data = new KycApplicationDto(kycApp);

        return serviceResponse;
    }

    private static KycApplicationDto CreateKycApplication(string customerId)
    {
        var customer = string.Empty; // GetCustomer(customerId)
        var kycConfig = string.Empty; // GetKycConfig(customer.countryCode);
        return new KycApplicationDto(customerId, kycConfig);
    }

    private StepActionPerformResultDto ExecuteStepAction(StepActionTemplate stepAction, KycApplicationDto kycApplication) 
    {
        StepActionPerformResultDto result = new StepActionPerformResultDto();

        if (!stepAction.CheckPreCondition()) 
        {
            return result;
        }

        stepAction.CreateStepIfNecessary();

        return result = stepAction.Perform();
    }

      public async Task<ServiceResponse<KycApplicationActionDto>> PerformStepAction(string kycApplicationId, StepAction stepAction, string metadata)
  {
      var serviceResponse = new ServiceResponse<KycApplicationActionDto>();
      KycApplicationDto kycApplication = (KycApplicationDto)_kycApplicationRepository.Get(new Guid(kycApplicationId));

      if (kycApplication is null)
      {
          serviceResponse.AddError(HttpStatusCode.NotFound.ToString());
          return serviceResponse;
      }

        // pre condicion para idv
      if (stepAction == StepAction.START_IDV && (kycApplication.StartIdvAttemptsExcedeed() > 3))
      {
          serviceResponse.AddError(KycKuadyErrors.RetryLimitExceeded.Code, KycKuadyErrors.RetryLimitExceeded.Message);
          return serviceResponse;
      }

      
        var secondAction = kycApp.nextAction; // START_IDV

        var stepAction = stepActionFactory.create(secondAction); // new StartIdvStepAction();

        if (!ExecuteStepAction(stepAction)) // step action START_IDV
        {
           return serviceResponse.addError(kuady error);
        } // IDV

      await _kycApplicationRepository.UpdateAsync(kycApplication, cancellationToken);
      serviceResponse.Data = new KycApplicationActionDto(kycApplication, result);

      return serviceResponse;
  }
}

public class KycApplicationActionDto
{
}

internal class _kycApplicationRepository
{
    internal static Task<object> AddAsync(KycApplication kycApp, object cancellationToken)
    {
        throw new NotImplementedException();
    }

    internal static Task<object> AddAsync(KycApplicationDto kycApp, string v)
    {
        throw new NotImplementedException();
    }

    internal static object Get(Guid guid)
    {
        throw new NotImplementedException();
    }
}

public class KycApplicationDto
{
    private KycApplication kycApp;
    private string customerId;
    private object kycConfig;
    private KycApplicationDto kycApp1;

    public KycApplicationDto(KycApplication kycApp)
    {
        this.kycApp = kycApp;
    }

    public KycApplicationDto(KycApplicationDto kycApp1)
    {
        this.kycApp1 = kycApp1;
    }

    public KycApplicationDto(string customerId, object kycConfig)
    {
        this.customerId = customerId;
        this.kycConfig = kycConfig;
    }

    internal static bool HasAlreadyAnActiveApplication(string customerId)
    {
        throw new NotImplementedException();
    }

    internal int StartIdvAttemptsExcedeed()
    {
        throw new NotImplementedException();
    }

    internal void UpdateCurrentStepStatus(CurrentStepStatus stepStatus)
    {
        throw new NotImplementedException();
    }

    internal void UpdateNextAction(StepAction kycApplicationNextAction)
    {
        throw new NotImplementedException();
    }

    internal void UpdateStatus(KycApplicationStatus status)
    {
        throw new NotImplementedException();
    }
}

public class ServiceResponse<T>
{
    public KycApplicationDto? Data { get; internal set; }

    internal ServiceResponse<KycApplicationDto> addError(object kuady, object error)
    {
        throw new NotImplementedException();
    }

    internal ServiceResponse<KycApplicationDto> addError(string v)
    {
        throw new NotImplementedException();
    }

    internal void AddError(object value)
    {
        throw new NotImplementedException();
    }
}