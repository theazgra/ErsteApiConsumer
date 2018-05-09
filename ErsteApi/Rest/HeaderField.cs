namespace ErsteApi.Rest
{
    struct HeaderField
    {
        string name;
        string value;

        internal HeaderField(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        internal string Name() => name;
        internal string Value() => value;
    }
}