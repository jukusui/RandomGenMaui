using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jukusui.RandomGen.Util;

[AcceptEmptyServiceProvider]
internal class MultiplyExtension : IMarkupExtension<double>
{

	[System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
    public double Value { get; set; }
    public double Factor { get; set; }

    public double ProvideValue(IServiceProvider serviceProvider)
    {
        return Value * Factor;
    }

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
    {
        return Value * Factor;
    }
}
