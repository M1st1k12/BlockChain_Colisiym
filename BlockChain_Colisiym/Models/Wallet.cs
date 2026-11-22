using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain_Colisiym.Models
{
    public class Wallet
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public byte[] PublicKey { get; set; }

        private byte[] PrivateKey { get; }

        public Wallet(string name, string address, byte[] publicKey, byte[] privateKey)
        {
            Name = name;
            Address = address;
            PublicKey = publicKey;
            PrivateKey = privateKey;
        }

        // Метод для подписи данных с помощью приватного ключа
        public byte[] Sign(byte[] data)
        {
            using var ecdsa = ECDsa.Create();
            ecdsa.ImportECPrivateKey(PrivateKey, out _); // Считывание приватного ключа
            return ecdsa.SignData(data, HashAlgorithmName.SHA256);
        }
    }
}
