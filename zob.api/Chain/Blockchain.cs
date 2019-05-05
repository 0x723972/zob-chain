using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

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

        public string Hash(Block block)
        {
            var encodedBlock = JsonConvert.SerializeObject(block);
            using (var sha256 = SHA256.Create())
            {
                var computedHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(encodedBlock));
                var hash = BitConverter.ToString(computedHash).Replace("-", "").ToLower();
                return hash;
            }
        }

        public bool IsChainValid(List<Block> chain)
        {
            var previousBlock = chain[0];
            var blockIndex = 1;
            while (blockIndex < chain.Count)
            {
                var block = chain[blockIndex];
                if (block.PreviousHash != Hash(previousBlock)) return false;
                var proof = block.Proof;
                var previousProof = previousBlock.Proof;
                using (var sha256 = SHA256.Create())
                {
                    var hashOperation = sha256.ComputeHash(Encoding.UTF8.GetBytes($"{proof^2 - previousProof^2}"));
                    var hash = BitConverter.ToString(hashOperation).Replace("-", "").ToLower();
                    if (!hash.StartsWith("0000")) return false;
                    previousBlock = block;
                    blockIndex++;
                }
            }
            return true;
        }
    }
}