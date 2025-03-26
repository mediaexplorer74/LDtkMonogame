

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameManager.AABB
{
    public static class SpriteBatchExtensions
    {
        static Texture2D pixelTexture;

        public static void DrawRect(this SpriteBatch spriteBatch, Box rect, Color color)
        {
            CreatePixelTexture(spriteBatch);
            spriteBatch.Draw(pixelTexture, rect.Position, null, color, 0, rect.Pivot,
                rect.Size, SpriteEffects.None, 0);
        }

        public static void DrawPoint(this SpriteBatch spriteBatch, Vector2 point, Color color)
        {
            CreatePixelTexture(spriteBatch);
            spriteBatch.Draw(pixelTexture, point, null, color, 0, Vector2.Zero,
                Vector2.One, SpriteEffects.None, 0);
        }

        static void CreatePixelTexture(SpriteBatch spriteBatch)
        {
            if (pixelTexture == null)
            {
                pixelTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                pixelTexture.SetData(new byte[] { 0xff, 0xff, 0xff, 0xFF });
            }
        }
    }
}
