using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamStor.TBS.Map;
using SpriteBatch = TeamStor.Engine.Graphics.SpriteBatch;

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
        
        public BuildingData(byte id, string name, BuildingType type, Rectangle textureRectangle)
        {
            Id = id;
            Name = name;
            Type = type;
            TextureRectangle = textureRectangle;
			
            Buildings.Add(id, this);
        }

        public static BuildingData FindByName(string name)
        {
            foreach(BuildingData data in Buildings.Values)
            {
                if(data.Name == name)
                    return data;
            }

            return null;
        }
    }
}