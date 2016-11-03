using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraOverlay : MonoBehaviour, IPointerClickHandler {

    public delegate void ClickHandler(int id);
    public event ClickHandler OnClick;

    public GameObject nameText;
    public GameObject noSignalText;

    private int id = 0;

    private bool isMain = false;

    private string cameraName = "Camera";
    public string CameraName {
        get {
            return cameraName;
        }
        set {
            cameraName = value;
            nameText.GetComponent<Text>().text = cameraName.ToUpper();
        }
    }

    private bool signal = true;
    public bool Signal {
        get {
            return signal;
        }
        set {
            signal = value;
            noSignalText.SetActive(!value);
        }
    }

    //Initialise main camera overlay
    public void Init(string name, bool isGrid) {
        isMain = true;
        CameraName = name;

        if (isGrid) {
            GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
        }
        else {
            GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            GetComponent<RectTransform>().anchorMax = new Vector2(0.75f, 1);
        }
        GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    //Initialise camera overlay grid
    public void Init(int id, string name, int order, int columns, int rows) {
        this.id = id;
        CameraName = name;

        int x = order % columns;
        int y = order / rows;
        float width = 1f / columns;
        float height = 1f / rows;
        
        GetComponent<RectTransform>().anchorMin = new Vector2(x * width, 1 - ((y + 1) * height));
        GetComponent<RectTransform>().anchorMax = new Vector2((x + 1) * width, 1 - (y * height));
        GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    //Initialise camera overlay column
    public void Init(int id, string name, int order) {
        this.id = id;
        CameraName = name;

        int rows = 4;

        int y = order;
        float width = 0.25f;
        float height = 1f / rows;

        GetComponent<RectTransform>().anchorMin = new Vector2(1 - width, 1 - ((y + 1) * height));
        GetComponent<RectTransform>().anchorMax = new Vector2(1, 1 - (y * height));
        GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (!isMain) {
            Debug.Log("Camera " + id + " was selected");
            if (OnClick != null) {
                OnClick(id);
            }
        }
    }
}
