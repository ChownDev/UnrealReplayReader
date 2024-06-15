using UnrealReplayReader.Fortnite.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnrealReplayReader.Fortnite.Models.Replay
{
    public record KillFeedEntry
    {
        public PlayerState FinisherOrDowner { get; set; }
        public PlayerState Player { get; set; }
        public EPlayerState CurrentPlayerState { get; set; }
        public EFortRarity ItemRarity { get; set; }
        public List<string> ItemType { get; } = new List<string>(); // Using List<string> to store multiple values
        public EDeathCause? DeathCause { get; set; } = EDeathCause.EDeathCauseMax;

        public double DeltaGameTimeSeconds { get; set; }
        public float Distance { get; set; }
        public bool DoNotDisplayInKillFeed { get; set; }
        public bool InsideSafeZone { get; set; }
        public string Biome { get; set; }
        public string PointOfInterest { get; set; }
        public bool IsTargeting { get; set; }
        public bool IsHeadshot { get; set; }

        public string[] DeathTags
        {
            get { return _deathTags; }
            set
            {
                _deathTags = value;
                UpdateWeaponTypes();
            }
        }

        private string[] _deathTags;

        public bool KilledSelf => FinisherOrDowner == Player;
        public bool HasError { get; set; }

        private void UpdateWeaponTypes()
        {
            if (_deathTags == null || _deathTags.Length == 0)
            {
                return;
            }

            // List of prefixes to ignore
            List<string> prefixesToIgnore = new List<string>
            {
                "Athena.",
                "Bacchus.",
                "AmmoType.",
                "Class.",
                "Curie.",
                "Cosmetics.",
                "Gameplay.",
                "GameplayCue.",
                "Homebase.",
                "Ability",
                "Abilities.",
                "AbilityWeapon.",
                "MovementMode.",
                "Pawn.",
                "HUD.",
                "Clambering.",
                "NPC.",
                "Rarity.",
                "WeaponMod.",
                "Quest",
                "Creative.",
                "AISpawnerData.",
                "Source.",
                "DBNO.",
                "Granted.",
                "phoebe.",
                "Vehicle.",
                "ModSetBucket.",
                "WeaponMods.",
                "GameplayEffect.",
                "Trait.",
                "weapon.ranged.",
                "Weapon.Ranged.",
                "Item.Sorts",
                "Weapon.Meta.",
                "Weapon.Ignore",
                "Animation.",
                "Weapon.RechargeAmmo",
                "VehicleMod.",
                "skill.sniper.headshotbuff",
                "Status.",
                "Item.Exclusive"
            };

            foreach (var deathTag in DeathTags)
            {
                if (deathTag == null)
                {
                    continue;
                }

                // Set ItemRarity based on deathTag
                switch (deathTag.ToLower())
                {
                    case "rarity.common":
                        ItemRarity = EFortRarity.Common;
                        break;
                    case "rarity.uncommon":
                        ItemRarity = EFortRarity.Uncommon;
                        break;
                    case "rarity.rare":
                        ItemRarity = EFortRarity.Rare;
                        break;
                    case "rarity.superrare":
                        ItemRarity = EFortRarity.Legendary;
                        break;
                    case "rarity.transcendent":
                        ItemRarity = EFortRarity.Transcendent;
                        break;
                    case "rarity.veryrare":
                        ItemRarity = EFortRarity.Epic;
                        break;
                    case "rarity.mythic":
                        ItemRarity = EFortRarity.Mythic;
                        break;
                }

                // Check if deathTag starts with any prefix in prefixesToIgnore
                bool ignoreTag = false;
                foreach (var prefix in prefixesToIgnore)
                {
                    if (deathTag.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    {
                        ignoreTag = true;
                        break;
                    }
                }

                // Add deathTag to ItemType if it doesn't start with any ignored prefix
                if (!ignoreTag)
                {
                    ItemType.Add(deathTag);
                }

                // Set Biome if not already set and deathTag starts with "Athena.Location.Biome"
                if (Biome == null && deathTag.StartsWith("Athena.Location.Biome", StringComparison.OrdinalIgnoreCase))
                {
                    Biome = deathTag.Split('.')[^1]; // Using character array
                }
                // Set PointOfInterest if not already set and deathTag starts with "Athena.Location.POI" or "Athena.Location.UnNamedPOI"
                else if (PointOfInterest == null &&
                         (deathTag.StartsWith("Athena.Location.POI", StringComparison.OrdinalIgnoreCase) ||
                          deathTag.StartsWith("Athena.Location.UnNamedPOI", StringComparison.OrdinalIgnoreCase)))
                {
                    PointOfInterest = deathTag.Split('.')[^1]; // Using character array
                }
            }
        }
    }
}
