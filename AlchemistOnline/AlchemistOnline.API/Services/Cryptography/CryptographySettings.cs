using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Services.Cryptography
{
    public class CryptographySettings
    {
        public const string CRYPTO_SECTION = "Cryptography";

        public string TokenSecret { get; set; }
        public int TokenHoursToLive { get; set; }

        public int MillisecondsToSpendHashing { get; set; }
        public string HashPepper { get; set; }

        public byte[] HashPepperBytes
        {
            get
            {
                if (string.IsNullOrEmpty(HashPepper))
                    return null;
                return Encoding.ASCII.GetBytes(HashPepper);
            }
        }
    }
}
