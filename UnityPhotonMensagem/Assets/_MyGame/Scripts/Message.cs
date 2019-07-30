using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Message : MonoBehaviourPunCallbacks
{   
    [Header("Componente da mensagem")]
    public Text txtMensagem;
    public float timeForHide = 3f;

    [Space]
    [Header("Mensagem na tela")]
    public string msgEnter = " entrou na sala";
    public string msgLeft = " saiu da sala";

    // Start is called before the first frame update
    void Awake()
    {
        txtMensagem.gameObject.SetActive(false);
    }

    void Start() {

        PhotonNetwork.NickName = "Player" + Random.Range(1000, 10000);
    }

    //exibir ou esconder a msg
    void ShowMessage(string msg) {

        txtMensagem.text = msg;
        txtMensagem.gameObject.SetActive(true);
        StartCoroutine(WaitForHide(timeForHide));
    }

    private IEnumerator WaitForHide(float waitFortime) {

        yield return new WaitForSeconds(waitFortime);
        txtMensagem.gameObject.SetActive(false);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) {
        ShowMessage(newPlayer.NickName + msgEnter);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) {
        ShowMessage(otherPlayer.NickName + msgLeft);
    }
}
