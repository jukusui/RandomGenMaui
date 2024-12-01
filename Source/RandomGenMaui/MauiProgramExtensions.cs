using Jukusui.RandomGen.Handler;
using Jukusui.RandomGen.View.Control;
using Microsoft.Extensions.Logging;


namespace Jukusui.RandomGen;

public static class MauiProgramExtensions
{
    public static MauiAppBuilder UseSharedMauiApp(this MauiAppBuilder builder)
    {
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("RandomGenIcon.ttf", "RandomGenIcon");
                fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIconsRegular");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .ConfigureMauiHandlers(Handlers =>
            {
                Handlers.AddHandler<NumberBox, NumberBoxHandler>();
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder;
    }
}
