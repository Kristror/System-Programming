
using UnityEngine;
using UnityEngine.UI;

public class NameFirst : MonoBehaviour
{
    [SerializeField]
    private Button buttonConnectClient;
    [SerializeField]
    private Button buttonShowNameInput;
    
    [SerializeField]
    private GameObject nameInputPanel;
    [SerializeField]
    private InputField nameInputField;


    [SerializeField]
    private Server server;
    [SerializeField]
    private Client client;
    void Start()
    {
        server.StartServer();
        buttonShowNameInput.onClick.AddListener(ShowNameinput);
        buttonConnectClient.onClick.AddListener(ConnectClientWithName);
        nameInputPanel.SetActive(false);
    }
    private void ShowNameinput()
    {
        nameInputPanel.SetActive(true);
        buttonShowNameInput.gameObject.SetActive(false);
    }
    private void ConnectClientWithName() 
    {
        //if((nameInputField.text != null)&&(nameInputField.text != ""))
        //    client.ConnectWithName(nameInputField.text);
    }

    private void OnDestroy()
    {
        server.ShutDownServer();
    }

}
