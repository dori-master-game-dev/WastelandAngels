using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using WLA.GameComponents;
using WLA.System.IO;

namespace WLA.System
{
    class ViewManager : Game
    {
        public static GraphicsDeviceManager graphics;
        public static ContentManager content;
        private SpriteBatch spriteBatch;

        private Configuration config;

        protected int VirtualWidth { get; set; }
        protected int VirtualHeight { get; set; }

        protected int TargetWidth { get; set; }
        protected int TargetHeight { get; set; }

        private List<View> views;

        private int size;
        private Vector2 offset;

        public ViewManager()
        {
            graphics = new GraphicsDeviceManager(this) { GraphicsProfile = GraphicsProfile.HiDef };
            Content.RootDirectory = "Content";
            content = Content;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            config = new Configuration();
            if (!Serializer.Exists("config.xml"))
            {
                config.Width = (int)Constants.WIDTH;
                config.Height = (int)Constants.HEIGHT;

                config.Save<Configuration>("config.xml");
            }
            else
            {
                config = Serializer.Load<Configuration>("config.xml");
            }

            VirtualWidth = config.Width;
            VirtualHeight = config.Height;

            size = (int)Math.Max(VirtualWidth / Constants.WIDTH, VirtualHeight / Constants.HEIGHT) * 2;

            TargetWidth = (int)Constants.WIDTH * size;
            TargetHeight = (int)Constants.HEIGHT * size;

            offset = new Vector2(VirtualWidth - TargetWidth, VirtualHeight - TargetHeight) / 2f;

            graphics.PreferredBackBufferWidth = VirtualWidth;
            graphics.PreferredBackBufferHeight = VirtualHeight;

            graphics.ApplyChanges();

            InputManager.InitializeFields();

            views = new List<View>() { new Level1(Vector2.Zero, VirtualWidth, VirtualHeight, Color.CornflowerBlue, new Vector2(10.5f, 32.5f) * Constants.TILE_SIZE) };

            views[0].Initialize();
            views[0].RaiseViewEvent += HandleViewEvent;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            views[0].LoadContent();

            base.LoadContent();
        }

        protected override void UnloadContent()
        {

            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.Begin();

            for (int i = 0; i < views.Count; ++i)
            {
                views[i].Update(gameTime);
            }

            InputManager.End();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            List<RenderTarget2D> renderTargets = new List<RenderTarget2D>();

            foreach (View view in views)
            {
                renderTargets.Add(view.GetRenderTarget(spriteBatch));
            }

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Viewport = new Viewport((int)offset.X, (int)offset.Y, TargetWidth, TargetHeight);
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointWrap, null, null, null, Matrix.CreateScale(size));

            for (int i = 0; i < views.Count; ++i)
            {
                spriteBatch.Draw(renderTargets[i], views[i].Position, new Color(Vector4.One * views[i].Opacity / 255f));
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void HandleViewEvent(object sender, WLAViewArgs e)
        {
            switch (e.Command)
            {
                case EventCommands.AddView:
                    {
                        AddView(e.TargetView);

                        break;
                    }

                case EventCommands.RemoveView:
                    {
                        RemoveView();

                        break;
                    }

                case EventCommands.RemoveAddView:
                    {
                        RemoveView();
                        AddView(e.TargetView);

                        break;
                    }
            }
        }

        private void AddView(View newView)
        {
            newView.Initialize();
            newView.RaiseViewEvent += HandleViewEvent;
            newView.LoadContent();

            views.Add(newView);
        }

        private void RemoveView()
        {
            views.Last().UnloadContent();
            views.RemoveAt(views.Count - 1);
        }
    }

    public enum Commands { None, AddView, RemoveView, RemoveAddView };
}
