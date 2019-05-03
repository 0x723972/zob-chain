using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

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
            var newProof = 1;
            var checkProof = false;
            while (!checkProof)
            {
                //var hashOperation = SHA256.Create();
                using (var sha256 = SHA256.Create())
                {
                    var hashOperation = sha256.ComputeHash(Encoding.UTF8.GetBytes($"{newProof^2 - previousProof^2}"));
                    var hash = BitConverter.ToString(hashOperation).Replace("-", "").ToLower();
                    if (hash.StartsWith("0000"))
                    {
                        checkProof = true;
                    }
                    else
                    {
                        newProof++;
                    }
                }
            }

        }
    }
}