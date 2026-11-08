using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain_Colisiym
{
    public class HashingService
    {
        public string ComputeBlockHash(Block block)
        {
            //формируем строку из данных блока для хеширования
            var blockData = $"{block.Index}{block.PreviousHash}{block.TimeStamp}{block.Data}{block.Author}";
            return ComputeHash(blockData);
        }

        private string ComputeHash(string data)
     {
            //используем SHA256 для вычисления хеша
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            var hashBytes = sha256.ComputeHash(bytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
