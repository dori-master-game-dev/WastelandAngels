using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WLA.System;
using WLA.Utilities;

namespace WLA.GameComponents.Sprites
{
    public class SpriteManager
    {
        public Spritesheet Sheet { get; }
        public Texture2D[] SheetSprites { get; }

        public bool Animated { get; }

        public int Count { get; }

        public int CurrentState { get; private set; }

        public float PlaybackSpeed { get; set; }

        public bool Pause
        {
            get
            {
                if (Animated)
                {
                    return animations[CurrentState].Pause;
                }

                return false;
            }

            set
            {
                if (Animated)
                {
                    animations[CurrentState].Pause = value;
                }
            }
        }

        private readonly int[] sprites;
        private readonly Animation[] animations;

        public SpriteManager(Spritesheet sheet, int count, int[] sprites, float playbackSpeed = 1f)
        {
            Sheet = sheet;
            SheetSprites = SpriteUtilities.CutSheet(ViewManager.graphics.GraphicsDevice, sheet);

            Animated = false;

            Count = count;

            this.sprites = sprites;

            PlaybackSpeed = playbackSpeed;
        }

        public SpriteManager(Spritesheet sheet, int count, Animation[] animations, float playbackSpeed = 1f)
        {
            Sheet = sheet;
            SheetSprites = SpriteUtilities.CutSheet(ViewManager.graphics.GraphicsDevice, sheet);

            Animated = true;

            Count = count;

            this.animations = animations;

            PlaybackSpeed = playbackSpeed;
        }

        public void Update(GameTime gameTime)
        {
            if (!Animated)
            {
                return;
            }

            animations[CurrentState].Update(gameTime, PlaybackSpeed);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Draw(spriteBatch, position, Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color tint)
        {
            if (Animated)
            {
                spriteBatch.Draw(SheetSprites[animations[CurrentState].FrameIndex], position, tint);
            }
            else
            {
                spriteBatch.Draw(SheetSprites[CurrentState], position, tint);
            }
        }

        public void ResetAnimation()
        {
            if (!Animated)
            {
                return;
            }

            animations[CurrentState].ResetAnimation();
        }

        public void ResetDuration()
        {
            if (!Animated)
            {
                return;
            }

            animations[CurrentState].ResetDuration();
        }

        public void NextState()
        {
            ++CurrentState;
            CurrentState %= Count;
        }

        public void SelectState(int targetState)
        {
            if (targetState < 0 || targetState >= Count)
            {
                return;
            }

            CurrentState = targetState;
        }
    }
}
