using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public Camera myCam;
    public float maxHeight = 0.1f;
    public float smooth = 1f;
    public float time = 1.5f;

    private MouseLook look = new MouseLook();

    private float defCamPos;
    private int direction = 1;
    private float progress;
    private float step;
	void Start () {
        defCamPos = myCam.transform.localPosition.y;
        look.Init(this.transform, myCam.transform);
	}
	
	// Update is called once per frame
	void Update () {
        float x = Input.GetAxis("Horizontal");
        float y = 0;
        float z = Input.GetAxis("Vertical");
        look.LookRotation(transform, myCam.transform);
        ShakeCam(z);
		if (Input.GetKey(KeyCode.Q)) 
		{
            y = 1;
		}
		else if (Input.GetKey(KeyCode.E)) 
		{
			y = -1;
		}
        
		if (x != 0 || z != 0 || y != 0 )
        {
            transform.Translate(x, y, z);
        }
        
            
	}
    private void ShakeCam(float z)
    {
        if (z == 0)
        {
            direction = 1;
            progress = 0;
        }
        else
        {
            progress += (Time.deltaTime * (1f / time)) * direction;
            if (Mathf.Abs(progress) >= 1)
            {
                direction *= -1;
            }
        }
        Vector3 destiny = new Vector3(myCam.transform.localPosition.x, defCamPos + progress * maxHeight, myCam.transform.localPosition.z);
        myCam.transform.localPosition = Vector3.Lerp(myCam.transform.localPosition, destiny, Time.deltaTime * smooth);
    }
}
