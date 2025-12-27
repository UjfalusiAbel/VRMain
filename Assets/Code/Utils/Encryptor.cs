using System.Text;

namespace VRMain.Assets.Code.Utils
{
    public class Encryptor
    {
        private const string _key = "6F0LOqGhMoo0JSIj";

        public string EncryptDecrypt(string data)
        {
            var result = new StringBuilder(data.Length);

            for (int i = 0; i < data.Length; i++)
            {
                char encryptedChar = (char)(data[i] ^ _key[i % _key.Length]);
                result.Append(encryptedChar);
            }

            return result.ToString();
        }
    }
}
