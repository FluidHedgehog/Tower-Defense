using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] BloodSystem blood;
    [SerializeField] ManaSystem mana;

    [SerializeField] TurretInstance plagueTower;
    [SerializeField] TurretInstance moonTower;
    [SerializeField] TurretInstance thornsTower;

    [SerializeField] GameObject plague;
    [SerializeField] GameObject moon;
    [SerializeField] GameObject thorns;

    [SerializeField] Spell boneSpell;
    [SerializeField] Spell frostSpell;
    [SerializeField] Spell fireSpell;

    [SerializeField] GameObject bone;
    [SerializeField] GameObject frost;
    [SerializeField] GameObject fire;

    void OnEnable()
    {
        ManaSystemEvents.OnManaAdded += CheckSpellButtons;
        ManaSystemEvents.OnManaRemoved += CheckSpellButtons;
        BloodSystemEvents.OnBloodAdded += CheckTowerButtons;
        BloodSystemEvents.OnBloodRemoved += CheckTowerButtons;

        CheckSpellButtons(0);
        CheckTowerButtons(0);
    }

    void OnDisable()
    {
        ManaSystemEvents.OnManaAdded -= CheckSpellButtons;
        ManaSystemEvents.OnManaRemoved -= CheckSpellButtons;
        BloodSystemEvents.OnBloodAdded -= CheckTowerButtons;
        BloodSystemEvents.OnBloodRemoved -= CheckTowerButtons;
    }

    void CheckTowerButtons(int i)
    {
        plague.SetActive(blood.currentBlood >= plagueTower.cost);
        moon.SetActive(blood.currentBlood >= moonTower.cost);
        thorns.SetActive(blood.currentBlood >= thornsTower.cost);
    }

    void CheckSpellButtons(int i)
    {
        bone.SetActive(mana.currentMana >= boneSpell.cost);
        frost.SetActive(mana.currentMana >= frostSpell.cost);
        fire.SetActive(mana.currentMana >= fireSpell.cost);
    }

}
