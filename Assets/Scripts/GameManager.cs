using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe
{
    /// <summary>
    /// Central coordinator for the Tic‑Tac‑Toe game.
    /// Manages whose turn it is, stores the board state, reacts to button clicks,
    /// checks for win/draw conditions and updates the UI accordingly.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        // Enumeration representing the three possible cell states.
        private enum Player { None, X, O }

        [Header("UI References")]
        [Tooltip("Array of the nine buttons representing the game board. These must be assigned in the inspector in row‑major order.")]
        public Button[] cellButtons;

        [Tooltip("Sprite used to draw an X in a cell.")]
        public Sprite xSprite;

        [Tooltip("Sprite used to draw an O in a cell.")]
        public Sprite oSprite;

        [Tooltip("Reference to a Text or TMP component used to display game status messages.")]
        public Text statusText;

        [Tooltip("Button used to restart the match. Should be disabled until the game ends.")]
        public Button restartButton;

        // Internal state
        private Player _currentPlayer = Player.X;
        private Player[] _boardState = new Player[9];
        private bool _gameOver = false;

        private void Awake()
        {
            // Sanity check: ensure we have exactly nine buttons hooked up.
            if (cellButtons == null || cellButtons.Length != 9)
            {
                Debug.LogError("GameManager: cellButtons must contain exactly 9 entries");
            }

            // Assign click handlers to each button
            for (int i = 0; i < cellButtons.Length; i++)
            {
                int index = i; // capture local copy for closure
                cellButtons[i].onClick.AddListener(() => OnCellClicked(index));
            }

            // Initialize restart button
            if (restartButton != null)
            {
                restartButton.onClick.AddListener(RestartGame);
                restartButton.gameObject.SetActive(false);
            }
        }

        private void Start()
        {
            StartNewGame();
        }

        /// <summary>
        /// Sets up a fresh board and resets UI.
        /// </summary>
        private void StartNewGame()
        {
            for (int i = 0; i < _boardState.Length; i++)
            {
                _boardState[i] = Player.None;
                // Clear any previous mark on the button
                var image = cellButtons[i].GetComponent<Image>();
                image.sprite = null;
                image.color = Color.white; // reset color so transparent icons display correctly
                cellButtons[i].interactable = true;
            }

            _currentPlayer = Player.X;
            _gameOver = false;
            UpdateStatusMessage();
            if (restartButton != null)
            {
                restartButton.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Called when a cell button is clicked. Marks the cell if possible and updates the game state.
        /// </summary>
        /// <param name="index">Index of the clicked button in the board array.</param>
        private void OnCellClicked(int index)
        {
            if (_gameOver) return;
            if (_boardState[index] != Player.None) return;

            // Update internal board state
            _boardState[index] = _currentPlayer;
            // Update UI
            var image = cellButtons[index].GetComponent<Image>();
            image.sprite = _currentPlayer == Player.X ? xSprite : oSprite;
            image.color = Color.white;
            cellButtons[index].interactable = false;

            // Check game outcome
            if (CheckForWinner())
            {
                _gameOver = true;
                statusText.text = $"Player {_currentPlayer} wins!";
                ShowRestartButton();
                return;
            }
            if (IsBoardFull())
            {
                _gameOver = true;
                statusText.text = "It's a draw.";
                ShowRestartButton();
                return;
            }

            // Switch player
            _currentPlayer = _currentPlayer == Player.X ? Player.O : Player.X;
            UpdateStatusMessage();
        }

        /// <summary>
        /// Displays the current player's turn in the UI.
        /// </summary>
        private void UpdateStatusMessage()
        {
            if (statusText != null)
            {
                statusText.text = $"Player {_currentPlayer}'s turn";
            }
        }

        /// <summary>
        /// Shows the restart button when the game has ended.
        /// </summary>
        private void ShowRestartButton()
        {
            if (restartButton != null)
            {
                restartButton.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// Resets the board and UI when the restart button is clicked.
        /// </summary>
        private void RestartGame()
        {
            StartNewGame();
        }

        /// <summary>
        /// Determines if the current player has achieved a winning line.
        /// </summary>
        /// <returns>True if the current player has three in a row; otherwise false.</returns>
        private bool CheckForWinner()
        {
            int[][] winningLines = new int[][]
            {
                new [] {0, 1, 2}, // Row 0
                new [] {3, 4, 5}, // Row 1
                new [] {6, 7, 8}, // Row 2
                new [] {0, 3, 6}, // Column 0
                new [] {1, 4, 7}, // Column 1
                new [] {2, 5, 8}, // Column 2
                new [] {0, 4, 8}, // Diagonal
                new [] {2, 4, 6}, // Diagonal
            };

            foreach (var line in winningLines)
            {
                if (_boardState[line[0]] == _currentPlayer &&
                    _boardState[line[1]] == _currentPlayer &&
                    _boardState[line[2]] == _currentPlayer)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if all cells on the board are filled.
        /// </summary>
        /// <returns>True if no empty cells remain; otherwise false.</returns>
        private bool IsBoardFull()
        {
            foreach (var cell in _boardState)
            {
                if (cell == Player.None) return false;
            }
            return true;
        }
    }
}
