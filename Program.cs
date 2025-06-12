using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
QuestPDF.Settings.License = LicenseType.Community;
// true by default
QuestPDF.Settings.UseEnvironmentFonts = false;
FontManager.RegisterFont(File.OpenRead("Fonts/Padauk-Regular.ttf"));

var app = builder.Build();

app.MapGet("/pdf", () =>
{
    // Create the PDF
    var document = Document.Create(container =>
    {
        container.Page(page =>
        {
            page.Margin(50);
            page.Size(PageSizes.A4);

            page.Content().Text("မြန်မာစာ စမ်းသပ်ချက်")
                          .FontFamily("Padauk")
                          .FontSize(24);
        });
    });

    // Export to MemoryStream
    using var stream = new MemoryStream();
    document.GeneratePdf(stream);
    stream.Position = 0;

    return Results.File(stream.ToArray(), "application/pdf", "output.pdf");
});

app.Run();
