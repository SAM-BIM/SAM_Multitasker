// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright (c) 2020–2026 Michal Dengusiak & Jakub Ziolkowski and contributors
using System.Text.Json.Nodes;
using System.Collections.Generic;

namespace SAM.Core.Multitasker
{
    public class MultitaskerInput : IJSAMObject
    {
        public Dictionary<string, MultitaskerVariable> dictionary;

        public MultitaskerInput()
        {

        }

        public MultitaskerInput(MultitaskerInput multitaskerInput)
        {
            if(multitaskerInput != null)
            {
                if(multitaskerInput.dictionary != null)
                {
                    dictionary = new Dictionary<string, MultitaskerVariable>();
                    foreach (MultitaskerVariable multitaskerVariable in multitaskerInput.dictionary.Values)
                    {
                        if(multitaskerVariable != null)
                        {
                            Add(new MultitaskerVariable(multitaskerVariable));
                        }
                    }
                }
            }
        }

        public MultitaskerInput(IEnumerable<MultitaskerVariable> multitaskerVariables)
        {
            if(multitaskerVariables != null)
            {
                dictionary = new Dictionary<string, MultitaskerVariable>();
                foreach (MultitaskerVariable multitaskerVariable in multitaskerVariables)
                {
                    if(!string.IsNullOrWhiteSpace(multitaskerVariable?.Name))
                    {
                        dictionary[multitaskerVariable.Name] = multitaskerVariable;
                    }
                }
            }
        }

        public MultitaskerInput(string name, object value)
        {
            dictionary = new Dictionary<string, MultitaskerVariable>();
            if(name != null)
            {
                dictionary[name] = new MultitaskerVariable(name, value);
            }
        }

        public MultitaskerInput(string name, ValueType valueType, object value)
        {
            dictionary = new Dictionary<string, MultitaskerVariable>();
            if (name != null)
            {
                dictionary[name] = new MultitaskerVariable(name, valueType, value);
            }
        }

        public MultitaskerInput(JsonObject jObject)
        {
            FromJsonObject(jObject);   
        }

        public bool Add(MultitaskerVariable multitaskerVariable)
        {
            if(string.IsNullOrWhiteSpace(multitaskerVariable?.Name))
            {
                return false;
            }

            if(dictionary == null)
            {
                dictionary = new Dictionary<string, MultitaskerVariable>();
            }

            dictionary[multitaskerVariable.Name] = multitaskerVariable;
            return true;
        }

        public bool FromJsonObject(JsonObject jObject)
        {
            if (jObject == null)
            {
                return false;
            }

            if (jObject.ContainsKey("MultitaskerVariables"))
            {
                dictionary = new Dictionary<string, MultitaskerVariable>();

                JsonArray jArray = jObject["MultitaskerVariables"] as JsonArray;
                foreach(JsonObject jObject_MultitaskerVariable in jArray)
                {
                    Add(new MultitaskerVariable(jObject_MultitaskerVariable));
                }
            }

            return true;
        }

        public JsonObject ToJsonObject()
        {
            JsonObject result = new JsonObject();
            result.Add("_type", Core.Query.FullTypeName(this));

            if(dictionary != null)
            {
                JsonArray jArray = new JsonArray();

                foreach(MultitaskerVariable multitaskerVariable in dictionary.Values)
                {
                    if(multitaskerVariable == null)
                    {
                        continue;
                    }

                    jArray.Add(multitaskerVariable.ToJsonObject());
                }

                result.Add("MultitaskerVariables", jArray);
            }

            return result;
        }

        public Dictionary<string, dynamic> Variables
        {
            get
            {
                if(dictionary == null)
                {
                    return null;
                }

                Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
                foreach(MultitaskerVariable multitaskerVariable in dictionary.Values)
                {
                    if(string.IsNullOrWhiteSpace(multitaskerVariable.Name))
                    {
                        continue;
                    }

                    result[multitaskerVariable.Name] = multitaskerVariable.Value;
                }

                return result;
            }
        }
    }
}
