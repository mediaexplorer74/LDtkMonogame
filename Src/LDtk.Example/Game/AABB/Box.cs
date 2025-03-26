

using System;
using System.Diagnostics.CodeAnalysis;

using Microsoft.Xna.Framework;

namespace GameManager.AABB
{
    public class Box(Vector2 position, Vector2 size, Vector2 pivot)
    {
        public Vector2 Position { get; set; } = position;

        public Vector2 Size { get; set; } = size;

        public Vector2 Pivot { get; set; } = pivot;

        public Vector2 TopLeft => Position - (Size * Pivot);

        public Vector2 BottomRight => Position + (Size * (Vector2.One - Pivot));

        public bool Contains(Vector2 point)
        {
            return point.X >= TopLeft.X && point.X <= BottomRight.X && point.Y >= TopLeft.Y && point.Y <= BottomRight.Y;
        }

        public bool Contains(Box rect)
        {
            return Contains(rect.TopLeft)
                        || Contains(rect.BottomRight)
                        || Contains(new Vector2(rect.TopLeft.X, rect.BottomRight.Y))
                        || Contains(new Vector2(rect.BottomRight.X, rect.TopLeft.Y));
        }

        [SuppressMessage("Usage", "CA1021:Avoid out parameters", Justification = "This is a try like method")]
        public bool RayCast(Vector2 rayOrigin, Vector2 rayDirection, out Vector2 contactPoint, out Vector2 contactNormal, out float hitNear)
        {
            hitNear = 0;
            contactPoint = default;
            contactNormal = default;

            Vector2 invdir = new(1f / rayDirection.X, 1f / rayDirection.Y);

            Vector2 near = (TopLeft - rayOrigin) * invdir;
            Vector2 far = (BottomRight - rayOrigin) * invdir;

            if (float.IsNaN(near.X) || float.IsNaN(near.Y) || float.IsNaN(far.X) || float.IsNaN(far.Y))
            {
                return false;
            }

            // Sort near and fars x and y
            if (near.X > far.X)
            {
                (far.X, near.X) = (near.X, far.X);
            }

            if (near.Y > far.Y)
            {
                (far.Y, near.Y) = (near.Y, far.Y);
            }

            if (near.X > far.Y || near.Y > far.X)
            {
                return false;
            }

            hitNear = MathF.Max(near.X, near.Y);
            float hitfar = MathF.Min(far.X, far.Y);

            if (hitfar < 0)
            {
                return false;
            }

            contactPoint = rayOrigin + (hitNear * rayDirection);

            if (near.X > near.Y)
            {
                if (invdir.X < 0)
                {
                    contactNormal = new Vector2(1, 0);
                }
                else
                {
                    contactNormal = new Vector2(-1, 0);
                }
            }
            else if (invdir.Y < 0)
            {
                contactNormal = new Vector2(0, 1);
            }
            else
            {
                contactNormal = new Vector2(0, -1);
            }

            return true;
        }

        [SuppressMessage("Usage", "CA1021:Avoid out parameters", Justification = "This is a try like method")]
        public bool Cast(Vector2 direction, Box target, out Vector2 contactPoint, out Vector2 contactNormal, out float hitNear, float deltaTime)
        {
            contactPoint = default;
            contactNormal = default;
            hitNear = float.PositiveInfinity;

            if (direction.X == 0 && direction.Y == 0)
            {
                return false;
            }

            Box expandedTarget = new(target.Position - (Size / 2f), target.Size + Size, target.Pivot);

            return expandedTarget.RayCast(Position, direction * deltaTime, out contactPoint, out contactNormal, out hitNear) && hitNear >= 0f && hitNear < 1f;
        }
    }
}
