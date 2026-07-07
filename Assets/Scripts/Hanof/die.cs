using UnityEngine;

//////////////////////////////////////////////  
//                                          //
//       This script shall live on:         //
//              Any Object !                //
//                                          //
//////////////////////////////////////////////

// script's purpose: once an object is enabled, it counts for the time we set
//                   and then destroys it ! :)

// script's requirements: nothing ! just put it on the thing u want to kill >:3
public class die : MonoBehaviour
{

    public float life = 1.5f;

    void OnEnable()
    {
       Destroy(gameObject,life);
    }

}
