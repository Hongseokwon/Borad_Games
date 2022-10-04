using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public virtual void Set_Card(Bang_Game_Manager.BANG_CARD_SHAPE _Shape, int _Num) { }
    public void Set_Sorting_Num(int _Snum) { Sorting_Num = _Snum; }

    protected Bang_Game_Manager.BANG_CARD_SHAPE Shape { get; set; }
    protected int Number { get; set; }

    protected Sprite Front_Sprite;
    protected Sprite Back_Sprite;
    protected Sprite Shape_Sprite;

    protected int Sorting_Num; 
}
