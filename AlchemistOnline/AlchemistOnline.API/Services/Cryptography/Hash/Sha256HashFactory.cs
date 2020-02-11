using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Services.Cryptography.Hash
{
    public class Sha256HashFactory: IHashFactory
    {
        private const int STORABLE_HASH_LENGTH = 256;

        private readonly CryptographySettings settings;

        public Sha256HashFactory(CryptographySettings settings)
        {
            this.settings = settings;
        }

        private byte[] GenerateSalt(int length)
        {
            using RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();

            byte[] salt = new byte[length];
            random.GetBytes(salt);

            return salt;
        }

        private byte[] AddSeasoning(byte[] phrase, byte[] salt)
        {
            byte[] seasonedPhrase = new byte[phrase.Length + salt.Length + settings.HashPepperBytes.Length];

            Array.Copy(settings.HashPepperBytes, seasonedPhrase, settings.HashPepperBytes.Length);
            Array.Copy(phrase, 0, seasonedPhrase, settings.HashPepperBytes.Length, phrase.Length);
            Array.Copy(salt, 0, seasonedPhrase, settings.HashPepperBytes.Length + phrase.Length, salt.Length);

            return seasonedPhrase;
        }

        private byte[] GetStorableHash(byte[] hash, byte[] salt, int hashingIterations)
        {
            HashMetaData metaData = new HashMetaData()
            {
                HashLength = hash.Length,
                HashingIterations = hashingIterations
            };

            byte[] metaBytes = metaData.GetBytes();
            byte[] combination = new byte[metaBytes.Length + hash.Length + salt.Length];

            Array.Copy(metaBytes, combination, metaBytes.Length);
            Array.Copy(hash, 0, combination, metaBytes.Length, hash.Length);
            Array.Copy(salt, 0, combination, metaBytes.Length + hash.Length, salt.Length);

            return combination;
        }

        public byte[] BuildHash(string phrase)
        {
            using SHA256 sha = SHA256.Create();

            byte[] phraseBytes = Encoding.ASCII.GetBytes(phrase);
            int hashSizeInBytes = sha.HashSize / 8;
            byte[] salt = GenerateSalt(STORABLE_HASH_LENGTH - hashSizeInBytes - Marshal.SizeOf(new HashMetaData()));

            byte[] hash = AddSeasoning(phraseBytes, salt);

            DateTime stopHashing = DateTime.UtcNow + new TimeSpan(0, 0, 0, 0, settings.MillisecondsToSpendHashing);
            int hashingIterations = 0;
            while (DateTime.UtcNow < stopHashing)
            {
                hash = sha.ComputeHash(hash);
                hashingIterations++;
            }

            return GetStorableHash(hash, salt, hashingIterations);
        }

        public bool ValidateString(string value, byte[] storableHash)
        {
            using SHA256 sha = SHA256.Create();

            int metaDataLength = Marshal.SizeOf(new HashMetaData());
            HashMetaData metaData = new HashMetaData(storableHash.Take(metaDataLength).ToArray());

            byte[] salt = storableHash.Skip(metaDataLength + metaData.HashLength).ToArray();

            byte[] valueBytes = Encoding.ASCII.GetBytes(value);
            byte[] valueHash = AddSeasoning(valueBytes, salt);

            for (int i = 0; i < metaData.HashingIterations; i++)
            {
                valueHash = sha.ComputeHash(valueHash);
            }

            byte[] comparableHash = GetStorableHash(valueHash, salt, metaData.HashingIterations);
            if (comparableHash.SequenceEqual(storableHash))
                return true;
            return false;
        }

        public Task SimulateValidateAsync()
        {
            return Task.Delay(settings.MillisecondsToSpendHashing);
        }
    }
}
