using UnityEngine;

public class PresetChanger : MonoBehaviour
{
    [SerializeField] MovementStats[] presets;
    int currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        SendStatS();
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.N))
       {
            currentIndex++;
            if (currentIndex == 4)
            {
                currentIndex = 0;
            }
       }
        SendStatS();
    }
    void SendStatS()
    {
        GetComponent<PlayerMovement>().SetStats(presets[currentIndex]);       
    }
    


    // enviar alscript de movimiento el movement preset correspondiente en cada momento
}
