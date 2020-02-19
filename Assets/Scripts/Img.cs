using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Image : MonoBehaviour, IPointerClickHandler
{
    private GameManager gm;

    private Vector3 q;

    //public GameObject BigImage;

    void Start()
    {
        gm = GameManager.instance;

        q = gm.startPosition - transform.position;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        gm.BigImage.SetActive(true);
        Debug.Log(gm.BigImage.GetComponent<Image>());
        gm.BigImage.GetComponent<Image>().SetFullScreenImage();
    }


    public void SetFullScreenImage()
    {
        transform.GetComponent<RawImage>().SetNativeSize();
    }


    /*void Update()
    {

        Debug.Log(q.y);

        if (q.y == 9.8f)
        {
            Debug.Log("Появилося");
        }
    }*/
}
