using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    // Nama item yang bisa diambil (misalnya "Korek")
    public string itemName = "Match";

    // Jarak maksimum player bisa mengambil item
    public float pickUpDistance = 2f;

    // Referensi player (akan otomatis ditemukan)
    private Transform player;

    // Apakah item sudah diambil?
    private bool isTaken = false;

    // Dipanggil saat game mulai
    void Start()
    {
        // Mencari player berdasarkan tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Jika item sudah diambil, hentikan script
        if (isTaken) return;

        // Hitung jarak player ke item
        float distance = Vector3.Distance(transform.position, player.position);

        // Jika player cukup dekat
        if (distance <= pickUpDistance)
        {
            // Tampilkan pesan di console (atau ganti menjadi UI nanti)
            Debug.Log("Tekan E untuk mengambil " + itemName);

            // Jika player menekan tombol E
            if (Input.GetKeyDown(KeyCode.E))
            {
                TakeItem();
            }
        }
    }

    void TakeItem()
    {
        isTaken = true;

        // Sembunyikan item dari scene
        gameObject.SetActive(false);

        // Beri tahu sistem lain bahwa player punya korek
        Debug.Log(itemName + " berhasil diambil!");

        // aktifkan status match di PlayerInventory 
        PlayerInventory.hasMatch = true;
    }
}
