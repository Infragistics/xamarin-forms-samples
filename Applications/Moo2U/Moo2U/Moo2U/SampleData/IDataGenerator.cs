namespace Moo2U.SampleData {
    using System;

    /// <summary>
    /// Interface IDataGenerator
    /// </summary>
    public interface IDataGenerator {

        /// <summary>
        /// Gets the boolean.
        /// </summary>
        /// <returns>Boolean that is randomly set to <c>true</c> or <c>false</c>.</returns>
        Boolean GetBoolean();

        /// <summary>
        /// Gets a randomly selected company name.
        /// </summary>
        /// <returns>String with a randomly selected company name.</returns>
        String GetCompanyName();

        /// <summary>
        /// Gets the date that is between minValue and maxValue.
        /// </summary>
        /// <param name="minValue">The min value.</param>
        /// <param name="maxValue">The max value.</param>
        /// <returns>DateTime that is between minValue and maxValue.</returns>
        DateTime GetDate(DateTime minValue, DateTime maxValue);

        /// <summary>
        /// Gets the decimal that is between minValue and maxValue.
        /// </summary>
        /// <param name="minValue">The min value.</param>
        /// <param name="maxValue">The max value.</param>
        /// <returns>Decimal that is between minValue and maxValue.</returns>
        Decimal GetDecimal(Int32 minValue, Int32 maxValue);

        /// <summary>
        /// Gets the Double that is between minValue and maxValue.
        /// </summary>
        /// <param name="minValue">The min value.</param>
        /// <param name="maxValue">The max value.</param>
        /// <returns>Double that is between minValue and maxValue.</returns>
        Double GetDouble(Int32 minValue, Int32 maxValue);

        /// <summary>
        /// Gets the randomly created email address.
        /// </summary>
        /// <returns>String with a randomly created email address.</returns>
        String GetEmail();

        /// <summary>
        /// Gets a randomly selected first name.
        /// </summary>
        /// <returns>String with a randomly selected first name.</returns>
        String GetFirstName();

        /// <summary>
        /// Gets a randomly selected full name
        /// </summary>
        /// <returns>String with a randomly selected full name.</returns>
        String GetFullName();

        /// <summary>
        /// Gets an integer that is between minValue and maxValue.
        /// </summary>
        /// <param name="minValue">The min value.</param>
        /// <param name="maxValue">The max value.</param>
        /// <returns>Integer that is between minValue and maxValue.</returns>
        Int32 GetInteger(Int32 minValue, Int32 maxValue);

        /// <summary>
        /// Gets a randomly selected last name.
        /// </summary>
        /// <returns>String with a randomly selected last name.</returns>
        String GetLastName();

        /// <summary>
        /// Gets a randomly generated phone number.
        /// </summary>
        /// <returns>String with a randomly generated phone number.</returns>
        String GetPhoneNumber();

        /// <summary>
        /// Gets the next sequential integer. Each time this property is accessed the sequential integer is incremented.
        /// </summary>
        /// <returns>Integer representing the next sequential integer.</returns>
        Int32 GetSequentialInteger();

        /// <summary>
        /// Gets a randomly generated social security number.
        /// </summary>
        /// <returns>String with a randomly generated social security number.</returns>
        String GetSsn();

        /// <summary>
        /// Gets a randomly selected state abbreviation.
        /// </summary>
        /// <returns>String with a randomly selected state abbreviation.</returns>
        String GetStateAbbreviation();

        /// <summary>
        /// Gets the street.
        /// </summary>
        /// <returns>String.</returns>
        String GetStreet();

        /// <summary>
        /// Gets a String of random lorin epsum having a length equal to or less than the max length argument.
        /// </summary>
        /// <param name="maxLength">Maximum length of the returned String.</param>
        /// <returns>String of random lorin epsum having a length equal to or less than the max length argument.</returns>
        String GetString(Int32 maxLength);

        /// <summary>
        /// Gets a String of random lorin epsum having a length equal to or less than the max length argument along with the specified String case applied.
        /// </summary>
        /// <param name="maxLength">Maximum length of the returned String.</param>
        /// <param name="stringCase">The String case rule to apply.</param>
        /// <returns>String of random lorin epsum having a length equal to or less than the max length argument along with the specified String case applied.</returns>
        String GetString(Int32 maxLength, StringCase stringCase);

        /// <summary>
        /// Gets a string of random lorin epsum having a length equal to or less than the max length argument along with the specified string case applied.
        /// </summary>
        /// <param name="maxLength">Maximum length of the returned string.</param>
        /// <param name="stringCase">The string case rule to apply.</param>
        /// <param name="removeSpaces">if set to <c>true</c> all spaces will be removed.</param>
        /// <returns>String of random lorin epsum having a length equal to or less than the max length argument along with the specified string case applied.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">stringCase - Value should be defined in the StringCase enum.</exception>
        String GetString(Int32 maxLength, StringCase stringCase, Boolean removeSpaces);

        /// <summary>
        /// Gets a randomly selected Url.
        /// </summary>
        /// <returns>String with a randomly selected Url.</returns>
        String GetUrl();

        /// <summary>
        /// Gets a randomly generated zip code.
        /// </summary>
        /// <returns>String with a randomly generated zip code.</returns>
        String GetZipCode();

        /// <summary>
        /// Seeds the sequential integer.
        /// </summary>
        /// <param name="seedValue">The seed value.</param>
        /// <param name="incrementValue">The increment value.</param>
        void SeedSequentialInteger(Int32 seedValue, Int32 incrementValue);

    }
}
