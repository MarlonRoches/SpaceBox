using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_Box
{
    public partial class FormInicio : Form
    {
        public FormInicio()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (VentanaParaElegirElArchivo.ShowDialog()==DialogResult.OK)
            {
                var frm = new FormDeSimulacion();
                //le enviamos la ruta que la ventana capturó

                //en el formulario original, ves la propiedades del textbox
                //la propiedad Modifiers tiene que estar en Public 
                //si no no podra recibir los datos
                frm.textBox1.Text = VentanaParaElegirElArchivo.FileName;
                this.Hide();
                frm.Show();
            }
            else
            {
                //el archivo no se subio, no hará nada
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //solamente abrimos el formulario que contendrá los comandos
            var frm = new FormComandos();
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
