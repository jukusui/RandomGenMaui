using Jukusui.RandomGen.Core;
using Jukusui.RandomGen.View.Control;
using Microsoft.Maui.Handlers;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace Jukusui.RandomGen.Handler;
partial class NumberBoxHandler : ViewHandler<NumberBox, UIStepper>
{
    protected override UIStepper CreatePlatformView()
    {
        throw new NotImplementedException();
    }
    private static void MapMaximum(NumberBoxHandler handler, NumberBox box)
    {
        throw new NotImplementedException();
    }

    private static void MapMinimum(NumberBoxHandler handler, NumberBox box)
    {
        throw new NotImplementedException();
    }

    private static void MapValue(NumberBoxHandler handler, NumberBox box)
    {
        throw new NotImplementedException();
    }


    protected override void ConnectHandler(UIStepper platformView)
    {
        base.ConnectHandler(platformView);
    }

    protected override void DisconnectHandler(UIStepper platformView)
    {
        platformView.Dispose();
        base.DisconnectHandler(platformView);
    }
}
