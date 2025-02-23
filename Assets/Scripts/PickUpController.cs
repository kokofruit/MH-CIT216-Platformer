using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    [SerializeField] float distance;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("MoveObject");
    }

    IEnumerator MoveObject()
    {
        while (true)
        {
            // Move up and down according to time
            Vector2 movement = new Vector2(0, Mathf.Sin(Time.time * speed) * distance);
            transform.Translate(movement);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
