using System;
using BepInEx; // requires BepInEx.dll and BepInEx.Harmony.dll
using UnboundLib; // requires UnboundLib.dll
using UnboundLib.Cards; // " "
using UnboundLib.Networking; // " "
using UnityEngine; // requires UnityEngine.dll, UnityEngine.CoreModule.dll, and UnityEngine.AssetBundleModule.dll
using SCards.Cards;
using System.IO;
using HarmonyLib; // requires 0Harmony.dll
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Collections;
using Photon.Pun;
using Jotunn.Utils;
using InControl;
using System.Linq;
using UnboundLib.GameModes;
// requires Assembly-CSharp.dll
// requires MMHOOK-Assembly-CSharp.dll

namespace SCards {
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin(ModId, ModName, ModVersion)]
    [BepInProcess("Rounds.exe")]
    public class SCards : BaseUnityPlugin {

        private void Awake() {

            new Harmony(ModId);

        }
        private void Start() {

            CustomCard.BuildCard<BrokenCard>();
            CustomCard.BuildCard<LightweightCard>();
            CustomCard.BuildCard<HeavyWeightCard>();
            CustomCard.BuildCard<SloppyReloadCard>();
            CustomCard.BuildCard<ExtendedMagCard>();

        }

        private const string ModId = "swords.rounds.plugins.scards";
        private const string ModName = "Sword's Cards";
        private const string ModVersion = "0.1.1";
    }
}

namespace SCards.Cards {

    public class SwordCard : CustomCard 
    {
        // No worky for now :(

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers) 
        {
            
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats) 
        {

            gun.bursts += 5;
            gun.destroyBulletAfter = 0.5f;
            gun.spread += 1.0f;
            gun.spread *= 2.0f;
            gun.gravity = 0f;
            gunAmmo.maxAmmo += 10;

        }
        public override void OnRemoveCard() 
        {
            
        }

        protected override string GetTitle() 
        {
            return "Sword";
        }
        protected override string GetDescription() 
        {
            return "It's like a shotgun, but shorter";
        }
        protected override GameObject GetCardArt() 
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity() 
        {
            return CardInfo.Rarity.Uncommon;
        }
        protected override CardInfoStat[] GetStats() 
        {
            return new CardInfoStat[] {
                new CardInfoStat {
                    positive = false,
                    stat = "Bullets",
                    amount = "Short-Range",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat {
                    positive = false,
                    stat = "Bullet Speed",
                    amount = "-65%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotLower
                },
                new CardInfoStat {
                    positive = true,
                    stat = "Not a gun",
                    amount = "100%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme() 
        {
            return CardThemeColor.CardThemeColorType.DefensiveBlue;
        }
    }

    public class BrokenCard : CustomCard
    {
        // High fire rate, lots of ammo, but no aiming

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {

        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {

            gun.recoil *= 1.5f;         // Recoil is visual gun recoil
            gun.attackSpeed /= 6.5f;    // attack speed higher means shooting slower
            gun.reloadTime /= 1.5f;
            gun.spread += 1f;           // spread starts at 0, so we need to make it 1 or higher first
            gun.spread *= 9.0f;
            gunAmmo.maxAmmo += 25;      // flat added ammo

        }
        public override void OnRemoveCard()
        {

        }

        protected override string GetTitle()
        {
            return "Broken Gun";
        }
        protected override string GetDescription()
        {
            return "Fast fire rate and lots of ammo, but no aiming";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Rare;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[] {
                new CardInfoStat {
                    positive = true,
                    stat = "Attack Speed",
                    amount = "+650%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat {
                    positive = true,
                    stat = "Ammo",
                    amount = "+25",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat {
                    positive = true,
                    stat = "Reload Speed",
                    amount = "+50%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat {
                    positive = false,
                    stat = "Spread",
                    amount = "+1000%",
                    simepleAmount = CardInfoStat.SimpleAmount.aHugeAmountOf
                },
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.DestructiveRed;
        }
    }

    public class LightweightCard : CustomCard
    {
        // Makes you harder to hit, but you hit for less

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {

        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {

            characterStats.movementSpeed *= 1.25f;
            characterStats.sizeMultiplier /= 1.20f;
            gun.damage *= 0.65f;
            gun.recoil /= 1.20f;
            gun.size /= 1.20f;

        }
        public override void OnRemoveCard()
        {

        }

        protected override string GetTitle()
        {
            return "Lightweight";
        }
        protected override string GetDescription()
        {
            return "You're hard to hit, but you don't hit hard";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Uncommon;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[] {
                new CardInfoStat {
                    positive = true,
                    stat = "Movement Speed",
                    amount = "+25%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat {
                    positive = true,
                    stat = "Size",
                    amount = "-20%",
                    simepleAmount = CardInfoStat.SimpleAmount.slightlySmaller
                },
                new CardInfoStat {
                    positive = false,
                    stat = "Damage",
                    amount = "-35%",
                    simepleAmount = CardInfoStat.SimpleAmount.lower
                },
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.PoisonGreen;
        }
    }

    public class HeavyWeightCard : CustomCard
    {
        // Makes you easier to hit, but you hit for more

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {

        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {

            characterStats.movementSpeed /= 1.30f;
            characterStats.sizeMultiplier *= 1.20f;
            gun.damage *= 1.70f;
            gun.recoil *= 1.20f;
            gun.size *= 1.20f;

        }
        public override void OnRemoveCard()
        {

        }

        protected override string GetTitle()
        {
            return "Heavyweight";
        }
        protected override string GetDescription()
        {
            return "Big and slow, but harder hits";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Common;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[] {
                new CardInfoStat {
                    positive = true,
                    stat = "Damage",
                    amount = "+70%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
                },
                new CardInfoStat {
                    positive = false,
                    stat = "Size",
                    amount = "+20%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
                },
                new CardInfoStat {
                    positive = false,
                    stat = "Movement Speed",
                    amount = "-30%",
                    simepleAmount = CardInfoStat.SimpleAmount.lower
                },
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.DefensiveBlue;
        }
    }

    public class SloppyReloadCard : CustomCard
    {
        // Less accurate but faster reload

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {

        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {

            gunAmmo.reloadTime -= 0.50f;
            gun.spread += 0.1f;

        }
        public override void OnRemoveCard()
        {

        }

        protected override string GetTitle()
        {
            return "Sloppy Reload";
        }
        protected override string GetDescription()
        {
            return "Good reload, bad shooting";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Common;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[] {
                new CardInfoStat {
                    positive = true,
                    stat = "Reload Time",
                    amount = "-0.50s",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat {
                    positive = false,
                    stat = "Spread",
                    amount = "A Little Bit Of",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.FirepowerYellow;
        }
    }

    public class ExtendedMagCard : CustomCard
    {
        // More bullets per mag

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {

        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {

            gunAmmo.maxAmmo += 4;
            gun.size += 0.2f;

        }
        public override void OnRemoveCard()
        {

        }

        protected override string GetTitle()
        {
            return "Extended Mag";
        }
        protected override string GetDescription()
        {
            return null;
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Common;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[] {
                new CardInfoStat {
                    positive = true,
                    stat = "Ammo",
                    amount = "+4",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.TechWhite;
        }
    }
}