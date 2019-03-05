using System;

//--------------------------------------------------------------------------------------------------------------//
//-- By Thierry Duriez                                                                                        --//
//-- Base class of all Data Transfer Object with default values                                               --//
//--------------------------------------------------------------------------------------------------------------//
namespace DatasAccesLayer
{
    public class CommonBase
    {
        // Standard null values of configuration

        public static int? Int_NullValue = null;
        public static bool? bool_NullValue = null;
        public static DateTime? DateTime_NullValue = null;
        public static decimal? Decimal_NullValue = null;

        public static DateTime DateTime_BaseValue = DateTime.Parse("01/01/1753 00:00:00");  //SQL Server compatible
        public static Guid Guid_EmptyValue = Guid.Empty;

        public static char Char_MinValue = char.MinValue;
        public static string String_EmptyValue = string.Empty;

        public static byte byte_MinValue = byte.MinValue;
        public static short Short_BaseValue = -1;
        public static int Int_BaseValue = -1;
        public static Int64 Int64_BaseValue = -1;
        public static float Float_BaseValue = -1;
        public static decimal Decimal_BaseValue = -1M;

        public static bool bool_FalseValue = false;
        public static bool bool_TrueValue = true;
    }
}
