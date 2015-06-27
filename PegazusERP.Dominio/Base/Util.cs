using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PegazusERP.Dominio.Aggregates.Base
{
    public static class Util
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

        public static string Capitalize(string value)
        {
            if (value == null)
                return string.Empty;

            if (value.Length == 0)
                return value;

            value = value.ToLower();

            StringBuilder result = new StringBuilder(value);
            result[0] = char.ToUpper(result[0]);
            for (int i = 1; i < result.Length; ++i)
            {
                if (char.IsWhiteSpace(result[i - 1]))
                    result[i] = char.ToUpper(result[i]);
            }

            result = result.Replace(" Da ", " da ");
            result = result.Replace(" Das ", " das ");
            result = result.Replace(" De ", " de ");
            result = result.Replace(" De, ", " de, ");
            result = result.Replace(" Do ", " do ");
            result = result.Replace(" Dos ", " dos ");
            result = result.Replace(" E ", " e ");
            result = result.Replace(" Ao ", " ao ");

            return result.ToString();
        }

        public static bool TryParseDateTime(string text, out Nullable<DateTime> nDate)
        {
            DateTime date;
            bool isParsed = DateTime.TryParse(text, out date);
            if (isParsed)
                nDate = new Nullable<DateTime>(date);
            else
                nDate = new Nullable<DateTime>();
            return isParsed;
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

        public static bool ValidarCNPJ(string cnpj)
        {
            int[] digitos, soma, resultado;
            int nrDig;
            string ftmt;
            bool[] CNPJOk;
            ftmt = "6543298765432";
            digitos = new int[14];
            soma = new int[2];
            soma[0] = 0;
            soma[1] = 0;
            resultado = new int[2];
            resultado[0] = 0;
            resultado[1] = 0;
            CNPJOk = new bool[2];
            CNPJOk[0] = false;
            CNPJOk[1] = false;

            try
            {
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(cnpj.Substring(nrDig, 1));
                    if (nrDig <= 11)
                        soma[0] += (digitos[nrDig] * int.Parse(ftmt.Substring(nrDig + 1, 1)));

                    if (nrDig <= 12)
                        soma[1] += (digitos[nrDig] * int.Parse(ftmt.Substring(nrDig, 1)));
                }

                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);

                    if ((resultado[nrDig] == 0) || (resultado[nrDig] == 1))
                        CNPJOk[nrDig] = (digitos[12 + nrDig] == 0);
                    else
                        CNPJOk[nrDig] = (digitos[12 + nrDig] == (11 - resultado[nrDig]));
                }

                return (CNPJOk[0] && CNPJOk[1]);
            }
            catch
            {
                return false;
            }
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

        public static string GerarSenhaRandom(int tamanho = 8)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, tamanho)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            return result;
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

        public static decimal Truncar(decimal valor)
        {
            valor *= 100;
            valor = Math.Truncate(valor);
            valor /= 100;

            return valor;
        }

        public static double Truncar(double valor)
        {
            valor *= 100;
            valor = Math.Truncate(valor);
            valor /= 100;

            return valor;
        }

        public static DateTime? ParseDateTime(string dateToParse)
        {
            DateTime? dataReferencia = null;
            DateTime parseDataReferencia;

            if (DateTime.TryParse(dateToParse, out parseDataReferencia))
                dataReferencia = parseDataReferencia;

            return dataReferencia;
        }

        public static int? ParseInt(string toParse)
        {
            int? valor = null;
            int parseValor;

            if (int.TryParse(toParse, out parseValor))
                valor = parseValor;

            return valor;
        }

        public static string RetornarMesAno(DateTime data) 
        {
           var dadoMes = "0";
            if (data.Month < 10) 
            {
                dadoMes = dadoMes + data.Month.ToString();
            }
            else
            {
                dadoMes = data.Month.ToString();
            }
            var ano = data.Year; 
            return dadoMes + "/" + ano;
        }


        public static string valorExtenso1(string wvalor)
        {
            string[] wunidade = { "", " e um", " e dois", " e trez", " e quatro", " e cinco", " e seis", " e sete", " e oito", " e nove" };
            string[] wdezes = { "", " e onze", " e doze", " e treze", " e quatorze", " e quinze", " e dezesseis", " e dezessete", " e dezoito", " e dezenove" };
            string[] wdezenas = { "", " e dez", " e vinte", " e trinta", " e quarenta", " e cinquenta", " e sessenta", " e setenta", " e oitenta", " e noventa" };
            string[] wcentenas = { "", " e cento", " e duzentos", " e trezentos", " e quatrocentos", " e quinhentos", " e seiscentos", " e setecentos", " e oitocentos", " e novecentos" };
            string[] wplural = { " bilhões", " milhões", " mil", "" };
            string[] wsingular = { " bilhão", " milhão", " mil", "" };
            string wextenso = "";
            string wfracao;
            wvalor = wvalor.Replace("R$", "");
            string wnumero = wvalor.Replace(",", "").Trim();
            wnumero = wnumero.Replace(".", "").PadLeft(14, '0');
            if (Int64.Parse(wnumero.Substring(0, 12)) > 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    wfracao = wnumero.Substring(i * 3, 3);
                    if (int.Parse(wfracao) != 0)
                    {
                        if (int.Parse(wfracao.Substring(0, 3)) == 100) wextenso += " e cem";
                        else
                        {
                            wextenso += wcentenas[int.Parse(wfracao.Substring(0, 1))];
                            if (int.Parse(wfracao.Substring(1, 2)) > 10 && int.Parse(wfracao.Substring(1, 2)) < 20) wextenso += wdezes[int.Parse(wfracao.Substring(2, 1))];
                            else
                            {
                                wextenso += wdezenas[int.Parse(wfracao.Substring(1, 1))];
                                wextenso += wunidade[int.Parse(wfracao.Substring(2, 1))];
                            }
                        }
                        if (int.Parse(wfracao) > 1) wextenso += wplural;
                        else wextenso += wsingular;
                    }
                }
                if (Int64.Parse(wnumero.Substring(0, 12)) > 1) wextenso += " reais";
                else wextenso += " real";
            }
            wfracao = wnumero.Substring(12, 2);
            if (int.Parse(wfracao) > 0)
            {
                if (int.Parse(wfracao.Substring(0, 2)) > 10 && int.Parse(wfracao.Substring(0, 2)) < 20) wextenso = wextenso + wdezes[int.Parse(wfracao.Substring(1, 1))];
                else
                {
                    wextenso += wdezenas[int.Parse(wfracao.Substring(0, 1))];
                    wextenso += wunidade[int.Parse(wfracao.Substring(1, 1))];
                }
                if (int.Parse(wfracao) > 1) wextenso += " centavos";
                else wextenso += " centavo";
            }
            if (wextenso != "") wextenso = wextenso.Substring(3, 1).ToUpper() + wextenso.Substring(4);
            else wextenso = "Nada";
            return wextenso;
        }

        public static string valorExtenso(decimal pdbl_Valor)
        {
            string strValorExtenso = ""; //Variável que irá armazenar o valor por extenso do número informado
            string strNumero = "";       //Irá armazenar o número para exibir por extenso 
            string strCentena = "";
            string strDezena = "";
            string strDezCentavo = "";

            decimal dblCentavos = 0;
            decimal dblValorInteiro = 0;
            int intContador = 0;
            bool bln_Bilhao = false;
            bool bln_Milhao = false;
            bool bln_Mil = false;
            bool bln_Unidade = false;

            //Verificar se foi informado um dado indevido 
            if (pdbl_Valor == 0 || pdbl_Valor <= 0)
            {
                throw new Exception("Valor não suportado pela Função. Verificar se há valor negativo ou nada foi informado");
            }
            if (pdbl_Valor > (decimal)9999999999.99)
            {
                throw new Exception("Valor não suportado pela Função. Verificar se o Valor está acima de 9999999999.99");
            }
            else //Entrada padrão do método
            {
                //Gerar Extenso Centavos 
                pdbl_Valor = (Decimal.Round(pdbl_Valor, 2));
                dblCentavos = pdbl_Valor - (Int64)pdbl_Valor;

                //Gerar Extenso parte Inteira
                dblValorInteiro = (Int64)pdbl_Valor;
                if (dblValorInteiro > 0)
                {
                    if (dblValorInteiro > 999)
                    {
                        bln_Mil = true;
                    }
                    if (dblValorInteiro > 999999)
                    {
                        bln_Milhao = true;
                        bln_Mil = false;
                    }
                    if (dblValorInteiro > 999999999)
                    {
                        bln_Mil = false;
                        bln_Milhao = false;
                        bln_Bilhao = true;
                    }

                    for (int i = (dblValorInteiro.ToString().Trim().Length) - 1; i >= 0; i--)
                    {
                        // strNumero = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) + 1, 1);
                        strNumero = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) - 1, 1);
                        switch (i)
                        {            /*******/
                            case 9:  /*Bilhão*
                                 /*******/
                                {
                                    strValorExtenso =  fcn_Numero_Unidade(strNumero) + ((int.Parse(strNumero) > 1) ? " Bilhões e" : " Bilhão e");
                                    bln_Bilhao = true;
                                    break;
                                }
                            case 8: /********/
                            case 5: //Centena*
                            case 2: /********/
                                {
                                    if (int.Parse(strNumero) > 0)
                                    {
                                        strCentena = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) - 1, 3);

                                        if (int.Parse(strCentena) > 100 && int.Parse(strCentena) < 200)
                                        {
                                            strValorExtenso = strValorExtenso + " Cento e ";
                                        }
                                        else
                                        {
                                            strValorExtenso = strValorExtenso + " " + fcn_Numero_Centena(strNumero);
                                        }
                                        if (intContador == 8)
                                        {
                                            bln_Milhao = true;
                                        }
                                        else if (intContador == 5)
                                        {
                                            bln_Mil = true;
                                        }
                                    }
                                    break;
                                }
                            case 7: /*****************/
                            case 4: //Dezena de Milhão*
                            case 1: /*****************/
                                {
                                    if (int.Parse(strNumero) > 0)
                                    {
                                        strDezena = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) - 1, 2);//

                                        if (int.Parse(strDezena) > 10 && int.Parse(strDezena) < 20)
                                        {
                                            strValorExtenso = strValorExtenso + (Right(strValorExtenso, 5).Trim() == "entos" ? " e " : " ")
                                            + fcn_Numero_Dezena0(Right(strDezena, 1));//corrigido

                                            bln_Unidade = true;
                                        }
                                        else
                                        {
                                            strValorExtenso = strValorExtenso + (Right(strValorExtenso, 5).Trim() == "entos" ? " e " : " ")
                                            + fcn_Numero_Dezena1(Left(strDezena, 1));//corrigido 

                                            bln_Unidade = false;
                                        }
                                        if (intContador == 7)
                                        {
                                            bln_Milhao = true;
                                        }
                                        else if (intContador == 4)
                                        {
                                            bln_Mil = true;
                                        }
                                    }
                                    break;
                                }
                            case 6: /******************/
                            case 3: //Unidade de Milhão* 
                            case 0: /******************/
                                {
                                    if (int.Parse(strNumero) > 0 && !bln_Unidade)
                                    {
                                        if ((Right(strValorExtenso, 5).Trim()) == "entos"
                                        || (Right(strValorExtenso, 3).Trim()) == "nte"
                                        || (Right(strValorExtenso, 3).Trim()) == "nta")
                                        {
                                            strValorExtenso = strValorExtenso + " e ";
                                        }
                                        else
                                        {
                                            strValorExtenso = strValorExtenso + " ";
                                        }
                                        strValorExtenso = strValorExtenso + fcn_Numero_Unidade(strNumero);
                                    }
                                    if (i == 6)
                                    {
                                        if (bln_Milhao || int.Parse(strNumero) > 0)
                                        {
                                            strValorExtenso = strValorExtenso + ((int.Parse(strNumero) == 1) && !bln_Unidade ? " Milhão" : " Milhões");
                                            strValorExtenso = strValorExtenso + ((int.Parse(strNumero) > 1000000) ? " " : " e");
                                            bln_Milhao = true;
                                        }
                                    }
                                    if (i == 3)
                                    {
                                        if (bln_Mil || int.Parse(strNumero) > 0)
                                        {
                                            strValorExtenso = strValorExtenso + " Mil";
                                            strValorExtenso = strValorExtenso + ((int.Parse(strNumero) > 1000) ? " " : " e");
                                            bln_Mil = true;
                                        }
                                    }
                                    if (i == 0)
                                    {
                                        if ((bln_Bilhao && !bln_Milhao && !bln_Mil
                                        && Right((dblValorInteiro.ToString().Trim()), 3) == "0")
                                        || (!bln_Bilhao && bln_Milhao && !bln_Mil
                                        && Right((dblValorInteiro.ToString().Trim()), 3) == "0"))
                                        {
                                            strValorExtenso = strValorExtenso + " e ";
                                        }
                                        strValorExtenso = strValorExtenso + ((Int64.Parse(dblValorInteiro.ToString())) > 1 ? " Reais" : " Real");
                                    }
                                    bln_Unidade = false;
                                    break;
                                }
                        }
                    }//
                }
                if (dblCentavos > 0)
                {

                    if (dblCentavos > 0 && dblCentavos < 0.1M)
                    {
                        strNumero = Right((Decimal.Round(dblCentavos, 2)).ToString().Trim(), 1);
                        strValorExtenso = strValorExtenso + ((dblCentavos > 0) ? " e " : " ")
                        + fcn_Numero_Unidade(strNumero) + ((dblCentavos > 0.01M) ? " Centavos" : " Centavo");
                    }
                    else if (dblCentavos > 0.1M && dblCentavos < 0.2M)
                    {
                        strNumero = Right(((Decimal.Round(dblCentavos, 2) - (decimal)0.1).ToString().Trim()), 1);
                        strValorExtenso = strValorExtenso + ((dblCentavos > 0) ? " " : " e ")
                        + fcn_Numero_Dezena0(strNumero) + " Centavos ";
                    }
                    else
                    {
                        strNumero = Right(dblCentavos.ToString().Trim(), 2);
                        strDezCentavo = Mid(dblCentavos.ToString().Trim(), 2, 1);

                        strValorExtenso = strValorExtenso + ((int.Parse(strNumero) > 0) ? " e " : " ");
                        strValorExtenso = strValorExtenso + fcn_Numero_Dezena1(Left(strDezCentavo, 1));

                        if ((dblCentavos.ToString().Trim().Length) > 2)
                        {
                            strNumero = Right((Decimal.Round(dblCentavos, 2)).ToString().Trim(), 1);
                            if (int.Parse(strNumero) > 0)
                            {
                                if (dblValorInteiro <= 0)
                                {
                                    if (Mid(strValorExtenso.Trim(), strValorExtenso.Trim().Length - 2, 1) == "e")
                                    {
                                        strValorExtenso = strValorExtenso + " e " + fcn_Numero_Unidade(strNumero);
                                    }
                                    else
                                    {
                                        strValorExtenso = strValorExtenso + " e " + fcn_Numero_Unidade(strNumero);
                                    }
                                }
                                else
                                {
                                    strValorExtenso = strValorExtenso + " e " + fcn_Numero_Unidade(strNumero);
                                }
                            }
                        }
                        strValorExtenso = strValorExtenso + " Centavos ";
                    }
                }
                if (dblValorInteiro < 1) strValorExtenso = Mid(strValorExtenso.Trim(), 2, strValorExtenso.Trim().Length - 2);
            }

            return strValorExtenso.Trim();
        }

        private static string fcn_Numero_Dezena0(string pstrDezena0)
        {
            //Vetor que irá conter o número por extenso 
            ArrayList array_Dezena0 = new ArrayList();

            array_Dezena0.Add("Onze");
            array_Dezena0.Add("Doze");
            array_Dezena0.Add("Treze");
            array_Dezena0.Add("Quatorze");
            array_Dezena0.Add("Quinze");
            array_Dezena0.Add("Dezesseis");
            array_Dezena0.Add("Dezessete");
            array_Dezena0.Add("Dezoito");
            array_Dezena0.Add("Dezenove");

            return array_Dezena0[((int.Parse(pstrDezena0)) - 1)].ToString();
        }

        private static string fcn_Numero_Dezena1(string pstrDezena1)
        {
            //Vetor que irá conter o número por extenso
            ArrayList array_Dezena1 = new ArrayList();

            array_Dezena1.Add("Dez");
            array_Dezena1.Add("Vinte");
            array_Dezena1.Add("Trinta");
            array_Dezena1.Add("Quarenta");
            array_Dezena1.Add("Cinquenta");
            array_Dezena1.Add("Sessenta");
            array_Dezena1.Add("Setenta");
            array_Dezena1.Add("Oitenta");
            array_Dezena1.Add("Noventa");

            return array_Dezena1[Int16.Parse(pstrDezena1) - 1].ToString();
        }

        private static string fcn_Numero_Centena(string pstrCentena)
        {
            //Vetor que irá conter o número por extenso
            ArrayList array_Centena = new ArrayList();

            array_Centena.Add("Cem");
            array_Centena.Add("Duzentos");
            array_Centena.Add("Trezentos");
            array_Centena.Add("Quatrocentos");
            array_Centena.Add("Quinhentos");
            array_Centena.Add("Seiscentos");
            array_Centena.Add("Setecentos");
            array_Centena.Add("Oitocentos");
            array_Centena.Add("Novecentos");

            return array_Centena[((int.Parse(pstrCentena)) - 1)].ToString();
        }

        private static  string fcn_Numero_Unidade(string pstrUnidade)
        {
            //Vetor que irá conter o número por extenso
            ArrayList array_Unidade = new ArrayList();

            array_Unidade.Add("Um");
            array_Unidade.Add("Dois");
            array_Unidade.Add("Três");
            array_Unidade.Add("Quatro");
            array_Unidade.Add("Cinco");
            array_Unidade.Add("Seis");
            array_Unidade.Add("Sete");
            array_Unidade.Add("Oito");
            array_Unidade.Add("Nove");

            return array_Unidade[(int.Parse(pstrUnidade) - 1)].ToString();
        }

        //Começa aqui os Métodos de Compatibilazação com VB 6 .........Left() Right() Mid()
        public static string Left(string param, int length)
        {
            //we start at 0 since we want to get the characters starting from the 
            //left and with the specified lenght and assign it to a variable
            if (param == "")
                return "";
            string result = param.Substring(0, length);
            //return the result of the operation 
            return result;
        }
        public static string Right(string param, int length)
        {
            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable 
            if (param == "")
                return "";
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }

        public static string Mid(string param, int startIndex, int length)
        {
            //start at the specified index in the string ang get N number of
            //characters depending on the lenght and assign it to a variable 
            string result = param.Substring(startIndex, length);
            //return the result of the operation
            return result;
        }

        public static string Mid(string param, int startIndex)
        {
            //start at the specified index and return all characters after it
            //and assign it to a variable
            string result = param.Substring(startIndex);
            //return the result of the operation 
            return result;
        }
    }
}
