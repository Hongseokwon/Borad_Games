using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bang_Card : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public virtual void Card_Use(int Player_Num, int Target_Num = -1) { }
    public virtual void Card_Use_Pre() { }
    public virtual void Bot_Card_Use_Pre() { }
    public virtual bool Bot_Card_Use_Check() { return false; }

    public virtual void Set_Card(Bang_Game_Manager.BANG_CARD_SHAPE _Shape, int _Num) { }
    public void Set_Sorting_Num(int _Snum) { Sorting_Num = _Snum; }

    public void Move_To_Unused_Card()
    {

    }

    public void Set_Card2()
    {
        Card_Shape_Image = gameObject.GetComponentsInChildren<Image>()[1];
        Card_Num = gameObject.GetComponentInChildren<Text>();
        Card_Image = gameObject.GetComponent<Image>();

        Card_Shape_Image.sprite = Shape_Sprite;
        Card_Num.text = Number.ToString();
        Card_Image_Back();
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-45f, 0f);

        Off_Shape_Num();
    }

    protected void Off_Shape_Num()
    {
        Card_Shape_Image.gameObject.SetActive(false);
        Card_Num.gameObject.SetActive(false);
    }

    protected void On_Shape_Num()
    {
        Card_Shape_Image.gameObject.SetActive(true);
        Card_Num.gameObject.SetActive(true);
    }

    public void Card_Image_Front()
    {
        Card_Image.sprite = Front_Sprite;
        On_Shape_Num();
    }

    public void Card_Image_Back()
    {
        Card_Image.sprite = Back_Sprite;
        Off_Shape_Num();
    }

    public Sprite Get_Card_Sprite()
    {
        return Front_Sprite;
    }

    public void Used_Card_Pos_1()
    {
        gameObject.GetComponent<Card_Image_Controller>().Zoom_In_Inactive();
        gameObject.GetComponent<Card_Image_Controller>().Drag_Inactive();
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(90f, 140f);
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(45f, 0f);
        Card_Image_Front();
    }

    public void Used_Card_Pos_2()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-45f, 0f);
        Card_Image_Back();
    }






    public Bang_Game_Manager.BANG_CARD_SHAPE Shape { get; set; }
    public int Number { get; set; }

    public Sprite Front_Sprite;
    public Sprite Back_Sprite;
    public Sprite Shape_Sprite;

    public int Sorting_Num;

    protected Image Card_Image;
    protected Text Card_Num;
    protected Image Card_Shape_Image;

    public Bang_Game_Manager.BANG_CARD My_Type;

    public bool Item;
}
