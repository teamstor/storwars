using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamStor.TBS.Map;
using SpriteBatch = TeamStor.Engine.Graphics.SpriteBatch;
using TeamStor.TBS.Gameplay;

namespace TeamStor.TBS.Building
{
    /// <summary>
    /// Static building data.
    /// Usually 32x16
    /// </summary>
    public class BuildingData
    {
        public const string BUILDINGS_TEXTURE = "textures/buildings.png";
        
        /// <summary>
        /// Available buildings.
        /// </summary>
        public static Dictionary<byte, BuildingData> Buildings { get; } = new Dictionary<byte, BuildingData>();

        public const int HQ_ID = 10;
        public const int LUMBER_MILL_ID = 20;
        public const int MINE_ID = 30;
        public const int QUARRY_ID = 40;
        public const int HOUSE_ID = 50;
        public const int BARRACKS_ID = 60;
        public const int BLACKSMITH_ID = 70;
        public const int WALL_ID = 80;
        public const int WATCHTOWER_ID = 90;

        /// <summary>
        /// Soviet buildings.
        /// </summary>
        public static class Soviet
        {
            /// <summary>
            /// Soviet HQ.
            /// </summary>
            public static BuildingData Headquarters = new BuildingData(
                HQ_ID + (int)Faction.Soviet, 
                "The Kremlin", 
                BuildingType.Headquarters,
                Faction.Soviet, 
                new Rectangle(192, 0, 32, 32));
        
            /// <summary>
            /// Soviet lumber mill.
            /// </summary>
            public static BuildingData LumberMill = new BuildingData(
                LUMBER_MILL_ID + (int)Faction.Soviet, 
                "Sawmill", 
                BuildingType.LumberMill,
                Faction.Soviet, 
                new Rectangle(64, 0, 32, 32));
            
            /// <summary>
            /// Soviet mine.
            /// </summary>
            public static BuildingData Mine = new BuildingData(
                MINE_ID + (int)Faction.Soviet, 
                "Factory", 
                BuildingType.Mine,
                Faction.Soviet, 
                new Rectangle(96, 0, 32, 32));
            
            /// <summary>
            /// Soviet quarry.
            /// </summary>
            public static BuildingData Quarry = new BuildingData(
                QUARRY_ID + (int)Faction.Soviet, 
                "Quarry", 
                BuildingType.Quarry,
                Faction.Soviet, 
                new Rectangle(256, 0, 32, 32));
            
            /// <summary>
            /// Soviet house.
            /// </summary>
            public static BuildingData House = new BuildingData(
                HOUSE_ID + (int)Faction.Soviet, 
                "Apartments", 
                BuildingType.House,
                Faction.Soviet, 
                new Rectangle(224, 0, 32, 32));
            
            /// <summary>
            /// Soviet barracks.
            /// </summary>
            public static BuildingData Barracks = new BuildingData(
                BARRACKS_ID + (int)Faction.Soviet, 
                "Camp", 
                BuildingType.Barracks,
                Faction.Soviet, 
                new Rectangle(0, 0, 32, 32));
            
            /// <summary>
            /// Soviet blacksmith.
            /// </summary>
            public static BuildingData Blacksmith = new BuildingData(
                BLACKSMITH_ID + (int)Faction.Soviet, 
                "Laboratory", 
                BuildingType.Blacksmith,
                Faction.Soviet, 
                new Rectangle(32, 0, 32, 32));
            
            /// <summary>
            /// Soviet wall.
            /// </summary>
            public static BuildingData Wall = new BuildingData(
                WALL_ID + (int)Faction.Soviet, 
                "Wall", 
                BuildingType.Wall,
                Faction.Soviet, 
                new Rectangle(160, 0, 32, 32));
            
            /// <summary>
            /// Soviet watchtower.
            /// </summary>
            public static BuildingData Watchtower = new BuildingData(
                WATCHTOWER_ID + (int)Faction.Soviet, 
                "Watchtower", 
                BuildingType.Watchtower,
                Faction.Soviet, 
                new Rectangle(128, 0, 32, 32));
        }
        
        /// <summary>
        /// ARLA buildings.
        /// </summary>
        public static class ARLA
        {
            /// <summary>
            /// ARLA HQ.
            /// </summary>
            public static BuildingData Headquarters = new BuildingData(
                HQ_ID + (int)Faction.ARLA, 
                "Dunkträdet", 
                BuildingType.Headquarters,
                Faction.ARLA, 
                new Rectangle(192, 32, 32, 32));
        
            // TODO: KASPER fixa textures
            /// <summary>
            /// ARLA lumber mill.
            /// </summary>
            public static BuildingData LumberMill = new BuildingData(
                LUMBER_MILL_ID + (int)Faction.ARLA, 
                "Sawmill", 
                BuildingType.LumberMill,
                Faction.ARLA, 
                new Rectangle(64, 0, 32, 32));
            
            // TODO: KASPER fixa textures
            /// <summary>
            /// ARLA mine.
            /// </summary>
            public static BuildingData Mine = new BuildingData(
                MINE_ID + (int)Faction.ARLA, 
                "Factory", 
                BuildingType.Mine,
                Faction.ARLA, 
                new Rectangle(96, 0, 32, 32));
            
            // TODO: KASPER fixa textures
            /// <summary>
            /// ARLA quarry.
            /// </summary>
            public static BuildingData Quarry = new BuildingData(
                QUARRY_ID + (int)Faction.ARLA, 
                "Quarry", 
                BuildingType.Quarry,
                Faction.ARLA, 
                new Rectangle(256, 0, 32, 32));
            
            // TODO: KASPER fixa textures
            /// <summary>
            /// ARLA house.
            /// </summary>
            public static BuildingData House = new BuildingData(
                HOUSE_ID + (int)Faction.ARLA, 
                "Apartments", 
                BuildingType.House,
                Faction.ARLA, 
                new Rectangle(224, 0, 32, 32));
            
            // TODO: KASPER fixa textures
            /// <summary>
            /// ARLA barracks.
            /// </summary>
            public static BuildingData Barracks = new BuildingData(
                BARRACKS_ID + (int)Faction.ARLA, 
                "Camp", 
                BuildingType.Barracks,
                Faction.ARLA, 
                new Rectangle(0, 0, 32, 32));
            
            // TODO: KASPER fixa textures
            /// <summary>
            /// ARLA blacksmith.
            /// </summary>
            public static BuildingData Blacksmith = new BuildingData(
                BLACKSMITH_ID + (int)Faction.ARLA, 
                "Laboratory", 
                BuildingType.Blacksmith,
                Faction.ARLA, 
                new Rectangle(32, 0, 32, 32));
            
            // TODO: KASPER fixa textures
            /// <summary>
            /// ARLA wall.
            /// </summary>
            public static BuildingData Wall = new BuildingData(
                WALL_ID + (int)Faction.ARLA, 
                "Wall", 
                BuildingType.Wall,
                Faction.ARLA, 
                new Rectangle(160, 0, 32, 32));
            
            // TODO: KASPER fixa textures
            /// <summary>
            /// ARLA watchtower.
            /// </summary>
            public static BuildingData Watchtower = new BuildingData(
                WATCHTOWER_ID + (int)Faction.ARLA, 
                "Watchtower", 
                BuildingType.Watchtower,
                Faction.ARLA, 
                new Rectangle(128, 0, 32, 32));
        }

      
        /// <summary>
        /// Building ID used in the building class.
        /// </summary>
        public byte Id;
        
        /// <summary>
        /// Building name.
        /// </summary>
        public virtual string Name { get; protected set;  }
        
        /// <summary>
        /// Building type.
        /// </summary>
        public virtual BuildingType Type { get; protected set;  }

        /// <summary>
        /// Faction that this building belongs to.
        /// </summary>
        public virtual Faction Faction { get; protected set; }

        /// <summary>
        /// Texture rectangle on buildings.png.
        /// </summary>
        public Rectangle TextureRectangle
        {
            get; protected set;
        }
        
        /// <summary>
        /// Draws this tile.
        /// </summary>
        public virtual void Draw(SpriteBatch batch, Texture2D tileTexture, double time, Point pos, int upgradeLevel)
        {
            // TODO: upgradeLevel
            
            batch.Texture(
                new Vector2(pos.X * 16, pos.Y * 16),
                tileTexture,
                Color.White,
                Vector2.One,
                TextureRectangle);
        }
        
        public BuildingData(byte id, string name, BuildingType type, Faction faction, Rectangle textureRectangle)
        {
            Id = id;
            Name = name;
            Type = type;
            Faction = faction;
            TextureRectangle = textureRectangle;
			
            Buildings.Add(id, this);
        }

        public static BuildingData FindByName(Faction faction, string name)
        {
            foreach(BuildingData data in Buildings.Values)
            {
                if(data.Faction == faction && data.Name == name)
                    return data;
            }

            return null;
        }
        
        public static BuildingData FindByFaction(Faction faction, BuildingType type)
        {
            switch(type)
            {
                case BuildingType.Headquarters:
                    return Buildings[(byte)(HQ_ID + (int)faction)];
                
                case BuildingType.LumberMill:
                    return Buildings[(byte)(LUMBER_MILL_ID + (int)faction)];
                
                case BuildingType.Mine:
                    return Buildings[(byte)(LUMBER_MILL_ID + (int)faction)];
                
                case BuildingType.Quarry:
                    return Buildings[(byte)(QUARRY_ID + (int)faction)];
                
                case BuildingType.House:
                    return Buildings[(byte)(HOUSE_ID + (int)faction)];
                
                case BuildingType.Barracks:
                    return Buildings[(byte)(BARRACKS_ID + (int)faction)];
                
                case BuildingType.Blacksmith:
                    return Buildings[(byte)(BLACKSMITH_ID + (int)faction)];
                
                case BuildingType.Wall:
                    return Buildings[(byte)(WALL_ID + (int)faction)];
                
                case BuildingType.Watchtower:
                    return Buildings[(byte)(WATCHTOWER_ID + (int)faction)];
            }

            return null;
        }
    }
}