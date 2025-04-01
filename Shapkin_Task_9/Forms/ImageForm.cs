using System.Drawing;
using System.Windows.Forms;

namespace Shapkin_Task_9.Forms
{
    public partial class ImageForm : Form
    {
        public ImageForm(Image image)
        {
            InitializeComponent();
            pictureBox1.Image = image;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }
    }
}
