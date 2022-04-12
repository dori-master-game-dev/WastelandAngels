using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WLA.GameComponents.Sprites
{
    public sealed class Animation
    {
        public string Name { get; }

        public int FrameCount { get; }
        public float[] FrameDurations { get; }

        public int StartingIndex { get; }

        public float PlaybackSpeed { get; set; }

        public int CurrentFrame { get; private set; }

        public int FrameIndex
        {
            get
            {
                return CurrentFrame + StartingIndex;
            }
        }

        public bool Pause { get; set; }

        public Animation(string name, int frameCount, float[] frameDurations, int startingIndex, float playbackSpeed = 1f)
        {
            Name = name;

            FrameCount = frameCount;
            FrameDurations = frameDurations;

            StartingIndex = startingIndex;

            PlaybackSpeed = playbackSpeed;

            CurrentFrame = 0;
            timeLeft = FrameDurations[0];

            Pause = false;
        }

        private float timeLeft;

        public void Update(GameTime gameTime, float multiplier)
        {
            if (Pause)
            {
                return;
            }

            timeLeft -= PlaybackSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * multiplier;

            if (timeLeft <= 0)
            {
                NextFrame();
            }
        }

        public void ResetAnimation()
        {
            SelectFrame(0);
        }

        public void ResetDuration()
        {
            timeLeft = FrameDurations[CurrentFrame];
        }

        public void NextFrame()
        {
            ++CurrentFrame;
            CurrentFrame %= FrameCount;

            timeLeft = FrameDurations[CurrentFrame];
        }

        public void SelectFrame(int targetFrame)
        {
            if (targetFrame < 0 || targetFrame >= FrameCount)
            {
                return;
            }

            CurrentFrame = targetFrame;

            timeLeft = FrameDurations[CurrentFrame];
        }
    }
}
