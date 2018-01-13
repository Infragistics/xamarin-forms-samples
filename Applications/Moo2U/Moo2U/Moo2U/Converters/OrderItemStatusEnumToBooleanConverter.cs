namespace Moo2U.Converters {
    using System;
    using System.Globalization;
    using Moo2U.Model;
    using Xamarin.Forms;

    public class OrderItemStatusEnumToBooleanConverter : IValueConverter {

        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture) {
            if (value == null) {
                return false;
            }

            if (Enum.TryParse(value.ToString(), out OrderItemStatus orderItemStatus)) {
                if (orderItemStatus == OrderItemStatus.NotDelivered) {
                    return true;
                }
            }
            return false;
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

    }
}
