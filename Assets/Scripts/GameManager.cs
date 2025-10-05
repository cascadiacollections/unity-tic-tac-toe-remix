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
        private Player currentPlayer = Player.X;
        private Player[] boardState = new Player[9];
        private bool gameOver = false;

        // Bitmask state: each bit represents a cell (0-8)
        private int xBoardMask = 0;
        private int oBoardMask = 0;

        // Winning patterns as bitmasks
        private static readonly int[] winningMasks = new int[]
        {
            0b000000111, // Row 0: cells 0,1,2
            0b000111000, // Row 1: cells 3,4,5
            0b111000000, // Row 2: cells 6,7,8
            0b001001001, // Column 0: cells 0,3,6
            0b010010010, // Column 1: cells 1,4,7
            0b100100100, // Column 2: cells 2,5,8
            0b100010001, // Diagonal: cells 0,4,8
            0b001010100, // Diagonal: cells 2,4,6
        };

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
            for (int i = 0; i < boardState.Length; i++)
            {
                boardState[i] = Player.None;
                // Clear any previous mark on the button
                var image = cellButtons[i].GetComponent<Image>();
                image.sprite = null;
                image.color = Color.white; // reset color so transparent icons display correctly
                cellButtons[i].interactable = true;
            }

            currentPlayer = Player.X;
            gameOver = false;
            xBoardMask = 0;
            oBoardMask = 0;
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
            if (gameOver) return;
            if (boardState[index] != Player.None) return;

            // Update internal board state
            boardState[index] = currentPlayer;
            
            // Update bitmask state
            int bitMask = 1 << index;
            if (currentPlayer == Player.X)
            {
                xBoardMask |= bitMask;
            }
            else
            {
                oBoardMask |= bitMask;
            }
            
            // Update UI
            var image = cellButtons[index].GetComponent<Image>();
            image.sprite = currentPlayer == Player.X ? xSprite : oSprite;
            image.color = Color.white;
            cellButtons[index].interactable = false;

            // Check game outcome
            if (CheckForWinner())
            {
                gameOver = true;
                statusText.text = $"Player {currentPlayer} wins!";
                ShowRestartButton();
                return;
            }
            if (IsBoardFull())
            {
                gameOver = true;
                statusText.text = "It's a draw.";
                ShowRestartButton();
                return;
            }

            // Switch player
            currentPlayer = currentPlayer == Player.X ? Player.O : Player.X;
            UpdateStatusMessage();
        }

        /// <summary>
        /// Displays the current player's turn in the UI.
        /// </summary>
        private void UpdateStatusMessage()
        {
            if (statusText != null)
            {
                statusText.text = $"Player {currentPlayer}'s turn";
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
            // Get the current player's bitmask
            int playerMask = currentPlayer == Player.X ? xBoardMask : oBoardMask;
            
            // Check if any winning pattern is satisfied
            foreach (int winMask in winningMasks)
            {
                // If all bits in the winning pattern are set in the player's mask
                if ((playerMask & winMask) == winMask)
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
            // All 9 cells are filled when the combined mask has all 9 bits set
            // 0b111111111 = 0x1FF = 511
            int combinedMask = xBoardMask | oBoardMask;
            return combinedMask == 0b111111111;
        }
    }
}
