using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card_Image_Controller : MonoBehaviour ,IPointerEnterHandler,IPointerExitHandler ,IBeginDragHandler,IDragHandler,IEndDragHandler
{

    public static GameObject Zoom_Pos = null;

    // Start is called before the first frame update
    void Start()
    {
        My_Image = gameObject.GetComponentInChildren<Image>();

        if (Zoom_Pos == null)
        {
            Zoom_Pos = GameObject.Find("Zoom_Pos");
            Zoom_Pos.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Zoom_In_Active()
    {
        Zoom_In = true;
    }

    public void Zoom_In_Inactive()
    {
        Zoom_In = false;
    }

    public void Drag_Active()
    {
        Drag = true;
    }

    public void Drag_Inactive()
    {
        Drag = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Zoom_In && Bang_Game_Manager.Instance.Zoom_In)
        {
            Zoom_Pos.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Zoom_In && Bang_Game_Manager.Instance.Zoom_In)
        {
            Zoom_Pos.SetActive(true);
            Zoom_Pos.GetComponent<Image>().sprite = My_Image.sprite;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Pre_Pos = gameObject.GetComponent<RectTransform>().anchoredPosition;

        if (Drag &&
            Bang_Game_Manager.Instance.Drag &&
            Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Player>().My_Turn_Check) 
        {
            Bang_Game_Manager.Instance.Zoom_In = false;
            Zoom_Pos.SetActive(false);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Drag &&
            Bang_Game_Manager.Instance.Drag &&
            Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Player>().My_Turn_Check)
        {
            transform.position = (Input.mousePosition);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Drag &&
            Bang_Game_Manager.Instance.Drag &&
            Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Player>().My_Turn_Check &&
            Input.mousePosition.y > 235
            )
        {
            gameObject.GetComponent<Bang_Card>().Card_Use_Pre();
        }
        else
        {
            Bang_Game_Manager.Instance.Zoom_In = true;
            gameObject.GetComponent<RectTransform>().anchoredPosition = Pre_Pos;
        }
    }



    public bool Zoom_In = false;
    public bool Drag = false;

    private Vector3 Pre_Pos;

    public Image My_Image;
}
