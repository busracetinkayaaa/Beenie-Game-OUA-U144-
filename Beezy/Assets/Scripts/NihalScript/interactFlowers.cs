using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class interactFlowers : MonoBehaviour
{
    public GameObject player;
    public Button collectButton;
    public Button useCapacity;
    private float interactDistance = 4f;
    public List<GameObject> interactPol = new List<GameObject>();
    public List<GameObject> interactVol = new List<GameObject>();
    public List<GameObject> interactSnowy = new List<GameObject>();
    public List<GameObject> interactDes = new List<GameObject>();
    public List<GameObject> interactSwm = new List<GameObject>();

    private List<GameObject> visitedObjects = new List<GameObject>();

    public Sprite[] animImgs;
    public Image animateImgObj;
    private int pollenCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("FantasyBee");
        collectButton.onClick.AddListener(OnClickButton);
        useCapacity.onClick.AddListener(OnClickUseCapacity);

        animateImgObj.sprite = animImgs[0];
    }

    // Update is called once per frame
    void Update()
    {
        bool anyObjectInRange = false;
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;
        float interactDistance = 0f;
        Vector3 buttonPosition = Vector3.zero;

        if (interactPol.Count > 0)
        {
            interactDistance = 5f;
            buttonPosition += new Vector3(150f, 200f, 0f);
            closestObject = FindClosestInteractiveObject(interactPol, ref closestDistance);
            anyObjectInRange = closestObject != null;
        }
        else if (interactVol.Count > 0)
        {
            interactDistance = 10f;
            buttonPosition += new Vector3(0f, 0f, 0f);
            closestObject = FindClosestInteractiveObject(interactVol, ref closestDistance);
            anyObjectInRange = closestObject != null;
        }
        else if (interactSnowy.Count > 0)
        {
            interactDistance = 35f;
            buttonPosition += new Vector3(0f, 0f, 0f);
            closestObject = FindClosestInteractiveObject(interactSnowy, ref closestDistance);
            anyObjectInRange = closestObject != null;
        }
        else if (interactDes.Count > 0)
        {
            interactDistance = 20f;
            buttonPosition += new Vector3(150f, 200f, 0f);
            closestObject = FindClosestInteractiveObject(interactDes, ref closestDistance);
            anyObjectInRange = closestObject != null;
        }
        else if (interactSwm.Count > 0)
        {
            interactDistance = 25f;
            buttonPosition += new Vector3(150f, 200f, 0f);
            closestObject = FindClosestInteractiveObject(interactSwm, ref closestDistance);
            anyObjectInRange = closestObject != null;
        }

        if (collectButton != null)
        {
            collectButton.gameObject.SetActive(anyObjectInRange);

            if (anyObjectInRange)
            {
                Vector3 objPosition = closestObject.transform.position;
                Vector3 buttonScreenPosition = Camera.main.WorldToScreenPoint(objPosition);

                buttonScreenPosition += buttonPosition;

                RectTransform canvasRectTransform = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
                Vector2 viewPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, buttonScreenPosition, null, out viewPos);
                collectButton.GetComponent<RectTransform>().anchoredPosition = viewPos;
            }
        }
    }

    public void AddInteractiveObject(GameObject obj)
    {
        if (interactPol.Contains(obj))
        {
            interactPol.Add(obj);
        }
        else if (interactVol.Contains(obj))
        {
            interactVol.Add(obj);
        }
        else if (interactSnowy.Contains(obj))
        {
            interactSnowy.Add(obj);
        }
        else if (interactDes.Contains(obj))
        {
            interactDes.Add(obj);
        }
        else if (interactSwm.Contains(obj))
        {
            interactSwm.Add(obj);
        }
    }

    public void RemoveInteractiveObject(GameObject obj)
    {
        if (interactPol.Contains(obj))
        {
            interactPol.Remove(obj);
        }
        else if (interactVol.Contains(obj))
        {
            interactVol.Remove(obj);
        }
        else if (interactSnowy.Contains(obj))
        {
            interactSnowy.Remove(obj);
        }
        else if (interactDes.Contains(obj))
        {
            interactDes.Remove(obj);
        }
        else if (interactSwm.Contains(obj))
        {
            interactSwm.Remove(obj);
        }
    }

    public void OnClickButton()
    {
        GameObject closestObject = FindClosestInteractiveObject();

        if (closestObject != null)
        {
            visitedObjects.Add(closestObject);

            if (useCapacity.interactable)
            {
                if (pollenCounter < 150)
                {
                    pollenCounter += 10;
                    if (pollenCounter >= 150)
                    {
                        collectButton.interactable = false;
                        useCapacity.interactable = false;
                    }
                }
            }
            else
            {
                if (pollenCounter < 100)
                {
                    pollenCounter += 10;
                    if (pollenCounter >= 100)
                    {
                        collectButton.interactable = false;
                    }
                }
            }
            UpdateAnimateImage();
        }
    }

    public void OnClickUseCapacity()
    {
        GameObject closestObject = FindClosestInteractiveObject();

        if (closestObject != null)
        {
            visitedObjects.Add(closestObject);

            if (pollenCounter < 150)
            {
                pollenCounter += 10;
                if (pollenCounter >= 150)
                {
                    collectButton.interactable = false;
                    useCapacity.interactable = false;
                }
            }
            UpdateAnimateImage();
        }
    }

    private GameObject FindClosestInteractiveObject(List<GameObject> objects, ref float closestDistance)
    {
        GameObject closestObject = null;

        foreach (GameObject obj in objects)
        {
            if (!visitedObjects.Contains(obj))
            {
                float distance = Vector3.Distance(player.transform.position, obj.transform.position);

                if (distance < interactDistance && distance < closestDistance)
                {
                    closestObject = obj;
                    closestDistance = distance;
                }
            }
        }

        return closestObject;
    }

    private GameObject FindClosestInteractiveObject()
    {
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;

        if (interactPol.Count > 0)
        {
            closestObject = FindClosestInteractiveObject(interactPol, ref closestDistance);
        }
        else if (interactVol.Count > 0)
        {
            closestObject = FindClosestInteractiveObject(interactVol, ref closestDistance);
        }
        else if (interactSnowy.Count > 0)
        {
            closestObject = FindClosestInteractiveObject(interactSnowy, ref closestDistance);
        }
        else if (interactDes.Count > 0)
        {
            closestObject = FindClosestInteractiveObject(interactDes, ref closestDistance);
        }
        else if (interactSwm.Count > 0)
        {
            closestObject = FindClosestInteractiveObject(interactSwm, ref closestDistance);
        }

        return closestObject;
    }

    private void UpdateAnimateImage()
    {
        int spriteIndex = Mathf.Clamp(pollenCounter / 10, 0, animImgs.Length - 1);
        animateImgObj.sprite = animImgs[spriteIndex];
    }
}