
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Silvermoon.Controls;
using Silvermoon.UI;
using Silvermoon.Classes;
using Silvermoon.OpenGL;
using Silvermoon.Windows;
using System.Drawing;
using System.Threading;
using Silvermoon.Shapes;
using uBalda.Backend;

namespace uBalda
{
    class GamePage : Page
    {
        private FontSprite font = new FontSprite(GlFontFamily.GenericSerif, 20, System.Drawing.FontStyle.Bold, true);
        private Cell curCell;
        private Vocabulary vocabulary;
        private GameField gameField;
        private AIPlayer aiPlayer;
        private HumanPlayer humanPlayer;
        private int fieldSize;
        private Button[,] buttons;
        private int btnSize;
        private TextBox textBox;
        private Label textLbl;
        private Button yesBtn, noBtn;
        private enum State { AIMove, HumanMove, HumanWord };
        private State state;

        public GamePage(int size)
            : base()
        {
            this.fieldSize = size;
            btnSize = 480 / fieldSize;

            vocabulary = new Vocabulary();
            gameField = new GameField(size, vocabulary.RandomWord(size));

            InitializePage();

            state = State.HumanMove;
            ChangeState();
        }

        private void InitializePage()
        {
            SetTransition(400, TransitionMask.Zoom, TransitionMask.Zoom);

            StackPanel panel = new StackPanel
            {
                Bounds = new Rectangle(0, 0, 480, 800)
            };

            Grid grid = new Grid
            {
                //Margin = Margin.Zero,
                //Height = 480
                Bounds = new Rectangle(0, 0, 480, 480)
            };
            for (int i = 0; i < fieldSize; i++)
            {
                grid.Columns.Add(btnSize);
                grid.Rows.Add(btnSize);
            }

            buttons = new Button[fieldSize, fieldSize];
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    int ii = i, jj = j;
                    Button btn = new Button
                    {
                        Content = gameField[i, j].ToString(),
                        Font = font,
                        Color = PhoneColors.DeepBlue,
                        Size = new Size(btnSize, btnSize)
                    };
                    buttons[i, j] = btn;
                    grid.SetControl(btn, j, i);
                }
            }

            panel.Controls.Add(grid);


            textLbl = new Label("Test");
            panel.Controls.Add(textLbl);

            yesBtn = new Button
            {
                Content = "yes",
                Font = font,
                Color = PhoneColors.DeepBlue,
                Size = new Size(btnSize, btnSize)
            };
            panel.Controls.Add(yesBtn);

            noBtn = new Button
            {
                Content = "no",
                Font = font,
                Color = PhoneColors.DeepBlue,
                Size = new Size(btnSize, btnSize)
            };
            panel.Controls.Add(noBtn);

            textBox = new TextBox
            {
                Bounds = new Rectangle(0, 50, 80, 50)
            };
            textBox.MaxLength = 1;
            textBox.Visible = false;
            textBox.LostFocus += (s, e) =>
            {
                buttons[curCell.x, curCell.y].Content = textBox.Text;

                state = State.HumanWord;
                ChangeState();
            };
            textBox.TextChanged += (s, e) =>
            {
                if (textBox.Text.Length > 0)
                {
                    textBox.Text = textBox.Text[textBox.Text.Length - 1].ToString();
                }
                buttons[curCell.x, curCell.y].Content = textBox.Text;
            };

            panel.Controls.Add(textBox);

            Controls.Add(panel);
        }

        private void ChangeState()
        {
            switch (state)
            {
                case State.AIMove:
                    for (int i = 0; i < fieldSize; i++)
                    {
                        for (int j = 0; j < fieldSize; j++)
                        {
                            buttons[i, j].RemoveTapHandlers();
                        }
                    }
                    break;
                case State.HumanMove:
                    for (int i = 0; i < fieldSize; i++)
                    {
                        for (int j = 0; j < fieldSize; j++)
                        {
                            int ii = i, jj = j;

                            buttons[ii, jj].RemoveTapHandlers();

                            buttons[ii, jj].Tap += (s, e) =>
                            {
                                if (textBox.Visible)
                                {
                                    textBox.Unfocus();
                                }
                                else if ((buttons[ii, jj].Content as StringShape).Text == "\0")
                                {
                                    curCell = new Backend.Cell(ii, jj);
                                    textBox.Text = "";
                                    textBox.Visible = true;
                                    textBox.SetFocus();
                                }
                            };
                        }
                    }

                    textLbl.Visible = false;
                    yesBtn.Visible = false;
                    noBtn.Visible = false;
                    textBox.Visible = false;
                    break;
                case State.HumanWord:
                    for (int i = 0; i < fieldSize; i++)
                    {
                        for (int j = 0; j < fieldSize; j++)
                        {
                            int ii = i, jj = j;

                            buttons[ii, jj].RemoveTapHandlers();

                            buttons[ii, jj].Tap += (s, e) =>
                            {
                            };
                        }
                    }

                    textLbl.Visible = true;
                    yesBtn.Visible = true;
                    noBtn.Visible = true;
                    textBox.Visible = false;
                    break;
            }

            UpdateView();
        }

        private void UpdateView()
        {
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    buttons[i, j].Content = gameField[i, j].ToString();
                }
            }
        }
    }
}
