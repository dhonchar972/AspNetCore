using Microsoft.Extensions.FileProviders;

namespace SimpleWebAppRouting;

public class Program
{
    public static void Main(string[] args)
    {
        //var builder =
        //    WebApplication.CreateBuilder(
        //        new WebApplicationOptions { WebRootPath = "static" }
        //    );  // изменяем папку для хранения статики
        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();

        DefaultFilesOptions options = new DefaultFilesOptions();
        options.DefaultFileNames.Clear(); // удаляем имена файлов по умолчанию
        options.DefaultFileNames.Add("hello.html"); // добавляем новое имя файла(вместо index.html)
        app.UseDefaultFiles(options); // установка параметров

        app.UseStaticFiles();   // подключение middleware дающего поддержку статических файлов 
        //app.UseDefaultFiles();  // подключение middleware дающего поддержку страниц html по умолчанию
        //app.UseStaticFiles(new StaticFileOptions() // обрабатывает запросы к каталогу wwwroot/html
        //{
        //    FileProvider = new PhysicalFileProvider(
        //    Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\html")),
        //    RequestPath = new PathString("/pages")
        //});

        app.Run(async (context) => await context.Response.WriteAsync("Hello World")); // добавления middleware в конвейер обработки запроса

        app.Run(); // запуск приложения
    }
}
