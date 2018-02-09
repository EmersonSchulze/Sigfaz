using System;
using Sigfaz.Infra.ComponentModel.DataAnnotations;

namespace Sigfaz.Infra.ComponentModel.Extensions
{
    public static class DateTimeExtension
    {
        private const string DATA_TIME_FORMAT = "ddMMyyyyHHmmss";
        private const string DATE_FORMAT = "ddMMyyyy";

        public static string GetFormat(DateTimeAttribute dateTimeAttribute)
        {
            return dateTimeAttribute == null ? DATE_FORMAT : DATA_TIME_FORMAT;
        }

        public static string ToFormattedString(this DateTime dateTime, DateTimeAttribute dateTimeAttribute)
        {
            return dateTime.ToString(GetFormat(dateTimeAttribute));
        }

        public static string ToFormattedString(this DateTime? dateTime, DateTimeAttribute dateTimeAttribute)
        {
            return !dateTime.HasValue ? String.Empty : ToFormattedString(dateTime.Value, dateTimeAttribute);
        }

        public static DateTime GetLastDayOfMonth(this DateTime dateTime)
        {
            var firstDayOfTheMonth = new DateTime(dateTime.Year, dateTime.Month, 1);
            return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
        }

        public static DateTime GetFirstDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public static string ToJsonString(this DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public static bool Between(this DateTime dateTime, DateTime minorDate, DateTime majorDate)
        {
            return (dateTime >= minorDate) && (dateTime <= majorDate);
        }

        /// <summary>
        /// Calculates the age in years of the current System.DateTime object today.
        /// </summary>
        /// <param name="birthDate">The date of birth</param>
        /// <returns>Age in years today. 0 is returned for a future date of birth.</returns>
        public static int Age(this DateTime birthDate)
        {
            return Age(birthDate, DateTime.Today);
        }
        /// <summary>
        /// Calculates the age in years of the current System.DateTime object on a later date.
        /// </summary>
        /// <param name="birthDate">The date of birth</param>
        /// <param name="laterDate">The date on which to calculate the age.</param>
        /// <returns>Age in years on a later day. 0 is returned as minimum.</returns>
        public static int Age(this DateTime birthDate, DateTime laterDate)
        {
            int age;
            age = laterDate.Year - birthDate.Year;
            if (age > 0)
            {
                age -= Convert.ToInt32(laterDate.Date < birthDate.Date.AddYears(age));
            }
            else
            {
                age = 0;
            }
            return age;
        }       

        public static string Duration(this DateTime dateTime, DateTime? referenceDateTime = null)
        {
            referenceDateTime = referenceDateTime ?? DateTime.Now;
            var intervalo = referenceDateTime.Value.Subtract(dateTime);
            return String.Format("{0}h {1}min {2}seg", intervalo.Hours + (intervalo.Days * 24), intervalo.Minutes, intervalo.Seconds);
        }

        public static string AgeText(this DateTime dateBegin, DateTime dateEnd)
        {
            int day1, day2, month1, month2, year1, year2, years, months, days;

            year1 = dateBegin.Year;
            month1 = dateBegin.Month;
            day1 = dateBegin.Day;

            year2 = dateEnd.Year;
            month2 = dateEnd.Month;
            day2 = dateEnd.Day;

            years = year2 - year1;
            months = 0;
            days = 0;

            if (month2 < month1)
            {
                months = months + 12;
                years = years - 1;
            }
            months = months + (month2 - month1);
            if (day2 < day1)
            {
                days = days + dateBegin.GetLastDayOfMonth().Day;
                if (months == 0)
                {
                    years = years - 1;
                    months = 11;
                }
                else
                {
                    months = months - 1;
                }
            }
            days = days + (day2 - day1);

            return String.Format("{0} ano(s), {1} mes(es) e {2} dia(s)", years, months, days);
        }

    }
}
