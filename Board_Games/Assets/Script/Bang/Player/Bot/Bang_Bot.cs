using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bang_Bot : Bang_Player
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Set_Class(Bang_Game_Manager.BANG_CLASS _Class)
    {
        Class_Sprite_Front = Bang_Image_Manager.Instance.Get_Class_Sprite(_Class);
        Class_Sprite_Back = Bang_Image_Manager.Instance.Get_Class_Sprite(Bang_Game_Manager.BANG_CLASS.BACK);

        if (_Class == Bang_Game_Manager.BANG_CLASS.SHERIFF)
            Image_Class.sprite = Class_Sprite_Front;
        else
            Image_Class.sprite = Class_Sprite_Back;

        Add_Class_Script(_Class);

        My_Class = _Class;
    }

    protected Sprite Class_Sprite_Back;

}
