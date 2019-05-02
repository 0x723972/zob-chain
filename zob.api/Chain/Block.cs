using System;

namespace zob.api.Chain
{
    public class Block
    {
        public int Index;
        public DateTime Timestamp;
        public int Proof;
        public string PreviousHash;
        public string Data;
    }
}