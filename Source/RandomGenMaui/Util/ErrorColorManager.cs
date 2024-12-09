using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jukusui.RandomGen.Util;

public class ErrorColorManager : BindableObject
{

    public static BindableProperty TextColorOverrideProperty =
    BindableProperty.CreateAttached(
        "TextColorOverride", typeof(Color), typeof(ErrorColorManager), null, propertyChanged: OnTextColorChanged);

    public static Color? GetTextColorOverride(BindableObject bindable) => (Color)bindable.GetValue(TextColorOverrideProperty);
    public static void GetTextColorOverride(BindableObject bindable, Color? color) => bindable.SetValue(TextColorOverrideProperty, color);

    private static void OnTextColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        UpdateColor(bindable);
    }


    public static BindableProperty EnableColorOverrideProperty =
    BindableProperty.CreateAttached(
        "EnableColorOverride", typeof(object), typeof(ErrorColorManager), null, propertyChanged: OnEnableColorOverrideChanged);

    public static object? GetEnableColorOverride(BindableObject bindable) => bindable.GetValue(EnableColorOverrideProperty);
    public static void GetEnableColorOverride(BindableObject bindable, object? value) => bindable.SetValue(EnableColorOverrideProperty, value);

    private static void OnEnableColorOverrideChanged(BindableObject bindable, object oldValue, object newValue)
    {
        UpdateColor(bindable);
    }

    private static void UpdateColor(BindableObject bindable)
    {
        if (bindable is Entry entry)
        {
            var color = GetTextColorOverride(bindable);
            var enable = GetEnableColorOverride(bindable);
            if (enable != null && color != null)
            {
                entry.SetValue(Entry.TextColorProperty, color);
            }
            else
            {
                entry.ClearValue(Entry.TextColorProperty);
            }
        }
    }


}
