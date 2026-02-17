using PatientApi.Data.Entities;
using PatientApi.Data.Enums;
using System.Linq.Expressions;

namespace PatientApi.Data.Extentions
{
    public static class QueryableExtentions
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            if (condition)
            {
                query = query.Where(predicate); 
            }

            return query;
        }

        /// <summary>
        /// Filter extention for searching patients by date of birth
        /// </summary>
        /// <param name="query">search query</param>
        /// <param name="dateParams">input string</param>
        /// <returns></returns>
        public static IQueryable<PatientEntity> FilterByBirthDate(this IQueryable<PatientEntity> query, string dateParams)
        {
            var (prefix, dateStr) = ParseDateParams(dateParams);

            var (start, end) = GetStartEndDate(dateStr);

            if (start == null || end == null)
                return query;

            return prefix switch
            {
                Prefix.Equal or Prefix.Approximately => query.Where(d => d.BirthDate >= start && d.BirthDate < end),
                Prefix.NotEqual => query.Where(d => d.BirthDate < start || d.BirthDate >= end),
                Prefix.GreaterThan  => query.Where(d => d.BirthDate >= start),
                Prefix.LessThan => query.Where(d => d.BirthDate < end),
                Prefix.GreaterOrEqual => query.Where(d => d.BirthDate >= start),
                Prefix.LessOrEqual => query.Where(d => d.BirthDate < end),
                Prefix.StartsAfter => query.Where(d => d.BirthDate >= end),
                Prefix.EndsBefore => query.Where(d => d.BirthDate < start),
                _ => query
            };
        }

        /// <summary>
        /// Parse prefix and date string from input string.
        /// If can't parse date, returns (Prefix.None, string.Empty)
        /// If the prefix is ​​not specified and the date is valid, set the prefix value to Equal
        /// </summary>
        /// <param name="input">input string</param>
        /// <returns></returns>
        private static (Prefix prefix, string dateStr) ParseDateParams(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || input.Length < 4)
                return (Prefix.None, string.Empty);

            string prefixStr = input.Substring(0, 2);

            var prefix = prefixStr switch
            {
                "eq" => Prefix.Equal,
                "ne" => Prefix.NotEqual,
                "gt" => Prefix.GreaterThan,
                "lt" => Prefix.LessThan,
                "ge" => Prefix.GreaterOrEqual,
                "le" => Prefix.LessOrEqual,
                "sa" => Prefix.StartsAfter,
                "eb" => Prefix.EndsBefore,
                "ap" => Prefix.Approximately,
                _ => Prefix.None
            };

            var dateStr = prefix != Prefix.None ? input.Substring(2) : input;
            prefix = prefix == Prefix.None ? Prefix.Equal : prefix;

            return (prefix, dateStr);
        }

        /// <summary>
        /// Gets start and end date from input date string depends on string format
        /// </summary>
        /// <param name="dateStr">input date string</param>
        /// <returns></returns>
        private static (DateTime? start, DateTime? end) GetStartEndDate(string dateStr)
        {
            if (!DateTime.TryParse(dateStr, out DateTime start))
                return (null, null);

            DateTime end;
            int length = dateStr.Trim().Length;

            if (length <= 4) // yyyy
            {
                end = start.AddYears(1);
            }    
            else if (length <= 7) // yyyy-mm
            {
                end = start.AddMonths(1);
            }
            else if (length <= 10) // yyyy-mm-dd
            {
                end = start.AddDays(1);
            }
            else
            {
                end = start.AddHours(1);
            }

            return (start, end);
        }
    }
}
