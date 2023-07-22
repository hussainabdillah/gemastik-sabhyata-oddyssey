using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour {

  public Vector2 cameraChange;
  public Vector3 playerChange;
  private CameraMovement cam;
  public bool needText;
  public string placeName;
  public GameObject text;
  public Text placeText;

  // Start is called before the first frame update
  void Start() {
    cam = Camera.main.GetComponent<CameraMovement>();
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("SelectedCharacter")) {
      cam.minPosition += cameraChange;
      cam.maxPosition += cameraChange;
      other.transform.position += playerChange;
      if (needText) {
        StartCoroutine(placeNameCo());
      }
    }
  }

  private IEnumerator placeNameCo(){
    text.SetActive(true);
    placeText.text = placeName;
    yield return new WaitForSeconds(4f);
    text.SetActive(false);
  }
}
