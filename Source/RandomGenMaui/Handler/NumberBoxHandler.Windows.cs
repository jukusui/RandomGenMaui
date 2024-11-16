using Jukusui.RandomGen.Core;
using Microsoft.Maui.Handlers;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jukusui.RandomGen.View.Control;

using PfNumberBox = Microsoft.UI.Xaml.Controls.NumberBox;
using VvNumberBox = Jukusui.RandomGen.View.Control.NumberBox;


namespace Jukusui.RandomGen.Handler;
partial class NumberBoxHandler : ViewHandler<VvNumberBox, PfNumberBox>
{
    protected override PfNumberBox CreatePlatformView() => new();


    private static void MapMaximum(NumberBoxHandler handler, VvNumberBox box)
    {
        handler.PlatformView.Maximum = box.Maximum;
    }

    private static void MapMinimum(NumberBoxHandler handler, VvNumberBox box)
    {
        handler.PlatformView.Minimum = box.Minimum;
    }

    private static void MapValue(NumberBoxHandler handler, VvNumberBox box)
    {
        handler.PlatformView.Value = box.Value;
    }


    protected override void ConnectHandler(PfNumberBox platformView)
    {
        platformView.ValueChanged += OnValueChanged;
        base.ConnectHandler(platformView);
    }

    private void OnValueChanged(PfNumberBox sender, NumberBoxValueChangedEventArgs args)
    {
        VirtualView.Value = args.NewValue;
    }

    protected override void DisconnectHandler(PfNumberBox platformView)
    {
        platformView.ValueChanged -= OnValueChanged;
        base.DisconnectHandler(platformView);
    }
}
