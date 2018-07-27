using System;
using System.Text.RegularExpressions;

namespace GameFramework
{
	public class CheckingString  
	{
        /// <summary>
        /// 检查是否包含汉字
        /// </summary>
        public static bool CheckIfHasChineseCharacters(string _string)
        {
            return CheckChineseCharacters(_string);
        }
        /// <summary>
        /// 检查是否全是汉字
        /// </summary>
        public static bool CheckIfAllChineseCharacters(string _string)
        {
            return CheckChineseCharacters(_string, true);
        }
        private static bool CheckChineseCharacters(string _string, bool _all = false)
        {
            char[] c = _string.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                //汉字UNICODE编码范围
                if (c[i] >= 0x4e00 && c[i] <= 0x9fbb)
                {
                    if (!_all) return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 检查中国身份证格式合法性
        /// </summary>
        public static bool CheckIfIDNumber(string _string)
        {
            if (string.IsNullOrEmpty(_string)) return false;
            string _rex1 = string.Empty;
            string _rex2 = string.Empty;
            switch (_string.Length)
            {
                case 15:
                    _rex1 = "\\d{8}[0-1]\\d[0-3]\\d{3}x";
                    _rex2 = "\\d{8}[0-1]\\d[0-3]\\d{4}";
                    break;
                case 18:
                    _rex1 = "\\d{6}[1-2][0,1,9]\\d{2}[0-1]\\d[0-3]\\d{4}x";
                    _rex2 = "\\d{6}[1-2][0,1,9]\\d{2}[0-1]\\d[0-3]\\d{5}";
                    break;
                default:
                    return false;
            }
            return Regex.IsMatch(_string, _rex1) || Regex.IsMatch(_string, _rex2);
        }
        /// <summary>
        /// 检查手机号
        /// </summary>
        public static bool CheckPhoneNumber (string _string)
        {
            return Regex.IsMatch(_string, "1\\d{10}");
        }
        /// <summary>
        /// 检查是否合法名称（只包含英文、数字、汉字、韩文、日文、下划线）
        /// </summary>
        public static bool CheckIfName(string _string)
        {
            char[] _cs = _string.ToCharArray();
            for (int i = 0; i < _cs.Length; i++)
            {
                if (_cs[i] == 95) continue;
                if (_cs[i] >= 48 && _cs[i] <= 57) continue;
                if (_cs[i] >= 65 && _cs[i] <= 90) continue;
                if (_cs[i] >= 97 && _cs[i] <= 122) continue;
                if (_cs[i] >= 128 && _cs[i] <= 237) continue;

                return false;
            }

            return true;
        }
        /// <summary>
        /// 检查是否合法密码（只包含英文、数字、符号）
        /// </summary>
        public static bool CheckIfPassword(string _string)
        {
            char[] _cs = _string.ToCharArray();
            for (int i = 0; i < _cs.Length; i++)
            {
                if (_cs[i] >= 33 && _cs[i] <= 126) continue;

                return false;
            }

            return true;
        }
	}
}