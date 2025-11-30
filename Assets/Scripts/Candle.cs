using UnityEngine;

public class Candle : MonoBehaviour
{
    public Light candleLight;            // Cahaya lilin
    public float interactDistance = 2f;  // Jarak interaksi
    public bool isLit = false;           // Apakah lilin sudah menyala?

    private Transform player;            // Referensi player

    void Start()
    {
        // Temukan player berdasarkan tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Matikan cahaya lilin saat awal game
        if (candleLight != null)
            candleLight.enabled = false;
    }

    void Update()
    {
        if (isLit) return;  // Jika sudah menyala, stop script

        // Hitung jarak player → lilin
        float distance = Vector3.Distance(transform.position, player.position);

        // Jika cukup dekat
        if (distance <= interactDistance)
        {
            Debug.Log("Tekan E untuk menyalakan lilin");

            // Tekan E untuk menyalakan
            if (Input.GetKeyDown(KeyCode.E))
            {
                TryLightCandle();
            }
        }
    }

    void TryLightCandle()
    {
        // Cek apakah player punya korek
        if (PlayerInventory.hasMatch)
        {
            LightCandle();
        }
        else
        {
            Debug.Log("Kamu butuh korek untuk menyalakan lilin.");
        }
    }

    void LightCandle()
    {
        isLit = true;

        // Menyalakan cahaya lilin
        if (candleLight != null)
            candleLight.enabled = true;

        Debug.Log("Lilin menyala!");

        // Tambahkan efek suara / partikel nanti
    }
}
