namespace Zivs.ScriptRunner.DataEncoding
{
    public interface IEncodingStrategy
    {
        string Encode(string data);

        string Decode(string data);
    }
}
