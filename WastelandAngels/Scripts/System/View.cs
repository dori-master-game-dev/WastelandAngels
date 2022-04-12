using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WLA.GameComponents;

namespace WLA.System
{
    public abstract class View
    {
        public Vector2 Position { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public Color BaseColor { get; set; }

        public bool Paused { get; private set; }
        public bool Transparent { get; private set; }

        public int Opacity{ get; private set; }

        private RenderTarget2D view;

        public event EventHandler<WLAViewArgs> RaiseViewEvent;

        public View(Vector2 position, int width, int height, Color baseColor)
        {
            Position = position;

            Width = width;
            Height = height;

            BaseColor = baseColor;
        }

        public virtual void Initialize()
        {
            view = new RenderTarget2D(ViewManager.graphics.GraphicsDevice, Width, Height);

            Paused = false;
            Transparent = false;

            Opacity = 255;
        }

        public virtual void LoadContent()
        {

        }

        public virtual void UnloadContent()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            if (Paused)
                return;

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public RenderTarget2D GetRenderTarget(SpriteBatch spriteBatch)
        {
            ViewManager.graphics.GraphicsDevice.SetRenderTarget(view);
            ViewManager.graphics.GraphicsDevice.Clear(Color.Transparent);
            if (Transparent)
            {
                return view;
            }

            ViewManager.graphics.GraphicsDevice.Clear(BaseColor);

            Draw(spriteBatch);

            return view;
        }

        public void EditViews(EventCommands command, View targetView)
        {
            OnViewEvent(this, new WLAViewArgs(command, targetView));
        }

        protected virtual void OnViewEvent(object sender, WLAViewArgs e)
        {
            RaiseViewEvent?.Invoke(sender, e);
        }
    }

    public enum EventCommands { None, AddView, RemoveView, RemoveAddView }

    public class WLAViewArgs : EventArgs
    {
        public EventCommands Command { get; set; }

        public View TargetView { get; set; }

        public WLAViewArgs(EventCommands command, View targetView)
        {
            Command = command;

            TargetView = targetView;
        }
    }
}
