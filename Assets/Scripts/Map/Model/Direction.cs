﻿using UnityEngine;
using System.Collections;

namespace Model
{
    public enum Direction
    {
        NORTH = 0, EAST, SOUTH, WEST
    }

    public class Utils
    {
        public static Direction[] GetAllDirections()
        {
            return (Direction[])System.Enum.GetValues(typeof(Direction));
        }

        public static Direction[] GetAllDirectionsExcluding(params Direction[] exclusions)
        {
            if(exclusions == null)
            {
                return GetAllDirections();
            }

            Direction[] directions = new Direction[4 - exclusions.Length];
            int directionCount = 0;
            foreach (Direction direction in GetAllDirections())
            {
                if (System.Array.IndexOf(exclusions, direction) < 0)
                {
                    directions[directionCount++] = direction;
                }
            }

            return directions;
        }

        public static Direction Reverse(Direction direction)
        {
            return (Direction)(((int)direction + 2) % 4);
        }
    }
}