using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceNowTest
{

    public static class Helper
    {
        public static int WordCount(this String str)
        {
            return str.Split(new char[] { ' ', '.', '?' },
                             StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public static Boolean WaintUntilPageIsLoaded(IWebDriver wd)
        {
            var previous = 0;
            var current = 0;
            var timeSliceMs = 500;
            var timeoutMs = 150000;
            do
            {
                previous = current;
                Thread.Sleep(timeSliceMs);
                timeoutMs -= timeSliceMs;
                current = wd.FindElement(By.XPath("//*")).ToString().Length;
            } while (current != previous && timeoutMs > 0);
            if (timeoutMs > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static String GetTimestamp()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        public static String GetShortTimestamp()
        {
            return DateTime.Now.ToString("HHmmss");
        }

        public static String GetRandomString(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var stringChars = new char[8];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            return new String(stringChars);
        }
    }  



}
