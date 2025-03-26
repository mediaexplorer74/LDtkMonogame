
using System;

using LDtk.Renderer;

using GameManager;
using GameManager.AABB;

using LDtkTypes;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameManager.Entities
{
    public class GunEntity
    {
        public Vector2 Position
        {
            get => data.Position;
            set => data.Position = value;
        }

        public bool Taken { get; set; }

        public Box Collider { get; set; } = new Box(Vector2.Zero, new Vector2(10, 16), data.Pivot);

        /*readonly*/
        static Gun_Pickup data;
        readonly Texture2D texture;
        readonly ExampleRenderer renderer;

        public GunEntity(Gun_Pickup data, Texture2D texture, ExampleRenderer renderer)
        {
            GunEntity.data = data; 
            this.texture = texture;
            this.renderer = renderer;
        }

        public void Update(float totalTime)
        {
            if (Taken)
            {
                return;
            }

            Position += new Vector2(0, -MathF.Sin(totalTime * 1.5f) * .1f);

            Collider.Position = Position;
        }

        public void Draw()
        {
            if (Taken)
            {
                return;
            }

            renderer.RenderEntity(data, texture);

            if (Game1.DebugF3)
            {
                renderer.SpriteBatch.DrawRect(Collider, new Color(128, 255, 0, 128));
            }
        }
    }
}
