using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice.LeetCode
{
    public class IntToEnglish
    {
        // The leetcod problem is even simpler, no cents.
        public string NumberToWords(int num)
        {
            string strText = string.Empty;
            string strCurrFormat = string.Empty;
            int intLowDigit = 0;
            string strCents = string.Empty;
            int intDigit1 = 0;
            int intDigit2 = 0;
            int intDigit3 = 0;
            string strSubText = string.Empty;
            int arrGroupIdx = 0;

            string[] arrOnes = new string[20];
            string[] arrTens = new string[10];
            string[] arrGroup = new string[4];

            arrOnes[0] = "Zero";
            arrOnes[1] = "One";
            arrOnes[2] = "Two";
            arrOnes[3] = "Three";
            arrOnes[4] = "Four";
            arrOnes[5] = "Five";
            arrOnes[6] = "Six";
            arrOnes[7] = "Seven";
            arrOnes[8] = "Eight";
            arrOnes[9] = "Nine";
            arrOnes[10] = "Ten";
            arrOnes[11] = "Eleven";
            arrOnes[12] = "Twelve";
            arrOnes[13] = "Thirteen";
            arrOnes[14] = "Fourteen";
            arrOnes[15] = "Fifteen";
            arrOnes[16] = "Sixteen";
            arrOnes[17] = "Seventeen";
            arrOnes[18] = "Eighteen";
            arrOnes[19] = "Nineteen";

            arrTens[0] = "Zero";
            arrTens[1] = "Ten";
            arrTens[2] = "Twenty";
            arrTens[3] = "Thirty";
            arrTens[4] = "Forty";
            arrTens[5] = "Fifty";
            arrTens[6] = "Sixty";
            arrTens[7] = "Seventy";
            arrTens[8] = "Eighty";
            arrTens[9] = "Ninety";

            arrGroup[0] = "Zero";
            arrGroup[1] = "Thousand";
            arrGroup[2] = "Million";
            arrGroup[3] = "Billion";


            if (num > 0)
            {
                strCurrFormat = num.ToString("#,###.00");
                intLowDigit = strCurrFormat.IndexOf(".", 0);

                while (intLowDigit > 0)
                {
                    intDigit3 = int.Parse(strCurrFormat.Substring(intLowDigit - 1, 1));

                    if (intLowDigit > 1)
                        intDigit2 = int.Parse(strCurrFormat.Substring(intLowDigit - 2, 1));
                    else
                        intDigit2 = 0;

                    if (intLowDigit > 2)
                        intDigit1 = int.Parse(strCurrFormat.Substring(intLowDigit - 3, 1));
                    else
                        intDigit1 = 0;

                    strSubText = string.Empty;

                    if (intDigit1 > 0)
                        strSubText = arrOnes[intDigit1] + " HUNDRED ";

                    if (intDigit2 > 0)
                    {
                        if (intDigit2 == 1)
                            strSubText = strSubText + arrOnes[intDigit3 + 10] + " ";
                        else
                        {
                            strSubText = strSubText + arrTens[intDigit2];
                            if (intDigit3 > 0)
                                strSubText = strSubText + "-" + arrOnes[intDigit3];

                            strSubText = strSubText + " ";
                        }
                    }
                    else
                    {
                        if (intDigit3 > 0)
                            strSubText = string.Format("{0}{1}{2}", strSubText, arrOnes[intDigit3], " ");

                    }

                    if (strSubText != "" && arrGroupIdx != 0)
                        strSubText = strSubText + arrGroup[arrGroupIdx] + " ";

                    strText = strSubText + strText;
                    intLowDigit = intLowDigit - 4;
                    arrGroupIdx = arrGroupIdx + 1;
                }
            }

            return strText;
        }

        //tricky part to get space right
        public string NumberToWords2(int num)
        {
            string res = string.Empty;
            int arrGroupIdx = 0;
            string[] arrOnes = new string[20];
            string[] arrTens = new string[10];
            string[] arrGroup = new string[4];

            arrOnes[0] = "Zero";
            arrOnes[1] = "One";
            arrOnes[2] = "Two";
            arrOnes[3] = "Three";
            arrOnes[4] = "Four";
            arrOnes[5] = "Five";
            arrOnes[6] = "Six";
            arrOnes[7] = "Seven";
            arrOnes[8] = "Eight";
            arrOnes[9] = "Nine";
            arrOnes[10] = "Ten";
            arrOnes[11] = "Eleven";
            arrOnes[12] = "Twelve";
            arrOnes[13] = "Thirteen";
            arrOnes[14] = "Fourteen";
            arrOnes[15] = "Fifteen";
            arrOnes[16] = "Sixteen";
            arrOnes[17] = "Seventeen";
            arrOnes[18] = "Eighteen";
            arrOnes[19] = "Nineteen";

            arrTens[0] = "Zero";
            arrTens[1] = "Ten";
            arrTens[2] = "Twenty";
            arrTens[3] = "Thirty";
            arrTens[4] = "Forty";
            arrTens[5] = "Fifty";
            arrTens[6] = "Sixty";
            arrTens[7] = "Seventy";
            arrTens[8] = "Eighty";
            arrTens[9] = "Ninety";

            arrGroup[0] = "Zero";
            arrGroup[1] = "Thousand";
            arrGroup[2] = "Million";
            arrGroup[3] = "Billion";

            //process three characters one time
            while(num >= 0)
            {
                string sub = string.Empty;
                int lastThree = num % 1000;
                int digit3 = lastThree % 10;
                int digit2 = lastThree / 10 % 10;
                int digit1 = lastThree / 100;

                if(digit1 > 0)
                    sub += arrOnes[digit1] + " Hundred ";
                
                if(digit2 > 0)
                {
                    if (digit2 == 1)
                        sub += arrOnes[digit2 + digit3] + " ";
                    else
                    {
                        sub += arrTens[digit2];
                        if(digit3 > 0)
                            sub += " " + arrOnes[digit3];
                        sub += " ";
                    }                       
                }
                else if (digit3 > 0)
                {
                    sub += arrOnes[digit3] + " ";
                }

                //add unit
                if (arrGroupIdx > 0)
                    sub += arrGroup[arrGroupIdx] + " ";

                res += sub;
                //move to next three
                num /= 1000;
                arrGroupIdx++;
            }

            return res;
        }


        //SurePayroll Way of converting currency to Text
        #region * string manipulation methods
        public static string Left(String strParam, int iLen)
        {
            if (iLen > 0)
                return strParam.Substring(0, iLen);
            else
                return strParam;
        }
        public static string Right(String strParam, int iLen)
        {
            if (iLen > 0)
                return strParam.Substring(strParam.Length - iLen, iLen);
            else
                return strParam;
        }
        public static string Mid(string param, int startIndex, int length)
        {
            string result = param.Substring(startIndex, length);
            return result;
        }
        #endregion

        public static string ConvertCurrencyToText_TSB(double dblAmount, int intLength)
        {
            string strText = string.Empty;
            string strCurrFormat = string.Empty;
            int intLowDigit = 0;
            string strCents = string.Empty;
            int intDigit1 = 0;
            int intDigit2 = 0;
            int intDigit3 = 0;
            string strSubText = string.Empty;
            int arrGroupIdx = 0;
            string[] arrOnes = new string[20];
            string[] arrTens = new string[10];
            string[] arrGroup = new string[4];

            arrOnes[0] = "ZERO";
            arrOnes[1] = "ONE";
            arrOnes[2] = "TWO";
            arrOnes[3] = "THREE";
            arrOnes[4] = "FOUR";
            arrOnes[5] = "FIVE";
            arrOnes[6] = "SIX";
            arrOnes[7] = "SEVEN";
            arrOnes[8] = "EIGHT";
            arrOnes[9] = "NINE";
            arrOnes[10] = "TEN";
            arrOnes[11] = "ELEVEN";
            arrOnes[12] = "TWELVE";
            arrOnes[13] = "THIRTEEN";
            arrOnes[14] = "FOURTEEN";
            arrOnes[15] = "FIFTEEN";
            arrOnes[16] = "SIXTEEN";
            arrOnes[17] = "SEVENTEEN";
            arrOnes[18] = "EIGHTEEN";
            arrOnes[19] = "NINETEEN";

            arrTens[0] = "ZERO";
            arrTens[1] = "TEN";
            arrTens[2] = "TWENTY";
            arrTens[3] = "THIRTY";
            arrTens[4] = "FORTY";
            arrTens[5] = "FIFTY";
            arrTens[6] = "SIXTY";
            arrTens[7] = "SEVENTY";
            arrTens[8] = "EIGHTY";
            arrTens[9] = "NINETY";

            arrGroup[0] = "ZERO";
            arrGroup[1] = "THOUSAND";
            arrGroup[2] = "MILLION";
            arrGroup[3] = "BILLION";

            if (dblAmount > 0)
            {
                strCurrFormat = dblAmount.ToString("#,###.00");
                intLowDigit = strCurrFormat.IndexOf(".", 0);
                strCents = Mid(strCurrFormat, intLowDigit + 1, 2);

                while (intLowDigit > 0)
                {
                    intDigit3 = int.Parse(Mid(strCurrFormat, intLowDigit - 1, 1));

                    if (intLowDigit > 1)
                        intDigit2 = int.Parse(Mid(strCurrFormat, intLowDigit - 2, 1));
                    else
                        intDigit2 = 0;

                    if (intLowDigit > 2)
                        intDigit1 = int.Parse(Mid(strCurrFormat, intLowDigit - 3, 1));
                    else
                        intDigit1 = 0;

                    strSubText = string.Empty;

                    if (intDigit1 > 0)
                        strSubText = arrOnes[intDigit1] + " HUNDRED ";

                    if (intDigit2 > 0)
                    {
                        if (intDigit2 == 1)
                            strSubText = strSubText + arrOnes[intDigit3 + 10] + " ";
                        else
                        {
                            strSubText = strSubText + arrTens[intDigit2];
                            if (intDigit3 > 0)
                                strSubText = strSubText + "-" + arrOnes[intDigit3];

                            strSubText = strSubText + " ";
                        }
                    }
                    else
                    {
                        if (intDigit3 > 0)
                            strSubText = string.Format("{0}{1}{2}", strSubText, arrOnes[intDigit3], " ");

                    }

                    if (strSubText != "" && arrGroupIdx != 0)
                        strSubText = strSubText + arrGroup[arrGroupIdx] + " ";

                    strText = strSubText + strText;
                    intLowDigit = intLowDigit - 4;
                    arrGroupIdx = arrGroupIdx + 1;
                }

                strText = strText + "& " + strCents + "/100";

                if (Left(strText, 1) == "&")
                    strText = "NO " + strText;

                if (intLength > 0)
                {
                    if (strText.Length > intLength)
                    {
                        strText = strText + "*****";
                    }
                    else
                    {
                        for (int iCtr = 1; iCtr <= (intLength - strText.Length); iCtr++)
                            strText = strText + "*";
                    }
                }
            }

            return strText;
        }
    }
}
