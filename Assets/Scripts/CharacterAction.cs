using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CharacterAction : MonoBehaviour
{
    Camera mainCamera;
    public GameObject actionCharacter;
    public GameObject illegalLocationPlane;
    public GameObject legalLocationPlane;

    private GameObject dragCharacter;
    private GameObject illegalSpotPlane;
    private GameObject legalSpotPlane;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        dragCharacter = Instantiate(actionCharacter, Vector3.zero, Quaternion.identity);
        dragCharacter.transform.rotation = Quaternion.Euler(60f, dragCharacter.transform.rotation.y, dragCharacter.transform.rotation.z);
        dragCharacter.name = "Drag Character";
        dragCharacter.SetActive(false);

        illegalSpotPlane = Instantiate(illegalLocationPlane, Vector3.zero, Quaternion.identity);
        illegalSpotPlane.SetActive(false);
        legalSpotPlane = Instantiate(legalLocationPlane, Vector3.zero, Quaternion.identity);
        legalSpotPlane.SetActive(false);
    }


    public void OnBeginDrag(BaseEventData bed)
    {
        Debug.Log("Character Drag Begun" + bed);
    }

    public void OnDrag(BaseEventData bed)
    {
        RaycastHit hit;
        Vector3 coor = ((PointerEventData)bed).position;

        if (Physics.Raycast(mainCamera.ScreenPointToRay(coor), out hit)) 
        {
            // Debug.Log(hit.transform);
            dragCharacter.SetActive(true);
            dragCharacter.GetComponent<Animator>().SetBool("isSwinging", true); // Every frame?
            dragCharacter.transform.position = new Vector3(hit.point.x, 0.5f, hit.point.z);

            if (hit.transform.tag == "Wood")
            {
                illegalSpotPlane.SetActive(false);
                legalSpotPlane.SetActive(true);
                legalSpotPlane.transform.position = new Vector3(hit.transform.position.x, 0.25f, hit.transform.position.z);
            }
            else
            {
                legalSpotPlane.SetActive(false);
                illegalSpotPlane.SetActive(true);
                illegalSpotPlane.transform.position = new Vector3(hit.transform.position.x, 0.25f, hit.transform.position.z);

            }
        } else
        {
            dragCharacter.SetActive(false);
            illegalSpotPlane.SetActive(false);
            legalSpotPlane.SetActive(false);
        }

    }

    public void OnEndDrag(BaseEventData bed)
    {
        dragCharacter.SetActive(false);
        illegalSpotPlane.SetActive(false);
        legalSpotPlane.SetActive(false);

        RaycastHit hit;
        Vector3 coor = ((PointerEventData)bed).position;

        // Debug.Log("DROP !!!!"  + bed);
        if (Physics.Raycast(mainCamera.ScreenPointToRay(coor), out hit))
        {
            if (hit.transform.tag == "Wood")
            {
                // Debug.Log("Legal drop!");
                GameObject character = Instantiate(actionCharacter, new Vector3(hit.transform.position.x, 0.25f, hit.transform.position.z), Quaternion.Euler(0, 180f, 0));
                character.GetComponent<Animator>().SetBool("isFelling", true);

            }
            else
            {
                // Debug.Log("Can't put lumberjack here.");
            }
        }
    }
}
