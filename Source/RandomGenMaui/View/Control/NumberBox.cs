using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Font = Microsoft.Maui.Font;

namespace Jukusui.RandomGen.View.Control;
public class NumberBox : Microsoft.Maui.Controls.View, ITextStyle
{
    #region Values

    public static readonly BindableProperty ValueProperty =
        BindableProperty.Create(nameof(Value), typeof(double), typeof(NumberBox), 0.0, BindingMode.TwoWay, propertyChanged: OnValueChanged);

    private static void OnValueChanged(BindableObject bindable, object oldValue, object newValue)
    {
    }

    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }


    public static readonly BindableProperty MinimumProperty =
        BindableProperty.Create(nameof(Minimum), typeof(double), typeof(NumberBox), 0.0);

    public double Minimum
    {
        get => (double)GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }

    public static readonly BindableProperty MaximumProperty =
        BindableProperty.Create(nameof(Maximum), typeof(double), typeof(NumberBox), 100.0);

    public double Maximum
    {
        get => (double)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }
    #endregion

    #region TextStyles

    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(NumberBox), null);

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }


    public static readonly BindableProperty FontProperty =
        BindableProperty.Create(nameof(Font), typeof(Font), typeof(NumberBox), null);

    public Font Font
    {
        get => (Font)GetValue(FontProperty);
        set => SetValue(FontProperty, value);
    }


    public static readonly BindableProperty CharacterSpacingProperty =
        BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(NumberBox), null);

    public double CharacterSpacing
    {
        get => (double)GetValue(CharacterSpacingProperty);
        set => SetValue(CharacterSpacingProperty, value);
    }

    #endregion

}
