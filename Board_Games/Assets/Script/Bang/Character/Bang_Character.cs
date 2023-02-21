using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bang_Character : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set_Sheriff_Hp()
    {
        Hp++;
        Max_Hp++;
    }

    public virtual void My_Turn_User_Get_Card()
    {
        Bang_Message_Manager.Instance.Create_Message("Player의 카드가져오기", 1);
        gameObject.GetComponent<Bang_Player>().Add_Card(Bang_Game_Manager.Instance.Get_Card());
        gameObject.GetComponent<Bang_Player>().Add_Card(Bang_Game_Manager.Instance.Get_Card());
    }
    public virtual void My_Turn_Bot_Get_Card()
    {
        Bang_Message_Manager.Instance.Create_Message(gameObject.GetComponent<Bang_Bot>().name + "의 카드가져오기", 1);
        gameObject.GetComponent<Bang_Player>().Add_Card(Bang_Game_Manager.Instance.Get_Card());
        gameObject.GetComponent<Bang_Player>().Add_Card(Bang_Game_Manager.Instance.Get_Card());
    }

    public void Hp_UI_Set()
    {
        for(int i=0; i<Max_Hp;++i)
        {
            if (i < Hp)
                gameObject.GetComponent<Bang_Player>().Life[i].SetActive(true);
            else
                gameObject.GetComponent<Bang_Player>().Life[i].SetActive(false);
        }
    }

    public void Hp_Down(int _n)
    {
        Hp -= _n;
        if (Hp < 1)
            gameObject.GetComponent<Bang_Player>().Player_Die();

        Hp_UI_Set();
    }

    public void Hp_Up(int _n)
    {
        Hp += _n;
        if (Hp > Max_Hp)
            Hp = Max_Hp;

        Hp_UI_Set();
    }

    public int Hp;
    public int Max_Hp;

    public Bang_Game_Manager.BANG_CHARACTER My_Character;
}
