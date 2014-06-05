using System.Globalization;
using System.Text.RegularExpressions;

namespace Depascalization
{
  public static class MatchExtensions
  {
    public static string _0(this Match match)
    {
      return match.Value[0].ToString(CultureInfo.InvariantCulture);
    }

    public static string _1(this Match match)
    {
      return match.Value[1].ToString(CultureInfo.InvariantCulture);
    }

    public static string _2(this Match match)
    {
      return match.Value[2].ToString(CultureInfo.InvariantCulture);
    }
  }
}