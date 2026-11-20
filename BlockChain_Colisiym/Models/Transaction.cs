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

        // Конструктор для создания транзакции
        public Transaction(string from, string to, decimal amount)
        {
            Id = Guid.NewGuid().ToString();
            From = from;
            To = to;
            Amount = amount;
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
    }
}
