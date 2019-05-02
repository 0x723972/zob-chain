using System;
using System.Collections.Generic;

namespace zob.api.Chain
{

    public class Blockchain
    {
        private List<Block> _chain;

        public Blockchain()
        {
            _chain = new List<Block>();
            CreateBlock(proof: 1, previousHash: "0");
        }

        public Block CreateBlock(int proof, string previousHash)
        {
            var block = new Block
            {
                Index = _chain.Count + 1,
                Timestamp = DateTime.UtcNow,
                Proof = proof,
                PreviousHash = previousHash
            };

            _chain.Add(block);
            return block;
        }

        public Block GetLastBlock()
        {
            return _chain[_chain.Count - 1];
        }

        public void ProofOfWork(int previousProof)
        {

        }
    }
}