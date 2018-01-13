namespace Moo2U.Converters {
    using System;
    using System.Globalization;
    using Moo2U.Model;
    using Xamarin.Forms;

    public class OrderStatusEnumToBooleanConverter : IValueConverter {

        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture) {
            if (value == null || parameter == null) {
                return false;
            }
            var checkValue = value.ToString();
            var targetValue = parameter.ToString();

            if (targetValue == OrderStatus.New.ToString() && (checkValue == OrderStatus.New.ToString() || checkValue == OrderStatus.Partial.ToString())) {
                return true;
            }

            return checkValue == targetValue;
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

    }
}
