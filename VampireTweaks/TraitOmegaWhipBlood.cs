public class TraitOmegaWhipBlood : TraitWhipLove
{
    public override void OnCreate(int lv)
    {
        this.owner.c_charges = 6;
    }

    public override void TrySetHeldAct(ActPlan p)
    {
        p.pos.ListCards(includeMasked: false).ForEach(action: card =>
        {
            var target = card.Chara;
            if (target == null)
            {
                return;
            }

            if (p.IsSelfOrNeighbor && EClass.pc.CanSee(c: card))
            {
                p.TrySetAct(
                    lang: "actWhip",
                    onPerform: () =>
                    {
                        EClass.pc.Say(lang: "use_whip", c1: target, c2: this.owner, ref1: null, ref2: null);
                        EClass.pc.PlaySound(id: "whip", v: 1f, spatial: true);
                        target.PlayAnime(id: AnimeID.Shiver, force: false);

                        Thing thing = ThingGen.Create(id: "1300");
                        
                        if (thing is null)
                        {
                            return false;
                        }
                        
                        TraitDrink trait = thing.trait as TraitDrink;

                        if (trait is null)
                        {
                            return false;
                        }
                        
                        ActEffect.Proc(id: trait.IdEffect, power: trait.Power, state: trait.owner.blessedState, cc: target, tc: null, actRef: new ActRef
                        {
                            n1 = trait.N1,
                            isPerfume = (trait is TraitPerfume),
                            refThing = trait.owner.Thing,
                            act = ((trait.source != null && trait.source.id != 0) ? ACT.Create(row: trait.source) : null)
                        });
                        
                        EClass.pc.PickOrDrop(p: EClass.pc.pos, t: CraftUtil.MakeBloodSample(sucker: EClass.pc, feeder: target), msg: true);

                        this.owner.ModCharge(a: -1, destroy: false);
                        if (this.owner.c_charges <= 0)
                        {
                            this.owner.Destroy();
                        }
                        return true;
                    },
                    tc: card,
                    cursor: null,
                    dist: 1,
                    isHostileAct: false,
                    localAct: true,
                    canRepeat: false
                );
            }
        });
    }
}
