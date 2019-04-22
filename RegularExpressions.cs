
namespace NT.Global.Strings
{
    public class RegularExpressions
    {
        public const string Email = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        //@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
        public const string Phone = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{9})[-. ]?([0-9]*)$";
        public const string InternationalPhone = @"^\(?([+][0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]*)$";
        public const string DateFormat = @"(\d{2}[ /\\.-]\d{2}[ /\\.-]\d{4})|(\d{4}[ /\\.-]\d{2}[ /\\.-]\d{2})";
        public const string Number = @"\d*";
        public const string String = @"^[A-z-\u0600-\u065F\u066A-\u06EF\u06FA-\u06FF-\s]+$";
        public const string Time = @"^[0-9]{1,2}[:][0-9]{1,2}([:][0-9]{1,2})?$";

        public const string arabicStrings = @"^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FF]*$";
        public const string arabicStringsAndNumbers = @"^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FF-\d]*$";
        public const string URL = @"(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?";
    }
}
