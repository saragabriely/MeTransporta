using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Teste03.ClassesComuns
{
    public class ValidaCampos
    {
        public static bool IsEmail(string strEmail)
        {
            //string strModelo = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            string strModelo = string.Format("{0}{1}",
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))",
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");

            if (Regex.IsMatch(strEmail, strModelo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CaracterEspecial(string strEmail)
        {
            if(Regex.IsMatch(strEmail, (@"[^a-zA-Z0-9]")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
