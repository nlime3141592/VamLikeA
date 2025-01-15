using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Unchord
{
    public class LobbyCanvas : UnchordCanvas
    {
        private Button _btnSettings;
        private Button _btnSelectCharacters;
        private Button _btnQuit;

        private void Awake()
        {
            _btnSettings = transform.Find("Navigator/SettingsButton").GetComponent<Button>();
            _btnSelectCharacters = transform.Find("Navigator/SelectCharacterButton").GetComponent<Button>();
            _btnQuit = transform.Find("Navigator/QuitButton").GetComponent<Button>();

            _btnSettings.onClick.AddListener(OnSettingsButtonClick);
            _btnSelectCharacters.onClick.AddListener(OnSelectCharactersButtonClick);
            _btnQuit.onClick.AddListener(OnQuitButtonClick);
        }

        private void OnSettingsButtonClick()
        {
            this.Hide();
            UIManager.Instance.SettingsCanvas.Show();
        }

        private void OnSelectCharactersButtonClick()
        {
            this.Hide();
            // UIManager.Instance.SelectCharactersCanvas.Show();

            // TEST: ĳ���� ���� ������ �����ϰ� �ٷ� ������ �����մϴ�.
            GameManager.Instance.StartGame();
        }

        private void OnQuitButtonClick()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}