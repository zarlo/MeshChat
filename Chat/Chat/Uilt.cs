using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Common
{
    public class Uilt
    {

        public static string Keygen(int l)
        {
            string temp = "";
            for (int i = 0; i<=l - 1; i++)
            {

                temp += Randomgen(3) + "-";

            }
            temp += Randomgen(3);

            return temp;
        }

        public static string Randomgen(int l, string c)
        {
            string s = c;
            Random r = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= l; i++)
            {
                int idx = r.Next(0, s.Length);
                sb.Append(s.Substring(idx, 1));
            }
            return sb.ToString();
        }

        public static string Randomgen(int l)
        {
            string s = "abcdefghijklnmopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_=!@#$%^&'*☺☻♥♦♣♠•◘○◙♀♂♪♫☼►◄↕‼¶§▬↨↑↓→←∟↔▲▼";        
            return Randomgen(l,s);
        }

        public string MarkDown(string MD)
        {

            return MD;
        }

    }
}
