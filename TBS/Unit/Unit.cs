using Microsoft.Xna.Framework;
using TeamStor.Engine.Graphics;
using TeamStor.TBS.Gameplay.States;

namespace TeamStor.TBS.Unit
{
    /// <summary>
    /// Base unit class.
    /// </summary>
    public abstract class Unit
    {
        /// <summary>
        /// Unit data for this unit.
        /// </summary>
        public UnitData UnitData;
        
        /// <summary>
        /// The game session this unit is in.
        /// </summary>
        public GameSessionState Session;
        
        /// <summary>
        /// The maximum distance this unit can walk in one turn.
        /// </summary>
        public virtual int MaxWalkDistance
        {
            get { return 1; }
        }

        /// <summary>
        /// Called when it's this units turn.
        /// </summary>
        public virtual void OnTurn(long turn)
        {
            
        }
        
        /// <summary>
        /// Called when the player tries to move this unit.
        /// </summary>
        /// <returns>If the unit was moved.</returns>
        public virtual bool OnUnitMove(Point newTile)
        {
            // TODO
            return false;
        }

        /// <summary>
        /// Draws this unit.
        /// </summary>
        /// <param name="batch">The SpriteBatch to use.</param>
        public virtual void Draw(SpriteBatch batch)
        {
            
        }
    }
}