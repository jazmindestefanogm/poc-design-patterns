public void ConfigureServices(IServiceCollection services)
{
    services.AddScoped<IVerificationService, EDVerificationService>();
    services.AddScoped<IVerificationService, IDVerificationService>();
    services.AddScoped<IStepAction, StartEdvStepAction>();
    services.AddScoped<IStepAction, StartIdvStepAction>();
}
