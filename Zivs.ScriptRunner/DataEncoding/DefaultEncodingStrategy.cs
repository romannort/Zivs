namespace Zivs.ScriptRunner.DataEncoding
{
    /// <summary>
    /// Null object for <see cref="IEncodingStrategy"/> interface.
    /// Does nothing to the data.
    /// </summary>
    internal class DefaultEncodingStrategy: IEncodingStrategy
    {
        public string Encode(string data)
        {
            return data;
        }

        public string Decode(string data)
        {
            return data;
        }
    }
}
