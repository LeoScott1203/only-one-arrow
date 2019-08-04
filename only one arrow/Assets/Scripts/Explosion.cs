using UnityEngine;

public class Explosion : MonoBehaviour
{
    public bool stunning = false;
    
    static float scalePerSecond = 10.0f;

    [SerializeField]
    float maxScale = 5.0f;
    float currentScale = 0.1f;

    public void Update()
    {
        currentScale += scalePerSecond * Time.deltaTime;
        transform.localScale = new Vector3(currentScale, currentScale, 1);

        if(currentScale >= maxScale)
        {
            Destroy(this.gameObject);
        }
    }
}