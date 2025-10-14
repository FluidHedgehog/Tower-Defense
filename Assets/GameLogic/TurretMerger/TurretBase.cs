using UnityEngine;

public class TurretBase : MonoBehaviour
{

    [Header("Basic")]
    public GameObject Thorns0;
    public GameObject Plague0;
    public GameObject Moon0;

    [Header("Basic +")]
    public GameObject Thorns1;
    public GameObject Plague1;
    public GameObject Moon1;

    [Header("Basic + +")]
    public GameObject Thorns2;
    public GameObject Plague2;
    public GameObject Moon2;

    [Header("Merged")]
    public GameObject Forest1;
    public GameObject Generosity1;
    public GameObject Willow1;

    [Header("Merged + ")]
    public GameObject Forest2;
    public GameObject Generosity2;
    public GameObject Willow2;

    [Header("Abomination")]
    public GameObject Abomination;



    void Start()
    {
        TurretMerger.Initialize(this);
    }


}
