namespace WebAppMvc;

public class Startup
{
    //default constructor
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    //we can use something like this
    //IWebHostEnvironment _env;
    //public Startup(IWebHostEnvironment env)
    //{
    //    _env = env;
    //}

    //опциональный метод, регистрирует сервисы, которые используются приложением
    public void ConfigureServices(IServiceCollection services)
    {
        //добавляет в коллекцию сервисов сервисы, которые необходимы для работы контроллеров MVC
        services.AddControllersWithViews();

        //без Views
        //services.AddControllers();

        //добавляет все сервисы фреймворка MVC (в том числе сервисы аутентификации, авторизации, валидации...)
        //services.AddMvc();

        //добавляет ТОЛЬКО основные сервисы фреймворка MVC, аутентификации, авторизации, валидации и проч. добавляй вручную
        //services.AddMvcCore();
    }

    //обязательный метод, устанавливает как приложение будет обрабатывать запрос
    //IApplicationBuilder нужен для установки компонентов, которые обрабатывают запрос
    //IWebHostEnvironment позволяет получить информацию о среде, в которой запускается приложение, и взаимодействовать с ней
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // если приложение в процессе разработки
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage(); //выводим информацию об ошибке, при ее наличии
        }

        app.UseHttpsRedirection(); //перенаправления всех HTTP-запросов на HTTPS.

        //app.UseHsts(); //применять протокол строгой транспортной безопасности HTTP (HSTS)

        app.UseStaticFiles(); //включает использование статики

        //app.UseAuthorization();

        app.UseRouting(); //добавляем возможности маршрутизации

        app.UseEndpoints(endpoints => // устанавливаем адреса, которые будут обрабатываться
        {
            // устанавливаем сопоставление маршрутов с контроллерами
            //endpoints.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");

            //обработка запроса, получаем констекст запроса в виде объекта context
            endpoints.MapGet("/", async context => 
            {
                // отправка ответа в виде строки "Hello World!"
                await context.Response.WriteAsync("Hello World!");
            });
        });
    }
}