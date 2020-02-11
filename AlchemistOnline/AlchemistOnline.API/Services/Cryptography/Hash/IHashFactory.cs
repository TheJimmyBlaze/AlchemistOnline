using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Services.Cryptography.Hash
{
    public interface IHashFactory
    {
        public byte[] BuildHash(string phrase);
        public bool ValidateString(string value, byte[] storableHash);
        public Task SimulateValidateAsync();
    }
}
