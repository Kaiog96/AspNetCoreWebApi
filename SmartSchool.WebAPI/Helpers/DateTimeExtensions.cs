using System;

namespace SmartSchool.WebAPI.Helpers
{
  public static class DateTimeExtensions
  {
    public static int GetCurrentAge(this DateTime dateTime)
    {
      var currenteDate = DateTime.UtcNow;
      int age = currenteDate.Year - dateTime.Year;

      if (currenteDate < dateTime.AddYears(age))
        age--;

      return age;
    }
  }
}