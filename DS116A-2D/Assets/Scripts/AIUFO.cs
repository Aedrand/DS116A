using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIUFO : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;
    public GameObject pickupPrefab;

    private Rigidbody2D ufoBody;
    private int count;

    // Use this for initialization
    void Start () {
        ufoBody = GetComponent<Rigidbody2D>();
        count = 0;
        setCountText();
    }
	
	void FixedUpdate () {
        Transform target = getNearestPickup();
        if(target != null)
        {
            ufoBody.AddForce((target.position - transform.position)*speed);
        }
        
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PickUp"))
        {
            collision.gameObject.SetActive(false);

            count++;
            setCountText();
        }
    }

    void setCountText()
    {
        countText.text = "Collected: " + count + "/12";

        if (count >= 12)
        {
            winText.text = "You Lose!";
            GameObject[] objs = GameObject.FindGameObjectsWithTag("PickUp");
            foreach (GameObject target in objs)
            {
                GameObject.Destroy(target);
            }

        }
    }

    Transform getNearestPickup()
    {
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("PickUp");
        List<Transform> locations = new List<Transform>();

        foreach(GameObject pickup in pickups)
        {
            locations.Add(pickup.transform);
        }

        if (locations.Count != 0)
        {
            Transform bestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = transform.position;
            foreach (Transform checkedPickup in locations)
            {
                Vector3 directionToTarget = checkedPickup.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = checkedPickup;
                }
            }
            return bestTarget;
        }
        else
        {
            return null;
        }
    }
}
