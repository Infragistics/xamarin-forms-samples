namespace Moo2U.Converters {
    using System;
    using System.Globalization;
    using Xamarin.Forms;

    public class EnumMatchesEnumToBooleanConverter : IValueConverter {

        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture) {
            if (value == null || parameter == null) {
                return false;
            }
            var checkValue = value.ToString();
            var targetValue = parameter.ToString();
            return checkValue == targetValue;
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

    }
}
