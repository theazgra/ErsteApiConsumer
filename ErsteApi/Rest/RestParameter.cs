namespace ErsteApi.Rest
{
    struct RestParameter
    {
        string name;
        string value;

        internal RestParameter(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        internal string Name() => name;
        internal string Value() => value;
    }
}