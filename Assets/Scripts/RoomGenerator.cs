using System.Collections;
using UnityEngine;

public class RoomGenerator : MonoBehaviour {

    public Room roomPrefab;

    Room roomInstance;

    public Player playerPrefab;

    Player playerInstance;

    public FogOfWar fogprefab;

    void Start ()
    {
        StartCoroutine(BeginGame());
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
    }

    private void RestartGame()
    {
        StopAllCoroutines();
        Destroy(roomInstance.gameObject);
        if (playerInstance != null)
        {
            Destroy(playerInstance.gameObject);
            foreach (var item in playerInstance.fogs)
            {
                Destroy(item.gameObject);
                
            }
            Destroy(playerInstance.Fog.gameObject);
            
        }
        StartCoroutine(BeginGame());
    }

    private IEnumerator BeginGame()
    {
        roomInstance = Instantiate(roomPrefab) as Room;
        yield return StartCoroutine(roomInstance.Generate());

        playerInstance = Instantiate(playerPrefab) as Player;     

        playerInstance.CreateRoomFog(roomInstance.size,fogprefab);
        playerInstance.SetLocation(roomInstance.GetCell(roomInstance.exit));
    }

}
