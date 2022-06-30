using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarExitEnterSystem : MonoBehaviour
{

    public MonoBehaviour CarController;
    public Transform Player;
    public Transform Car;
    public GameObject DriveHint;

    [Header("Cameras")]
    public GameObject PlayerCam;
    public GameObject CarCam;

    private bool Candrive;
    //private bool driving;

    // Start is called before the first frame update
    void Start()
    {
        // shift from car to human character so that the player knows the location of both
        PlayerCam.gameObject.SetActive(true);
        CarCam.gameObject.SetActive(false);
        CarController.enabled = false;
        DriveHint.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Candrive)
        {
            CarController.enabled = true;
            DriveHint.gameObject.SetActive(false);
            //driving = true;
            Player.transform.SetParent(Car);
            Player.gameObject.SetActive(false);
            // camera switch
            PlayerCam.gameObject.SetActive(false);
            CarCam.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.O) && Candrive)
        {
            CarController.enabled = false;
            DriveHint.gameObject.SetActive(true);
            //driving = false;
            Player.transform.SetParent(null);
            Player.gameObject.SetActive(true);
            // camera switch
            PlayerCam.gameObject.SetActive(true);
            CarCam.gameObject.SetActive(false);
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DriveHint.gameObject.SetActive(true); // hint the player how to drive
            Candrive = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DriveHint.gameObject.SetActive(false);
            Candrive = false;
        }
    }
        
}
