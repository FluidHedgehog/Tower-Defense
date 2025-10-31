using System.Linq;
using UnityEngine;

public static class TurretMerger
{
    public static TurretBase turretBase;

    public static int situation;
    public static GameObject turret { get; set; }
    public static GameObject target { get; set; }
    public static Vector3Int turretPos { get; set; }

    public static void Initialize(TurretBase tBase)
    {
        turretBase = tBase;
    }

    public static bool CanMerge(GameObject selected, GameObject target)
    {
        if (selected == null || target == null) return false;

        if (selected == target) return false;

        int selectedGroup = GetMergeGroup(selected);
        int targetGroup = GetMergeGroup(target);

        if (selectedGroup == 2 && targetGroup == 2) return false;

        if (selectedGroup + targetGroup >= 3) return false;
        else return true;
    }

    public static int GetMergeGroup(GameObject turret)
    {
        var kind = turret.GetComponent<TurretInstance>().towerType;
        return kind switch
        {
            TowerType.Basic => 0,
            TowerType.Upgraded => 1,
            TowerType.Abomination or TowerType.Mighty => 2,
            _ => 2
        };
    }

    public static int GetMergeCode(GameObject turret)
    {
        var kind = turret.GetComponent<TurretInstance>().towerKind;
        return kind switch
        {
            TowerKind.Thorns => 0,
            TowerKind.Plague => 1,
            TowerKind.Moon => 2,
            TowerKind.Forest => 10,
            TowerKind.Generosity => 11,
            TowerKind.Willow => 12,
            TowerKind.Abomination => 20,
            _ => -1
        };
    }

    public static bool MergeTowers(GameObject tower1, GameObject tower2, Vector3Int tile)
    {
        

        if (!CanMerge(tower1, tower2))
        {
            return false;
        }

        if(tower1.scene.name == null || tower2.scene.name == null)
        {
            Debug.LogError("Cannot merge prefab assets!");
            return false;
        }

        int code1 = GetMergeCode(tower1);
        int code2 = GetMergeCode(tower2);
        int combinedCode = code1 * 100 + code2;
        
        int[] group = { GetMergeGroup(tower1), GetMergeGroup(tower2) };

        GameObject mergeResult = GetMergeResult(combinedCode, group.Max());

        int mergeCost = mergeResult.GetComponent<TurretInstance>().cost;

        if (!BloodSystemEvents.TriggerPassValue(mergeCost))
        {
            return false;
        }

        BloodSystemEvents.TriggerBloodRemoved(mergeCost);

        Object.Destroy(tower1);
        Object.Destroy(tower2);

        GridHelper.SetToWorld(tile, out Vector3 worldPos);

        Vector3 mergePosition = worldPos;
        // mergePosition.x += 0.5f;
        // mergePosition.y += 0.5f;

        //GridHelper.AddTurret(tile, mergeResult);
        GameObject currentObj = Object.Instantiate(mergeResult, mergePosition, Quaternion.identity);
        GridHelper.AddTurret(tile, currentObj);
        return true;
        //ChangeStates.ChangeStateNow(0);
    }

    private static GameObject GetMergeResult(int combinedCode, int group)
    {
        switch (combinedCode)
        {
            case 0:
                {
                    return group switch
                    {
                        0 => turretBase.Thorns1,
                        1 => turretBase.Thorns2,
                        _ => null
                    };
                }

            case 101:
                {
                    return group switch
                    {
                        0 => turretBase.Plague1,
                        1 => turretBase.Plague2,
                        _ => null
                    };
                }

            case 202:
                {
                    return group switch
                    {
                        0 => turretBase.Moon1,
                        1 => turretBase.Moon2,
                        _ => null
                    };
                }

            case 1 or 100:
                {
                    return group switch //Thorns + Plague
                    {
                        0 => turretBase.Forest1,
                        1 => turretBase.Forest2,
                        _ => null
                    };
                } 
                
            case 2 or 200:
                {
                    return group switch //Moon + Thorns
                    {
                        0 => turretBase.Willow1,
                        1 => turretBase.Willow2,
                        _ => null
                    };
                } 
            
            case 102 or 201:
                {
                    return group switch //Moon + Plague
                    {
                        0 => turretBase.Generosity1,
                        1 => turretBase.Generosity2,
                        _ => null
                    };
                } 
            case 111 or 1101 or 211 or 1102:
                {
                    return group switch
                    {
                        1 => turretBase.Generosity2,
                        _ => null
                    };
                }
            case 1000 or 10 or 1001 or 110:
                {
                    return group switch
                    {
                        1 => turretBase.Forest2,
                        _ => null
                    };
                }
            case 1200 or 12 or 212 or 1202:
                {
                    return group switch
                    {
                        1 => turretBase.Willow2,
                        _ => null
                    };
                }
                
            case int: { return turretBase.Abomination; }
        }
    }
}
