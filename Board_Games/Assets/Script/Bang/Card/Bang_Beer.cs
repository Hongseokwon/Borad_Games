using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bang_Beer : Bang_Card
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
        My_Type = Bang_Game_Manager.BANG_CARD.BEER;

        Shape = _Shape;
        Number = _Num;

        Shape_Sprite = Bang_Image_Manager.Instance.Get_Shape_Sprite(_Shape);
        Back_Sprite = Bang_Image_Manager.Instance.Get_Card_Sprite(Bang_Game_Manager.BANG_CARD.BACK);
        Front_Sprite = Bang_Image_Manager.Instance.Get_Card_Sprite(Bang_Game_Manager.BANG_CARD.BEER);
    }

    public override void Card_Use(int Player_Num, int Target_Num = -1)
    {

        if (Player_Num == 0)
            Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_User>().Deck_Card_Use(gameObject);

        string Str_Player = Bang_Game_Manager.Instance.Player_List[Player_Num].GetComponent<Bang_Player>().name;

        Bang_Message_Manager.Instance.Create_Message(Str_Player + "이(가) 맥주 카드를 사용합니다.", 1);

        Bang_Game_Manager.Instance.Player_List[Player_Num].GetComponent<Bang_Player>().Use_Beer();

        if (Player_Num != 0)
            Bang_Game_Manager.Instance.Bot_Card_Use_Done();
    }

    public override void Card_Use_Pre()
    {
        if (Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Character>().Hp <
            Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Character>().Max_Hp)
        Card_Use(Bang_Game_Manager.Instance.Get_Now_Player());

        else
        {
            Bang_Message_Manager.Instance.Create_Message("User의 체력을 더이상 회복할수 없습니다.", 1);
            Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Player>().Set_Card_Pos();
            Bang_Game_Manager.Instance.Zoom_In = true;
        }
    }

    public override bool Bot_Card_Use_Check()
    {
        int n = Bang_Game_Manager.Instance.Get_Now_Player();

        if (Bang_Game_Manager.Instance.Player_List[n].GetComponent<Bang_Character>().Hp <
            Bang_Game_Manager.Instance.Player_List[n].GetComponent<Bang_Character>().Max_Hp)
            return true;

        return false;
    }

    public override void Bot_Card_Use_Pre()
    {
        Card_Use(Bang_Game_Manager.Instance.Get_Now_Player());
    }
}
