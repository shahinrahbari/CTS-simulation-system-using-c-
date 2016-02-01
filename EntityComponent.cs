using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsoleApplication1;
using MASIM.classes;

namespace MASIM
{
    public partial class EntityComponent : UserControl
    {
        public EntityComponent(SIMSystem _sys , BaseComponent _bc)
        {
            sys = _sys;
            InitializeComponent();
            bc = _bc;
            happyness = 0;
            
            
        }
        public BaseComponent bc;
        public SIMSystem sys;
        public SimEntity entity;
        public ProbabilityDitributor pd = new ProbabilityDitributor("Uniform",0,1 , "");
        public string name;
        public int creatingRate;
        public string pType;
        public int happyness;
        




        private void EntityComponent_Load(object sender, EventArgs e)
        {
            entity = new SimEntity(creatingRate, name, pd , pType , happyness );
            button1_Click(sender, e);
            bc.label1.Text = entity.name;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            name = textBox1.Text;
        }

        

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            creatingRate = Convert.ToInt32(textBox3.Text.ToString());
        }

        
        private void SaveEntityScript()
        {
            entity = new SimEntity(creatingRate, name, pd );
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Size = new Size(239, 229);
            label2.Visible = true;
            label3.Visible = true;
            button1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            entity = new SimEntity(creatingRate, name, pd , pType,happyness);
            bc.label1.Text = entity.name;
        }

        public void RefreshPD()
        {
            for (int i = 0; i < sys.PDcomponets.Count; i++)
            {
                if (sys.PDcomponets[i].pd.name == textBox2.Text)
                {
                    pd = sys.PDcomponets[i].pd;
                    entity = new SimEntity(creatingRate, name, pd);
                }
            }
        }

        private void EntityComponent_MouseDown(object sender, MouseEventArgs e)
        {
            //this.BringToFront();
            //Form1.draggedControl = this;
            //this.DoDragDrop(this.Text, DragDropEffects.Copy |
            //DragDropEffects.Move);
        }

        private void EntityComponent_DragEnter(object sender, DragEventArgs e)
        {
            //if (e.Data.GetDataPresent(DataFormats.Text))
            //    e.Effect = DragDropEffects.Copy;
            //else
            //    e.Effect = DragDropEffects.None;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }


        public void Sync()
        {
            textBox2.Text = entity.pd.name;
            textBox1.Text = entity.name;
            textBox3.Text = entity.craetingProbability.ToString();
            for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                if (comboBox1.Items[i].ToString() == entity.PType)
                {
                    comboBox1.SelectedIndex = i;
                }
            }
            textBox4.Text = entity.happyness.ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            RefreshPD();

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            pType = comboBox1.Text;
        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                happyness = Convert.ToInt32(textBox4.Text);
            }
        }
    }
}
