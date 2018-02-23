using Microsoft.Xna.Framework;
using TeamStor.Engine;
using TeamStor.Engine.Graphics;

namespace TeamStor.TBS.Online
{
    public class OnlineErrorState : GameState
    {
        private string _error;
        
        public OnlineErrorState(string error)
        {
            _error = error;
        }
        
        public override void OnEnter(GameState previousState)
        {
            throw new System.NotImplementedException();
        }

        public override void OnLeave(GameState nextState)
        {
            throw new System.NotImplementedException();
        }

        public override void Update(double deltaTime, double totalTime, long count)
        {
            throw new System.NotImplementedException();
        }

        public override void FixedUpdate(long count)
        {
            throw new System.NotImplementedException();
        }

        public override void Draw(SpriteBatch batch, Vector2 screenSize)
        {
            throw new System.NotImplementedException();
        }
    }
}