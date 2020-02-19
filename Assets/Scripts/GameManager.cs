using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    private string url = "http://data.ikppbb.com/test-task-unity-data/pics/";

    public int size;

    private string[] images;
    private GameObject[] imagesGO;

    public GameObject imagePrefab;

    public GameObject contentImages;
    private RectTransform contentRectTransform;

    public Scrollbar sb;

    private bool downloadingImages;
    
    [HideInInspector]
    public Vector3 startPosition;
    [HideInInspector]
    public bool isImageFullcreen;

    private int quantityImages;

    private float screenHeight;
    private float contentHeight;

    public GameObject BigImage;
    public GameObject BigImageSubstrate;

    void Awake()
    {
        downloadingImages = true;
        Invoke("SetContentHeight", 0.1f);
        
        screenHeight = Screen.height;

        startPosition = contentImages.transform.position;
        imagesGO = new GameObject[size];

        InitializeImagesArray();

        quantityImages = 1;
        CreateImage(10);

        contentRectTransform = contentImages.GetComponent<RectTransform>();
    }

    private void InitializeImagesArray()
    {
        images = new string[size];

        for (int i = 1; i < size; i++)
        {
            images[i] = url + i + ".jpg";
        }
    }

    void SetContentHeight()
    {
        contentHeight = contentImages.GetComponent<RectTransform>().sizeDelta.y;
        sb.interactable = true;
        downloadingImages = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isImageFullcreen)
            {
                BigImageSubstrate.SetActive(false);
                isImageFullcreen = false;
                SetOrientation("portrait");
            }
            else
            {
                Application.Quit();
                Debug.Log("quit");
            }
        }
    }

    public void SetOrientation(string orientation)
    {
        switch (orientation)
        {
            case "portrait":
                Screen.orientation = ScreenOrientation.Portrait;
                Screen.orientation = ScreenOrientation.AutoRotation;

                Screen.autorotateToLandscapeLeft = false;
                Screen.autorotateToLandscapeRight = false;
                break;
            case "landscape":
                Screen.autorotateToLandscapeLeft = true;
                Screen.autorotateToLandscapeRight = true;
                break;
        }
    }
    

    public void CreateImage(int amount)
    {
        for (int i = 1; i < amount + 1; i++, quantityImages++)
        {
            if (quantityImages < size)
            {
                imagesGO[quantityImages] = Instantiate(imagePrefab, contentImages.transform);
                if (i < amount + 1)
                {
                    StartCoroutine(DownloadImage(images[quantityImages], imagesGO[quantityImages]));
                }
            }
        }
    }

    IEnumerator waitDownloading()
    {
        yield return new WaitForEndOfFrame();
        contentHeight = contentImages.GetComponent<RectTransform>().sizeDelta.y;
        downloadingImages = false;
    }

    IEnumerator DownloadImage(string MediaUrl, GameObject img)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            img.GetComponent<RawImage>().texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            img.GetComponent<RawImage>().raycastTarget = true;
        }
    }

    public void NextImages()
    {
        if (contentRectTransform.anchoredPosition.y + screenHeight > contentHeight && !downloadingImages)
        {
            downloadingImages = true;
            CreateImage(4);
            StartCoroutine(waitDownloading());
        }
    }
    
}
