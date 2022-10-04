using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bang_Player : MonoBehaviour
{
    public Bang_Player()
    {
        My_Turn_Check = false;
        Dead_Check = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Set_Class(Bang_Game_Manager.BANG_CLASS _Class) { }
    
    public void Set_Character(Bang_Game_Manager.BANG_CHARACTER _Character)
    {
        Image_Character.sprite = Bang_Image_Manager.Instance.Get_Character_Sprite(_Character);

        Add_Character_Script(_Character);
    }

    protected void Add_Class_Script(Bang_Game_Manager.BANG_CLASS _Class)
    {
        switch (_Class)
        {
            case Bang_Game_Manager.BANG_CLASS.DEPUTY:
                gameObject.AddComponent<Bang_Deputy>();
                break;
            case Bang_Game_Manager.BANG_CLASS.OUTLAW:
                gameObject.AddComponent<Bang_Outlaw>();
                break;
            case Bang_Game_Manager.BANG_CLASS.RENEGADE:
                gameObject.AddComponent<Bang_Renegade>();
                break;
            case Bang_Game_Manager.BANG_CLASS.SHERIFF:
                gameObject.AddComponent<Bang_Sheriff>();
                break;
        }
    }
    protected void Add_Character_Script(Bang_Game_Manager.BANG_CHARACTER _Character)
    {

    }

    public Image Image_Class;
    public Image Image_Character;
    public Image Image_Weapon;

    protected Sprite Class_Sprite_Front;

    protected Bang_Game_Manager.BANG_CLASS My_Class { get; set; }

    protected bool My_Turn_Check;
    protected bool Dead_Check;
}
