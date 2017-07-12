﻿namespace Blockchain
{
    public class Miner
    {
        public Blockchain Blockchain { get; }
        private IBlockFactory _blockFactory;
        public Miner Before;
        public Miner Next;

        public Miner(IBlockFactory blockFactory)
        {
            _blockFactory = blockFactory;
            var genesisBlock = _blockFactory.GenerateGenesisBlock();
            Blockchain = new Blockchain(genesisBlock);
        }

        public Miner(IBlockFactory blockFactory, Miner minerBefore)
        {
            _blockFactory = blockFactory;
            var genesisBlock = _blockFactory.GenerateGenesisBlock();
            Blockchain = new Blockchain(genesisBlock);

            FixPointers(minerBefore);
        }

        private void FixPointers(Miner minerBefore)
        {
            Before = minerBefore;
            Next = minerBefore.Next;
            if (minerBefore.Next != null)
            {
                minerBefore.Next.Before = this;
            }
            minerBefore.Next = this;
        }

        public void AddBlock(string data)
        {
            var newBlock = _blockFactory.GenerateNextBlock(Blockchain.GetLastBlock(), data);
            Blockchain.AddBlock(newBlock);
        }
    }
}