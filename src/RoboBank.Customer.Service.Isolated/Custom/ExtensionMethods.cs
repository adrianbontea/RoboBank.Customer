using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using AutoMapper;
using Newtonsoft.Json.Linq;

namespace RoboBank.Customer.Service.Isolated.Custom
{
    public static class ExtensionMethods
    {
        internal static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(this IMappingExpression<TSource, TDestination> map, Expression<Func<TDestination, object>> selector)
        {
            return map.ForMember(selector, config => config.Ignore());
        }

        internal static IDictionary<string, JToken> ToJsonNetDictionary(this IDictionary<string, object> genericDictionary)
        {
            return genericDictionary.ToDictionary(entry => entry.Key, entry => JToken.FromObject(entry.Value));
        }

        internal static IDictionary<string, object> ToGenericDictionary(this IDictionary<string, JToken> jsonNetDictionary)
        {
            var result = new Dictionary<string, object>();

            foreach (var entry in jsonNetDictionary)
            {
                if (entry.Value is JObject)
                {
                    result.Add(entry.Key, (entry.Value as JObject).ToDictionary());
                }
                else if (entry.Value is JArray)
                {
                    result.Add(entry.Key, (entry.Value as JArray).ToList());
                }
                else if (entry.Value is JValue)
                {
                    result.Add(entry.Key, (entry.Value as JValue).Value);
                }
            }

            return result;
        }

        internal static bool IsJsonContent(this HttpContent content)
        {
            if (content == null)
            {
                throw new ArgumentNullException("content");
            }

            if (content.Headers == null || content.Headers.ContentType == null)
            {
                return false;
            }

            MediaTypeHeaderValue contentType = content.Headers.ContentType;
            return
                contentType.MediaType.Equals("application/json", StringComparison.OrdinalIgnoreCase) ||
                contentType.MediaType.Equals("text/json", StringComparison.OrdinalIgnoreCase);
        }

        private static IDictionary<string, object> ToDictionary(this JObject jObject)
        {
            var result = jObject.ToObject<Dictionary<string, object>>();

            var jObjectKeys = (from r in result
                let key = r.Key
                let value = r.Value
                where value is JObject
                select key).ToList();

            var jArrayKeys = (from r in result
                let key = r.Key
                let value = r.Value
                where value is JArray
                select key).ToList();

            var jValueKeys = (from r in result
                let key = r.Key
                let value = r.Value
                where value is JValue
                select key).ToList();

            jArrayKeys.ForEach(key => result[key] = (result[key] as JArray).ToList());
            jObjectKeys.ForEach(key => result[key] = (result[key] as JObject).ToDictionary());
            jValueKeys.ForEach(key => result[key] = (result[key] as JValue).Value);

            return result;
        }

        private static IList<object> ToList(this JArray jArray)
        {
            var result = new List<object>();

            foreach (var child in jArray.Children())
            {
                if (child is JObject)
                {
                    result.Add((child as JObject).ToDictionary());
                }

                if (child is JArray)
                {
                    result.Add((child as JArray).ToList());
                }

                if (child is JValue)
                {
                    result.Add((child as JValue).Value);
                }
            }

            return result;
        }
    }
}