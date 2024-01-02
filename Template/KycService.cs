class KycApplicationService 
{
    // endpoint de post para crear nueva kyc app
    public async ServiceResponse<KycAppDto> StartKycApplication(string customerId) // esto se usa en el controller
    {

        var serviceResponse = new ServiceResponse<KycApplicationDto>();
        
        if (HasAlreadyAnApp(customerId))
        {
            return;
        }

        var kycApp = CreateKycApplication(customerId);

        var firstAction = kycApp.nextAction; // START_EDV

        var stepAction = stepActionFactory.create(firstAction); // new StartEdvStepAction();

        if (!ExecuteStepAction(stepAction)) // step action START_EDV
        {
           return serviceResponse.addError(kuady error);
        } // EDV

        await _kycApplicationRepository.AddAsync(kycApp, cancellationToken);

        serviceResponse.Data = new KycApplicationDto(kycApp);

        return serviceResponse;
    }

    private static KycApplication CreateKycApplication(string customerId)
    {
        var customer = getCustomer(customerId)
        var kycConfig = GetKycConfig(customer.countryCode);
        return new KycApplication(customerId, kycConfig);
    }

    private void ExecuteStepAction(StepActionTemplate stepAction, KycApplication kycApplication) {

        if (!stepAction.CheckPreCondition()) 
        {
            return;
        }

        stepAction.CreateStepIfNecessary();

        StepActionPerformResult result = stepAction.Perform();

        kycApplication.UpdateStatus(result.kycstatus);
        kycApplication.UpdateCurrentStepStatus(result.stepstatus);
        kycApplication.UpdateNextAction(result.nextAction);
    }

      public async Task<ServiceResponse<KycApplicationActionDto>> PerformStepAction(string kycApplicationId, StepAction stepAction, metadata)
  {
      var serviceResponse = new ServiceResponse<KycApplicationActionDto>();
      var kycApplication = _kycApplicationRepository.Get(new Guid(kycApplicationId));

      if (kycApplication is null)
      {
          serviceResponse.AddError(HttpStatusCode.NotFound.ToString());
          return serviceResponse;
      }

        // pre condicion para idv
      if (stepAction == StepAction.START_IDV && kycApplication.StartIdvAttemptsExcedeed())
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