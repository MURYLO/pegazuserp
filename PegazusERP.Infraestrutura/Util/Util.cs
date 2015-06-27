using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Reflection;
using System.ComponentModel;
using System.Linq.Expressions;

namespace PegazusERP.Infraestrutura.Util
{
    public class Util
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                MailAddress ma = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string TitleToUrl(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            byte[] bytes = System.Text.Encoding.GetEncoding("iso-8859-8").GetBytes(text);
            text = System.Text.Encoding.UTF8.GetString(bytes);

            return Regex.Replace(text, @"[^A-Za-z0-9_\.~]+", "-");
        }

        public static bool ValidarCPF(string cpf)
        {
            if (cpf.Length != 11)
                return false;

            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (cpf[i] != cpf[0])
                    igual = false;

            if (igual || cpf == "12345678909")
                return false;

            int[] numeros = new int[11];
            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(
                    cpf[i].ToString());

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else if (numeros[10] != 11 - resultado)
                return false;

            return true;
        }


        public static string OnlyNumbers(string toNormalize)
        {
            if (toNormalize == null)
                return null;

            List<char> numbers = new List<char>("0123456789");
            StringBuilder toReturn = new StringBuilder(toNormalize.Length);
            CharEnumerator enumerator = toNormalize.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (numbers.Contains(enumerator.Current))
                    toReturn.Append(enumerator.Current);
            }

            return toReturn.ToString();
        }

        public static string FormataCpfCnpj(string text)
        {
            text = OnlyNumbers(text);

            if (string.IsNullOrWhiteSpace(text))
                return null;

            if (text.Length == 11)
                return Convert.ToUInt64(text).ToString(@"000\.000\.000\-00");

            else if (text.Length == 14)
                return Convert.ToUInt64(text).ToString(@"00\.000\.000\/0000\-00");

            else
                return null;
        }

        public static string GetEnumDescription<TEnum>(TEnum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if ((attributes != null) && (attributes.Length > 0))
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static string FormatarTelefone(string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone))
                return null;

            if (telefone.Length < 10)
                return telefone;

            int tam = telefone.Length - 6;
            string aux = string.Empty;
            for (int cont = 0; cont < tam; cont++)
                aux = aux + "#";

            long aa = long.Parse(telefone);

            var formatado = String.Format("{0:(##) ####-" + aux + "}", aa);

            return formatado;
        }

        public static bool IsDebugMode()
        {
            bool isDebug = false;

            #if DEBUG
                isDebug = true;
            #endif

            return isDebug;
        }

        public static LambdaExpression GetExpression<T>(string propertyName)
        {
            Type type = typeof(T);
            var parameter = Expression.Parameter(type);
            var memberExpression = Expression.Property(parameter, propertyName);
            var lambdaExpression = Expression.Lambda(memberExpression, parameter);
            return lambdaExpression;
        }
    }
}
