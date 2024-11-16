using Android.Widget;
using Jukusui.RandomGen.Core;
using Jukusui.RandomGen.View.Control;
using Microsoft.Maui.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jukusui.RandomGen.Handler
{
    partial class NumberBoxHandler : ViewHandler<NumberBox, NumberPicker>
    {
        protected override NumberPicker CreatePlatformView() => new NumberPicker(Context);

        private static void MapMaximum(NumberBoxHandler handler, NumberBox box)
        {
            handler.PlatformView.MaxValue = (int)double.Round(box.Maximum);
        }

        private static void MapMinimum(NumberBoxHandler handler, NumberBox box)
        {
            handler.PlatformView.MinValue = (int)double.Round(box.Minimum);
        }

        private static void MapValue(NumberBoxHandler handler, NumberBox box)
        {
            handler.PlatformView.Value = (int)double.Round(box.Value);
        }



        protected override void ConnectHandler(NumberPicker platformView)
        {
            base.ConnectHandler(platformView);
        }

        protected override void DisconnectHandler(NumberPicker platformView)
        {
            platformView.Dispose();
            base.DisconnectHandler(platformView);
        }
    }
}
