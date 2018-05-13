using RestSharp;

namespace ErsteApi.Rest
{
    struct RestParameter
    {
        string name;
        string value;
        ParameterType parameterType;

        internal RestParameter(string name, string value, ParameterType parameterType = ParameterType.UrlSegment)
        {
            this.name = name;
            this.value = value;
            this.parameterType = parameterType;
        }

        internal string Name() => name;
        internal string Value() => value;
        internal ParameterType ParamType() => parameterType;
    }
}
