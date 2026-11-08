using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain_Colisiym
{
    public  class Block
    {
        //нумерация блока
        public int Index { get; set; }

        //хеш текущего блока
        public string Hash { get; set; }

        //данные блока
        public string Data { get; set; }

        //хеш преведущего блока
        public string PreviousHash { get; set; }
        public DateTime TimeStamp { get; set; }

       public string Author { get; set; }
    }
}
