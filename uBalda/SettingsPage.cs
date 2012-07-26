using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Silvermoon.Controls;
using Silvermoon.Shapes;
using Silvermoon.Images;
using Silvermoon.Windows.Styles;
using Silvermoon.OpenGL;
using Silvermoon.Windows;
using Silvermoon.UI;
using Silvermoon.Controls.Forms;
using Silvermoon.Controls.Base;
using System.Drawing;

namespace uBalda
{
    public class SettingsPage : Page
    {
        private FontSprite font = new FontSprite(GlFontFamily.GenericSerif, 20, System.Drawing.FontStyle.Bold, true);
        private Label sizeLabel;
        private Button[] sizeBtns;

        public SettingsPage()
            : base()
        {
            InitializePage();
        }

        private void InitializePage()
        {
            Toolbar = new Toolbar();
            ToolbarButton toolbarBtn = new ToolbarButton { Content = ">" };
            toolbarBtn.Tap += (s, e) =>
            {
                for (int i = 0; i < sizeBtns.Length; i++)
                {
                    if (sizeBtns[i].Color == PhoneColors.Accent)
                    {
                        NavigateTo(new GamePage(i + 4));
                        break;
                    }
                }
            };
            (Toolbar as Toolbar).Buttons.Add(toolbarBtn);

            SetTransition(400, TransitionMask.Zoom, TransitionMask.Zoom);

            Panel panel = new Panel { Margin = new Margin(0), Height = 800 };

            panel.Controls.Add(new Label("Размер поля:") { Font = font });

            sizeBtns = new Button[8 - 4 + 1];
            for (int i = 4; i <= 8; i++)
            {
                Button btn = new Button
                {
                    Content = i.ToString(),
                    Color = (i == 5 ? PhoneColors.Accent : PhoneColors.Highlight),
                    Font = font,
                    Bounds = new Rectangle((i - 4) * 90, 100, 90, 90)
                };
                btn.Tap += (s, e) => 
                {
                    for (int j = 0; j < sizeBtns.Length; j++)
                    {
                        sizeBtns[j].Color = PhoneColors.Highlight;
                    }
                    btn.Color = PhoneColors.Accent;
                };
                sizeBtns[i - 4] = btn;
                panel.Controls.Add(sizeBtns[i - 4]);
            }

            Controls.Add(panel);
        }
    }
}
