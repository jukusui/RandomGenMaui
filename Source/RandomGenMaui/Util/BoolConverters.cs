using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jukusui.RandomGen.Util;
internal class BoolConverters : IMultiValueConverter
{

    Func<bool, bool, bool> orFunc = (old, current) => old || current;
    Func<bool, bool, bool> andFunc = (old, current) => old && current;

    public BoolOperations Operation { get; set; }

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        return values != null && values.Select(v => (v as bool?) ?? false).Aggregate(Operation == BoolOperations.And ? andFunc : orFunc);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

}
public enum BoolOperations
{
    And,
    Or,
}