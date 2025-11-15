using System.Linq;
using TMPro;
using Unity.Collections;
using UnityEngine;
using System.Collections.Generic;

public class SpellCooldownUI : MonoBehaviour
{

    [SerializeField] SpellCaster spellCaster;
    [SerializeField] TextMeshProUGUI[] cooldownText;

    void Update()
    {
        if (spellCaster == null || cooldownText == null) return;

        var cdInfo = spellCaster.GetCooldownInfo();

        cooldownText[0].text = $"{GetCooldown(cdInfo, SpellType.Bones):F1}s";
        cooldownText[1].text = $"{GetCooldown(cdInfo, SpellType.Frost):F1}s";
        cooldownText[2].text = $"{GetCooldown(cdInfo, SpellType.Fire):F1}s";
    }

    float GetCooldown(Dictionary<SpellType, float> cdInfo, SpellType type)
    {
        return cdInfo.ContainsKey(type) ? Mathf.Max(0, cdInfo[type]) : 0f;
    }
}

