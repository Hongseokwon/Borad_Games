using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bang_Image_Manager : MonoBehaviour
{
    private static Bang_Image_Manager instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static Bang_Image_Manager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Sprite Get_Class_Sprite(Bang_Game_Manager.BANG_CLASS _Class)
    {
        return Bang_Class[(int)_Class];
    }

    public Sprite Get_Character_Sprite(Bang_Game_Manager.BANG_CHARACTER _Character)
    {
        return Bang_Character[(int)_Character];
    }

    public Sprite Get_Card_Sprite(Bang_Game_Manager.BANG_CARD _Card)
    {
        return Bang_Card[(int)_Card];
    }


    public Sprite[] Bang_Class = new Sprite[(int)Bang_Game_Manager.BANG_CLASS.END];
    public Sprite[] Bang_Character = new Sprite[(int)Bang_Game_Manager.BANG_CHARACTER.END];
    public Sprite[] Bang_Card = new Sprite[(int)Bang_Game_Manager.BANG_CARD.END];
    public Sprite[] Bang_Card_Shape = new Sprite[(int)Bang_Game_Manager.BANG_CARD_SHAPE.END];
}
