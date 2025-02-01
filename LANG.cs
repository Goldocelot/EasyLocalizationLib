namespace EasyLocalizationLib
{
    internal class LANG
    {
        private string _language;

        private LANG(string language)
        {
            _language = language;
        }

        public static readonly LANG CH1 = new LANG("CH1");
        public static readonly LANG CH2 = new LANG("CH2");
        public static readonly LANG CZ = new LANG("CZ");
        public static readonly LANG DE = new LANG("DE");
        public static readonly LANG EN = new LANG("EN");
        public static readonly LANG ES = new LANG("ES");
        public static readonly LANG FR = new LANG("FR");
        public static readonly LANG IT = new LANG("IT");
        public static readonly LANG JP = new LANG("JP");
        public static readonly LANG KR = new LANG("KR");
        public static readonly LANG PL = new LANG("PL");
        public static readonly LANG PT = new LANG("PT");
        public static readonly LANG RU = new LANG("RU");
        public static readonly LANG UK = new LANG("UK");

        public override string ToString()
        {
            return _language;
        }
    }
}
