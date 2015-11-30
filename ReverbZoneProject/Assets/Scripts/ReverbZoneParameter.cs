using UnityEngine;
using System.Collections;

public class ReverbZoneParameter : MonoBehaviour {

    FMOD.Studio.EventInstance reverb;
    FMOD.Studio.ParameterInstance reverbDistance;
    private float distance;
    public Transform player;

    // Use this for initialization
    void Start () {
        //distance = Vector3.Distance(transform.position, player.position);
    }

    void OnTriggerEnter() {
        reverb = FMODUnity.RuntimeManager.CreateInstance("event:/snapshot/" + gameObject.name);
        reverb.start();
        reverb.getParameter("reverbDistance", out reverbDistance);
        reverbDistance.setValue(0f);
        print( gameObject.name + " triggered" );
    }

    void OnTriggerExit() {
        print( gameObject.name + " stopped" );
        reverb.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //reverbDistance.setValue(20f);
    }

    // Update is called once per frame
    void Update () {

    }
}
