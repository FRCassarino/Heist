using System;

namespace Heist
{
#if WINDOWS || XBOX
    static class Program
    {
		public static Game1 game;

        static void Main(string[] args)
        {
			game = new Game1();
            game.Run();
        }
    }
#endif
}