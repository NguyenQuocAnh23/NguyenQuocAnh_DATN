using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace WebBanHangOnline.Models.Commons
{
    public class Filter
    {
        public static string ConvertToUnSign2(string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }
            sb = sb.Replace('Đ', 'D');
            sb = sb.Replace('đ', 'd');

            string result = sb.ToString().Normalize(NormalizationForm.FormD);
            result = Regex.Replace(result, @"\p{M}", ""); // Xóa các dấu thanh và dấu mũ

            // Chuyển các từ thành dạng thường và cách nhau bằng dấu gạch ngang
            string[] words = result.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            result = string.Join("-", words).ToLower();

            return result;
        }
    }
}