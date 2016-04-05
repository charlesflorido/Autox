using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public CarController car;
    public float moveSpeed;

    private Vector3 targetPos;

    // Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        targetPos = new Vector3(car.gameObject.transform.position.x, car.gameObject.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        
    }
}
