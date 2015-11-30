using UnityEngine;
using System.Collections;
using System.Linq;

public class CharController : MonoBehaviour {

    public float movementSpeed;
    public int velocityThreshold;
    public float panAmount = 0.3f;
    bool leftFoot = true;
    bool rightFoot = true;
    bool isMoving;
    float magnitude;
    string[] alphabetString = new string[] { "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z" };
    void MoveForward() {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, movementSpeed);
    }

    // Use this for initialization
    void Start() {

    }

    //Check collisions with reverb zones
    //void OnTriggerEnter(Collider other) {
    //    if ( other.gameObject.tag == "ReverbZone" ) {
    //        print("contact with reverb zone!");
    //    }
    //}
    // Update is called once per frame
    void Update() {
        magnitude = GetComponent<Rigidbody>().velocity.magnitude;
        //print(magnitude);
        
        //Character move script
        if ( Input.GetKeyDown(KeyCode.LeftShift) && magnitude < velocityThreshold ) {
            if ( rightFoot ) {
                MoveForward();
                //print("left normal footstep");
                leftFoot = true;
                rightFoot = false;
                FMODUnity.RuntimeManager.PlayOneShot("event:/character/footstep", transform.position - Vector3.right * panAmount);
            } else if ( !rightFoot && magnitude < 1f ) {
                MoveForward();
                //print("left double step");
                FMODUnity.RuntimeManager.PlayOneShot("event:/character/footstep", transform.position - Vector3.right * panAmount);
                FMODUnity.RuntimeManager.PlayOneShot("event:/character/doubleFootstep", transform.position - Vector3.left * panAmount);
            }
        }

        if ( Input.GetKeyDown(KeyCode.RightShift) && magnitude < velocityThreshold ) {
            if ( leftFoot ) {
                MoveForward();
                //print("right normal footstep");
                rightFoot = true;
                leftFoot = false;
                FMODUnity.RuntimeManager.PlayOneShot("event:/character/footstep", transform.position - Vector3.left * panAmount);
            } else if ( !leftFoot && magnitude < 1f ) {
                MoveForward();
                //print("right double step");
                FMODUnity.RuntimeManager.PlayOneShot("event:/character/footstep", transform.position - Vector3.left * panAmount);
                FMODUnity.RuntimeManager.PlayOneShot("event:/character/doubleFootstep", transform.position - Vector3.right * panAmount);
            }
        }

        //clap function
        if ( Input.GetKeyDown(KeyCode.Space) ) {
            print("CLAP!");
            FMODUnity.RuntimeManager.PlayOneShot("event:/character/clap",transform.position);
        }

        //Say different letters
        if ( Input.anyKeyDown ) {
            //print(Input.inputString);
            bool targetStringInArray = alphabetString.Contains(Input.inputString);
            if ( targetStringInArray ) {
                FMODUnity.RuntimeManager.PlayOneShot("event:/character/alphabet/" + Input.inputString, transform.position);
                print("Playing " + Input.inputString);
            } else {
                print("oops I registered " + Input.inputString);
            }

        }
    }


}