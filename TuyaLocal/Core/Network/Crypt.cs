namespace TuyaLocal.Core.Network
{
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class Crypt
    {
        public Crypt(string key)
        {
            Key = key;
        }

        private string Key { get; }
    }
}
