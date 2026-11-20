using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain_Colisiym.Models
{
    public  class Block
    {
        //нумерация блока
        public int Index { get; set; }

        //хеш текущего блока
        public string Hash { get; set; }

        //данные блока
        //public string Data { get; set; }
        public string Data { get; set; }

        // Список транзакций, которые содержатся в блоке
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        //хеш преведущего блока
        public string PreviousHash { get; set; }
        public DateTime TimeStamp { get; set; }

       public string Author { get; set; }
        // Поле для хранения nonce, используемого в процессе майнинга
        public int Nonce { get; set; } 

        // Длительность майнинга блока
        public decimal MiningDuration { get; set; }

        public int DifficultyMining { get; set; } 

    }
}
