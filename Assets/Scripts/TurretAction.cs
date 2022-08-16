using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurretAction : MonoBehaviour
{
    Camera mainCamera;
    // public GameObject gameManager;

    public GameObject turret;
    public GameObject illegalLocationPlane;
    private GameObject illegalPlaneInstance;
    public GameObject legalLocationPlane;
    private GameObject legalPlaneInstance;


    private GameObject dragTurret;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        dragTurret = Instantiate(turret, Vector3.zero, Quaternion.identity);
        TurretController tc = dragTurret.GetComponentInChildren<TurretController>();
        tc.enabled = false;
        dragTurret.SetActive(false);

        illegalPlaneInstance = Instantiate(illegalLocationPlane, Vector3.zero, Quaternion.identity);
        illegalPlaneInstance.SetActive(false);
        legalPlaneInstance = Instantiate(legalLocationPlane, Vector3.zero, Quaternion.identity);
        legalPlaneInstance.SetActive(false);
    }

    public void OnBeginDrag(BaseEventData bed)
    {
        Debug.Log("Turret Drag Begun" + bed);
    }

    public void OnMouseDrag(BaseEventData bed)
    {
        RaycastHit hit;
        Vector3 coor = ((PointerEventData)bed).position;

        if (Physics.Raycast(mainCamera.ScreenPointToRay(coor), out hit))
        {
            dragTurret.SetActive(true);
            dragTurret.transform.position = new Vector3(hit.point.x, 0.5f, hit.point.z);

            if (hit.transform.tag == "Grass")
            {
                legalPlaneInstance.transform.position = new Vector3(hit.transform.position.x, 0.25f, hit.transform.position.z);
                legalPlaneInstance.SetActive(true);
                illegalPlaneInstance.SetActive(false);
            }
            else
            {
                illegalPlaneInstance.transform.position = new Vector3(hit.transform.position.x, 0.25f, hit.transform.position.z);
                illegalPlaneInstance.SetActive(true);
                legalPlaneInstance.SetActive(false);
            }
        }
        else
        {
            dragTurret.SetActive(false);
            illegalPlaneInstance.SetActive(false);
            legalPlaneInstance.SetActive(false);
        }
    }

    public void OnEndDrag(BaseEventData bed)
    {
        dragTurret.SetActive(false);
        illegalPlaneInstance.SetActive(false);
        legalPlaneInstance.SetActive(false);

        RaycastHit hit;
        Vector3 coor = ((PointerEventData)bed).position;

        if (Physics.Raycast(mainCamera.ScreenPointToRay(coor), out hit))
        {
            if (hit.transform.tag == "Grass")
            {
                // Debug.Log("Legal drop!");
                GameObject turretInstance = Instantiate(turret, new Vector3(hit.transform.position.x, 0.25f, hit.transform.position.z), Quaternion.identity);
                TurretController tc = turretInstance.GetComponentInChildren<TurretController>() as TurretController;
            }
            else
            {
                // Debug.Log("Can't put turret here.");
            }
        }
    }
}
