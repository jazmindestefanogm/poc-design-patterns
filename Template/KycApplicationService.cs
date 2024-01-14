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