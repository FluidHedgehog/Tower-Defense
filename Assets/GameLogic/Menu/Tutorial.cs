using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public void TurnMeOn(GameObject gameObject){
        gameObject.SetActive(true);
    }

    public void TurnMeOff(GameObject gameObject){
        gameObject.SetActive(false);
    }
}
