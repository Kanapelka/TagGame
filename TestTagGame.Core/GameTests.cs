using System;
using NUnit.Framework;
using TagGame.Core;

namespace TestTagGame.Core
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void TestGameIsSolvedWhenStartedWithoutSeed()
        {
            var game = new Game();
            game.Start();

            Assert.True(game.IsSolved());
            Assert.AreEqual(0, game.Moves);
        }
        
        [Test]
        public void TestGameIsShuffledWhenStartedWithSeed()
        {
            var game = new Game();
            game.Start(1024 + DateTime.Now.DayOfYear);

            Assert.False(game.IsSolved());
            Assert.AreEqual(0, game.Moves);
        }

        [Test]
        public void TestGameIsNotSolvedWhenStartedWithoutSeedAndShifted()
        {
            var game = new Game();
            game.Start();
            game.PressAt(2, 3);

            Assert.False(game.IsSolved());
            Assert.AreEqual(1, game.Moves);
        }

        [Test]
        public void TestTilesIsLastRowShiftedHorizontally()
        {
            var game = new Game();
            game.Start();
            game.PressAt(3, 3);
            game.PressAt(0, 3);
            
            Assert.AreEqual(0, game.GetDigitAt(0, 3));
            Assert.AreEqual(13, game.GetDigitAt(1, 3));
            Assert.AreEqual(14, game.GetDigitAt(2, 3));
            Assert.AreEqual(15, game.GetDigitAt(3, 3));
            Assert.AreEqual(3, game.Moves);
        }
        
        [Test]
        public void TestTilesIsLastColumnShiftedVertically()
        {
            var game = new Game();
            game.Start();
            game.PressAt(3, 3);
            game.PressAt(3, 0);
            
            Assert.AreEqual(0, game.GetDigitAt(3, 0));
            Assert.AreEqual(4, game.GetDigitAt(3, 1));
            Assert.AreEqual(8, game.GetDigitAt(3, 2));
            Assert.AreEqual(12, game.GetDigitAt(3, 3));
            Assert.AreEqual(3, game.Moves);
        }
        
        [Test]
        public void TestTilesNotShiftedWhenPressedOnZero()
        {
            var game = new Game();
            game.Start();
            game.PressAt(3, 3);
            
            Assert.AreEqual(0, game.GetDigitAt(3, 3));
            Assert.AreEqual(0, game.Moves);
        }
        
        [Test]
        public void TestTilesNotShiftedWhenPressedOutTheBoard()
        {
            var game = new Game();
            game.Start();
            game.PressAt(-1, -1);
            
            Assert.AreEqual(0, game.GetDigitAt(3, 3));
            Assert.AreEqual(0, game.Moves);
        }
        
        [Test]
        public void TestTilesNotShiftedWhenPressedDiagonally()
        {
            var game = new Game();
            game.Start();
            game.PressAt(2, 2);
            
            Assert.AreEqual(0, game.GetDigitAt(3, 3));
            Assert.AreEqual(0, game.Moves);
        }
    }
}