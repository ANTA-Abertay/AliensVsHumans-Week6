using UnityEngine;


public class CameraController : MonoBehaviour
{
    public Transform TPlayer;         // public variable to have the players transform info so i can get its x and y in the start to ove the cam there
    public GameObject Gplayer;        //Public variable to store a reference to the player game object


    private Vector3 _offset;            //Private variable to store the offset distance between the player and camera

    // Use this for initialization
    void Start()
    {
       // set the camera in line with the player but keeping the z distance
        transform.position = new Vector3(TPlayer.position.x,TPlayer.position.y + 2,transform.position.z);
         //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        _offset = transform.position - Gplayer.transform.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        //Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = Gplayer.transform.position + _offset;
    }
}