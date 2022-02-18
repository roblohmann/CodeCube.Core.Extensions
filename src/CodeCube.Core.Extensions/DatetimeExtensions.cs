using System;
using System.Globalization;
using CodeCube.Core.Extensions.Resources;

namespace CodeCube.Core.Extensions
{
    ///<summary>
    /// Class with extension methods for datetime
    ///</summary>
    public static class DateTimeExtensions
    {
        ///<summary>
        ///</summary>
        ///<param name="dateTime"></param>
        ///<param name="allowFutureDate"></param>
        ///<returns></returns>
        public static string ToPrettyDate(this DateTime dateTime, bool allowFutureDate = false)
        {
            //get the elapsed time since the date to convert
            var timeSpanElapsed = DateTime.Now.Subtract(dateTime);
            var inFuture = false;

            //time difference in seconds
            var differenceInSeconds = Convert.ToInt32(timeSpanElapsed.TotalSeconds);

            //time difference in hours.
            var differenceInHours = timeSpanElapsed.TotalHours;

            //get the difference in days
            var differenceInDays = Math.Round(timeSpanElapsed.TotalDays);

            if (differenceInSeconds < 0)
            {
                inFuture = true;

                if (!allowFutureDate) return Translations.JustNow;
            }

            if (differenceInDays < 0) return string.Empty;

            //difference smaller then one day
            if (Convert.ToInt32(differenceInDays) == 0)
            {
                // smaller then one minute
                if (differenceInSeconds < 60) return Translations.JustNow;
                if (differenceInSeconds < 120)
                {
                    //is the value in the future?
                    return inFuture ? Translations.InAnMinute : Translations.AMinuteAgo;
                }
                if (differenceInSeconds < 3600)
                {
                    //if the value in the future?
                    return string.Format(inFuture ? Translations.InXMinutes : Translations.xMinutesAgo, Math.Floor((double)differenceInSeconds / 60));
                }
                if (differenceInSeconds < 7200)
                {
                    //is the value in the future
                    return inFuture ? Translations.InAnHour : Translations.OneHourAgo;
                }
                if (differenceInSeconds < 86400)
                {
                    //if the value in the future?
                    return string.Format(inFuture ? Translations.InXHours : Translations.xHoursAgo, Math.Floor((double)differenceInSeconds / 3600));
                }
            }
            else if (differenceInHours < 24)
            {
                return string.Format(inFuture ? Translations.InXHours : Translations.xHoursAgo, Math.Floor((double)differenceInSeconds / 3600));
            }
            else if (Convert.ToInt32(differenceInDays) == 1) //is the difference one day
            {
                //is the value in the future?
                return inFuture ? Translations.Tomorrow : Translations.Yesterday;
            }
            else if (differenceInDays < 7)// is the difference smaller then a week
            {
                //is the value in the future?
                return string.Format(inFuture ? Translations.InXDays : Translations.xDaysAgo, differenceInDays);
            }
            else if (Convert.ToInt32(differenceInDays) == 7) //is the difference a week?
            {
                //is the value in the future
                return inFuture ? Translations.InAWeek : Translations.OneWeekAgo;
            }
            else if (differenceInDays < (7 * 6)) //is the difference between a week and a month?
            {
                //is the value in the future?
                return string.Format(inFuture ? Translations.InXWeeks : Translations.xWeeksAgo, Math.Ceiling(differenceInDays / 7));
            }
            else if (differenceInDays < 365) //is the difference between a month an a year?
            {
                //is the value in the future?
                return string.Format(inFuture ? Translations.InXMonths : Translations.xMonthsAgo, Math.Ceiling(differenceInDays / (365 / 12)));
            }
            else // the difference is bigger then a year
            {
                return string.Format(Translations.xYearsAgo, Math.Round(differenceInDays / 365));
            }

            return dateTime.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns an string as readable (dutch) datetime format
        /// </summary>
        /// <example>22 december 2010</example>
        /// <param name="date">The date to convert</param>
        /// <returns>Readable datetime as string</returns>
        public static string AsReadableDate(this DateTime date)
        {
            return $"{date:d MMMM yyyy}";
        }

        /// <summary>
        /// Returns an string as readable (dutch) datetime format
        /// </summary>
        /// <example>22 december 2010 15:49</example>
        /// <param name="date">The date to convert</param>
        /// <returns>Readable datetime as string</returns>
        public static string AsReadableDateTime(this DateTime date)
        {
            return $"{date:d MMMM yyyy HH:mm}";
        }

        /// <summary>
        /// Returns the provided DateTime as UNIX timestamp
        /// </summary>
        /// <param name="value">date to convert</param>
        public static double ConvertToTimestamp(this DateTime value)
        {
            //create Timespan by subtracting the value provided from
            //the Unix Epoch
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());

            //return the total seconds (which is a UNIX timestamp)
            return span.TotalSeconds;
        }

        /// <summary>
        /// Returns the Date from the provided DateTime in the ISO8601 format.
        /// </summary>
        /// <param name="dateTime">The DateTime to convert.</param>
        /// <returns>The Date in ISO8601 format.</returns>
        public static string ToISODate(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns the DateTime in the ISO8601 format.
        /// </summary>
        /// <param name="dateTime">The DateTime to convert.</param>
        /// <returns>The date and time in ISO8601 format.</returns>
        public static string ToISODateTime(this DateTime dateTime)
        {
            return dateTime.ToString("s", CultureInfo.InvariantCulture);
        }
    }
}
