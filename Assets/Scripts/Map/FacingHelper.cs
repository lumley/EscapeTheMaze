using Map.Model;
using UnityEngine;

namespace Map
{
    public class FacingHelper
    {
        public static Direction GetFacingDirection(Transform transform)
        {
            if (IsPlayerFacingIntoTheDirectionOf(transform, Vector3.forward))
            {
                return Direction.NORTH;
            }
            if (IsPlayerFacingIntoTheDirectionOf(transform, Vector3.back))
            {
                return Direction.SOUTH;
            }
            if (IsPlayerFacingIntoTheDirectionOf(transform, Vector3.right))
            {
                return Direction.EAST;
            }
            if (IsPlayerFacingIntoTheDirectionOf(transform, Vector3.left))
            {
                return Direction.WEST;
            }
            throw new InvalidDirectionException();
        }

        public static Vector3 GetDirectionVector(Direction direction)
        {
            switch (direction)
            {
                case Direction.NORTH: return Vector3.forward;
                case Direction.EAST: return Vector3.right;
                case Direction.SOUTH: return Vector3.back;
                case Direction.WEST: return Vector3.left;
                default: throw new InvalidDirectionException();
            }
        }

        public static Vector3 GetRotationVector(Direction direction)
        {
            switch (direction)
            {
                case Direction.NORTH: return Vector3.zero;
                case Direction.EAST: return Vector3.up * 90;
                case Direction.SOUTH: return Vector3.up * 180;
                case Direction.WEST: return Vector3.up * -90;
                default: throw new InvalidDirectionException();
            }
        }

        private static bool IsPlayerFacingIntoTheDirectionOf(Transform transform, Vector3 direction)
        {
            float angle = Vector3.Angle(transform.forward, direction);
            return angle >= -45.0f && angle <= 45.0f;
        }

        public class InvalidDirectionException : System.Exception
        {
            public InvalidDirectionException() : base() { }
            public InvalidDirectionException(string message) : base(message) { }
            public InvalidDirectionException(string message, System.Exception inner) : base(message, inner) { }


            protected InvalidDirectionException(System.Runtime.Serialization.SerializationInfo info,
                                                System.Runtime.Serialization.StreamingContext context)
            { }
        }
    }
}
