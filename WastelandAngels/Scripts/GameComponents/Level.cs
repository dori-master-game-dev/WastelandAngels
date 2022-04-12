using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WLA.System;
using WLA.GameComponents.Entities;
using WLA.Tiled;
using WLA.Events;

namespace WLA.GameComponents
{
    public abstract class Level : View
    {
        protected readonly string mapName;
        public TileMap Map { get; protected set; }

        public Camera LevelCamera { get; protected set; }

        public EnemyTest enemy;

        public Player Player { get; protected set; }
        public List<Entity> Creatures { get; protected set; }

        private Texture2D fade;
        protected float timeLeft;
        protected Vector2 initialPlayerPosition;

        protected EventManager eventManager;

        public Level(string mapName, Vector2 position, int width, int height, Vector2 initialPlayerPosition) : base(position, width, height, Color.White)
        {
            this.mapName = mapName;
            this.initialPlayerPosition = initialPlayerPosition;
        }

        public Level(string mapName, Vector2 position, int width, int height, Color baseColor, Vector2 initialPlayerPosition) : base(position, width, height, baseColor)
        {
            this.mapName = mapName;
            this.initialPlayerPosition = initialPlayerPosition;
        }

        public override void Initialize()
        {
            fade = new Texture2D(ViewManager.graphics.GraphicsDevice, Width, Height);
            Color[] data = new Color[Width * Height];

            for (int i = 0; i < Width * Height; ++i)
            {
                data[i] = Color.Black;
            }

            fade.SetData(data);

            timeLeft = 1f;

            base.Initialize();
        }

        public override void LoadContent()
        {
            Map = ViewManager.content.Load<TileMap>("Levels/" + mapName);
            Map.LoadContent();

            base.LoadContent();
        }

        public override void UnloadContent()
        {
            Map.UnloadContent();
            Player.UnloadContent();

            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (timeLeft > 0)
            {
                timeLeft -= (float)gameTime.ElapsedGameTime.TotalSeconds * 2;
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointWrap);

            spriteBatch.Draw(fade, Vector2.Zero, new Color(Vector4.One * timeLeft));

            spriteBatch.End();

            base.Draw(spriteBatch);
        }
    }
}
