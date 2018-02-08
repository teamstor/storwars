using System.Collections.Generic;
using TeamStor.TBS.Gameplay;

namespace TeamStor.TBS.Unit
{
    /// <summary>
    /// Static unit data.
    /// 16x16
    /// </summary>
    public class UnitData
    {
        public const string UNITS_TEXTURE = "textures/units.png";
        
        /// <summary>
        /// Available units.
        /// </summary>
        public static Dictionary<byte, UnitData> Units { get; } = new Dictionary<byte, UnitData>();
        
        /// <summary>
        /// Unit ID.
        /// </summary>
        public byte Id;
        
        /// <summary>
        /// Unit name.
        /// </summary>
        public virtual string Name { get; protected set;  }
        
        /// <summary>
        /// Faction that this unit belongs to.
        /// </summary>
        public virtual Faction Faction { get; protected set; }
        
        /// <summary>
        /// Tier of HQ needed to create this unit.
        /// </summary>
        public virtual int Tier { get; protected set; }

        public UnitData(byte id, string name, Faction faction, int tier)
        {
            Id = id;
            Name = name;
            Faction = faction;
            Tier = tier;
            
            Units.Add(id, this);
        }
        
        public static UnitData FindByName(Faction faction, string name)
        {
            foreach(UnitData data in Units.Values)
            {
                if(data.Faction == faction && data.Name == name)
                    return data;
            }

            return null;
        }
    }
}