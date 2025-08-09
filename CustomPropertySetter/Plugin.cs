using BepInEx;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;

namespace CustomPropertySetter
{
    [BepInPlugin(Constants.PluginGuid, Constants.PluginName, Constants.PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        private Rect windowRect = new Rect(100, 100, 200, 200);

        private string prop1;
        private string prop2;

        private bool shouldBeOpen;
        
        private void Start()
        {
            Hashtable properties = new Hashtable();
            properties.Add("Custom Property Setter", "Stop reading my custom props bruv");
            PhotonNetwork.LocalPlayer.SetCustomProperties(properties);
        }

        private void OnGUI()
        {
            if (!shouldBeOpen)
                return;
            
            GUIStyle windowStyle = new GUIStyle(GUI.skin.window);
            windowStyle.fontSize = 16;
            windowStyle.alignment = TextAnchor.UpperCenter;
            
            windowRect.height = 0;
            windowRect = GUILayout.Window(148, windowRect, DrawWindow, "Custom Property Setter", windowStyle);
        }

        private void DrawWindow(int id)
        {
            GUILayout.BeginVertical();
            GUILayout.Space(30);
            
            prop1 = GUILayout.TextField(prop1);
            prop2 = GUILayout.TextField(prop2);
            
            if (GUILayout.Button("Set Custom Properties"))
            {
                Hashtable properties = new Hashtable();
                properties.Add(prop1, prop2);
                PhotonNetwork.LocalPlayer.SetCustomProperties(properties);
            }
            
            GUILayout.EndVertical();
            GUI.DragWindow(new Rect(0, 0, windowRect.width, 25));
        }

        private void Update()
        {
            if (UnityInput.Current.GetKeyDown(KeyCode.F1))
                shouldBeOpen = !shouldBeOpen;
        }
    }
}