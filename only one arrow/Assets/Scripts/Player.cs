using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player MainPlayer // I'd name it Player but I can't, probably for good reason; it's just so enemies know who to aim for
    {
        get;
        private set;
    }

    public void Awake()
    {
        MainPlayer = this;
    }
}
