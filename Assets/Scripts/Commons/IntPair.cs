﻿using System;
using Map.Model;

namespace Commons
{
    [Serializable]
    public struct IntPair {
        public int x;
        public int y;

        public IntPair(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public IntPair(IntPair other)
        {
            this.x = other.x;
            this.y = other.y;
        }

        public override string ToString()
        {
            return "(" + this.x + ", " + this.y + ")";
        }

        public override bool Equals(object o)
        {
            if (!(o is IntPair))
            {
                return false;
            }

            var other = (IntPair)o;
            return this == other;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash *= 23 + x;
            hash *= 23 + y;
            return hash;
        }

        public bool Equals(IntPair other)
        {
            return this == other;
        }

        public static bool operator ==(IntPair first, IntPair second)
        {
            return first.x == second.x && first.y == second.y;
        }

        public static bool operator !=(IntPair first, IntPair second)
        {
            return !(first == second);
        }

        public static IntPair operator +(IntPair first, IntPair second)
        {
            return new IntPair(first.x + second.x, first.y + second.y);
        }

        public static IntPair operator -(IntPair first, IntPair second)
        {
            return new IntPair(first.x - second.x, first.y - second.y);
        }

        public IntPair Move(Direction direction)
        {
            return Move(direction, 1);
        }

        public IntPair Move(Direction direction, int positions)
        {
            int intDirection = (int)direction;
            int xPosition = (intDirection & 0x1) * (intDirection - 2) * positions;
            int yPosition = ((intDirection + 1) & 0x1) * (intDirection - 1) * positions;
            return this + new IntPair(xPosition, yPosition);
        }

    }
}
