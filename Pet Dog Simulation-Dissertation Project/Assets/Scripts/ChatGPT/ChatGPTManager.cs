using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using OpenAI.Chat;
using System;
using TMPro;

public class ChatGPTManager : MonoBehaviour
{
    [Header("Authentication")]
    public string apiKey, organisaionID;
    private OpenAIClient api;
    public string userName;

    [Header("Chat")]
    [Header("Settings")]
    [SerializeField] List<Message> chatPrompts = new List<Message>();

    [Header("Events")]
    public static Action onMessageReceived;
    public static Action<string> onChatGPTMessageReceived;

    [Header("Input Field")]
    public TMP_InputField askMsgInputField;
    [SerializeField] private TextMeshProUGUI responseMsgText;

    public GameObject responseBubblePrefab;

    private void Awake()
    {
        Authenticate();InitializeGPT();
        //askMsgInputField.gameObject.SetActive(false);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }



    private void Authenticate()
    {
        api = new OpenAIClient(new OpenAIAuthentication(apiKey, organisaionID));
    }

    private void InitializeGPT()
    {
        Message prompt = new Message(Role.System, "I am a cute puppy named Jason!");

        chatPrompts.Add(prompt);


    }

    public async void AskMessageCallback()
    {
        Message prompt = new Message(Role.User, askMsgInputField.text);
        chatPrompts.Add(prompt);
        askMsgInputField.text = "";

        ChatRequest request = new ChatRequest(messages: chatPrompts, model: OpenAI.Models.Model.GPT4_Turbo);

        try
        {

            var result = await api.ChatEndpoint.GetCompletionAsync(request);
            Message chatResult = new Message(Role.System, result.FirstChoice.ToString());
            chatPrompts.Add(chatResult);
            onChatGPTMessageReceived?.Invoke(result.FirstChoice.ToString());
            responseMsgText.ForceMeshUpdate();
            CreateResponseBubble(result.FirstChoice.ToString());


        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    public void CreateResponseBubble(string message)
    {

       
        responseBubblePrefab.SetActive(true);

        responseMsgText.text = message;
        responseMsgText.ForceMeshUpdate();
        onMessageReceived?.Invoke();
    }
}
