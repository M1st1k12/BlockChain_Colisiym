using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BlockChain_Colisiym.Models;

namespace BlockChain_Colisiym.Services
{
    public class WalletService
    {
        public Wallet CreateWallet(string name)
        {
            // Генерация пары ключей (публичного и приватного)
            using var ecdsa = ECDsa.Create();
            var privateKey = ecdsa.ExportECPrivateKey(); // Считывание приватного ключа
            var publicKey = ecdsa.ExportSubjectPublicKeyInfo(); // Считывание публичного ключа
            // Генерация адреса на основе публичного ключа
            var address = GetAddress(publicKey); // Сокращение адреса до 10 символов
            return new Wallet(name, address, publicKey, privateKey);
        }

        private string GetAddress(byte[] publicKey)
        {
            byte[] hash = SHA256.Create().ComputeHash(publicKey);
            return Convert.ToHexString(hash).ToLower()[..10];
        }

        public bool VerifySignature(byte[] data, byte[] signature, byte[] publicKey)
        {
            using var ecdsa = ECDsa.Create(); // Создание нового экземпляра ECDsa для проверки подписи
            ecdsa.ImportSubjectPublicKeyInfo(publicKey, out _); // Считывание публичного ключа
            return ecdsa.VerifyData(data, signature, HashAlgorithmName.SHA256); // Проверка подписи данных с помощью публичного ключа
        }
    }
}
