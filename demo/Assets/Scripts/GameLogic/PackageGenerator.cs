using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

using TMPro;

[RequireComponent(typeof(GameObject))]
public class PackageGenerator : MonoBehaviour
{
    public GameObject pickupClass;
    public GameObject[] dropoffZones;
    public GameObject[] pickupZones;
    
    public GameObject currentStart;
    public GameObject currentStop;

    public GameObject character;
    private ThirdPersonCharacter characterControlScript;
    public float timeRemaining = 30.0f;
	public TextMeshProUGUI currentDeliveryText;
    public TextMeshProUGUI currentDeliveryTime;

    bool timerRunning = false;
    int nDeliveries = 0;
    int currentDeliveryRating = 5;
    int totalRating = 0;
    public GameObject directionArrow;
    RectTransform rt;

    // Start is called before the first frame update
    void Start()
    {
        rt = directionArrow.GetComponent<RectTransform>();
        characterControlScript = character.GetComponent<ThirdPersonCharacter>();

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

        if(currentStart != null){

            directionArrow.SetActive(true);
            GameObject target = currentStart;
            
            if(characterControlScript.count > 0){
                target = currentStop;
            }
            // Get the position of the object in screen space
            Vector3 objScreenPos = Camera.main.WorldToScreenPoint(target.transform.position);

            // Get the directional vector between your arrow and the object
            Vector3 dir = (objScreenPos - rt.position).normalized;

            // Calculate the angle 
            // We assume the default arrow position at 0° is "up"
            float angle = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(dir, Vector3.up));

            // Use the cross product to determine if the angle is clockwise
            // or anticlockwise
            Vector3 cross = Vector3.Cross(dir, Vector3.up);
            angle = -Mathf.Sign(cross.z) * angle;

            // Update the rotation of your arrow
            rt.localEulerAngles = new Vector3(rt.localEulerAngles.x, rt.localEulerAngles.y, angle);

        }

        if(characterControlScript.pickupMade == true){

            startTimer();
            characterControlScript.pickupMade = false;
        }

        if(timerRunning){
            if(currentDeliveryRating > 1){

                timeRemaining -= Time.deltaTime;
                currentDeliveryText.text = "Current Delivery Rating: " + currentDeliveryRating + " stars";
                currentDeliveryTime.text = "Score decreases in " + timeRemaining;

                if(timeRemaining < 0){
                    timeRemaining = 30f;
                    currentDeliveryRating -= 1;
                }
            }
        }

        if(characterControlScript.deliveryMade == true){
            nDeliveries++;
            clearActive();
            characterControlScript.deliveryMade = false;
            
            totalRating += currentDeliveryRating;
            timerRunning = false;
            timeRemaining = 0f;

            
            characterControlScript.ratingHud.text = "Average Delivery Rating: " + totalRating / nDeliveries + " stars";
            currentDeliveryText.text = "Current Delivery Rating: " + currentDeliveryRating + " stars";
            currentDeliveryTime.text = "Score decreases in " + timeRemaining;

        }
    }

    void startTimer(){
        timerRunning = true;
        timeRemaining = 30.0f;
        currentDeliveryRating = 5;
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
