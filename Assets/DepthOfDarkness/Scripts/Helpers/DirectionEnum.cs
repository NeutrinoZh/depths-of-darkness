using System.ComponentModel;
using ModestTree;
using UnityEngine;

namespace DD.Game {
    public enum Direction {
        RIGHT,
        UPRIGHT,
        UP,
        UPLEFT,
        LEFT,
        DOWNLEFT,
        DOWN,
        DOWNRIGHT,
    }

    public static class DirectionUtils {
        public static Direction GetDirectionFromVector(Vector2 _vector) {
            float angle = Mathf.Atan2(_vector.y, _vector.x);
            int octant = (int)Mathf.Round(8 * angle / (2*Mathf.PI) + 8) % 8;
            return (Direction)octant;
        }

        public static string ToPrettyString(this Direction _direction) {
            switch (_direction) {
                case Direction.UPLEFT:
                    return "UpLeft";
                case Direction.UP:
                    return "Up";
                case Direction.UPRIGHT:
                    return "UpRight";

                case Direction.RIGHT:
                    return "Right";

                case Direction.DOWNRIGHT:
                    return "DownRight";
                case Direction.DOWN:
                    return "Down";
                case Direction.DOWNLEFT:
                    return "DownLeft";

                case Direction.LEFT:
                    return "Left";

                default:
                    throw new InvalidEnumArgumentException($" Direction: '{_direction}'");
            }
        }
    }
}