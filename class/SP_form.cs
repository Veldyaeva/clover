using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace SewingProduction 
{
    public class SP_form: Form
    {
        // Свойства по умолчанию
        private Color backgroundColor = Color.AliceBlue;
        private bool showOkButton = true;
        private bool showCancelButton = true;


        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }

        public bool ShowOkButton
        {
            get { return showOkButton; }
            set { showOkButton = value; }
        }

        public bool ShowCancelButton
        {
            get { return showCancelButton; }
            set { showCancelButton = value; }
        }

        public SP_form()
        {
            // Установка свойств по умолчанию при создании формы
            this.BackColor = backgroundColor;
            InitializeButtons();
        }

        private void InitializeButtons()
        {
            Button okButton = null;
            if (showOkButton)
            {
                okButton = new Button();
                okButton.Text = "OK";
                okButton.Location = new Point(10, this.Height - 40); // Позиционирование кнопки
                okButton.Click += OkButton_Click;
                this.Controls.Add(okButton);
            }

            if (showCancelButton)
            {
                Button cancelButton = new Button();
                cancelButton.Text = "Cancel";
                //Point okButtonLocation = this.PointToClient(okButton.Location);
                cancelButton.Location = new Point(okButton.Right + 10, this.Height - 40); // Позиционирование кнопки
                cancelButton.Click += CancelButton_Click;
                this.Controls.Add(cancelButton);
            }
        }

        protected virtual void OkButton_Click(object sender, EventArgs e)
        {
            // Действия по нажатию на кнопку OK
            this.Close();
        }

        protected virtual void CancelButton_Click(object sender, EventArgs e)
        {
            // Действия по нажатию на кнопку Cancel
            this.Close();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.BackColor = backgroundColor; // Убеждаемся, что цвет фона установлен корректно
        }
    }
}
