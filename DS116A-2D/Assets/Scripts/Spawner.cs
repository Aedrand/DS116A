using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {
    public GameObject pickupPrefab;
    public Text winText;
    public int interval;

    private float nextTime = 0;
    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextTime && winText.text == "")
        {
            GameObject.Instantiate(pickupPrefab, new Vector3(Random.Range(-27.0f, 27.0f), Random.Range(-11.0f, 11.0f), 0), Quaternion.identity);

            nextTime += interval;
        }
    }
}
