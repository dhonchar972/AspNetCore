var builder = WebApplication.CreateBuilder();

//lifecicle:
//Transient: при каждом обращении к сервису создается новый объект сервиса
//Scoped: для каждого запроса создается свой объект сервиса
//Singleton: объект сервиса создается при первом обращении к нему
builder.Services.AddTransient<ITimeService, ShortTimeService>();// registration, like @Bean
//builder.Services.AddScoped<ITimeService, ShortTimeService>();
//builder.Services.AddSingleton<ITimeService, ShortTimeService>();
var app = builder.Build();

app.Run(async context =>
{
    var timeService = app.Services.GetService<ITimeService>(); //something like @Autowired
    await context.Response.WriteAsync($"Time: {timeService?.GetTime()}");
});

app.Run();

interface ITimeService
{
    string GetTime();
}
class ShortTimeService : ITimeService
{
    public string GetTime() => DateTime.Now.ToShortTimeString();
}

class TimeMessage
{
    ITimeService timeService; //constructor injection, similar with methods
    public TimeMessage(ITimeService timeService)
    {
        this.timeService = timeService;
    }
    public string GetTime() => $"Time: {timeService.GetTime()}";
}
