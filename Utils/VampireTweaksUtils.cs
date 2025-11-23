namespace VampireTweaks
{
    internal static class VampireTweaksUtils
    {
        internal static int GetOrCreateRerolledUid(int originalUid)
        {
            var cards = EClass.game.cards;
            var newUid = cards.uidNext;
            cards.uidNext++;

            VampireTweaksConfig.BloodWhipUidMap[key: originalUid] = newUid;
            VampireTweaksConfig.SaveBloodWhipUidMap();
            
            var uidMappingText = $"UID {originalUid} → {newUid}";

            Msg.SetColor(color: Msg.colors.TalkGod);
            Msg.Say(idLang: "textEnc2", ref1: ModInfo.Name, ref2: $"{uidMappingText}");

            return newUid;
        }
        
        internal static int GetMappedOrOriginalUid(int originalUid)
        {
            if (VampireTweaksConfig.BloodWhipUidMap.TryGetValue(
                    key: originalUid,
                    value: out var mapped))
            {
                return mapped;
            }

            return originalUid;
        }
        
        internal static void AnnounceBloodMeal(Chara sucker, Chara feeder)
        {
            var uid = VampireTweaksConfig.BloodWhipUidMap[key: feeder.uid];
            
            var meal = CraftUtil.MakeBloodMeal(sucker: sucker, feeder: feeder);

            var msg = $"[UID {uid}] {meal.Name} | ";

            foreach (var el in meal.elements.dict.Values)
            {
                msg += $"{el.Name}={el.Value} ";
            }

            Msg.SetColor(color: Msg.colors.TalkGod);
            Msg.Say(
                idLang: "textEnc2",
                ref1: ModInfo.Name,
                ref2: msg
            );
            
            meal.Destroy();
        }
    }
}