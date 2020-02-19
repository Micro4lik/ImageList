using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Img : MonoBehaviour, IPointerClickHandler
{
    private GameManager gm;

    void Start()
    {
        gm = GameManager.instance;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        gm.isImageFullcreen = true;
        gm.BigImageSubstrate.SetActive(true);
        gm.BigImage.GetComponent<RawImage>().texture = transform.GetComponent<RawImage>().texture;
        gm.SetOrientation("landscape");
    }


    public void SetFullScreenImage()
    {
        //transform.GetComponent<RawImage>().SetNativeSize();
    }

}
