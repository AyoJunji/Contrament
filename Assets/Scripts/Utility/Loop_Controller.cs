using UnityEngine;
using System.Collections;

public class Loop_Controller : MonoBehaviour 
{
    public float scrollSpeed = 0.5F;
    public Renderer rend;
    [SerializeField] Rigidbody2D playerRB;
    
    void Start() {
    rend = GetComponent<Renderer>();
    }
    void Update() {
        
        if(LevelManager.gamestate == GameState.Game && PlayerController.playerControllerCS.input.x < 0) {
            float offset = Time.time * -scrollSpeed;
            rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        }
        if(LevelManager.gamestate == GameState.Game && PlayerController.playerControllerCS.input.x > 0) {
            float offset = Time.time * scrollSpeed;
            rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        }
    }
}