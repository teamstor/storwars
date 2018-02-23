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
        
        /// <summary>
        /// Creates new units.
        /// </summary>
        Barracks,
        
        /// <summary>
        /// Provides upgrades to units.
        /// </summary>
        Blacksmith,
        
        /// <summary>
        /// Stops units from going through.
        /// </summary>
        Wall,
        
        /// <summary>
        /// Shoots nearby enemy units and reveals part of the map.
        /// </summary>
        Watchtower
    }
}