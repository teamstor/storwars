using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamStor.Engine;
using TeamStor.TBS.Gameplay.Ingame;
using TeamStor.TBS.Map;
using TeamStor.TBS.Online;
using SpriteBatch = TeamStor.Engine.Graphics.SpriteBatch;

namespace TeamStor.TBS.Gameplay.States
{
    /// <summary>
    /// In-game state.
    /// Players can't join while the game is in this state and if a player leaves all their buildings will explode.
    /// </summary>
    public class GameplayState : GameState
    {
        /// <summary>
        /// Multiplayer data like server and client.
        /// </summary>
        public OnlineData OnlineData
        {
            get;
            private set;
        }

        /// <summary>
        /// Game data like map and players.
        /// </summary>
        public GameData GameData
        {
            get;
            private set;
        }

        /// <summary>
        /// The game camera.
        /// </summary>
        public Camera Camera
        {
            get;
            private set;
        }

        /// <summary>
        /// The fog of war.
        /// </summary>
        public FogOfWar FogOfWar
        {
            get;
            private set;
        }
        
        public 
        
        public override void OnEnter(GameState previousState)
        {
            GameData = new GameData(MapData.Load("data/maps/test.tsmap"));
            
            Camera = new Camera(this);
            FogOfWar = new FogOfWar(Game, GameData.Map);
        }

        public override void OnLeave(GameState nextState)
        {
        }

        public override void Update(double deltaTime, double totalTime, long count)
        {
            Camera.Update(deltaTime, totalTime);
            
            FogOfWar.RevealedAreas.Clear();
            FogOfWar.RevealedAreas.Add(new Rectangle(10, 10, 4, 4));
        }

        public override void FixedUpdate(long count)
        {
        }

        public override void Draw(SpriteBatch batch, Vector2 screenSize)
        {
            FogOfWar.UpdateRenderTarget(batch);
            
            batch.SamplerState = SamplerState.PointWrap;
            batch.Transform = Camera.Transform;
            
            Rectangle drawRectangle = new Rectangle(-1000, -1000, GameData.Map.Width * 16 + 2000, GameData.Map.Height * 16 + 2000);
            batch.Texture(drawRectangle, Assets.Get<Texture2D>("textures/gameplay/bgwater_" + ((int)((Game.Time * 2) % 4) + 1) + ".png"), Color.White, drawRectangle);
            
            GameData.Map.Draw(false, Game, Assets,
                new Rectangle(
                    (int)-(Camera.Translation.X / Camera.Zoom.Value),
                    (int)-(Camera.Translation.Y / Camera.Zoom.Value),
                    (int)(screenSize.X / Math.Floor(Camera.Zoom.Value)),
                    (int)(screenSize.Y / Math.Floor(Camera.Zoom.Value))));
            GameData.Map.Draw(true, Game, Assets,
                new Rectangle(
                    (int)-(Camera.Translation.X / Camera.Zoom.Value),
                    (int)-(Camera.Translation.Y / Camera.Zoom.Value),
                    (int)(screenSize.X / Math.Floor(Camera.Zoom.Value)),
                    (int)(screenSize.Y / Math.Floor(Camera.Zoom.Value))));
            
            FogOfWar.Draw(batch);
        }
    }
}