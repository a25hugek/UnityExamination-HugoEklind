using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{

    [SerializeField] private int levelToLoad;
    [SerializeField] private GameObject doorClosed, doorOpen;
    [SerializeField] private AudioClip doorOpenSound, doorCloseSound;

    private AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void LoadLevel() {
        SceneManager.LoadScene(levelToLoad);
    }

    private void CloseDoor() {
        doorOpen.SetActive(false);
        doorClosed.SetActive(true);
        audioSource.PlayOneShot(doorCloseSound, 1f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.CompareTag("Player")) {
            doorClosed.SetActive(false);
            doorOpen.SetActive(true);
            other.GetComponent<Rigidbody2D>().transform.position = transform.position;
            other.gameObject.SetActive(false);
            audioSource.PlayOneShot(doorOpenSound, 1f);
            Invoke("CloseDoor", 1f);   
            Invoke("LoadLevel", 2f);    
        }

    }
}
