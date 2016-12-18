﻿// Copyright (c) Aura development team - Licensed under GNU GPL
// For more information, see license file in the main folder

namespace Aura.Channel.World.Dungeons.Generation
{
	public static class Direction
	{
		public const int Up = 0;
		public const int Right = 1;
		public const int Down = 2;
		public const int Left = 3;

		public static int GetOppositeDirection(int dir)
		{
			if (dir == Direction.Up)
				return Direction.Down;
			else if (dir == Direction.Right)
				return Direction.Left;
			else if (dir == Direction.Down)
				return Direction.Up;
			else if (dir == Direction.Left)
				return Direction.Right;
			else
				return -1;
		}
	}
}
