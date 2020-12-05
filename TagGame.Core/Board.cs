using System;

namespace TagGame.Core
{
    internal readonly struct Board
    {
        private readonly int[,] _map;
        private readonly int _size;

        public Board(int size)
        {
            _size = size;
            _map = new int[size, size];
        }

        private void Set(Tile tile, int value)
        {
            if (tile.IsOnBoard(_size))
                _map[tile.X, tile.Y] = value;
        }

        public int Get(Tile tile)
        {
            if (tile.X == GetZero().X && tile.Y == GetZero().Y || !tile.IsOnBoard(_size))
                return 0;
            return _map[tile.X, tile.Y];
        }

        public void Generate(int seed = 0)
        {
            var value = 0;
            for (var y = 0; y < _size; y++)
            for (var x = 0; x < _size; x++)
                Set(new Tile(x, y), ++value);
            Set(new Tile(_size), 0);
            
            Shuffle(seed);
        }

        private void Shuffle(int seed)
        {
            var random = new Random(seed);
            for (var i = 0; i < seed; i++)
                Shift(new Tile(random.Next(_size), random.Next(_size)));
        }

        public int Shift(Tile tile)
        {
            if (!tile.IsOnBoard(_size)) 
                return 0;

            var zero = GetZero();
            if (tile.X != zero.X && tile.Y != zero.Y)
                return 0;
            
            while (tile.X != GetZero().X)
                Shift(Math.Sign(tile.X - GetZero().X), 0);

            while (tile.Y != GetZero().Y)
                Shift(0,Math.Sign(tile.Y - GetZero().Y));
            
            var moves = Math.Abs(tile.X - zero.X) + Math.Abs(tile.Y - zero.Y);

            return moves;
        }

        private void Shift(int offsetX, int offsetY)
        {
            var zero = GetZero();
            var newZeroTile = zero.Shift(offsetX, offsetY);
            Set(zero, Get(newZeroTile));
            Set(newZeroTile, 0);
        }

        public Tile GetZero()
        {
            for (var y = 0; y < _size; y++)
            for (var x = 0; x < _size; x++)
                if (_map[x, y] == 0)
                    return new Tile(x, y);
            return new Tile(_size);
        }
    }
}