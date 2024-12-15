using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jukusui.RandomGen.View;
internal class FallbackProvider : BindableObject
{


    public static readonly BindableProperty Value1Property =
        BindableProperty.Create(
            nameof(Value1), typeof(string), typeof(FallbackProvider), propertyChanged: OnChanged);


    public string? Value1
    {
        get => (string)GetValue(Value1Property);
        set => SetValue(Value1Property, value);
    }

    public static readonly BindableProperty Value2Property =
        BindableProperty.Create(
            nameof(Value2), typeof(string), typeof(FallbackProvider), propertyChanged: OnChanged);

    public string? Value2
    {
        get => (string)GetValue(Value2Property);
        set => SetValue(Value2Property, value);
    }

    private static BindablePropertyKey OutputPropertyKey =
    BindableProperty.CreateReadOnly(
        nameof(Output), typeof(string), typeof(FallbackProvider), "Hoge");

    public static BindableProperty OutputProperty => OutputPropertyKey.BindableProperty;

    public string? Output
    {
        get => (string)GetValue(OutputProperty);
        private set => SetValue(OutputPropertyKey, value);
    }


    private static void OnChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is FallbackProvider fallback)
        {
            fallback.Output = fallback.Value1 ?? fallback.Value2;
        }
    }

}
