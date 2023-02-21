using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bang_Wells_Fargo : Bang_Card
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
        My_Type = Bang_Game_Manager.BANG_CARD.WELLS_FARGO;

        Shape = _Shape;
        Number = _Num;

        Shape_Sprite = Bang_Image_Manager.Instance.Get_Shape_Sprite(_Shape);
        Back_Sprite = Bang_Image_Manager.Instance.Get_Card_Sprite(Bang_Game_Manager.BANG_CARD.BACK);
        Front_Sprite = Bang_Image_Manager.Instance.Get_Card_Sprite(Bang_Game_Manager.BANG_CARD.WELLS_FARGO);

        Item = false;
    }

    public override void Card_Use(int Player_Num, int Target_Num = 0)
    {
        if (Player_Num == 0)
            Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_User>().Deck_Card_Use(gameObject);

        Bang_Message_Manager.Instance.Create_Message(Bang_Game_Manager.Instance.Player_List[Player_Num].GetComponent<Bang_Player>().name + "이(가) 웰스 파고 은행 카드를 사용하였습니다.", 2);

        for (int i = 0; i < 3; ++i)
            Bang_Game_Manager.Instance.Player_List[Player_Num].GetComponent<Bang_Player>().Add_Card(Bang_Game_Manager.Instance.Get_Card());

        if (Player_Num != 0)
            Bang_Game_Manager.Instance.Bot_Card_Use_Done();
    }

    public override void Card_Use_Pre()
    {
        Card_Use(0);
    }

    public override bool Bot_Card_Use_Check()
    {
        return true;
    }

    public override void Bot_Card_Use_Pre()
    {
        Card_Use(Bang_Game_Manager.Instance.Get_Now_Player());
    }
}
