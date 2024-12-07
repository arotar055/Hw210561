using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TicTacToeApp
{
    public class GameLogic
    {
        private Button[,] gridButtons;
        private bool isXPlayerTurn;

        public GameLogic(Button[,] buttons, bool playerStarts)
        {
            gridButtons = buttons;
            isXPlayerTurn = playerStarts;
        }

        public void StartNewGame()
        {
            foreach (var button in gridButtons)
            {
                button.Text = string.Empty;
                button.Enabled = true;
                button.BackColor = SystemColors.Control;
            }

            if (!isXPlayerTurn)
            {
                // Если компьютер ходит первым, делаем ход
                AIPlay();
            }
        }

        public void PlayerMove(Button selectedButton)
        {
            if (string.IsNullOrEmpty(selectedButton.Text))
            {
                selectedButton.Text = isXPlayerTurn ? "X" : "O";
                selectedButton.Enabled = false;

                if (IsWinningConditionMet())
                {
                    MessageBox.Show($"{(isXPlayerTurn ? "X" : "O")} выиграл!", "Конец игры");
                    DisableAllButtons();
                    return;
                }

                if (IsDrawConditionMet())
                {
                    MessageBox.Show("Ничья!", "Конец игры");
                    return;
                }

                isXPlayerTurn = !isXPlayerTurn;

                if (!isXPlayerTurn)
                {
                    AIPlay();
                }
            }
        }

        private void AIPlay()
        {
            // Здесь реализуйте логику хода компьютера (можно использовать простую случайную логику)
            MakeRandomMove();

            if (IsWinningConditionMet())
            {
                MessageBox.Show("O выиграл!", "Конец игры");
                DisableAllButtons();
                return;
            }

            if (IsDrawConditionMet())
            {
                MessageBox.Show("Ничья!", "Конец игры");
            }

            isXPlayerTurn = true;
        }

        private void MakeRandomMove()
        {
            Random rng = new Random();
            var availableButtons = gridButtons.Cast<Button>().Where(b => string.IsNullOrEmpty(b.Text)).ToList();
            if (availableButtons.Any())
            {
                var buttonToClick = availableButtons[rng.Next(availableButtons.Count)];
                buttonToClick.Text = "O";
                buttonToClick.Enabled = false;
            }
        }

        private bool IsWinningConditionMet()
        {
            for (int i = 0; i < 3; i++)
            {
                if (gridButtons[i, 0].Text == gridButtons[i, 1].Text && gridButtons[i, 1].Text == gridButtons[i, 2].Text && !string.IsNullOrEmpty(gridButtons[i, 0].Text))
                    return true;
                if (gridButtons[0, i].Text == gridButtons[1, i].Text && gridButtons[1, i].Text == gridButtons[2, i].Text && !string.IsNullOrEmpty(gridButtons[0, i].Text))
                    return true;
            }

            if (gridButtons[0, 0].Text == gridButtons[1, 1].Text && gridButtons[1, 1].Text == gridButtons[2, 2].Text && !string.IsNullOrEmpty(gridButtons[0, 0].Text))
                return true;

            if (gridButtons[0, 2].Text == gridButtons[1, 1].Text && gridButtons[1, 1].Text == gridButtons[2, 0].Text && !string.IsNullOrEmpty(gridButtons[0, 2].Text))
                return true;

            return false;
        }

        private bool IsDrawConditionMet()
        {
            return gridButtons.Cast<Button>().All(b => !string.IsNullOrEmpty(b.Text)) && !IsWinningConditionMet();
        }

        private void DisableAllButtons()
        {
            foreach (var button in gridButtons)
            {
                button.Enabled = false;
            }
        }
    }
}
