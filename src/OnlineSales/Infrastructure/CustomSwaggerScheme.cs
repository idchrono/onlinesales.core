﻿// <copyright file="CustomSwaggerScheme.cs" company="WavePoint Co. Ltd.">
// Licensed under the MIT license. See LICENSE file in the samples root for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using AutoMapper.Internal;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using OnlineSales.DataAnnotations;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OnlineSales.Infrastructure
{
    public class CustomSwaggerScheme : ISchemaFilter
    {
        private static readonly string CurrencySymbolsRegex = CurrencyCodeAttribute.GetAllCurrencyCodesRegex();

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.Namespace!.Contains("OnlineSales") && schema.Properties.Any())
            {
                var properties = context.Type.GetProperties();
                foreach (var propertySchema in schema.Properties)
                {
                    propertySchema.Value.Title = CreateTitle(propertySchema.Key);
                    var property = properties.FirstOrDefault(m => m.Name.Equals(propertySchema.Key, StringComparison.OrdinalIgnoreCase));
                    if (property != null)
                    {
                        if (property.GetCustomAttribute<SwaggerHideAttribute>() != null)
                        {
                            propertySchema.Value.Extensions.Add("x-hide", new OpenApiBoolean(true));
                        }

                        if (property.GetCustomAttribute<SwaggerUniqueAttribute>() != null)
                        {
                            propertySchema.Value.Extensions.Add("x-unique", new OpenApiBoolean(true));
                        }

                        if (GetTypeFromNullable(property.PropertyType).IsEnum)
                        {
                            propertySchema.Value.Example = new OpenApiString(Activator.CreateInstance(GetTypeFromNullable(property.PropertyType))!.ToString());
                        }
                        else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?))
                        {
                            SetIntegerExample(property, propertySchema.Value);
                        }
                        else if (property.PropertyType == typeof(string))
                        {
                            if (property.GetCustomAttribute<EmailAddressAttribute>() != null)
                            {
                                propertySchema.Value.Pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){1,63})+)$";
                                SetStringExample(property, propertySchema.Value, "example@example.com");
                            }
                            else if (property.GetCustomAttribute<CurrencyCodeAttribute>() != null)
                            {
                                propertySchema.Value.Pattern = CurrencySymbolsRegex;
                                SetStringExample(property, propertySchema.Value, "USD");
                            }
                            else
                            {
                                SetStringExample(property, propertySchema.Value);
                            }
                        }
                        else if (property.PropertyType == typeof(double) || property.PropertyType == typeof(double?) || property.PropertyType == typeof(decimal) || property.PropertyType == typeof(decimal?))
                        {
                            SetDoubleExample(property, propertySchema.Value);
                        }
                        else if (property.PropertyType == typeof(string[]))
                        {
                            SetStringArrayExample(property, propertySchema.Value);
                        }
                        else if (property.PropertyType == typeof(Dictionary<string, string>))
                        {
                            SetDictionaryArrayExample(property, propertySchema.Value);
                        }
                        else if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?))
                        {
                            propertySchema.Value.Example = new OpenApiBoolean(true);
                        }
                        else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                        {
                            SetStringExample(property, propertySchema.Value, new DateTime(2023, 04, 18, 12, 0, 0, DateTimeKind.Utc).ToString("O"));
                            propertySchema.Value.Pattern = @"^(\d{4})-(1[0-2]|0[1-9])-(3[01]|[12][0-9]|0[1-9])T(2[0-4]|1[0-9]|0[1-9]):(2[0-4]|1[0-9]|0[1-9]):([1-5]?0[0-9]).(\d{7})Z$";
                        }
                    }
                }
            }
        }

        private static Type GetTypeFromNullable(Type type)
        {
            return type.IsNullableType() ? Nullable.GetUnderlyingType(type)! : type;
        }

        private static void SetIntegerExample(PropertyInfo property, OpenApiSchema schema)
        {
            var attr = property.GetCustomAttribute<SwaggerExampleAttribute<int>>();
            schema.Example = new OpenApiInteger(attr != null ? attr.Value : 1);
        }

        private static void SetDoubleExample(PropertyInfo property, OpenApiSchema schema)
        {
            var attr = property.GetCustomAttribute<SwaggerExampleAttribute<double>>();
            schema.Example = new OpenApiDouble(attr != null ? attr.Value : 1.0);
        }

        private static void SetStringExample(PropertyInfo property, OpenApiSchema schema, string defaultValue = "string")
        {
            var attr = property.GetCustomAttribute<SwaggerExampleAttribute<string>>();
            if (attr != null)
            {
                if (schema.Pattern != null)
                {
                    var regex = new Regex(schema.Pattern);
                    if (regex.IsMatch(attr.Value))
                    {
                        schema.Example = new OpenApiString(attr.Value);
                    }
                    else
                    {
                        schema.Example = new OpenApiString(defaultValue);
                    }
                }
                else
                {
                    schema.Example = new OpenApiString(attr.Value);
                }
            }
            else
            {
                schema.Example = new OpenApiString(defaultValue);
            }
        }

        private static void SetStringArrayExample(PropertyInfo property, OpenApiSchema schema)
        {
            var attr = property.GetCustomAttribute<SwaggerExampleAttribute<string[]>>();
            var array = new OpenApiArray();
            array.AddRange(attr != null ? attr.Value.Select(s => new OpenApiString(s)) : new List<OpenApiString>() { new OpenApiString("string1"), new OpenApiString("string2") });
            schema.Example = array;
        }

        private static void SetDictionaryArrayExample(PropertyInfo property, OpenApiSchema schema)
        {
            var attr = property.GetCustomAttribute<SwaggerExampleAttribute<Dictionary<string, string>>>();
            var array = new OpenApiObject();
            array.AddRangeIfNotExists<string, IOpenApiAny>(attr != null ? attr.Value.ToDictionary(x => x.Key, x => (IOpenApiAny)new OpenApiString(x.Value)) : new Dictionary<string, IOpenApiAny>() { { "key1", new OpenApiString("value1") }, { "key2", new OpenApiString("value2") } });
            schema.Example = array;
        }

        private static string CreateTitle(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return string.Empty;
            }

            var newText = new StringBuilder(name.Length * 2);
            newText.Append(char.ToUpper(name[0]));
            for (var i = 1; i < name.Length; i++)
            {
                if (char.IsUpper(name[i]))
                {
                    newText.Append(' ');
                }

                newText.Append(name[i]);
            }

            return newText.ToString();
        }
    }
}