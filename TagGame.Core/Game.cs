namespace TagGame.Core
{
    public class Game
    {
        private readonly int _size;
        private readonly Board _board;

        public int Moves { get; private set; }

        public Game(int size = 4)
        {
            _size = size;
            _board = new Board(size);
        }

        public void Start(int seed = 0)
        {
            _board.Generate(seed);
            Moves = 0;
        }

        public int PressAt(int x, int y)
        {
            return PressAt(new Tile(x, y));
        }

        private int PressAt(Tile tile)
        {
            var moves = _board.Shift(tile);
            Moves += moves;

            return moves;
        }

        public int GetDigitAt(int x, int y)
        {
            return GetDigitAt(new Tile(x, y));
        }

        private int GetDigitAt(Tile tile)
        {
            return _board.Get(tile);
        }

        public bool IsSolved()
        {
            if (_board.GetZero().X != _size - 1 && _board.GetZero().Y != _size - 1)
                return false;
            
            var digit = 0;
            for (var y = 0; y < _size; y++)
            for (var x = 0; x < _size; x++)
                if (_board.Get(new Tile(x, y)) == 0)
                    return x == _size - 1 && y == _size - 1;
                else if (_board.Get(new Tile(x, y)) != ++digit)
                    return false;

            return true;
        }
    }
}