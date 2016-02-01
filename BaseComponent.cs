using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MASIM.classes
{
    public partial class BaseComponent : UserControl 
    {
        public UserControl component;
        public Form1 form;
        public List<string> nextItems;
        public BaseComponent()
        {
            InitializeComponent();
            nextItems = new List<string>();
        }

        public void setComponentLocation()
        {
            int lx = form.rectangleShape1.Location.X;
            int ly = form.rectangleShape1.Location.Y;
            int sx = form.rectangleShape1.Size.Width;
            int sy = form.rectangleShape1.Size.Height;
            int x = lx + ((sx - component.Size.Width) / 2);
            int y = ly + ((sy - component.Size.Height) / 2);
            component.Location = new Point(x, y);
        }

        public void GetComponent(UserControl _component ,Form1 _form)
        {
            component = _component;
            this.BackColor = _component.BackColor;
            form = _form;
        }
        private void BaseComponent_Load(object sender, EventArgs e)
        {
            setComponentLocation();
            BaseComponent_Click(sender, e);
        }

        private void BaseComponent_Click(object sender, EventArgs e)
        {
            form.InvisibleAllComponents();
            component.Visible = true;
            component.BringToFront();
        }

        public System.Drawing.Point GetCenterPosition()
        {
            int x = this.Location.X + (this.Size.Width / 2);
            int y = this.Location.Y + (this.Size.Height / 2);
            return new Point(x, y);
        }

        private void SetBaseComponentIndex()
        {
            for (int i = 0; i < Form1.baseComponents.Count; i++)
            {
                if (this == Form1.baseComponents[i])
                {
                    Form1.baseControllIndex = i;
                }
            }
        }

        private void BaseComponent_MouseDown(object sender, MouseEventArgs e)
        {
            SetBaseComponentIndex();
            BaseComponent_Click(sender, e);
            this.BringToFront();
            Form1.draggedControl = this;
            this.DoDragDrop(this.Text, DragDropEffects.Copy |
            DragDropEffects.Move);
        }

        private void BaseComponent_DragEnter(object sender, DragEventArgs e)
        {
            
            BaseComponent_Click(sender, e);
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }
    }
}
