using UnityEngine;
using TMPro;

public class CollectionSystem : MonoBehaviour
{

    [SerializeField] private AudioClip cherrySound;
    [SerializeField] private TMP_Text cherryText;
    [SerializeField] private GameObject cherryParticles;
    
    private int cherriesCollected = 0;
    
    private AudioSource audioSource;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Cherry")) {
            audioSource.PlayOneShot(cherrySound, 0.5f);
            Destroy(other.gameObject);
            cherriesCollected++;
            cherryText.text = "" + cherriesCollected;
            Instantiate(cherryParticles, other.transform.position, Quaternion.identity);
        }
    }
}
