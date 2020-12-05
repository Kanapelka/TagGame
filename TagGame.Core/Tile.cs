namespace TagGame.Core
{
    internal readonly struct Tile
    {
        public int X { get; }
        public int Y { get; }

        public Tile(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Tile(int size)
        {
            X = size - 1;
            Y = size - 1;
        }

        public bool IsOnBoard(int size)
        {
            return X >= 0 && X < size && Y >= 0 && Y < size;
        }

        public Tile Shift(int offsetX, int offsetY)
        {
            return new Tile(X + offsetX, Y + offsetY);
        }
    }
}