using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bang_Missed : Bang_Card
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Set_Card(Bang_Game_Manager.BANG_CARD_SHAPE _Shape, int _Num)
    {
        My_Type = Bang_Game_Manager.BANG_CARD.MISSED;

        Shape = _Shape;
        Number = _Num;

        Shape_Sprite = Bang_Image_Manager.Instance.Get_Shape_Sprite(_Shape);
        Back_Sprite = Bang_Image_Manager.Instance.Get_Card_Sprite(Bang_Game_Manager.BANG_CARD.BACK);
        Front_Sprite = Bang_Image_Manager.Instance.Get_Card_Sprite(Bang_Game_Manager.BANG_CARD.MISSED);
    }

    public override void Card_Use(int Player_Num, int Target_Num = 0)
    {

    }

    public override bool Bot_Card_Use_Check()
    {
        return false;
    }

    public override void Card_Use_Pre()
    {
        Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Player>().Set_Card_Pos();
    }
}
