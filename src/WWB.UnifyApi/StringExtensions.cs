﻿using System;
using System.Text.RegularExpressions;

namespace WWB.UnifyApi
{
    public static class StringExtensions
    {
        /// <summary>
        /// 手机号掩码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string PhoneToAnonymous(this string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentNullException("phone is empty");
            }
            if (phone.StartsWith("0"))
            {
                phone = phone.Substring(1);
            }

            return Regex.Replace(phone, "(\\d{4})\\d{3}(\\d{4})", "$1***$2");
        }
    }
}