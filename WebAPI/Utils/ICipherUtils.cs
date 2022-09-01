namespace WebAPI.Utils
{
    public interface ICipherUtils
    {
        string Encrypt(string cipherText);
        string Decrypt(string cipherText);
    }
}
