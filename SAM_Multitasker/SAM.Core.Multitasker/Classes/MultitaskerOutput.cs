// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright (c) 2020–2026 Michal Dengusiak & Jakub Ziolkowski and contributors
using System.Text.Json.Nodes;
using System;
using System.Drawing;

namespace SAM.Core.Multitasker
{
    public class MultitaskerOutput : IJSAMObject
    {
        private ValueType valueType;
        private object result;

        public MultitaskerOutput(object result)
        {
            this.result = result;
            valueType = result == null ? ValueType.Undefined : result.ValueType();
        }

        public MultitaskerOutput(MultitaskerOutput multitaskerOutput)
        {
            if(multitaskerOutput != null)
            {
                valueType = multitaskerOutput.valueType;
                result = multitaskerOutput.result;
            }
        }

        public MultitaskerOutput(JsonObject jObject)
        {
            FromJsonObject(jObject);
        }

        public object Result
        {
            get
            {
                return result;
            }
        }

        public bool FromJsonObject(JsonObject jObject)
        {
            if(jObject.ContainsKey("ValueType"))
            {
                valueType = Core.Query.Enum<ValueType>(jObject["ValueType"]?.GetValue<string>() ?? default(string));
            }

            if(jObject.ContainsKey("Result"))
            {
                switch (valueType)
                {
                    case ValueType.Boolean:
                        result = jObject["Result"]?.GetValue<bool>() ?? default(bool);
                        return true;

                    case ValueType.Color:
                        result = new SAMColor(jObject["Result"] as JsonObject).ToColor();
                        return true;

                    case ValueType.DateTime:
                        result = jObject["Result"]?.GetValue<DateTime>() ?? default(DateTime);
                        return true;

                    case ValueType.Double:
                        result = jObject["Result"]?.GetValue<double>() ?? default(double);
                        return true;

                    case ValueType.Guid:
                        if (!Enum.TryParse(jObject["Result"]?.GetValue<string>() ?? default(string), out Guid guid))
                        {
                            return false;
                        }
                        result = guid;
                        return true;

                    case ValueType.IJSAMObject:
                        JsonObject jObject_Temp = jObject["Result"] as JsonObject;
                        if (jObject_Temp == null)
                        {
                            return false;
                        }

                        result = new JSAMObjectWrapper(jObject_Temp).ToIJSAMObject();
                        return true;

                    case ValueType.Integer:
                        result = jObject["Result"]?.GetValue<int>() ?? default(int);
                        return true;

                    case ValueType.String:
                        result = jObject["Result"]?.GetValue<string>() ?? default(string);
                        return true;
                }
            }

            return true;
        }

        public JsonObject ToJsonObject()
        {
            JsonObject jObject = new JsonObject();
            jObject.Add("_type", Core.Query.FullTypeName(this));

            jObject.Add("ValueType", valueType.ToString());

            if (valueType != ValueType.Undefined)
            {
                object value = null;
                switch (valueType)
                {
                    case ValueType.Boolean:
                        if (Core.Query.TryConvert(result, out bool @bool))
                        {
                            value = @bool;
                        }
                        break;

                    case ValueType.Color:
                        if (Core.Query.TryConvert(result, out Color color))
                        {
                            value = new SAMColor(color).ToJsonObject();
                        }
                        break;

                    case ValueType.DateTime:
                        if (Core.Query.TryConvert(result, out DateTime dateTime))
                        {
                            value = dateTime;
                        }
                        break;

                    case ValueType.Double:
                        if (Core.Query.TryConvert(result, out double @double))
                        {
                            value = @double;
                        }
                        break;

                    case ValueType.Guid:
                        if (Core.Query.TryConvert(result, out Guid @guid))
                        {
                            value = @guid;
                        }
                        break;

                    case ValueType.IJSAMObject:
                        value = ((IJSAMObject)result).ToJsonObject();
                        break;

                    case ValueType.Integer:
                        if (Core.Query.TryConvert(result, out int @int))
                        {
                            value = @int;
                        }
                        break;

                    case ValueType.String:
                        if (Core.Query.TryConvert(result, out string @string))
                        {
                            value = @string;
                        }
                        break;
                }

                if (value != null)
                {
                    jObject.Add("Result", value as dynamic);
                }
            }

            return jObject;
        }
    }
}
