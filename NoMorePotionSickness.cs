using System.ComponentModel;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace NoMorePotionSickness
{
    public class NoMorePotionSickness : Mod
    {
    }
    [Label("No More Potion Sickness")]
    public class Config : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        public static Config Instance;

        public override void OnLoaded()
        {
            Instance = this;
        }

        [Header("No More Mana/Potion Sickness")]
        [Label("Get rid of the Potion Sickness Debuff")]
        public bool RemovePS;

        [Label("Get rid of the Mana Sickness Debuff")]
        [DefaultValue(true)]
        public bool RemoveMS;

        [Label("Constant Mana Flower effect")]
        [DefaultValue(false)]
        public bool RemoveMF; //Remove mother fvcker
    }
    public class NoMorePotionSicknessModPlayer : ModPlayer
    {
        public override void PostUpdateEquips()
        {
            if (Config.Instance.RemoveMF)
                player.manaFlower = true;
        }
        public override void PreUpdate()
        {
            if (Config.Instance.RemoveMS)
            {
                for (int ManaSicknessDebuff = 0; ManaSicknessDebuff < Player.MaxBuffs; ManaSicknessDebuff++)
                    if (player.buffType[ManaSicknessDebuff] == BuffID.ManaSickness)
                        player.DelBuff(ManaSicknessDebuff);
                player.buffImmune[BuffID.ManaSickness] = true;
                player.manaSick = false;
                player.manaSickReduction = 0;
            }
            if (Config.Instance.RemovePS)
            {
                for (int PotionSicknessDebuff = 0; PotionSicknessDebuff < Player.MaxBuffs; PotionSicknessDebuff++)
                    if (player.buffType[PotionSicknessDebuff] == BuffID.PotionSickness)
                        player.DelBuff(PotionSicknessDebuff);
                player.buffImmune[BuffID.PotionSickness] = true;
                Item.potionDelay = 0;
            }
            if (Config.Instance.RemovePS && Config.Instance.RemoveMS)
            {
                Item.restorationDelay = 0;
            }
        }
    }
}