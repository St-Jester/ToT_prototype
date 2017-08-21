using System.Collections;
using UnityEngine;

public class RoomGenerator : MonoBehaviour {

    public Room roomPrefab;
    Room roomInstance;
    public Player playerPrefab;
    Player playerInstance;
    public FogOfWar fogprefab;

    void Start () {
        StartCoroutine(BeginGame());
	}

    private IEnumerator BeginGame()
    {
        roomInstance = Instantiate(roomPrefab) as Room;
        yield return StartCoroutine(roomInstance.Generate());

        playerInstance = Instantiate(playerPrefab,new Vector3(
            roomInstance.GetCell(new IntVector2(0, 1)).transform.position.x,
            0f,
            roomInstance.GetCell(new IntVector2(0, 1)).transform.position.z),
            Quaternion.identity) as Player;       

        playerInstance.SetPlayerInstance(roomInstance.GetCell(new IntVector2(0,1)));
        playerInstance.CreateRoomFog(roomInstance.size,fogprefab);
    }
}
