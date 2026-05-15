
using System.Text.Json.Nodes;
using System;

namespace SAM.Core.Multitasker
{
    public class Script : IJSAMObject
    {
        private ProgrammingLanguage programmingLanguage;
        private string code;
        private string name;

        public Script(Script script)
        {
            if(script != null)
            {
                programmingLanguage = script.programmingLanguage;
                code = script.code;
                name = script.name;
            }
        }

        public Script(JsonObject jObject)
        {
            FromJsonObject(jObject);
        }

        public Script(ProgrammingLanguage programmingLanguage, string name, string code)
        {
            this.programmingLanguage = programmingLanguage;
            this.code = code;
            this.name = name;
        }

        public Script(string code)
        {
            programmingLanguage = ProgrammingLanguage.Undefined;
            this.code = code;
        }

        public override string ToString()
        {
            return code?.ToString();
        }

        public override int GetHashCode()
        {
            return (new Tuple<string, string, ProgrammingLanguage>(name, code, programmingLanguage)).GetHashCode();
        }

        public override bool Equals(object @object) 
        {
            if(@object?.GetType() != GetType())
            {
                return false;
            }

            Script script = @object as Script;
            if(script == null)
            {
                return false;
            }


            return script.code == code && script.programmingLanguage == programmingLanguage && script.name == name;
        }

        public bool FromJsonObject(JsonObject jObject)
        {
            if(jObject == null)
            {
                return false;
            }

            if(jObject.ContainsKey("Code"))
            {
                code = jObject["Code"]?.GetValue<string>() ?? default(string);
            }

            if (jObject.ContainsKey("Name"))
            {
                name = jObject["Name"]?.GetValue<string>() ?? default(string);
            }

            if (jObject.ContainsKey("ProgrammingLanguage"))
            {
                programmingLanguage = Core.Query.Enum<ProgrammingLanguage>(jObject["ProgrammingLanguage"]?.GetValue<string>() ?? default(string));
            }

            return true;
        }

        public string Code
        {
            get
            {
                return code;
            }

            set
            {
                code = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public ProgrammingLanguage ProgrammingLanguage
        {
            get
            {
                return programmingLanguage;
            }

            set
            {
                programmingLanguage = value;
            }
        }

        public static implicit operator Script(string code)
        {
            if (code == null)
            {
                return null;
            }

            return new Script(code);
        }

        public static implicit operator string(Script script)
        {
            if (script == null)
            {
                return null;
            }

            return script.code;
        }

        public JsonObject ToJsonObject()
        {
            JsonObject result = new JsonObject();
            result.Add("_type", Core.Query.FullTypeName(this));

            if(code != null)
            {
                result.Add("Code", code);
            }

            if (name != null)
            {
                result.Add("Name", code);
            }

            result.Add("ProgrammingLanguage", programmingLanguage.ToString());

            return result;
        }
    }
}
