﻿using System;

namespace CrushMe.Web.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static TResult SafeGet<T, TResult>(this T obj, Func<T, TResult> memberExpression)
        {
            return SafeGet(obj, memberExpression, default(TResult));
        }

        public static TResult SafeGet<T, TResult>(this T obj, Func<T, TResult> memberExpression, TResult defaultValue)
        {
            try
            {
                var result = memberExpression(obj);
                return result;
            }
            catch (NullReferenceException)
            {
                return defaultValue;
            }
        }
    }
}
