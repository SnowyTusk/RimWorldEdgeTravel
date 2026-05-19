using UnityEngine;
using Verse;

namespace SnowyTusk.EdgeTravel
{
    public sealed class EdgeTravelMod : Mod
    {
        public static EdgeTravelSettings Settings;

        public static bool AutoTravelEnabled
        {
            get { return Settings == null || Settings.AutoTravelEnabled; }
        }

        public EdgeTravelMod(ModContentPack content) : base(content)
        {
            Settings = GetSettings<EdgeTravelSettings>();
        }

        public override string SettingsCategory()
        {
            return "Edge Travel";
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(inRect);
            listing.CheckboxLabeled(
                "Включить автоматический переход через край карты",
                ref Settings.AutoTravelEnabled,
                "Если включено, колонисты в режиме призыва могут переходить с края карты в соседнюю область.");
            listing.Gap(8f);
            listing.CheckboxLabeled(
                "Один раз показать предупреждение об альфа-версии",
                ref Settings.ShowAlphaWarning,
                "Показывает одноразовое напоминание, что Edge Travel находится в ранней альфа-версии и безопаснее всего проверяется на тестовых сохранениях.");
            listing.End();
            Settings.Write();
        }
    }

    public sealed class EdgeTravelSettings : ModSettings
    {
        public bool AutoTravelEnabled = true;
        public bool ShowAlphaWarning = true;
        public bool AlphaWarningShown;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref AutoTravelEnabled, "autoTravelEnabled", true);
            Scribe_Values.Look(ref ShowAlphaWarning, "showAlphaWarning", true);
            Scribe_Values.Look(ref AlphaWarningShown, "alphaWarningShown", false);
        }
    }
}
