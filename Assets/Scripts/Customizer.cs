using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customizer : MonoBehaviour
{
    public PlayerController PC;

    List<Texture2D> skins, heads;

    public int skinsCount, headsCount;

    public int codeHead;

//     void Awake()
//     {
//         skins = new List<Texture2D>();

//         heads = new List<Texture2D>();
//         skins.Add(LoadTexture("Samples/Sprites/Customized/Skins/MarlowStandart.png"));

//         heads.Add(LoadTexture("Samples/Sprites/Customized/Heads/MarlowHeadStandart.png"));
//         heads.Add(LoadTexture("Samples/Sprites/Customized/Heads/MarlowHead2.png"));

       
//         skinsCount = skins.Count;
//         headsCount = heads.Count;
//     }

//     void Update()
//     {
//          if (Input.GetKeyDown(KeyCode.Space) /*&& isGroundedFunc()*/)                                              
//         {
//             Debug.Log("space pressed");
//             ChangeHead();
//         }
//     }

//     void ChangeHead()
//     {
//         Debug.Log(heads[0]);
//         Debug.Log(heads[1]);
//         if(codeHead<heads.Count)
//             PC.ChangeHead(heads[codeHead]);
//     }
}
