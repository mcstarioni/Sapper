using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    public Camera cam;
    private Transform selected;
	void Start () {
        selected = null;
	}
	void Update () {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && hit.transform != null && hit.transform != selected )
        {
            CellBehaviour cell = hit.transform.GetComponent<CellBehaviour>();
            cell.Select();
            if (selected != null)
                selected.GetComponent<CellBehaviour>().Unselect();
            selected = hit.transform;
            //print(selected == null);
        }

        if (Input.anyKeyDown)
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Transform obj = hit.transform;
                CellBehaviour cell = obj.GetComponentInParent<CellBehaviour>();
                //print(cell.GetComponent<Renderer>().material.color.ToString() + " " + cell.status);
                if (Input.GetKeyDown(KeyCode.Mouse0))
                    cell.OnTouch();
                else
                    if (Input.GetKeyDown(KeyCode.Mouse1))
                        cell.SetFlag();
            }
        }
	}
}
