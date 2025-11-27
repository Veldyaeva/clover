using System.Windows.Forms;

namespace SewingProduction
{
    public partial class TextSplashScreen : Form
    {
        public TextSplashScreen(string message)
        {
            InitializeComponent();
            SetMessage(message);
        }

        public void SetMessage(string message)
        {
            lblMessage.Text = string.IsNullOrWhiteSpace(message) ? "" : message;
        }
    }
}


