using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerRegistration : MonoBehaviour {

    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    Dictionary<PlayerIndex, GamePadState> prevStates;

	void Start () {
        Players.PlayerList = new List<PlayerIndex>();

        prevStates = new Dictionary<PlayerIndex, GamePadState>();
        prevStates.Add(PlayerIndex.One, state);
        prevStates.Add(PlayerIndex.Two, state);
        prevStates.Add(PlayerIndex.Three, state);
        prevStates.Add(PlayerIndex.Four, state);
	}
	
	void Update () {
        for (int i = 0; i < 4; i++)
        {
            playerIndex = (PlayerIndex)i;
            state = GamePad.GetState(playerIndex);
            prevState = prevStates[playerIndex];
            RegisterPlayer();
            prevStates[playerIndex] = state;
        }
	}


    void RegisterPlayer()
    {
        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed && !Players.PlayerList.Contains(playerIndex))
        {
            Players.PlayerList.Add(playerIndex);
            StartCoroutine(Rumble(0f, 1f, 1f, playerIndex));
        }
    }
    private IEnumerator Rumble(float slowRumble, float fastRumble, float time, PlayerIndex playerIndex)
    {
        GamePad.SetVibration(playerIndex, slowRumble, fastRumble);
        yield return new WaitForSeconds(time);
        GamePad.SetVibration(playerIndex, 0, 0);
    }
}


public static class Players
{
    public static List<PlayerIndex> PlayerList;
}