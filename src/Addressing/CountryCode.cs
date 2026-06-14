using System;
using System.Collections.Generic;
using System.Linq;

namespace ISOCodex.Addressing
{
    public readonly struct CountryCode : IEquatable<CountryCode>
    {
        public string Code { get; }

        public static CountryCode AD => new CountryCode("AD");
        public static CountryCode AE => new CountryCode("AE");
        public static CountryCode AF => new CountryCode("AF");
        public static CountryCode AG => new CountryCode("AG");
        public static CountryCode AI => new CountryCode("AI");
        public static CountryCode AL => new CountryCode("AL");
        public static CountryCode AM => new CountryCode("AM");
        public static CountryCode AO => new CountryCode("AO");
        public static CountryCode AQ => new CountryCode("AQ");
        public static CountryCode AR => new CountryCode("AR");
        public static CountryCode AS => new CountryCode("AS");
        public static CountryCode AT => new CountryCode("AT");
        public static CountryCode AU => new CountryCode("AU");
        public static CountryCode AW => new CountryCode("AW");
        public static CountryCode AX => new CountryCode("AX");
        public static CountryCode AZ => new CountryCode("AZ");
        public static CountryCode BA => new CountryCode("BA");
        public static CountryCode BB => new CountryCode("BB");
        public static CountryCode BD => new CountryCode("BD");
        public static CountryCode BE => new CountryCode("BE");
        public static CountryCode BF => new CountryCode("BF");
        public static CountryCode BG => new CountryCode("BG");
        public static CountryCode BH => new CountryCode("BH");
        public static CountryCode BI => new CountryCode("BI");
        public static CountryCode BJ => new CountryCode("BJ");
        public static CountryCode BL => new CountryCode("BL");
        public static CountryCode BM => new CountryCode("BM");
        public static CountryCode BN => new CountryCode("BN");
        public static CountryCode BO => new CountryCode("BO");
        public static CountryCode BQ => new CountryCode("BQ");
        public static CountryCode BR => new CountryCode("BR");
        public static CountryCode BS => new CountryCode("BS");
        public static CountryCode BT => new CountryCode("BT");
        public static CountryCode BV => new CountryCode("BV");
        public static CountryCode BW => new CountryCode("BW");
        public static CountryCode BY => new CountryCode("BY");
        public static CountryCode BZ => new CountryCode("BZ");
        public static CountryCode CA => new CountryCode("CA");
        public static CountryCode CC => new CountryCode("CC");
        public static CountryCode CD => new CountryCode("CD");
        public static CountryCode CF => new CountryCode("CF");
        public static CountryCode CG => new CountryCode("CG");
        public static CountryCode CH => new CountryCode("CH");
        public static CountryCode CI => new CountryCode("CI");
        public static CountryCode CK => new CountryCode("CK");
        public static CountryCode CL => new CountryCode("CL");
        public static CountryCode CM => new CountryCode("CM");
        public static CountryCode CN => new CountryCode("CN");
        public static CountryCode CO => new CountryCode("CO");
        public static CountryCode CR => new CountryCode("CR");
        public static CountryCode CU => new CountryCode("CU");
        public static CountryCode CV => new CountryCode("CV");
        public static CountryCode CW => new CountryCode("CW");
        public static CountryCode CX => new CountryCode("CX");
        public static CountryCode CY => new CountryCode("CY");
        public static CountryCode CZ => new CountryCode("CZ");
        public static CountryCode DE => new CountryCode("DE");
        public static CountryCode DJ => new CountryCode("DJ");
        public static CountryCode DK => new CountryCode("DK");
        public static CountryCode DM => new CountryCode("DM");
        public static CountryCode DO => new CountryCode("DO");
        public static CountryCode DZ => new CountryCode("DZ");
        public static CountryCode EC => new CountryCode("EC");
        public static CountryCode EE => new CountryCode("EE");
        public static CountryCode EG => new CountryCode("EG");
        public static CountryCode EH => new CountryCode("EH");
        public static CountryCode ER => new CountryCode("ER");
        public static CountryCode ES => new CountryCode("ES");
        public static CountryCode ET => new CountryCode("ET");
        public static CountryCode FI => new CountryCode("FI");
        public static CountryCode FJ => new CountryCode("FJ");
        public static CountryCode FM => new CountryCode("FM");
        public static CountryCode FO => new CountryCode("FO");
        public static CountryCode FR => new CountryCode("FR");
        public static CountryCode GA => new CountryCode("GA");
        public static CountryCode GB => new CountryCode("GB");
        public static CountryCode GD => new CountryCode("GD");
        public static CountryCode GE => new CountryCode("GE");
        public static CountryCode GF => new CountryCode("GF");
        public static CountryCode GG => new CountryCode("GG");
        public static CountryCode GH => new CountryCode("GH");
        public static CountryCode GI => new CountryCode("GI");
        public static CountryCode GL => new CountryCode("GL");
        public static CountryCode GM => new CountryCode("GM");
        public static CountryCode GN => new CountryCode("GN");
        public static CountryCode GP => new CountryCode("GP");
        public static CountryCode GQ => new CountryCode("GQ");
        public static CountryCode GR => new CountryCode("GR");
        public static CountryCode GT => new CountryCode("GT");
        public static CountryCode GU => new CountryCode("GU");
        public static CountryCode GW => new CountryCode("GW");
        public static CountryCode GY => new CountryCode("GY");
        public static CountryCode HK => new CountryCode("HK");
        public static CountryCode HM => new CountryCode("HM");
        public static CountryCode HN => new CountryCode("HN");
        public static CountryCode HR => new CountryCode("HR");
        public static CountryCode HT => new CountryCode("HT");
        public static CountryCode HU => new CountryCode("HU");
        public static CountryCode ID => new CountryCode("ID");
        public static CountryCode IE => new CountryCode("IE");
        public static CountryCode IL => new CountryCode("IL");
        public static CountryCode IM => new CountryCode("IM");
        public static CountryCode IN => new CountryCode("IN");
        public static CountryCode IO => new CountryCode("IO");
        public static CountryCode IQ => new CountryCode("IQ");
        public static CountryCode IR => new CountryCode("IR");
        public static CountryCode IS => new CountryCode("IS");
        public static CountryCode IT => new CountryCode("IT");
        public static CountryCode JE => new CountryCode("JE");
        public static CountryCode JM => new CountryCode("JM");
        public static CountryCode JO => new CountryCode("JO");
        public static CountryCode JP => new CountryCode("JP");
        public static CountryCode KE => new CountryCode("KE");
        public static CountryCode KG => new CountryCode("KG");
        public static CountryCode KH => new CountryCode("KH");
        public static CountryCode KI => new CountryCode("KI");
        public static CountryCode KM => new CountryCode("KM");
        public static CountryCode KN => new CountryCode("KN");
        public static CountryCode KP => new CountryCode("KP");
        public static CountryCode KR => new CountryCode("KR");
        public static CountryCode KW => new CountryCode("KW");
        public static CountryCode KY => new CountryCode("KY");
        public static CountryCode KZ => new CountryCode("KZ");
        public static CountryCode LA => new CountryCode("LA");
        public static CountryCode LB => new CountryCode("LB");
        public static CountryCode LC => new CountryCode("LC");
        public static CountryCode LI => new CountryCode("LI");
        public static CountryCode LK => new CountryCode("LK");
        public static CountryCode LR => new CountryCode("LR");
        public static CountryCode LS => new CountryCode("LS");
        public static CountryCode LT => new CountryCode("LT");
        public static CountryCode LU => new CountryCode("LU");
        public static CountryCode LV => new CountryCode("LV");
        public static CountryCode LY => new CountryCode("LY");
        public static CountryCode MA => new CountryCode("MA");
        public static CountryCode MC => new CountryCode("MC");
        public static CountryCode MD => new CountryCode("MD");
        public static CountryCode ME => new CountryCode("ME");
        public static CountryCode MF => new CountryCode("MF");
        public static CountryCode MG => new CountryCode("MG");
        public static CountryCode MH => new CountryCode("MH");
        public static CountryCode MK => new CountryCode("MK");
        public static CountryCode ML => new CountryCode("ML");
        public static CountryCode MM => new CountryCode("MM");
        public static CountryCode MN => new CountryCode("MN");
        public static CountryCode MO => new CountryCode("MO");
        public static CountryCode MP => new CountryCode("MP");
        public static CountryCode MQ => new CountryCode("MQ");
        public static CountryCode MR => new CountryCode("MR");
        public static CountryCode MS => new CountryCode("MS");
        public static CountryCode MT => new CountryCode("MT");
        public static CountryCode MU => new CountryCode("MU");
        public static CountryCode MV => new CountryCode("MV");
        public static CountryCode MW => new CountryCode("MW");
        public static CountryCode MX => new CountryCode("MX");
        public static CountryCode MY => new CountryCode("MY");
        public static CountryCode MZ => new CountryCode("MZ");
        public static CountryCode NA => new CountryCode("NA");
        public static CountryCode NC => new CountryCode("NC");
        public static CountryCode NE => new CountryCode("NE");
        public static CountryCode NF => new CountryCode("NF");
        public static CountryCode NG => new CountryCode("NG");
        public static CountryCode NI => new CountryCode("NI");
        public static CountryCode NL => new CountryCode("NL");
        public static CountryCode NO => new CountryCode("NO");
        public static CountryCode NP => new CountryCode("NP");
        public static CountryCode NR => new CountryCode("NR");
        public static CountryCode NU => new CountryCode("NU");
        public static CountryCode NZ => new CountryCode("NZ");
        public static CountryCode OM => new CountryCode("OM");
        public static CountryCode PA => new CountryCode("PA");
        public static CountryCode PE => new CountryCode("PE");
        public static CountryCode PF => new CountryCode("PF");
        public static CountryCode PG => new CountryCode("PG");
        public static CountryCode PH => new CountryCode("PH");
        public static CountryCode PK => new CountryCode("PK");
        public static CountryCode PL => new CountryCode("PL");
        public static CountryCode PM => new CountryCode("PM");
        public static CountryCode PN => new CountryCode("PN");
        public static CountryCode PR => new CountryCode("PR");
        public static CountryCode PT => new CountryCode("PT");
        public static CountryCode PW => new CountryCode("PW");
        public static CountryCode PY => new CountryCode("PY");
        public static CountryCode QA => new CountryCode("QA");
        public static CountryCode RE => new CountryCode("RE");
        public static CountryCode RO => new CountryCode("RO");
        public static CountryCode RS => new CountryCode("RS");
        public static CountryCode RU => new CountryCode("RU");
        public static CountryCode RW => new CountryCode("RW");
        public static CountryCode SA => new CountryCode("SA");
        public static CountryCode SB => new CountryCode("SB");
        public static CountryCode SC => new CountryCode("SC");
        public static CountryCode SD => new CountryCode("SD");
        public static CountryCode SE => new CountryCode("SE");
        public static CountryCode SG => new CountryCode("SG");
        public static CountryCode SH => new CountryCode("SH");
        public static CountryCode SI => new CountryCode("SI");
        public static CountryCode SJ => new CountryCode("SJ");
        public static CountryCode SK => new CountryCode("SK");
        public static CountryCode SL => new CountryCode("SL");
        public static CountryCode SM => new CountryCode("SM");
        public static CountryCode SN => new CountryCode("SN");
        public static CountryCode SO => new CountryCode("SO");
        public static CountryCode SR => new CountryCode("SR");
        public static CountryCode SS => new CountryCode("SS");
        public static CountryCode ST => new CountryCode("ST");
        public static CountryCode SV => new CountryCode("SV");
        public static CountryCode SX => new CountryCode("SX");
        public static CountryCode SY => new CountryCode("SY");
        public static CountryCode SZ => new CountryCode("SZ");
        public static CountryCode TC => new CountryCode("TC");
        public static CountryCode TD => new CountryCode("TD");
        public static CountryCode TF => new CountryCode("TF");
        public static CountryCode TG => new CountryCode("TG");
        public static CountryCode TH => new CountryCode("TH");
        public static CountryCode TJ => new CountryCode("TJ");
        public static CountryCode TK => new CountryCode("TK");
        public static CountryCode TL => new CountryCode("TL");
        public static CountryCode TM => new CountryCode("TM");
        public static CountryCode TN => new CountryCode("TN");
        public static CountryCode TO => new CountryCode("TO");
        public static CountryCode TR => new CountryCode("TR");
        public static CountryCode TT => new CountryCode("TT");
        public static CountryCode TV => new CountryCode("TV");
        public static CountryCode TZ => new CountryCode("TZ");
        public static CountryCode UA => new CountryCode("UA");
        public static CountryCode UG => new CountryCode("UG");
        public static CountryCode UM => new CountryCode("UM");
        public static CountryCode US => new CountryCode("US");
        public static CountryCode UY => new CountryCode("UY");
        public static CountryCode UZ => new CountryCode("UZ");
        public static CountryCode VA => new CountryCode("VA");
        public static CountryCode VC => new CountryCode("VC");
        public static CountryCode VE => new CountryCode("VE");
        public static CountryCode VG => new CountryCode("VG");
        public static CountryCode VI => new CountryCode("VI");
        public static CountryCode VN => new CountryCode("VN");
        public static CountryCode VU => new CountryCode("VU");
        public static CountryCode WF => new CountryCode("WF");
        public static CountryCode WS => new CountryCode("WS");
        public static CountryCode YE => new CountryCode("YE");
        public static CountryCode YT => new CountryCode("YT");
        public static CountryCode ZA => new CountryCode("ZA");
        public static CountryCode ZM => new CountryCode("ZM");
        public static CountryCode ZW => new CountryCode("ZW");

        private CountryCode(string code)
        {
            Code = code.ToUpperInvariant();
        }

        public static CountryCode Parse(string input)
        {
            if (TryParse(input, out var countryCode))
            {
                return countryCode;
            }

            throw new ArgumentException(
                $"Invalid country code: '{input}'. Must be an ISO 3166-1 alpha-2 code.");
        }

        public static bool TryParse(string input, out CountryCode countryCode)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                var code = input.ToUpperInvariant();

                if (_validCodes.Contains(code))
                {
                    countryCode = new CountryCode(code);
                    return true;
                }
            }

            countryCode = default;
            return false;
        }

        public static bool IsValid(string input) => TryParse(input, out _);

        public static IEnumerable<CountryCode> All => _validCodes.Select(code => new CountryCode(code));

        public override string ToString() => Code;

        public override bool Equals(object? obj) => obj is CountryCode other && Equals(other);

        public bool Equals(CountryCode other) => Code == other.Code;

        public override int GetHashCode() => Code is null ? 0 : Code.GetHashCode();

        public static bool operator ==(CountryCode left, CountryCode right) => left.Equals(right);

        public static bool operator !=(CountryCode left, CountryCode right) => !(left == right);

        private static readonly HashSet<string> _validCodes = new HashSet<string>
        {
            "AD","AE","AF","AG","AI","AL","AM","AO","AQ","AR","AS","AT","AU","AW","AX","AZ",
            "BA","BB","BD","BE","BF","BG","BH","BI","BJ","BL","BM","BN","BO","BQ","BR","BS",
            "BT","BV","BW","BY","BZ","CA","CC","CD","CF","CG","CH","CI","CK","CL","CM","CN",
            "CO","CR","CU","CV","CW","CX","CY","CZ","DE","DJ","DK","DM","DO","DZ","EC","EE",
            "EG","EH","ER","ES","ET","FI","FJ","FM","FO","FR","GA","GB","GD","GE","GF","GG",
            "GH","GI","GL","GM","GN","GP","GQ","GR","GT","GU","GW","GY","HK","HM","HN","HR",
            "HT","HU","ID","IE","IL","IM","IN","IO","IQ","IR","IS","IT","JE","JM","JO","JP",
            "KE","KG","KH","KI","KM","KN","KP","KR","KW","KY","KZ","LA","LB","LC","LI","LK",
            "LR","LS","LT","LU","LV","LY","MA","MC","MD","ME","MF","MG","MH","MK","ML","MM",
            "MN","MO","MP","MQ","MR","MS","MT","MU","MV","MW","MX","MY","MZ","NA","NC","NE",
            "NF","NG","NI","NL","NO","NP","NR","NU","NZ","OM","PA","PE","PF","PG","PH","PK",
            "PL","PM","PN","PR","PT","PW","PY","QA","RE","RO","RS","RU","RW","SA","SB","SC",
            "SD","SE","SG","SH","SI","SJ","SK","SL","SM","SN","SO","SR","SS","ST","SV","SX",
            "SY","SZ","TC","TD","TF","TG","TH","TJ","TK","TL","TM","TN","TO","TR","TT","TV",
            "TZ","UA","UG","UM","US","UY","UZ","VA","VC","VE","VG","VI","VN","VU","WF","WS",
            "YE","YT","ZA","ZM","ZW"
        };
    }
}
