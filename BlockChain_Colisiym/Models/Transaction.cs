using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain_Colisiym.Models
{
    public class Transaction
    {
        public string Id { get; set; }
        // Отправитель транзакции
        public string From { get; set; }
        // Получатель транзакции
        public string To { get; set; }
        // Сумма транзакции
        public decimal Amount { get; set; }
        // Дата и время создания транзакции
        public DateTime Timestamp { get; set; }

        // Публичный ключ отправителя транзакции
        public byte[] PublicKey { get; set; }

        // Подпись транзакции, созданная отправителем с помощью приватного ключа
        public byte[] Signature { get; set; }

        // Конструктор для создания транзакции
        public Transaction(string from, string to, decimal amount)
        {
            Id = Guid.NewGuid().ToString();
            From = from;
            To = to;
            Amount = amount;
            Timestamp = DateTime.Now;
        }

        public Transaction(string from, string to, decimal amount, byte[] publicKey)
        {
            Id = Guid.NewGuid().ToString();
            From = from;
            To = to;
            Amount = amount;
            PublicKey = publicKey;
            Timestamp = DateTime.Now;
        }

        // Метод для получения строкового представления транзакции в виде "Id|From|To|Amount|Timestamp"
        public string ToRawString()
        {
            return $"{Id}|{From}|{To}|{Amount}|{Timestamp}";
        }

        // Метод для получения строкового представления транзакции в виде "Transaction ID: {Id}, From: {From}, To: {To}, Amount: {Amount}, Timestamp: {Timestamp}"
        public override string ToString()
        {
            return $"Transaction ID: {Id}, From: {From}, To: {To}, Amount: {Amount}, Timestamp: {Timestamp}";
        }

        // Метод для получения данных транзакции в виде массива байтов, которые будут подписаны отправителем
        public byte[] GetDataToSign()
        {
            string dataToSign = $"{From}|{To}|{Amount}|{Timestamp}";
            return Encoding.UTF8.GetBytes(dataToSign);
        }
    }
}
