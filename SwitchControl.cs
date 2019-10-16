using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadCondition
{
    public partial class SwitchControl : UserControl
    {
        public SwitchControl()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true); this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.BackColor = Color.Transparent;
            this.Cursor = Cursors.Hand;
            this.Size = new Size(87, 37);

        }

        private void SwitchControl_Load(object sender, EventArgs e)
        {

        }
        public bool isSwitch = false;
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rec = new Rectangle(0, 0, this.Size.Width, this.Size.Height);
            if (isSwitch)
            {
                g.DrawImage(Properties.Resources.On, rec);
            }
            else
            {
                g.DrawImage(Properties.Resources.Off, rec);
            }
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            isSwitch = !isSwitch;
            this.Invalidate();
            base.OnMouseClick(e);
        }
    }
}
