using UnityEngine;

public enum TowerType { Basic, Upgraded, Mighty, Abomination }
public enum TowerKind { Thorns, Plague, Moon, Forest, Generosity, Willow, Abomination }

public class TurretInstance : MonoBehaviour
{

    [SerializeField] public TowerType towerType;
    [SerializeField] public TowerKind towerKind;

}