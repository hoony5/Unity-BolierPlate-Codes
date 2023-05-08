using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DebugConsole
{
    public class DebugConsole : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] private TMP_InputField inputCommand;
        [SerializeField] private TextMeshProUGUI placeholderCommand;

        [SerializeField] private DebugCommand[] commands;
        
        [SerializeField] private CanvasGroup console;
        [SerializeField] private TextMeshProUGUI historyPrefab;

        private DebugCommand _currentCommand;
        private int _currentTarget;
        
        private bool _open = false;

        private void Open()
        {
            _open = true;
            console.alpha = 1;
            EventSystem.current.SetSelectedGameObject(inputCommand.gameObject);
            placeholderCommand.text = "";
        }
        
        private void Close()
        {
            _open = false;
            console.alpha = 0;
            inputCommand.text = "";
            EventSystem.current.SetSelectedGameObject(null);
        }

        private void TabInput()
        {
            var split = inputCommand.text.Split(' ');
            inputCommand.SetTextWithoutNotify(placeholderCommand.text + " ");
            inputCommand.MoveTextEnd(false);
            split = inputCommand.text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            switch (split.Length)
            {
                case 1:
                    _currentCommand = commands.First(i =>
                        string.Equals(i.Name, split[0], StringComparison.CurrentCultureIgnoreCase));
                    break;
                case 2 when
                    (!string.IsNullOrWhiteSpace(_currentCommand.TargetOptionsType) && _currentCommand != null):
                    _currentTarget = _currentCommand.Targets.FindIndex(i =>
                        string.Equals(i.name, split[1], StringComparison.CurrentCultureIgnoreCase));
                    break;
            }
        }

        private void ReturnInput()
        {
            Debug.Log(_currentTarget);
            var split = inputCommand.text.Split(' ');
            
            switch (_currentCommand.NumParameters)
            {
                case 0 when string.IsNullOrWhiteSpace(_currentCommand.TargetOptionsType):
                    _currentCommand.Execute(target, "");
                    break;
                case 0: 
                    _currentCommand.Execute(target, _currentCommand.Targets[_currentTarget].name);
                    break;
                case 1:
                    _currentCommand.Execute(target, _currentCommand.Targets[_currentTarget].name, split[2]);
                    break;
                case 2:
                    _currentCommand.Execute(target, _currentCommand.Targets[_currentTarget].name, split[2], split[3]);
                    break;
            }
            
            
            var history = Instantiate(historyPrefab, historyPrefab.transform.parent, false);
            history.text = $"{_currentCommand.Name} {(_currentCommand.Targets.Any() ? _currentCommand.Targets[_currentTarget].name : "")}";
            history.gameObject.SetActive(true);
            
            ClearInput();
            
            inputCommand.MoveTextStart(false);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(inputCommand.gameObject);
        }

        private void ClearInput()
        {
            inputCommand.MoveTextStart(false);
            inputCommand.text = "";
            placeholderCommand.text = "";
        }

        private void HandleOpenInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Close();

            if (Input.GetKeyDown(KeyCode.Tab))
                TabInput();

            if (Input.GetKeyDown(KeyCode.Return))
                ReturnInput();
        }

        private void Update()
        {
            if (_open)
                HandleOpenInput();
            else if (Input.GetKeyDown(KeyCode.Tab))
                Open();
        }

        public void OnTextChanged(string text)
        {
            var split = inputCommand.text.Split(' ');
            
            switch (split.Length)
            {
                case 1:
                    
                    var foundCommands = commands.Where(i => i.Name.ToLower().StartsWith(split[0].ToLower())).ToList();
      
                    placeholderCommand.text = foundCommands.Count > 0 ? foundCommands[0].Name : "";
                    if (!string.IsNullOrWhiteSpace(placeholderCommand.text))
                        inputCommand.SetTextWithoutNotify(placeholderCommand.text.Substring(0, inputCommand.text.Length));
                    break;
                case 2:
                    
                    var foundItems = _currentCommand.Targets.Where(i => i.name.ToLower().StartsWith(split[1].ToLower())).ToList();

                    if (foundItems.Count > 0)
                    {
                        placeholderCommand.text = foundItems.Count > 0 ? $"{split[0]} {foundItems[0].name}" : split[0];
                        inputCommand.SetTextWithoutNotify(
                            placeholderCommand.text.Substring(0, inputCommand.text.Length));
                    } else placeholderCommand.text = "";

                    break;
            }
        }
}
}