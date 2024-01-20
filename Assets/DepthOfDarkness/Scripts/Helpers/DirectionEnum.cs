using System;

using UnityEngine;

namespace DD.Game {
    public enum Direction {
        RIGHT,
        UP,
        LEFT,
        DOWN,

        /*DOWNRIGHT,
        UPRIGHT,
        DOWNLEFT,
        UPLEFT,*/
    }

    public static class DirectionUtils {
        public static Direction Get4DirectionFromVector(Vector2 _vector) {
            float angle = Mathf.Atan2(_vector.y, _vector.x);
            int octant = (int)Mathf.Round(4 * angle / (2 * Mathf.PI) + 4) % 4;
            return (Direction)octant;
        }
        public static Direction Get8DirectionFromVector(Vector2 _vector) {
            float angle = Mathf.Atan2(_vector.y, _vector.x);
            int octant = (int)Mathf.Round(8 * angle / (2 * Mathf.PI) + 8) % 8;
            return (Direction)octant;
        }

        public static string ToPrettyString(this Direction _direction) {
            switch (_direction) {
                case Direction.UP:
                    return "Up";

                case Direction.RIGHT:
                    return "Right";

                case Direction.DOWN:
                    return "Down";

                case Direction.LEFT:
                    return "Left";


                /*case Direction.UPRIGHT:
                    return "UpRight";
                case Direction.UPLEFT:
                    return "UpLeft";
                case Direction.DOWNRIGHT:
                    return "DownRight";
                case Direction.DOWNLEFT:
                    return "DownLeft";*/

                default:
                    throw new ArgumentException($"Can't convert to pretty string {nameof(_direction)}: '{_direction}'");
            }
        }
    }
}