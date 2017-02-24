using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextRotator : MonoBehaviour {
	internal Transform player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetPoint = player.position;
		Quaternion targetRotation = Quaternion.LookRotation( transform.position - targetPoint);
		transform.rotation = targetRotation;
        //Тут алгоритм какой нибудь надо придумать
        //this.transform.rotation = Quaternion.LookRotation(Vector3.Cross(Char.position, transform.rotation.eulerAngles.normalized));
	}
}
/*public class LookAtPlayer : MonoBehaviour
{
	public float tilt;//наклон корабля
	Transform playerShip;
	void Update()
	{

		var rigidbody = GetComponent<Rigidbody>();

		Plane enemyShip = new Plane(Vector3.up, transform.position);
		//определяет позицию курсора
		playerShip = GameObject.Find("Player").transform;

		// Определение целевой ротации.
		Vector3 targetPoint = playerShip.position;
		Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
		// Поворот к целевой точке.
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20 * Time.deltaTime) * Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt * 2 * Time.deltaTime);

	}
}*/ 