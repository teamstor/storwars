namespace TeamStor.TBS.Building
{
    /// <summary>
    /// Building type.
    /// </summary>
    public enum BuildingType
    {
        /// <summary>
        /// Creates peasants/workers.
        /// Every player starts with this at the spawnpoint.
        /// </summary>
        Headquarters,
        
        /// <summary>
        /// Generates lumber.
        /// </summary>
        LumberMill,
        
        /// <summary>
        /// Generates gold.
        /// </summary>
        Mine,
        
        /// <summary>
        /// Generates stone.
        /// </summary>
        Quarry,
        
        /// <summary>
        /// Gives more housing to a player, allowing more units to be created
        /// </summary>
        House,
        
        UnitCreator,
        
        /// <summary>
        /// Provides upgrades to units.
        /// </summary>
        Blacksmith,
        
        /// <summary>
        /// Units can't go through walls.
        /// </summary>
        Wall,
        
        /// <summary>
        /// Shoots nearby enemy units and reveals part of the map.
        /// </summary>
        Watchtower
    }
}