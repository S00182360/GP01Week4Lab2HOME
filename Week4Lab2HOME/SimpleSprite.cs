using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprites
{
    public class SimpleSprite
    {
        public Texture2D Image;
        public Vector2 Position;
        public Rectangle BoundingRect;
        public bool Visible = true;
        public string text = "Here I am";

        public SimpleSprite(Texture2D spriteImage,
                            Vector2 startPosition)
        {
            Image = spriteImage;
            Position = startPosition;
            BoundingRect = new Rectangle((int)startPosition.X, (int)startPosition.Y, Image.Width, Image.Height);

        }

        public bool InCollision(SimpleSprite other)
        {
            return BoundingRect.Intersects(other.BoundingRect);
        }

        public void draw(SpriteBatch sp)
        {
            if (Visible)
                sp.Draw(Image, Position, Color.White);
        }
        public void draw(SpriteBatch sp, SpriteFont font)
        //text draw on sprite
        {
            if (Visible)
            {
                Vector2 textSize = font.MeasureString(text);
                int ypos = (int)Position.Y - 20;
                int xpos = (int)BoundingRect.Center.ToVector2().X - (int)textSize.X / 2;
                sp.Draw(Image, Position, Color.White);
                sp.DrawString(font, text, new Vector2(xpos, ypos), Color.White);
            }

        }
        public void draw(SpriteBatch sp, float scale)
        //scalled draw, pass in scale
        {
            if (Visible)
                sp.Draw(Image, Position * scale,   //Position*scale for smaller viewport
                            null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
        }
        public void draw(SpriteBatch sp, Rectangle bound)
        //normal draw
        {
            if (Visible)
                sp.Draw(Image, bound, Color.White);
        }
        public void Move(Vector2 delta)
        //movement
        {
            Position += delta;
            BoundingRect = new Rectangle((int)Position.X,
                (int)Position.Y, Image.Width, Image.Height);
            BoundingRect.X = (int)Position.X;
            BoundingRect.Y = (int)Position.Y;
        }
    }
}
