namespace Moo2U.SampleData {
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// Represents DataGenerator, used to create sample data at design-time, test-time, or run-time
    /// </summary>
    public sealed class DataGenerator : IDataGenerator {

        readonly List<String> _streetNames = new List<String> { "Thomas Edison Ave", "21st Street", "Pinewood Blvd", "Commerce Drive", "Forsgate Drive", "RTE 130", "Friendship Rd" };
        readonly List<String> _firstNames = new List<String> { "Tom", "Dick", "Harry", "Jim", "Susan", "Susie", "Abel", "Ann", "William", "Josh" };
        readonly List<String> _lastNames = new List<String> { "Washington", "McDonald", "Smith", "Jones", "O'Malley", "Love", "Fox", "Smithfield", "West", "Jordon" };
        readonly List<String> _companyNames = new List<String> { "Acme Inc.", "Little Richie Software", "Complex Objects LTD", "Designers Rock", "Simple Solutions", "Power Developers", "WPF Disciples", "Cupcake Factory", "Le Chocolateur", "Gianluca's Pizza" };
        readonly List<String> _urls = new List<String> { "http://microsoft.com", "http://oceanware.wordpress.com", "http://agsmith.wordpress.com/", "http://www.beacosta.com/blog/", "http://wekempf.spaces.live.com/default.aspx", "http://x-coders.com/blogs/sneaky/default.aspx", "http://blogs.ugidotnet.org/corrado/Default.aspx", "http://sachabarber.net/", "http://weblogs.asp.net/scottgu/" };
        Int32 _currentStreetName;
        Int32 _currentFirstName;
        Int32 _currentLastName;
        Int32 _currentCompanyName;
        Int32 _currentUrl;
        Int32 _seedValue;
        Int32 _incrementValue = 1;
        const Int32 _WORD_LOWER_BOUND = 0;
        readonly Int32 _wordUpperBound;
        const Int32 _STATES_LOWER_BOUND = 0;
        readonly Int32 _statesUpperBound;
        const String STRING_WHITE_SPACE = " ";
        readonly StringBuilder _sb = new StringBuilder();
        readonly Random _random;
        readonly String[] _words = { "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "duis", "autem", "vel", "eum", "iriure", "dolor", "in", "hendrerit", "in", "vulputate", "velit", "esse", "molestie", "consequat", "vel", "illum", "dolore", "eu", "feugiat", "nulla", "facilisis", "at", "vero", "eros", "et", "accumsan", "et", "iusto", "odio", "dignissim", "qui", "blandit", "praesent", "luptatum", "zzril", "delenit", "augue", "duis", "dolore", "te", "feugait", "nulla", "facilisi", "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer", "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod", "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat", "volutpat", "ut", "wisi", "enim", "ad", "minim", "veniam", "quis", "nostrud", "exerci", "tation", "ullamcorper", "suscipit", "lobortis", "nisl", "ut", "aliquip", "ex", "ea", "commodo", "consequat", "duis", "autem", "vel", "eum", "iriure", "dolor", "in", "hendrerit", "in", "vulputate", "velit", "esse", "molestie", "consequat", "vel", "illum", "dolore", "eu", "feugiat", "nulla", "facilisis", "at", "vero", "eros", "et", "accumsan", "et", "iusto", "odio", "dignissim", "qui", "blandit", "praesent", "luptatum", "zzril", "delenit", "augue", "duis", "dolore", "te", "feugait", "nulla", "facilisi", "nam", "liber", "tempor", "cum", "soluta", "nobis", "eleifend", "option", "congue", "nihil", "imperdiet", "doming", "id", "quod", "mazim", "placerat", "facer", "possim", "assum", "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer", "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod", "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat", "volutpat", "ut", "wisi", "enim", "ad", "minim", "veniam", "quis", "nostrud", "exerci", "tation", "ullamcorper", "suscipit", "lobortis", "nisl", "ut", "aliquip", "ex", "ea", "commodo", "consequat", "duis", "autem", "vel", "eum", "iriure", "dolor", "in", "hendrerit", "in", "vulputate", "velit", "esse", "molestie", "consequat", "vel", "illum", "dolore", "eu", "feugiat", "nulla", "facilisis", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "at", "accusam", "aliquyam", "diam", "diam", "dolore", "dolores", "duo", "eirmod", "eos", "erat", "et", "nonumy", "sed", "tempor", "et", "et", "invidunt", "justo", "labore", "stet", "clita", "ea", "et", "gubergren", "kasd", "magna", "no", "rebum", "sanctus", "sea", "sed", "takimata", "ut", "vero", "voluptua", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum" };
        readonly String[] _states = { "AL", "AK", "AS", "AZ", "AR", "CA", "CO", "CT", "DE", "DC", "FM", "FL", "GA", "GU", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MH", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "MP", "OH", "OK", "OR", "PW", "PA", "PR", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VI", "VA", "WA", "WV", "WI", "WY", "AA", "AE", "AP" };

        /// <summary>
        /// Initializes a new instance of the <see cref="DataGenerator"/> class.
        /// </summary>
        public DataGenerator() {
            _random = new Random();
            _wordUpperBound = _words.Length - 1;
            _statesUpperBound = _states.Length - 1;
            SeedSequentialInteger(1000, 1);
        }

        /// <summary>
        /// Seeds the sequential integer.
        /// </summary>
        /// <param name="seedValue">The seed value.</param>
        /// <param name="incrementValue">The increment value.</param>
        public void SeedSequentialInteger(Int32 seedValue, Int32 incrementValue) {
            _seedValue = seedValue;
            _incrementValue = incrementValue;
        }

        /// <summary>
        /// Gets the next sequential integer. Each time this property is accessed the sequential integer is incremented.
        /// </summary>
        /// <returns>Integer representing the next sequential integer.</returns>
        public Int32 GetSequentialInteger() {
            _seedValue += _incrementValue;
            return _seedValue;
        }

        /// <summary>
        /// Gets the date that is between minValue and maxValue.
        /// </summary>
        /// <param name="minValue">The min value.</param>
        /// <param name="maxValue">The max value.</param>
        /// <returns>DateTime that is between minValue and maxValue.</returns>
        public DateTime GetDate(DateTime minValue, DateTime maxValue) {

            TimeSpan ts = maxValue - minValue;

            Int32 dateDiff = Math.Abs(ts.TotalDays) > Int32.MaxValue ? Int32.MaxValue : Convert.ToInt32(Math.Abs(ts.TotalDays));
            
            return minValue.AddDays(GetInteger(0, dateDiff));
        }

        /// <summary>
        /// Gets the decimal that is between minValue and maxValue.
        /// </summary>
        /// <param name="minValue">The min value.</param>
        /// <param name="maxValue">The max value.</param>
        /// <returns>Decimal that is between minValue and maxValue.</returns>
        public Decimal GetDecimal(Int32 minValue, Int32 maxValue) {
            return Convert.ToDecimal(_random.Next(minValue, maxValue) + _random.NextDouble());
        }

        /// <summary>
        /// Gets the double that is between minValue and maxValue.
        /// </summary>
        /// <param name="minValue">The min value.</param>
        /// <param name="maxValue">The max value.</param>
        /// <returns>Double that is between minValue and maxValue.</returns>
        public Double GetDouble(Int32 minValue, Int32 maxValue) {
            return _random.Next(minValue, maxValue) + _random.NextDouble();
        }

        /// <summary>
        /// Gets the randomly created email address.
        /// </summary>
        /// <returns>String with a randomly created email address.</returns>
        public String GetEmail() {
            return $"{GetString(7, StringCase.Lower, true)}@{GetString(11, StringCase.Lower, true)}.com";
        }

        /// <summary>
        /// Gets a randomly selected first name.
        /// </summary>
        /// <returns>String with a randomly selected first name.</returns>
        public String GetFirstName() {
            _currentFirstName += 1;
            if(_currentFirstName > _firstNames.Count - 1) {
                _currentFirstName = 0;
            }
            return _firstNames[_currentFirstName];
        }

        /// <summary>
        /// Gets the street.
        /// </summary>
        /// <returns>String.</returns>
        public String GetStreet() {
            _currentStreetName += 1;
            if (_currentStreetName > _streetNames.Count - 1) {
                _currentStreetName = 0;
            }
            return _streetNames[_currentStreetName];
        }

        /// <summary>
        /// Gets a randomly selected Url.
        /// </summary>
        /// <returns>String with a randomly selected Url.</returns>
        public String GetUrl() {
            _currentUrl += 1;
            if(_currentUrl > _urls.Count - 1) {
                _currentUrl = 0;
            }
            return _urls[_currentUrl];
        }

        /// <summary>
        /// Gets a randomly selected company name.
        /// </summary>
        /// <returns>String with a randomly selected company name.</returns>
        public String GetCompanyName() {
            _currentCompanyName += 1;
            if(_currentCompanyName > _companyNames.Count - 1) {
                _currentCompanyName = 0;
            }
            return _companyNames[_currentCompanyName];
        }

        /// <summary>
        /// Gets a randomly selected last name.
        /// </summary>
        /// <returns>String with a randomly selected last name.</returns>
        public String GetLastName() {
            _currentLastName += 1;
            if(_currentLastName > _lastNames.Count - 1) {
                _currentLastName = 0;
            }
            return _lastNames[_currentLastName];
        }

        /// <summary>
        /// Gets a randomly selected full name
        /// </summary>
        /// <returns>String with a randomly selected full name.</returns>
        public String GetFullName() {
            return String.Concat(GetFirstName(), STRING_WHITE_SPACE, GetLastName());
        }

        /// <summary>
        /// Gets an integer that is between minValue and maxValue.
        /// </summary>
        /// <param name="minValue">The min value.</param>
        /// <param name="maxValue">The max value.</param>
        /// <returns>Integer that is between minValue and maxValue.</returns>
        public Int32 GetInteger(Int32 minValue, Int32 maxValue) {
            return _random.Next(minValue, maxValue);
        }

        /// <summary>
        /// Gets a randomly generated social security number.
        /// </summary>
        /// <returns>String with a randomly generated social security number.</returns>
        public String GetSsn() {
            return $"({GetInteger(100, 999)}-{GetInteger(10, 99)}-{GetInteger(1000, 9999)}";
        }

        /// <summary>
        /// Gets the boolean.
        /// </summary>
        /// <returns>Boolean that is randomly set to <c>true</c> or <c>false</c>.</returns>
        public bool GetBoolean() {
            return _random.Next(0, 49) % 2 == 0;
        }

        /// <summary>
        /// Gets a randomly generated phone number.
        /// </summary>
        /// <returns>String with a randomly generated phone number.</returns>
        public String GetPhoneNumber() {
            return $"({GetInteger(100, 999)}) {GetInteger(100, 999)}-{GetInteger(1000, 9999)}";
        }

        /// <summary>
        /// Gets a randomly generated zip code.
        /// </summary>
        /// <returns>String with a randomly generated zip code.</returns>
        public String GetZipCode() {
            return GetInteger(10000, 99999).ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Gets a randomly selected state abbreviation.
        /// </summary>
        /// <returns>String with a randomly selected state abbreviation.</returns>
        public String GetStateAbbreviation() {
            return _states[_random.Next(_STATES_LOWER_BOUND, _statesUpperBound)];
        }

        /// <summary>
        /// Gets a string of random lorin epsum having a length equal to or less than the max length argument.
        /// </summary>
        /// <param name="maxLength">Maximum length of the returned string.</param>
        /// <returns>String of random lorin epsum having a length equal to or less than the max length argument.</returns>
        public String GetString(Int32 maxLength) {
            return GetString(maxLength, StringCase.None, false);
        }

        /// <summary>
        /// Gets a string of random lorin epsum having a length equal to or less than the max length argument along with the specified string case applied.
        /// </summary>
        /// <param name="maxLength">Maximum length of the returned string.</param>
        /// <param name="stringCase">The string case rule to apply.</param>
        /// <returns>String of random lorin epsum having a length equal to or less than the max length argument along with the specified string case applied.</returns>
        public String GetString(Int32 maxLength, StringCase stringCase) {
            return GetString(maxLength, stringCase, false);
        }

        /// <summary>
        /// Gets a string of random lorin epsum having a length equal to or less than the max length argument along with the specified string case applied.
        /// </summary>
        /// <param name="maxLength">Maximum length of the returned string.</param>
        /// <param name="stringCase">The string case rule to apply.</param>
        /// <param name="removeSpaces">if set to <c>true</c> all spaces will be removed.</param>
        /// <returns>String of random lorin epsum having a length equal to or less than the max length argument along with the specified string case applied.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">stringCase - Value should be defined in the StringCase enum.</exception>
        public String GetString(Int32 maxLength, StringCase stringCase, Boolean removeSpaces) {
            _sb.Clear();
            _sb.Length = 0;
            while(_sb.Length < maxLength) {
                _sb.Append(_words[_random.Next(_WORD_LOWER_BOUND, _wordUpperBound)]);
                if(!removeSpaces) {
                    _sb.Append(STRING_WHITE_SPACE);
                }
            }
            _sb.Length = maxLength;

            switch(stringCase) {
                case StringCase.Lower:
                    return _sb.ToString().ToLower();
                case StringCase.None:
                    return _sb.ToString();
                case StringCase.Upper:
                    return _sb.ToString().ToUpper();
                default:
                    throw new ArgumentOutOfRangeException(nameof(stringCase), "Value should be defined in the StringCase enum.");
            }
        }

    }
}
