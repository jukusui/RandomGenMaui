using Jukusui.RandomGen.Core;
using Jukusui.RandomGen.View.Control;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Text;

//#if IOS || MACCATALYST
//using PlatformView = Jukusui.RandomGen.;
//#elif ANDROID
//using PlatformView = Jukusui.RandomGen.;
//#elif WINDOWS
//using PlatformView = Jukusui.RandomGen.;
//#elif (NETSTANDARD || !PLATFORM) || (NET6_0_OR_GREATER && !IOS && !ANDROID)
//using PlatformView = System.Object;
//#endif

namespace Jukusui.RandomGen.Handler;

public partial class NumberBoxHandler
{
    public static IPropertyMapper<NumberBox, NumberBoxHandler> PropertyMapper = new PropertyMapper<NumberBox, NumberBoxHandler>(ViewHandler.ViewMapper)
    {
        [nameof(NumberBox.Background)] = MapBackground,
#if WINDOWS
        [nameof(NumberBox.TextColor)] = MapTextColor,
#endif
        [nameof(NumberBox.Minimum)] = MapMinimum,
        [nameof(NumberBox.Maximum)] = MapMaximum,
        [nameof(NumberBox.Value)] = MapValue,
    };



    public NumberBoxHandler() : base(PropertyMapper)
    {
    }


    public static void MapBackground(NumberBoxHandler handler, NumberBox box) =>
        handler.PlatformView?.UpdateBackground(box);

#if WINDOWS
    public static void MapTextColor(NumberBoxHandler handler, NumberBox box) =>
        handler.PlatformView?.UpdateTextColor(box);
#endif
}
