using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class PackageGenerator : MonoBehaviour
{
    public GameObject pickupClass;
    public GameObject[] dropoffZones;
    public GameObject[] pickupZones;
    
    public GameObject currentStart;
    public GameObject currentStop;

    public float timeRemaining = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentStart == null){
            int pickupIndex = Random.Range(0, pickupZones.Length);
            int dropoffIndex = Random.Range(0, dropoffZones.Length);

            currentStart = pickupZones[pickupIndex];
            currentStop = dropoffZones[dropoffIndex];

            currentStart.SetActive(true);
            currentStop.SetActive(true);

            spawnPackage();
        }
    }

    void spawnPackage(){
        
        Instantiate(pickupClass, new Vector3(currentStart.transform.position.x, currentStart.transform.position.y, currentStart.transform.position.z), Quaternion.identity);
        Debug.Log(currentStart.transform.position);
    }

    public void clearActive(){
        if(currentStart != null){
            currentStart.SetActive(false);
        }

        if(currentStop != null){
            currentStop.SetActive(false);
        }

        currentStart = null;
        currentStop = null;
    }
}
