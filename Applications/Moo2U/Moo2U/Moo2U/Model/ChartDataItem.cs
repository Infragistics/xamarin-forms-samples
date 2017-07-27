namespace Moo2U.Model {
    using System;
    using System.Globalization;

    public class ChartDataItem {

        readonly DataItemKind _dataItemKind;

        public DateTime Date { get; set; }

        public String HourLabel { get; set; }

        public String MonthLabel => this.Date.ToString("MM-dd");

        public String ToolTipText => $"{_dataItemKind} {this.Value:N0}";

        public Double Value { get; set; }

        public String WeekLabel => this.Date.DayOfWeek.ToString().Substring(0, 1);

        public String YearLabel => CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(this.Date.Month);

        public ChartDataItem(DataItemKind dataItemKind) {
            _dataItemKind = dataItemKind;
            if (!Enum.IsDefined(typeof(DataItemKind), dataItemKind)) {
                throw new ArgumentOutOfRangeException(nameof(dataItemKind), "Value should be defined in the DataItemKind enum.");
            }
        }

    }
}
