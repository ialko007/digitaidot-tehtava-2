using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed = GameManager.instance.getSpeed();
        gameObject.transform.position += new Vector3(-speed * Time.deltaTime, 0.0f, 0.0f);
        if (transform.position.x < -20.0f)
        {
            Destroy(gameObject);
        }
    }
}
