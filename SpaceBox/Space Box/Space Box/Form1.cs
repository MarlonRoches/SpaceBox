using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_Box
{
    public partial class FormDeSimulacion : Form
    {
        public static string Ruta { get; set; }
        public static string[,] Matriz = new string[15, 15];
        public static int PosX;
        public static int PosY;
        public bool Encontrado=false;
        public bool SimulacionIniciada=false;
        public static int Pasos = 0;

        public static int Instrucciones=0;
        public static int Puntos=0;
        //pendientes
        //validar que solo haya un planeta y solo una box
        //
        //

        public FormDeSimulacion()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        public void NombreDeLaNave(string Nombre,string Apellido)
        {
            LabelNombreDeLaNave.Text = $"GUA-{Nombre[Nombre.Length-1]}{Apellido[0]}-{Nombre.Length+Apellido.Length}";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //recibimos la ruta que nos pusieron en el formulario de inicio para utilizarla luego
            Ruta = textBox1.Text;
            textBox1.Visible = false;
        }

        void LlenarMapa(string Ruta)
        {
            //variable para acceder al archivo y leer su contenido, recibe como parametro la ruta deonde se encuentra el archivo
            var LectorDelArchivo = new StreamReader(Ruta);
            var linea = LectorDelArchivo.ReadLine();//string para almacenar la linea actual
            var indicaciones = "";//almacenaremos las letras
            //mientras no sea el final del archivo
            while (linea !=null)
            {//para cada letra dentro de la linea
                
                indicaciones += linea;

                //leemos la siguiente
                linea =LectorDelArchivo.ReadLine();
            }
            var borrar = "";
            var iteracion = 0;
            Matriz = new string[15, 15];

            for (int y = 0; y < 15; y++)
            {
                for (int x = 0; x < 15; x++)
                {
                    if (Convert.ToString(indicaciones[iteracion]) == "B")
                    {
                        if (Encontrado==false)
                        {

                        PosX = x;PosY = y;
                        Encontrado = true;
                        }
                        else
                        {

                            //dos cajas duplicadas
                        }
                    }
                    Matriz[y, x] += Convert.ToString(indicaciones[iteracion]);
                    borrar += $"[{y.ToString().PadLeft(2,'0')},{x.ToString().PadLeft(2, '0')}]|";
                    iteracion++;

                }
            }
            AsignarImagenes();
            
            LectorDelArchivo.Close();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {//aqui leemos las teclas presionadas


            //Los timers son para que se ejecute una accion por cada cierta cantidad de tiempo, que se puede modificar en el timer.interval = cantidaddetics
            if (SimulacionIniciada==true)
            {//por daca instruccion dada, le summaremos una

                Instrucciones++;
                labelInstrucciones.Text = Instrucciones.ToString();
                if (e.KeyData == Keys.W)
            {//mover arriba
                Timer_ir_Arriba.Enabled=false;
                Timer_ir_Abajo.Enabled=false;
                Timer_ir_Izquierda.Enabled=false;
                Timer_ir_Derecha.Enabled=false;
                Timer_ir_Arriba.Enabled=true;
            }
            else if (e.KeyData == Keys.S)
            {//abajo


                Timer_ir_Arriba.Enabled = false;
                Timer_ir_Abajo.Enabled = false;
                Timer_ir_Izquierda.Enabled = false;
                Timer_ir_Derecha.Enabled = false;
                Timer_ir_Abajo.Enabled=true;
                //Matriz[PosY, PosX] = "F";
                //PosY++;
                //Matriz[PosY, PosX] = "B";
            }
            else if (e.KeyData == Keys.A)
            {//izquierda

                Timer_ir_Arriba.Enabled = false;
                Timer_ir_Abajo.Enabled = false;
                Timer_ir_Izquierda.Enabled = false;
                Timer_ir_Derecha.Enabled = false;
                Timer_ir_Izquierda.Enabled=true;
                //Matriz[PosY, PosX] = "F";
                //PosX--;
                //Matriz[PosY, PosX] = "B";
            }
            else if (e.KeyData == Keys.D)
            {//deracha

                Timer_ir_Arriba.Enabled = false;
                Timer_ir_Abajo.Enabled = false;
                Timer_ir_Izquierda.Enabled = false;
                Timer_ir_Derecha.Enabled = false;
                Timer_ir_Derecha.Enabled=true;
                //Matriz[PosY, PosX] = "F";
                //PosX++;
                //Matriz[PosY, PosX] = "B";

            }
            AsignarImagenes();
            }

        }
        private void label1_Click(object sender, EventArgs e)
        {
        }
        private void Nombre_Click(object sender, EventArgs e)
        {
        }private void textBoxApellido_TextChanged(object sender, EventArgs e)
        {
        }
        private void label2_Click(object sender, EventArgs e)
        {
        } private void label3_Click(object sender, EventArgs e)
        {
        }
        private void label4_Click(object sender, EventArgs e)
        { 
        } 
        private void label5_Click(object sender, EventArgs e)
        {
        }private void labelPuntos_Click(object sender, EventArgs e)
        {
        }private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void textBoxNombre_TextChanged(object sender, EventArgs e)
        {
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //automatico arriba
            if (PosY-1 >= 0)
            { // si no se sale del rango de la matriz

                if (Matriz[PosY-1, PosX]=="C")
                {// si toca un asteroide
                    Timer_ir_Arriba.Enabled = false;
                    Timer_ir_Arriba.Stop();
                }
                else if (Matriz[PosY - 1, PosX] == "D")
                {//si llega a la tierra
                    label9.Visible = true;
                    label8.Visible = true;
                    Timer_ir_Arriba.Enabled = false;
                    Timer_ir_Arriba.Stop(); button2.Enabled = true;

                }
                else
                {//avanza
                    if (Matriz[PosY - 1, PosX] == "E")
                    {// si toca diamante
                    Puntos = Puntos + 50;
                    labelPuntos.Text = Puntos.ToString();

                    }

                    Matriz[PosY, PosX] = "F";
                    PosY--; Pasos++;
                    Matriz[PosY, PosX] = "B";

                }
                LabelPasos.Text = Pasos.ToString();
                    AsignarImagenes();
            }
            else
            {
                Timer_ir_Arriba.Enabled = false;
                Timer_ir_Arriba.Stop(); Matriz = new string[15, 15];
                PosX = 0;
                PosY = 0;
                Encontrado = false;
                Timer_ir_Arriba.Enabled = false;
                Timer_ir_Abajo.Enabled = false;
                Timer_ir_Izquierda.Enabled = false;
                Timer_ir_Derecha.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = true;
                SimulacionIniciada = false; 
                HOUSTON_TENEMOS_UN_PROBLEMA.Visible = true;
                MisionFallida.Visible = true;
                //button4.Enabled = true;

            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //automatico abajo
            if (PosY + 1 <= 14)
            { // si no se sale del rango de la matriz

                if (Matriz[PosY + 1, PosX] == "C")
                {// si toca un asteroide
                    Timer_ir_Abajo.Enabled = false;
                    Timer_ir_Abajo.Stop();
                }
                else if (Matriz[PosY + 1, PosX] == "D")
                {//si llega a la tierra
                    label9.Visible = true;
                    label8.Visible = true;
                    Timer_ir_Abajo.Enabled = false;
                    Timer_ir_Abajo.Stop(); button2.Enabled = true;

                }
                else
                {//avanza

                    if (Matriz[PosY + 1, PosX]== "E")
                    {// si toca diamante
                        Puntos = Puntos + 50;
                        labelPuntos.Text = Puntos.ToString();

                    }
                    Matriz[PosY, PosX] = "F";
                    PosY++; Pasos++;
                    Matriz[PosY, PosX] = "B";

                }
                AsignarImagenes(); LabelPasos.Text = Pasos.ToString();

            }
            else
            {
                Timer_ir_Abajo.Enabled = false;
                Timer_ir_Abajo.Stop(); Matriz = new string[15, 15];
                PosX = 0;
                PosY = 0;
                Encontrado = false;
                Timer_ir_Arriba.Enabled = false;
                Timer_ir_Abajo.Enabled = false;
                Timer_ir_Izquierda.Enabled = false;
                Timer_ir_Derecha.Enabled = false;
                SimulacionIniciada = false;
                button1.Enabled = false;
                button2.Enabled = true; HOUSTON_TENEMOS_UN_PROBLEMA.Visible = true;
                MisionFallida.Visible = true; //button4.Enabled = true;

            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            //automatico izquierda

            if (PosX - 1 >= 0)
            { // si no se sale del rango de la matriz

                if (Matriz[PosY, PosX-1] == "C")
                {// si toca un asteroide
                    Timer_ir_Izquierda.Enabled = false;
                    Timer_ir_Izquierda.Stop();
                }
                else if (Matriz[PosY , PosX-1] == "D")
                {//si llega a la tierra
                    label9.Visible = true;
                    label8.Visible = true;
                    Timer_ir_Izquierda.Enabled = false;
                    Timer_ir_Izquierda.Stop(); button2.Enabled = true;

                }
                else
                {//avanza
                    if (Matriz[PosY, PosX-1]== "E")
                    {// si toca diamante
                        Puntos = Puntos + 50;
                        labelPuntos.Text = Puntos.ToString();

                    }
                    Matriz[PosY, PosX] = "F";
                    PosX--; Pasos++;
                    Matriz[PosY, PosX] = "B";

                }
                AsignarImagenes(); LabelPasos.Text = Pasos.ToString();

            }
            else
            {
                Timer_ir_Izquierda.Enabled = false;
                Timer_ir_Izquierda.Stop(); Matriz = new string[15, 15];
                PosX = 0;
                PosY = 0;
                Encontrado = false;
                Timer_ir_Arriba.Enabled = false;
                Timer_ir_Abajo.Enabled = false; SimulacionIniciada = false;

                Timer_ir_Izquierda.Enabled = false;
                Timer_ir_Derecha.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = true; HOUSTON_TENEMOS_UN_PROBLEMA.Visible = true;
                MisionFallida.Visible = true; //button4.Enabled = true;

            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            //automatico dERECHA

            if (PosX +1 <= 14)
            { // si no se sale del rango de la matriz

                if (Matriz[PosY, PosX + 1] == "C")
                {// si toca un asteroide
                    Timer_ir_Derecha.Enabled = false;
                    Timer_ir_Derecha.Stop();
                }
                else if (Matriz[PosY, PosX + 1] == "D")
                {//si llega a la tierra
                    label9.Visible = true;
                    label8.Visible = true;
                    Timer_ir_Derecha.Enabled = false;
                    Timer_ir_Derecha.Stop();
                    button2.Enabled = true;

                }
                else
                {//avanza
                    if (Matriz[PosY, PosX + 1]== "E")
                    {// si toca diamante
                        Puntos = Puntos + 50;
                        labelPuntos.Text = Puntos.ToString();

                    }
                    Matriz[PosY, PosX] = "F";
                    PosX++; Pasos++;
                    Matriz[PosY, PosX] = "B";

                }
                AsignarImagenes(); LabelPasos.Text = Pasos.ToString();

            }
            else
            {
                Timer_ir_Derecha.Enabled = false;
                Timer_ir_Derecha.Stop();

                Matriz = new string[15, 15];
                PosX = 0;
                PosY = 0; SimulacionIniciada = false;

                Encontrado = false;
                Timer_ir_Arriba.Enabled = false;
                Timer_ir_Abajo.Enabled = false;
                Timer_ir_Izquierda.Enabled = false;
                Timer_ir_Derecha.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = true; HOUSTON_TENEMOS_UN_PROBLEMA.Visible = true;
                MisionFallida.Visible = true;
                //button4.Enabled = true;

            }
        }

        void AsignarImagenes()
        {
            if (Matriz[0, 0] == "A")
            {
                
                pictureBox1.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[0, 1] == "A")
            {
                pictureBox2.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[0, 2] == "A")
            {
                pictureBox3.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[0, 3] == "A")
            {
                pictureBox4.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[0, 4] == "A")
            {
                pictureBox5.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[0, 5] == "A")
            {
                pictureBox6.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[0, 6] == "A")
            {
                pictureBox7.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[0, 7] == "A")
            {
                pictureBox8.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[0, 8] == "A")
            {
                pictureBox9.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[0, 9] == "A")
            {
                pictureBox10.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[0, 10] == "A")
            {
                pictureBox11.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[0, 11] == "A")
            {
                pictureBox12.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[0, 12] == "A")
            {
                pictureBox13.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[0, 13] == "A")
            {
                pictureBox14.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[0, 14] == "A")
            {
                pictureBox15.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[1, 0] == "A")
            {
                pictureBox16.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[1, 1] == "A")
            {
                pictureBox17.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[1, 2] == "A")
            {
                pictureBox18.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[1, 3] == "A")
            {
                pictureBox19.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[1, 4] == "A")
            {
                pictureBox20.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[1, 5] == "A")
            {
                pictureBox21.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[1, 6] == "A")
            {
                pictureBox22.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[1, 7] == "A")
            {
                pictureBox23.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[1, 8] == "A")
            {
                pictureBox24.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[1, 9] == "A")
            {
                pictureBox25.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[1, 10] == "A")
            {
                pictureBox26.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[1, 11] == "A")
            {
                pictureBox27.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[1, 12] == "A")
            {
                pictureBox28.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[1, 13] == "A")
            {
                pictureBox29.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[1, 14] == "A")
            {
                pictureBox30.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[2, 0] == "A")
            {
                pictureBox31.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[2, 1] == "A")
            {
                pictureBox32.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[2, 2] == "A")
            {
                pictureBox33.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[2, 3] == "A")
            {
                pictureBox34.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[2, 4] == "A")
            {
                pictureBox35.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[2, 5] == "A")
            {
                pictureBox36.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[2, 6] == "A")
            {
                pictureBox37.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[2, 7] == "A")
            {
                pictureBox38.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[2, 8] == "A")
            {
                pictureBox39.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[2, 9] == "A")
            {
                pictureBox40.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[2, 10] == "A")
            {
                pictureBox41.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[2, 11] == "A")
            {
                pictureBox42.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[2, 12] == "A")
            {
                pictureBox43.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[2, 13] == "A")
            {
                pictureBox44.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[2, 14] == "A")
            {
                pictureBox45.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[3, 0] == "A")
            {
                pictureBox46.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[3, 1] == "A")
            {
                pictureBox47.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[3, 2] == "A")
            {
                pictureBox48.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[3, 3] == "A")
            {
                pictureBox49.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[3, 4] == "A")
            {
                pictureBox50.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[3, 5] == "A")
            {
                pictureBox51.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[3, 6] == "A")
            {
                pictureBox52.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[3, 7] == "A")
            {
                pictureBox53.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[3, 8] == "A")
            {
                pictureBox54.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[3, 9] == "A")
            {
                pictureBox55.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[3, 10] == "A")
            {
                pictureBox56.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[3, 11] == "A")
            {
                pictureBox57.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[3, 12] == "A")
            {
                pictureBox58.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[3, 13] == "A")
            {
                pictureBox59.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[3, 14] == "A")
            {
                pictureBox60.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[4, 0] == "A")
            {
                pictureBox61.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[4, 1] == "A")
            {
                pictureBox62.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[4, 2] == "A")
            {
                pictureBox63.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[4, 3] == "A")
            {
                pictureBox64.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[4, 4] == "A")
            {
                pictureBox65.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[4, 5] == "A")
            {
                pictureBox66.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[4, 6] == "A")
            {
                pictureBox67.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[4, 7] == "A")
            {
                pictureBox68.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[4, 8] == "A")
            {
                pictureBox69.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[4, 9] == "A")
            {
                pictureBox70.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[4, 10] == "A")
            {
                pictureBox71.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[4, 11] == "A")
            {
                pictureBox72.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[4, 12] == "A")
            {
                pictureBox73.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[4, 13] == "A")
            {
                pictureBox74.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[4, 14] == "A")
            {
                pictureBox75.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[5, 0] == "A")
            {
                pictureBox76.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[5, 1] == "A")
            {
                pictureBox77.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[5, 2] == "A")
            {
                pictureBox78.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[5, 3] == "A")
            {
                pictureBox79.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[5, 4] == "A")
            {
                pictureBox80.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[5, 5] == "A")
            {
                pictureBox81.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[5, 6] == "A")
            {
                pictureBox82.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[5, 7] == "A")
            {
                pictureBox83.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[5, 8] == "A")
            {
                pictureBox84.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[5, 9] == "A")
            {
                pictureBox85.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[5, 10] == "A")
            {
                pictureBox86.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[5, 11] == "A")
            {
                pictureBox87.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[5, 12] == "A")
            {
                pictureBox88.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[5, 13] == "A")
            {
                pictureBox89.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[5, 14] == "A")
            {
                pictureBox90.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[6, 0] == "A")
            {
                pictureBox91.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[6, 1] == "A")
            {
                pictureBox92.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[6, 2] == "A")
            {
                pictureBox93.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[6, 3] == "A")
            {
                pictureBox94.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[6, 4] == "A")
            {
                pictureBox95.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[6, 5] == "A")
            {
                pictureBox96.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[6, 6] == "A")
            {
                pictureBox97.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[6, 7] == "A")
            {
                pictureBox98.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[6, 8] == "A")
            {
                pictureBox99.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[6, 9] == "A")
            {
                pictureBox100.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[6, 10] == "A")
            {
                pictureBox101.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[6, 11] == "A")
            {
                pictureBox102.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[6, 12] == "A")
            {
                pictureBox103.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[6, 13] == "A")
            {
                pictureBox104.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[6, 14] == "A")
            {
                pictureBox105.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[7, 0] == "A")
            {
                pictureBox106.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[7, 1] == "A")
            {
                pictureBox107.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[7, 2] == "A")
            {
                pictureBox108.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[7, 3] == "A")
            {
                pictureBox109.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[7, 4] == "A")
            {
                pictureBox110.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[7, 5] == "A")
            {
                pictureBox111.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[7, 6] == "A")
            {
                pictureBox112.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[7, 7] == "A")
            {
                pictureBox113.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[7, 8] == "A")
            {
                pictureBox114.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[7, 9] == "A")
            {
                pictureBox115.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[7, 10] == "A")
            {
                pictureBox116.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[7, 11] == "A")
            {
                pictureBox117.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[7, 12] == "A")
            {
                pictureBox118.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[7, 13] == "A")
            {
                pictureBox119.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[7, 14] == "A")
            {
                pictureBox120.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[8, 0] == "A")
            {
                pictureBox121.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[8, 1] == "A")
            {
                pictureBox122.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[8, 2] == "A")
            {
                pictureBox123.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[8, 3] == "A")
            {
                pictureBox124.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[8, 4] == "A")
            {
                pictureBox125.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[8, 5] == "A")
            {
                pictureBox126.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[8, 6] == "A")
            {
                pictureBox127.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[8, 7] == "A")
            {
                pictureBox128.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[8, 8] == "A")
            {
                pictureBox129.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[8, 9] == "A")
            {
                pictureBox130.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[8, 10] == "A")
            {
                pictureBox131.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[8, 11] == "A")
            {
                pictureBox132.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[8, 12] == "A")
            {
                pictureBox133.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[8, 13] == "A")
            {
                pictureBox134.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[8, 14] == "A")
            {
                pictureBox135.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[9, 0] == "A")
            {
                pictureBox136.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[9, 1] == "A")
            {
                pictureBox137.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[9, 2] == "A")
            {
                pictureBox138.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[9, 3] == "A")
            {
                pictureBox139.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[9, 4] == "A")
            {
                pictureBox140.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[9, 5] == "A")
            {
                pictureBox141.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[9, 6] == "A")
            {
                pictureBox142.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[9, 7] == "A")
            {
                pictureBox143.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[9, 8] == "A")
            {
                pictureBox144.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[9, 9] == "A")
            {
                pictureBox145.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[9, 10] == "A")
            {
                pictureBox146.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[9, 11] == "A")
            {
                pictureBox147.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[9, 12] == "A")
            {
                pictureBox148.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[9, 13] == "A")
            {
                pictureBox149.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[9, 14] == "A")
            {
                pictureBox150.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[10, 0] == "A")
            {
                pictureBox151.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[10, 1] == "A")
            {
                pictureBox152.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[10, 2] == "A")
            {
                pictureBox153.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[10, 3] == "A")
            {
                pictureBox154.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[10, 4] == "A")
            {
                pictureBox155.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[10, 5] == "A")
            {
                pictureBox156.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[10, 6] == "A")
            {
                pictureBox157.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[10, 7] == "A")
            {
                pictureBox158.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[10, 8] == "A")
            {
                pictureBox159.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[10, 9] == "A")
            {
                pictureBox160.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[10, 10] == "A")
            {
                pictureBox161.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[10, 11] == "A")
            {
                pictureBox162.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[10, 12] == "A")
            {
                pictureBox163.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[10, 13] == "A")
            {
                pictureBox164.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[10, 14] == "A")
            {
                pictureBox165.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[11, 0] == "A")
            {
                pictureBox166.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[11, 1] == "A")
            {
                pictureBox167.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[11, 2] == "A")
            {
                pictureBox168.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[11, 3] == "A")
            {
                pictureBox169.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[11, 4] == "A")
            {
                pictureBox170.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[11, 5] == "A")
            {
                pictureBox171.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[11, 6] == "A")
            {
                pictureBox172.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[11, 7] == "A")
            {
                pictureBox173.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[11, 8] == "A")
            {
                pictureBox174.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[11, 9] == "A")
            {
                pictureBox175.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[11, 10] == "A")
            {
                pictureBox176.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[11, 11] == "A")
            {
                pictureBox177.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[11, 12] == "A")
            {
                pictureBox178.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[11, 13] == "A")
            {
                pictureBox179.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[11, 14] == "A")
            {
                pictureBox180.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[12, 0] == "A")
            {
                pictureBox181.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[12, 1] == "A")
            {
                pictureBox182.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[12, 2] == "A")
            {
                pictureBox183.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[12, 3] == "A")
            {
                pictureBox184.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[12, 4] == "A")
            {
                pictureBox185.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[12, 5] == "A")
            {
                pictureBox186.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[12, 6] == "A")
            {
                pictureBox187.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[12, 7] == "A")
            {
                pictureBox188.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[12, 8] == "A")
            {
                pictureBox189.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[12, 9] == "A")
            {
                pictureBox190.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[12, 10] == "A")
            {
                pictureBox191.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[12, 11] == "A")
            {
                pictureBox192.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[12, 12] == "A")
            {
                pictureBox193.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[12, 13] == "A")
            {
                pictureBox194.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[12, 14] == "A")
            {
                pictureBox195.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[13, 0] == "A")
            {
                pictureBox196.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[13, 1] == "A")
            {
                pictureBox197.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[13, 2] == "A")
            {
                pictureBox198.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[13, 3] == "A")
            {
                pictureBox199.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[13, 4] == "A")
            {
                pictureBox200.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[13, 5] == "A")
            {
                pictureBox201.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[13, 6] == "A")
            {
                pictureBox202.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[13, 7] == "A")
            {
                pictureBox203.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[13, 8] == "A")
            {
                pictureBox204.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[13, 9] == "A")
            {
                pictureBox205.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[13, 10] == "A")
            {
                pictureBox206.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[13, 11] == "A")
            {
                pictureBox207.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[13, 12] == "A")
            {
                pictureBox208.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[13, 13] == "A")
            {
                pictureBox209.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[13, 14] == "A")
            {
                pictureBox210.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[14, 0] == "A")
            {
                pictureBox211.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[14, 1] == "A")
            {
                pictureBox212.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[14, 2] == "A")
            {
                pictureBox213.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[14, 3] == "A")
            {
                pictureBox214.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[14, 4] == "A")
            {
                pictureBox215.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[14, 5] == "A")
            {
                pictureBox216.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[14, 6] == "A")
            {
                pictureBox217.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[14, 7] == "A")
            {
                pictureBox218.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[14, 8] == "A")
            {
                pictureBox219.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[14, 9] == "A")
            {
                pictureBox220.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[14, 10] == "A")
            {
                pictureBox221.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[14, 11] == "A")
            {
                pictureBox222.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[14, 12] == "A")
            {
                pictureBox223.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[14, 13] == "A")
            {
                pictureBox224.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[14, 14] == "A")
            {
                pictureBox225.Image = Image.FromFile("Vacio.png");
            }
            if (Matriz[0, 0] == "B")
            {
                pictureBox1.Image = Image.FromFile("Box.png");
            }
            if (Matriz[0, 1] == "B")
            {
                pictureBox2.Image = Image.FromFile("Box.png");
            }
            if (Matriz[0, 2] == "B")
            {
                pictureBox3.Image = Image.FromFile("Box.png");
            }
            if (Matriz[0, 3] == "B")
            {
                pictureBox4.Image = Image.FromFile("Box.png");
            }
            if (Matriz[0, 4] == "B")
            {
                pictureBox5.Image = Image.FromFile("Box.png");
            }
            if (Matriz[0, 5] == "B")
            {
                pictureBox6.Image = Image.FromFile("Box.png");
            }
            if (Matriz[0, 6] == "B")
            {
                pictureBox7.Image = Image.FromFile("Box.png");
            }
            if (Matriz[0, 7] == "B")
            {
                pictureBox8.Image = Image.FromFile("Box.png");
            }
            if (Matriz[0, 8] == "B")
            {
                pictureBox9.Image = Image.FromFile("Box.png");
            }
            if (Matriz[0, 9] == "B")
            {
                pictureBox10.Image = Image.FromFile("Box.png");
            }
            if (Matriz[0, 10] == "B")
            {
                pictureBox11.Image = Image.FromFile("Box.png");
            }
            if (Matriz[0, 11] == "B")
            {
                pictureBox12.Image = Image.FromFile("Box.png");
            }
            if (Matriz[0, 12] == "B")
            {
                pictureBox13.Image = Image.FromFile("Box.png");
            }
            if (Matriz[0, 13] == "B")
            {
                pictureBox14.Image = Image.FromFile("Box.png");
            }
            if (Matriz[0, 14] == "B")
            {
                pictureBox15.Image = Image.FromFile("Box.png");
            }
            if (Matriz[1, 0] == "B")
            {
                pictureBox16.Image = Image.FromFile("Box.png");
            }
            if (Matriz[1, 1] == "B")
            {
                pictureBox17.Image = Image.FromFile("Box.png");
            }
            if (Matriz[1, 2] == "B")
            {
                pictureBox18.Image = Image.FromFile("Box.png");
            }
            if (Matriz[1, 3] == "B")
            {
                pictureBox19.Image = Image.FromFile("Box.png");
            }
            if (Matriz[1, 4] == "B")
            {
                pictureBox20.Image = Image.FromFile("Box.png");
            }
            if (Matriz[1, 5] == "B")
            {
                pictureBox21.Image = Image.FromFile("Box.png");
            }
            if (Matriz[1, 6] == "B")
            {
                pictureBox22.Image = Image.FromFile("Box.png");
            }
            if (Matriz[1, 7] == "B")
            {
                pictureBox23.Image = Image.FromFile("Box.png");
            }
            if (Matriz[1, 8] == "B")
            {
                pictureBox24.Image = Image.FromFile("Box.png");
            }
            if (Matriz[1, 9] == "B")
            {
                pictureBox25.Image = Image.FromFile("Box.png");
            }
            if (Matriz[1, 10] == "B")
            {
                pictureBox26.Image = Image.FromFile("Box.png");
            }
            if (Matriz[1, 11] == "B")
            {
                pictureBox27.Image = Image.FromFile("Box.png");
            }
            if (Matriz[1, 12] == "B")
            {
                pictureBox28.Image = Image.FromFile("Box.png");
            }
            if (Matriz[1, 13] == "B")
            {
                pictureBox29.Image = Image.FromFile("Box.png");
            }
            if (Matriz[1, 14] == "B")
            {
                pictureBox30.Image = Image.FromFile("Box.png");
            }
            if (Matriz[2, 0] == "B")
            {
                pictureBox31.Image = Image.FromFile("Box.png");
            }
            if (Matriz[2, 1] == "B")
            {
                pictureBox32.Image = Image.FromFile("Box.png");
            }
            if (Matriz[2, 2] == "B")
            {
                pictureBox33.Image = Image.FromFile("Box.png");
            }
            if (Matriz[2, 3] == "B")
            {
                pictureBox34.Image = Image.FromFile("Box.png");
            }
            if (Matriz[2, 4] == "B")
            {
                pictureBox35.Image = Image.FromFile("Box.png");
            }
            if (Matriz[2, 5] == "B")
            {
                pictureBox36.Image = Image.FromFile("Box.png");
            }
            if (Matriz[2, 6] == "B")
            {
                pictureBox37.Image = Image.FromFile("Box.png");
            }
            if (Matriz[2, 7] == "B")
            {
                pictureBox38.Image = Image.FromFile("Box.png");
            }
            if (Matriz[2, 8] == "B")
            {
                pictureBox39.Image = Image.FromFile("Box.png");
            }
            if (Matriz[2, 9] == "B")
            {
                pictureBox40.Image = Image.FromFile("Box.png");
            }
            if (Matriz[2, 10] == "B")
            {
                pictureBox41.Image = Image.FromFile("Box.png");
            }
            if (Matriz[2, 11] == "B")
            {
                pictureBox42.Image = Image.FromFile("Box.png");
            }
            if (Matriz[2, 12] == "B")
            {
                pictureBox43.Image = Image.FromFile("Box.png");
            }
            if (Matriz[2, 13] == "B")
            {
                pictureBox44.Image = Image.FromFile("Box.png");
            }
            if (Matriz[2, 14] == "B")
            {
                pictureBox45.Image = Image.FromFile("Box.png");
            }
            if (Matriz[3, 0] == "B")
            {
                pictureBox46.Image = Image.FromFile("Box.png");
            }
            if (Matriz[3, 1] == "B")
            {
                pictureBox47.Image = Image.FromFile("Box.png");
            }
            if (Matriz[3, 2] == "B")
            {
                pictureBox48.Image = Image.FromFile("Box.png");
            }
            if (Matriz[3, 3] == "B")
            {
                pictureBox49.Image = Image.FromFile("Box.png");
            }
            if (Matriz[3, 4] == "B")
            {
                pictureBox50.Image = Image.FromFile("Box.png");
            }
            if (Matriz[3, 5] == "B")
            {
                pictureBox51.Image = Image.FromFile("Box.png");
            }
            if (Matriz[3, 6] == "B")
            {
                pictureBox52.Image = Image.FromFile("Box.png");
            }
            if (Matriz[3, 7] == "B")
            {
                pictureBox53.Image = Image.FromFile("Box.png");
            }
            if (Matriz[3, 8] == "B")
            {
                pictureBox54.Image = Image.FromFile("Box.png");
            }
            if (Matriz[3, 9] == "B")
            {
                pictureBox55.Image = Image.FromFile("Box.png");
            }
            if (Matriz[3, 10] == "B")
            {
                pictureBox56.Image = Image.FromFile("Box.png");
            }
            if (Matriz[3, 11] == "B")
            {
                pictureBox57.Image = Image.FromFile("Box.png");
            }
            if (Matriz[3, 12] == "B")
            {
                pictureBox58.Image = Image.FromFile("Box.png");
            }
            if (Matriz[3, 13] == "B")
            {
                pictureBox59.Image = Image.FromFile("Box.png");
            }
            if (Matriz[3, 14] == "B")
            {
                pictureBox60.Image = Image.FromFile("Box.png");
            }
            if (Matriz[4, 0] == "B")
            {
                pictureBox61.Image = Image.FromFile("Box.png");
            }
            if (Matriz[4, 1] == "B")
            {
                pictureBox62.Image = Image.FromFile("Box.png");
            }
            if (Matriz[4, 2] == "B")
            {
                pictureBox63.Image = Image.FromFile("Box.png");
            }
            if (Matriz[4, 3] == "B")
            {
                pictureBox64.Image = Image.FromFile("Box.png");
            }
            if (Matriz[4, 4] == "B")
            {
                pictureBox65.Image = Image.FromFile("Box.png");
            }
            if (Matriz[4, 5] == "B")
            {
                pictureBox66.Image = Image.FromFile("Box.png");
            }
            if (Matriz[4, 6] == "B")
            {
                pictureBox67.Image = Image.FromFile("Box.png");
            }
            if (Matriz[4, 7] == "B")
            {
                pictureBox68.Image = Image.FromFile("Box.png");
            }
            if (Matriz[4, 8] == "B")
            {
                pictureBox69.Image = Image.FromFile("Box.png");
            }
            if (Matriz[4, 9] == "B")
            {
                pictureBox70.Image = Image.FromFile("Box.png");
            }
            if (Matriz[4, 10] == "B")
            {
                pictureBox71.Image = Image.FromFile("Box.png");
            }
            if (Matriz[4, 11] == "B")
            {
                pictureBox72.Image = Image.FromFile("Box.png");
            }
            if (Matriz[4, 12] == "B")
            {
                pictureBox73.Image = Image.FromFile("Box.png");
            }
            if (Matriz[4, 13] == "B")
            {
                pictureBox74.Image = Image.FromFile("Box.png");
            }
            if (Matriz[4, 14] == "B")
            {
                pictureBox75.Image = Image.FromFile("Box.png");
            }
            if (Matriz[5, 0] == "B")
            {
                pictureBox76.Image = Image.FromFile("Box.png");
            }
            if (Matriz[5, 1] == "B")
            {
                pictureBox77.Image = Image.FromFile("Box.png");
            }
            if (Matriz[5, 2] == "B")
            {
                pictureBox78.Image = Image.FromFile("Box.png");
            }
            if (Matriz[5, 3] == "B")
            {
                pictureBox79.Image = Image.FromFile("Box.png");
            }
            if (Matriz[5, 4] == "B")
            {
                pictureBox80.Image = Image.FromFile("Box.png");
            }
            if (Matriz[5, 5] == "B")
            {
                pictureBox81.Image = Image.FromFile("Box.png");
            }
            if (Matriz[5, 6] == "B")
            {
                pictureBox82.Image = Image.FromFile("Box.png");
            }
            if (Matriz[5, 7] == "B")
            {
                pictureBox83.Image = Image.FromFile("Box.png");
            }
            if (Matriz[5, 8] == "B")
            {
                pictureBox84.Image = Image.FromFile("Box.png");
            }
            if (Matriz[5, 9] == "B")
            {
                pictureBox85.Image = Image.FromFile("Box.png");
            }
            if (Matriz[5, 10] == "B")
            {
                pictureBox86.Image = Image.FromFile("Box.png");
            }
            if (Matriz[5, 11] == "B")
            {
                pictureBox87.Image = Image.FromFile("Box.png");
            }
            if (Matriz[5, 12] == "B")
            {
                pictureBox88.Image = Image.FromFile("Box.png");
            }
            if (Matriz[5, 13] == "B")
            {
                pictureBox89.Image = Image.FromFile("Box.png");
            }
            if (Matriz[5, 14] == "B")
            {
                pictureBox90.Image = Image.FromFile("Box.png");
            }
            if (Matriz[6, 0] == "B")
            {
                pictureBox91.Image = Image.FromFile("Box.png");
            }
            if (Matriz[6, 1] == "B")
            {
                pictureBox92.Image = Image.FromFile("Box.png");
            }
            if (Matriz[6, 2] == "B")
            {
                pictureBox93.Image = Image.FromFile("Box.png");
            }
            if (Matriz[6, 3] == "B")
            {
                pictureBox94.Image = Image.FromFile("Box.png");
            }
            if (Matriz[6, 4] == "B")
            {
                pictureBox95.Image = Image.FromFile("Box.png");
            }
            if (Matriz[6, 5] == "B")
            {
                pictureBox96.Image = Image.FromFile("Box.png");
            }
            if (Matriz[6, 6] == "B")
            {
                pictureBox97.Image = Image.FromFile("Box.png");
            }
            if (Matriz[6, 7] == "B")
            {
                pictureBox98.Image = Image.FromFile("Box.png");
            }
            if (Matriz[6, 8] == "B")
            {
                pictureBox99.Image = Image.FromFile("Box.png");
            }
            if (Matriz[6, 9] == "B")
            {
                pictureBox100.Image = Image.FromFile("Box.png");
            }
            if (Matriz[6, 10] == "B")
            {
                pictureBox101.Image = Image.FromFile("Box.png");
            }
            if (Matriz[6, 11] == "B")
            {
                pictureBox102.Image = Image.FromFile("Box.png");
            }
            if (Matriz[6, 12] == "B")
            {
                pictureBox103.Image = Image.FromFile("Box.png");
            }
            if (Matriz[6, 13] == "B")
            {
                pictureBox104.Image = Image.FromFile("Box.png");
            }
            if (Matriz[6, 14] == "B")
            {
                pictureBox105.Image = Image.FromFile("Box.png");
            }
            if (Matriz[7, 0] == "B")
            {
                pictureBox106.Image = Image.FromFile("Box.png");
            }
            if (Matriz[7, 1] == "B")
            {
                pictureBox107.Image = Image.FromFile("Box.png");
            }
            if (Matriz[7, 2] == "B")
            {
                pictureBox108.Image = Image.FromFile("Box.png");
            }
            if (Matriz[7, 3] == "B")
            {
                pictureBox109.Image = Image.FromFile("Box.png");
            }
            if (Matriz[7, 4] == "B")
            {
                pictureBox110.Image = Image.FromFile("Box.png");
            }
            if (Matriz[7, 5] == "B")
            {
                pictureBox111.Image = Image.FromFile("Box.png");
            }
            if (Matriz[7, 6] == "B")
            {
                pictureBox112.Image = Image.FromFile("Box.png");
            }
            if (Matriz[7, 7] == "B")
            {
                pictureBox113.Image = Image.FromFile("Box.png");
            }
            if (Matriz[7, 8] == "B")
            {
                pictureBox114.Image = Image.FromFile("Box.png");
            }
            if (Matriz[7, 9] == "B")
            {
                pictureBox115.Image = Image.FromFile("Box.png");
            }
            if (Matriz[7, 10] == "B")
            {
                pictureBox116.Image = Image.FromFile("Box.png");
            }
            if (Matriz[7, 11] == "B")
            {
                pictureBox117.Image = Image.FromFile("Box.png");
            }
            if (Matriz[7, 12] == "B")
            {
                pictureBox118.Image = Image.FromFile("Box.png");
            }
            if (Matriz[7, 13] == "B")
            {
                pictureBox119.Image = Image.FromFile("Box.png");
            }
            if (Matriz[7, 14] == "B")
            {
                pictureBox120.Image = Image.FromFile("Box.png");
            }
            if (Matriz[8, 0] == "B")
            {
                pictureBox121.Image = Image.FromFile("Box.png");
            }
            if (Matriz[8, 1] == "B")
            {
                pictureBox122.Image = Image.FromFile("Box.png");
            }
            if (Matriz[8, 2] == "B")
            {
                pictureBox123.Image = Image.FromFile("Box.png");
            }
            if (Matriz[8, 3] == "B")
            {
                pictureBox124.Image = Image.FromFile("Box.png");
            }
            if (Matriz[8, 4] == "B")
            {
                pictureBox125.Image = Image.FromFile("Box.png");
            }
            if (Matriz[8, 5] == "B")
            {
                pictureBox126.Image = Image.FromFile("Box.png");
            }
            if (Matriz[8, 6] == "B")
            {
                pictureBox127.Image = Image.FromFile("Box.png");
            }
            if (Matriz[8, 7] == "B")
            {
                pictureBox128.Image = Image.FromFile("Box.png");
            }
            if (Matriz[8, 8] == "B")
            {
                pictureBox129.Image = Image.FromFile("Box.png");
            }
            if (Matriz[8, 9] == "B")
            {
                pictureBox130.Image = Image.FromFile("Box.png");
            }
            if (Matriz[8, 10] == "B")
            {
                pictureBox131.Image = Image.FromFile("Box.png");
            }
            if (Matriz[8, 11] == "B")
            {
                pictureBox132.Image = Image.FromFile("Box.png");
            }
            if (Matriz[8, 12] == "B")
            {
                pictureBox133.Image = Image.FromFile("Box.png");
            }
            if (Matriz[8, 13] == "B")
            {
                pictureBox134.Image = Image.FromFile("Box.png");
            }
            if (Matriz[8, 14] == "B")
            {
                pictureBox135.Image = Image.FromFile("Box.png");
            }
            if (Matriz[9, 0] == "B")
            {
                pictureBox136.Image = Image.FromFile("Box.png");
            }
            if (Matriz[9, 1] == "B")
            {
                pictureBox137.Image = Image.FromFile("Box.png");
            }
            if (Matriz[9, 2] == "B")
            {
                pictureBox138.Image = Image.FromFile("Box.png");
            }
            if (Matriz[9, 3] == "B")
            {
                pictureBox139.Image = Image.FromFile("Box.png");
            }
            if (Matriz[9, 4] == "B")
            {
                pictureBox140.Image = Image.FromFile("Box.png");
            }
            if (Matriz[9, 5] == "B")
            {
                pictureBox141.Image = Image.FromFile("Box.png");
            }
            if (Matriz[9, 6] == "B")
            {
                pictureBox142.Image = Image.FromFile("Box.png");
            }
            if (Matriz[9, 7] == "B")
            {
                pictureBox143.Image = Image.FromFile("Box.png");
            }
            if (Matriz[9, 8] == "B")
            {
                pictureBox144.Image = Image.FromFile("Box.png");
            }
            if (Matriz[9, 9] == "B")
            {
                pictureBox145.Image = Image.FromFile("Box.png");
            }
            if (Matriz[9, 10] == "B")
            {
                pictureBox146.Image = Image.FromFile("Box.png");
            }
            if (Matriz[9, 11] == "B")
            {
                pictureBox147.Image = Image.FromFile("Box.png");
            }
            if (Matriz[9, 12] == "B")
            {
                pictureBox148.Image = Image.FromFile("Box.png");
            }
            if (Matriz[9, 13] == "B")
            {
                pictureBox149.Image = Image.FromFile("Box.png");
            }
            if (Matriz[9, 14] == "B")
            {
                pictureBox150.Image = Image.FromFile("Box.png");
            }
            if (Matriz[10, 0] == "B")
            {
                pictureBox151.Image = Image.FromFile("Box.png");
            }
            if (Matriz[10, 1] == "B")
            {
                pictureBox152.Image = Image.FromFile("Box.png");
            }
            if (Matriz[10, 2] == "B")
            {
                pictureBox153.Image = Image.FromFile("Box.png");
            }
            if (Matriz[10, 3] == "B")
            {
                pictureBox154.Image = Image.FromFile("Box.png");
            }
            if (Matriz[10, 4] == "B")
            {
                pictureBox155.Image = Image.FromFile("Box.png");
            }
            if (Matriz[10, 5] == "B")
            {
                pictureBox156.Image = Image.FromFile("Box.png");
            }
            if (Matriz[10, 6] == "B")
            {
                pictureBox157.Image = Image.FromFile("Box.png");
            }
            if (Matriz[10, 7] == "B")
            {
                pictureBox158.Image = Image.FromFile("Box.png");
            }
            if (Matriz[10, 8] == "B")
            {
                pictureBox159.Image = Image.FromFile("Box.png");
            }
            if (Matriz[10, 9] == "B")
            {
                pictureBox160.Image = Image.FromFile("Box.png");
            }
            if (Matriz[10, 10] == "B")
            {
                pictureBox161.Image = Image.FromFile("Box.png");
            }
            if (Matriz[10, 11] == "B")
            {
                pictureBox162.Image = Image.FromFile("Box.png");
            }
            if (Matriz[10, 12] == "B")
            {
                pictureBox163.Image = Image.FromFile("Box.png");
            }
            if (Matriz[10, 13] == "B")
            {
                pictureBox164.Image = Image.FromFile("Box.png");
            }
            if (Matriz[10, 14] == "B")
            {
                pictureBox165.Image = Image.FromFile("Box.png");
            }
            if (Matriz[11, 0] == "B")
            {
                pictureBox166.Image = Image.FromFile("Box.png");
            }
            if (Matriz[11, 1] == "B")
            {
                pictureBox167.Image = Image.FromFile("Box.png");
            }
            if (Matriz[11, 2] == "B")
            {
                pictureBox168.Image = Image.FromFile("Box.png");
            }
            if (Matriz[11, 3] == "B")
            {
                pictureBox169.Image = Image.FromFile("Box.png");
            }
            if (Matriz[11, 4] == "B")
            {
                pictureBox170.Image = Image.FromFile("Box.png");
            }
            if (Matriz[11, 5] == "B")
            {
                pictureBox171.Image = Image.FromFile("Box.png");
            }
            if (Matriz[11, 6] == "B")
            {
                pictureBox172.Image = Image.FromFile("Box.png");
            }
            if (Matriz[11, 7] == "B")
            {
                pictureBox173.Image = Image.FromFile("Box.png");
            }
            if (Matriz[11, 8] == "B")
            {
                pictureBox174.Image = Image.FromFile("Box.png");
            }
            if (Matriz[11, 9] == "B")
            {
                pictureBox175.Image = Image.FromFile("Box.png");
            }
            if (Matriz[11, 10] == "B")
            {
                pictureBox176.Image = Image.FromFile("Box.png");
            }
            if (Matriz[11, 11] == "B")
            {
                pictureBox177.Image = Image.FromFile("Box.png");
            }
            if (Matriz[11, 12] == "B")
            {
                pictureBox178.Image = Image.FromFile("Box.png");
            }
            if (Matriz[11, 13] == "B")
            {
                pictureBox179.Image = Image.FromFile("Box.png");
            }
            if (Matriz[11, 14] == "B")
            {
                pictureBox180.Image = Image.FromFile("Box.png");
            }
            if (Matriz[12, 0] == "B")
            {
                pictureBox181.Image = Image.FromFile("Box.png");
            }
            if (Matriz[12, 1] == "B")
            {
                pictureBox182.Image = Image.FromFile("Box.png");
            }
            if (Matriz[12, 2] == "B")
            {
                pictureBox183.Image = Image.FromFile("Box.png");
            }
            if (Matriz[12, 3] == "B")
            {
                pictureBox184.Image = Image.FromFile("Box.png");
            }
            if (Matriz[12, 4] == "B")
            {
                pictureBox185.Image = Image.FromFile("Box.png");
            }
            if (Matriz[12, 5] == "B")
            {
                pictureBox186.Image = Image.FromFile("Box.png");
            }
            if (Matriz[12, 6] == "B")
            {
                pictureBox187.Image = Image.FromFile("Box.png");
            }
            if (Matriz[12, 7] == "B")
            {
                pictureBox188.Image = Image.FromFile("Box.png");
            }
            if (Matriz[12, 8] == "B")
            {
                pictureBox189.Image = Image.FromFile("Box.png");
            }
            if (Matriz[12, 9] == "B")
            {
                pictureBox190.Image = Image.FromFile("Box.png");
            }
            if (Matriz[12, 10] == "B")
            {
                pictureBox191.Image = Image.FromFile("Box.png");
            }
            if (Matriz[12, 11] == "B")
            {
                pictureBox192.Image = Image.FromFile("Box.png");
            }
            if (Matriz[12, 12] == "B")
            {
                pictureBox193.Image = Image.FromFile("Box.png");
            }
            if (Matriz[12, 13] == "B")
            {
                pictureBox194.Image = Image.FromFile("Box.png");
            }
            if (Matriz[12, 14] == "B")
            {
                pictureBox195.Image = Image.FromFile("Box.png");
            }
            if (Matriz[13, 0] == "B")
            {
                pictureBox196.Image = Image.FromFile("Box.png");
            }
            if (Matriz[13, 1] == "B")
            {
                pictureBox197.Image = Image.FromFile("Box.png");
            }
            if (Matriz[13, 2] == "B")
            {
                pictureBox198.Image = Image.FromFile("Box.png");
            }
            if (Matriz[13, 3] == "B")
            {
                pictureBox199.Image = Image.FromFile("Box.png");
            }
            if (Matriz[13, 4] == "B")
            {
                pictureBox200.Image = Image.FromFile("Box.png");
            }
            if (Matriz[13, 5] == "B")
            {
                pictureBox201.Image = Image.FromFile("Box.png");
            }
            if (Matriz[13, 6] == "B")
            {
                pictureBox202.Image = Image.FromFile("Box.png");
            }
            if (Matriz[13, 7] == "B")
            {
                pictureBox203.Image = Image.FromFile("Box.png");
            }
            if (Matriz[13, 8] == "B")
            {
                pictureBox204.Image = Image.FromFile("Box.png");
            }
            if (Matriz[13, 9] == "B")
            {
                pictureBox205.Image = Image.FromFile("Box.png");
            }
            if (Matriz[13, 10] == "B")
            {
                pictureBox206.Image = Image.FromFile("Box.png");
            }
            if (Matriz[13, 11] == "B")
            {
                pictureBox207.Image = Image.FromFile("Box.png");
            }
            if (Matriz[13, 12] == "B")
            {
                pictureBox208.Image = Image.FromFile("Box.png");
            }
            if (Matriz[13, 13] == "B")
            {
                pictureBox209.Image = Image.FromFile("Box.png");
            }
            if (Matriz[13, 14] == "B")
            {
                pictureBox210.Image = Image.FromFile("Box.png");
            }
            if (Matriz[14, 0] == "B")
            {
                pictureBox211.Image = Image.FromFile("Box.png");
            }
            if (Matriz[14, 1] == "B")
            {
                pictureBox212.Image = Image.FromFile("Box.png");
            }
            if (Matriz[14, 2] == "B")
            {
                pictureBox213.Image = Image.FromFile("Box.png");
            }
            if (Matriz[14, 3] == "B")
            {
                pictureBox214.Image = Image.FromFile("Box.png");
            }
            if (Matriz[14, 4] == "B")
            {
                pictureBox215.Image = Image.FromFile("Box.png");
            }
            if (Matriz[14, 5] == "B")
            {
                pictureBox216.Image = Image.FromFile("Box.png");
            }
            if (Matriz[14, 6] == "B")
            {
                pictureBox217.Image = Image.FromFile("Box.png");
            }
            if (Matriz[14, 7] == "B")
            {
                pictureBox218.Image = Image.FromFile("Box.png");
            }
            if (Matriz[14, 8] == "B")
            {
                pictureBox219.Image = Image.FromFile("Box.png");
            }
            if (Matriz[14, 9] == "B")
            {
                pictureBox220.Image = Image.FromFile("Box.png");
            }
            if (Matriz[14, 10] == "B")
            {
                pictureBox221.Image = Image.FromFile("Box.png");
            }
            if (Matriz[14, 11] == "B")
            {
                pictureBox222.Image = Image.FromFile("Box.png");
            }
            if (Matriz[14, 12] == "B")
            {
                pictureBox223.Image = Image.FromFile("Box.png");
            }
            if (Matriz[14, 13] == "B")
            {
                pictureBox224.Image = Image.FromFile("Box.png");
            }
            if (Matriz[14, 14] == "B")
            {
                pictureBox225.Image = Image.FromFile("Box.png");
            }
            if (Matriz[0, 0] == "C")
            {
                pictureBox1.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[0, 1] == "C")
            {
                pictureBox2.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[0, 2] == "C")
            {
                pictureBox3.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[0, 3] == "C")
            {
                pictureBox4.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[0, 4] == "C")
            {
                pictureBox5.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[0, 5] == "C")
            {
                pictureBox6.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[0, 6] == "C")
            {
                pictureBox7.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[0, 7] == "C")
            {
                pictureBox8.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[0, 8] == "C")
            {
                pictureBox9.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[0, 9] == "C")
            {
                pictureBox10.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[0, 10] == "C")
            {
                pictureBox11.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[0, 11] == "C")
            {
                pictureBox12.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[0, 12] == "C")
            {
                pictureBox13.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[0, 13] == "C")
            {
                pictureBox14.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[0, 14] == "C")
            {
                pictureBox15.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[1, 0] == "C")
            {
                pictureBox16.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[1, 1] == "C")
            {
                pictureBox17.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[1, 2] == "C")
            {
                pictureBox18.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[1, 3] == "C")
            {
                pictureBox19.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[1, 4] == "C")
            {
                pictureBox20.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[1, 5] == "C")
            {
                pictureBox21.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[1, 6] == "C")
            {
                pictureBox22.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[1, 7] == "C")
            {
                pictureBox23.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[1, 8] == "C")
            {
                pictureBox24.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[1, 9] == "C")
            {
                pictureBox25.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[1, 10] == "C")
            {
                pictureBox26.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[1, 11] == "C")
            {
                pictureBox27.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[1, 12] == "C")
            {
                pictureBox28.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[1, 13] == "C")
            {
                pictureBox29.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[1, 14] == "C")
            {
                pictureBox30.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[2, 0] == "C")
            {
                pictureBox31.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[2, 1] == "C")
            {
                pictureBox32.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[2, 2] == "C")
            {
                pictureBox33.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[2, 3] == "C")
            {
                pictureBox34.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[2, 4] == "C")
            {
                pictureBox35.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[2, 5] == "C")
            {
                pictureBox36.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[2, 6] == "C")
            {
                pictureBox37.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[2, 7] == "C")
            {
                pictureBox38.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[2, 8] == "C")
            {
                pictureBox39.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[2, 9] == "C")
            {
                pictureBox40.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[2, 10] == "C")
            {
                pictureBox41.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[2, 11] == "C")
            {
                pictureBox42.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[2, 12] == "C")
            {
                pictureBox43.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[2, 13] == "C")
            {
                pictureBox44.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[2, 14] == "C")
            {
                pictureBox45.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[3, 0] == "C")
            {
                pictureBox46.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[3, 1] == "C")
            {
                pictureBox47.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[3, 2] == "C")
            {
                pictureBox48.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[3, 3] == "C")
            {
                pictureBox49.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[3, 4] == "C")
            {
                pictureBox50.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[3, 5] == "C")
            {
                pictureBox51.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[3, 6] == "C")
            {
                pictureBox52.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[3, 7] == "C")
            {
                pictureBox53.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[3, 8] == "C")
            {
                pictureBox54.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[3, 9] == "C")
            {
                pictureBox55.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[3, 10] == "C")
            {
                pictureBox56.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[3, 11] == "C")
            {
                pictureBox57.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[3, 12] == "C")
            {
                pictureBox58.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[3, 13] == "C")
            {
                pictureBox59.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[3, 14] == "C")
            {
                pictureBox60.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[4, 0] == "C")
            {
                pictureBox61.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[4, 1] == "C")
            {
                pictureBox62.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[4, 2] == "C")
            {
                pictureBox63.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[4, 3] == "C")
            {
                pictureBox64.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[4, 4] == "C")
            {
                pictureBox65.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[4, 5] == "C")
            {
                pictureBox66.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[4, 6] == "C")
            {
                pictureBox67.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[4, 7] == "C")
            {
                pictureBox68.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[4, 8] == "C")
            {
                pictureBox69.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[4, 9] == "C")
            {
                pictureBox70.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[4, 10] == "C")
            {
                pictureBox71.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[4, 11] == "C")
            {
                pictureBox72.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[4, 12] == "C")
            {
                pictureBox73.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[4, 13] == "C")
            {
                pictureBox74.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[4, 14] == "C")
            {
                pictureBox75.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[5, 0] == "C")
            {
                pictureBox76.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[5, 1] == "C")
            {
                pictureBox77.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[5, 2] == "C")
            {
                pictureBox78.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[5, 3] == "C")
            {
                pictureBox79.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[5, 4] == "C")
            {
                pictureBox80.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[5, 5] == "C")
            {
                pictureBox81.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[5, 6] == "C")
            {
                pictureBox82.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[5, 7] == "C")
            {
                pictureBox83.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[5, 8] == "C")
            {
                pictureBox84.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[5, 9] == "C")
            {
                pictureBox85.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[5, 10] == "C")
            {
                pictureBox86.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[5, 11] == "C")
            {
                pictureBox87.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[5, 12] == "C")
            {
                pictureBox88.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[5, 13] == "C")
            {
                pictureBox89.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[5, 14] == "C")
            {
                pictureBox90.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[6, 0] == "C")
            {
                pictureBox91.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[6, 1] == "C")
            {
                pictureBox92.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[6, 2] == "C")
            {
                pictureBox93.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[6, 3] == "C")
            {
                pictureBox94.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[6, 4] == "C")
            {
                pictureBox95.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[6, 5] == "C")
            {
                pictureBox96.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[6, 6] == "C")
            {
                pictureBox97.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[6, 7] == "C")
            {
                pictureBox98.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[6, 8] == "C")
            {
                pictureBox99.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[6, 9] == "C")
            {
                pictureBox100.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[6, 10] == "C")
            {
                pictureBox101.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[6, 11] == "C")
            {
                pictureBox102.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[6, 12] == "C")
            {
                pictureBox103.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[6, 13] == "C")
            {
                pictureBox104.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[6, 14] == "C")
            {
                pictureBox105.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[7, 0] == "C")
            {
                pictureBox106.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[7, 1] == "C")
            {
                pictureBox107.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[7, 2] == "C")
            {
                pictureBox108.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[7, 3] == "C")
            {
                pictureBox109.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[7, 4] == "C")
            {
                pictureBox110.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[7, 5] == "C")
            {
                pictureBox111.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[7, 6] == "C")
            {
                pictureBox112.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[7, 7] == "C")
            {
                pictureBox113.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[7, 8] == "C")
            {
                pictureBox114.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[7, 9] == "C")
            {
                pictureBox115.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[7, 10] == "C")
            {
                pictureBox116.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[7, 11] == "C")
            {
                pictureBox117.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[7, 12] == "C")
            {
                pictureBox118.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[7, 13] == "C")
            {
                pictureBox119.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[7, 14] == "C")
            {
                pictureBox120.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[8, 0] == "C")
            {
                pictureBox121.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[8, 1] == "C")
            {
                pictureBox122.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[8, 2] == "C")
            {
                pictureBox123.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[8, 3] == "C")
            {
                pictureBox124.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[8, 4] == "C")
            {
                pictureBox125.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[8, 5] == "C")
            {
                pictureBox126.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[8, 6] == "C")
            {
                pictureBox127.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[8, 7] == "C")
            {
                pictureBox128.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[8, 8] == "C")
            {
                pictureBox129.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[8, 9] == "C")
            {
                pictureBox130.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[8, 10] == "C")
            {
                pictureBox131.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[8, 11] == "C")
            {
                pictureBox132.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[8, 12] == "C")
            {
                pictureBox133.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[8, 13] == "C")
            {
                pictureBox134.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[8, 14] == "C")
            {
                pictureBox135.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[9, 0] == "C")
            {
                pictureBox136.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[9, 1] == "C")
            {
                pictureBox137.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[9, 2] == "C")
            {
                pictureBox138.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[9, 3] == "C")
            {
                pictureBox139.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[9, 4] == "C")
            {
                pictureBox140.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[9, 5] == "C")
            {
                pictureBox141.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[9, 6] == "C")
            {
                pictureBox142.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[9, 7] == "C")
            {
                pictureBox143.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[9, 8] == "C")
            {
                pictureBox144.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[9, 9] == "C")
            {
                pictureBox145.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[9, 10] == "C")
            {
                pictureBox146.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[9, 11] == "C")
            {
                pictureBox147.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[9, 12] == "C")
            {
                pictureBox148.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[9, 13] == "C")
            {
                pictureBox149.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[9, 14] == "C")
            {
                pictureBox150.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[10, 0] == "C")
            {
                pictureBox151.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[10, 1] == "C")
            {
                pictureBox152.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[10, 2] == "C")
            {
                pictureBox153.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[10, 3] == "C")
            {
                pictureBox154.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[10, 4] == "C")
            {
                pictureBox155.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[10, 5] == "C")
            {
                pictureBox156.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[10, 6] == "C")
            {
                pictureBox157.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[10, 7] == "C")
            {
                pictureBox158.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[10, 8] == "C")
            {
                pictureBox159.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[10, 9] == "C")
            {
                pictureBox160.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[10, 10] == "C")
            {
                pictureBox161.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[10, 11] == "C")
            {
                pictureBox162.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[10, 12] == "C")
            {
                pictureBox163.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[10, 13] == "C")
            {
                pictureBox164.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[10, 14] == "C")
            {
                pictureBox165.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[11, 0] == "C")
            {
                pictureBox166.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[11, 1] == "C")
            {
                pictureBox167.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[11, 2] == "C")
            {
                pictureBox168.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[11, 3] == "C")
            {
                pictureBox169.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[11, 4] == "C")
            {
                pictureBox170.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[11, 5] == "C")
            {
                pictureBox171.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[11, 6] == "C")
            {
                pictureBox172.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[11, 7] == "C")
            {
                pictureBox173.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[11, 8] == "C")
            {
                pictureBox174.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[11, 9] == "C")
            {
                pictureBox175.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[11, 10] == "C")
            {
                pictureBox176.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[11, 11] == "C")
            {
                pictureBox177.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[11, 12] == "C")
            {
                pictureBox178.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[11, 13] == "C")
            {
                pictureBox179.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[11, 14] == "C")
            {
                pictureBox180.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[12, 0] == "C")
            {
                pictureBox181.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[12, 1] == "C")
            {
                pictureBox182.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[12, 2] == "C")
            {
                pictureBox183.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[12, 3] == "C")
            {
                pictureBox184.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[12, 4] == "C")
            {
                pictureBox185.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[12, 5] == "C")
            {
                pictureBox186.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[12, 6] == "C")
            {
                pictureBox187.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[12, 7] == "C")
            {
                pictureBox188.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[12, 8] == "C")
            {
                pictureBox189.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[12, 9] == "C")
            {
                pictureBox190.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[12, 10] == "C")
            {
                pictureBox191.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[12, 11] == "C")
            {
                pictureBox192.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[12, 12] == "C")
            {
                pictureBox193.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[12, 13] == "C")
            {
                pictureBox194.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[12, 14] == "C")
            {
                pictureBox195.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[13, 0] == "C")
            {
                pictureBox196.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[13, 1] == "C")
            {
                pictureBox197.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[13, 2] == "C")
            {
                pictureBox198.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[13, 3] == "C")
            {
                pictureBox199.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[13, 4] == "C")
            {
                pictureBox200.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[13, 5] == "C")
            {
                pictureBox201.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[13, 6] == "C")
            {
                pictureBox202.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[13, 7] == "C")
            {
                pictureBox203.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[13, 8] == "C")
            {
                pictureBox204.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[13, 9] == "C")
            {
                pictureBox205.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[13, 10] == "C")
            {
                pictureBox206.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[13, 11] == "C")
            {
                pictureBox207.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[13, 12] == "C")
            {
                pictureBox208.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[13, 13] == "C")
            {
                pictureBox209.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[13, 14] == "C")
            {
                pictureBox210.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[14, 0] == "C")
            {
                pictureBox211.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[14, 1] == "C")
            {
                pictureBox212.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[14, 2] == "C")
            {
                pictureBox213.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[14, 3] == "C")
            {
                pictureBox214.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[14, 4] == "C")
            {
                pictureBox215.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[14, 5] == "C")
            {
                pictureBox216.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[14, 6] == "C")
            {
                pictureBox217.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[14, 7] == "C")
            {
                pictureBox218.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[14, 8] == "C")
            {
                pictureBox219.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[14, 9] == "C")
            {
                pictureBox220.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[14, 10] == "C")
            {
                pictureBox221.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[14, 11] == "C")
            {
                pictureBox222.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[14, 12] == "C")
            {
                pictureBox223.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[14, 13] == "C")
            {
                pictureBox224.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[14, 14] == "C")
            {
                pictureBox225.Image = Image.FromFile("Asteroide.png");
            }
            if (Matriz[0, 0] == "D")
            {
                pictureBox1.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[0, 1] == "D")
            {
                pictureBox2.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[0, 2] == "D")
            {
                pictureBox3.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[0, 3] == "D")
            {
                pictureBox4.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[0, 4] == "D")
            {
                pictureBox5.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[0, 5] == "D")
            {
                pictureBox6.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[0, 6] == "D")
            {
                pictureBox7.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[0, 7] == "D")
            {
                pictureBox8.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[0, 8] == "D")
            {
                pictureBox9.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[0, 9] == "D")
            {
                pictureBox10.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[0, 10] == "D")
            {
                pictureBox11.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[0, 11] == "D")
            {
                pictureBox12.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[0, 12] == "D")
            {
                pictureBox13.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[0, 13] == "D")
            {
                pictureBox14.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[0, 14] == "D")
            {
                pictureBox15.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[1, 0] == "D")
            {
                pictureBox16.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[1, 1] == "D")
            {
                pictureBox17.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[1, 2] == "D")
            {
                pictureBox18.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[1, 3] == "D")
            {
                pictureBox19.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[1, 4] == "D")
            {
                pictureBox20.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[1, 5] == "D")
            {
                pictureBox21.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[1, 6] == "D")
            {
                pictureBox22.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[1, 7] == "D")
            {
                pictureBox23.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[1, 8] == "D")
            {
                pictureBox24.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[1, 9] == "D")
            {
                pictureBox25.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[1, 10] == "D")
            {
                pictureBox26.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[1, 11] == "D")
            {
                pictureBox27.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[1, 12] == "D")
            {
                pictureBox28.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[1, 13] == "D")
            {
                pictureBox29.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[1, 14] == "D")
            {
                pictureBox30.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[2, 0] == "D")
            {
                pictureBox31.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[2, 1] == "D")
            {
                pictureBox32.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[2, 2] == "D")
            {
                pictureBox33.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[2, 3] == "D")
            {
                pictureBox34.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[2, 4] == "D")
            {
                pictureBox35.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[2, 5] == "D")
            {
                pictureBox36.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[2, 6] == "D")
            {
                pictureBox37.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[2, 7] == "D")
            {
                pictureBox38.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[2, 8] == "D")
            {
                pictureBox39.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[2, 9] == "D")
            {
                pictureBox40.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[2, 10] == "D")
            {
                pictureBox41.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[2, 11] == "D")
            {
                pictureBox42.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[2, 12] == "D")
            {
                pictureBox43.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[2, 13] == "D")
            {
                pictureBox44.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[2, 14] == "D")
            {
                pictureBox45.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[3, 0] == "D")
            {
                pictureBox46.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[3, 1] == "D")
            {
                pictureBox47.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[3, 2] == "D")
            {
                pictureBox48.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[3, 3] == "D")
            {
                pictureBox49.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[3, 4] == "D")
            {
                pictureBox50.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[3, 5] == "D")
            {
                pictureBox51.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[3, 6] == "D")
            {
                pictureBox52.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[3, 7] == "D")
            {
                pictureBox53.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[3, 8] == "D")
            {
                pictureBox54.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[3, 9] == "D")
            {
                pictureBox55.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[3, 10] == "D")
            {
                pictureBox56.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[3, 11] == "D")
            {
                pictureBox57.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[3, 12] == "D")
            {
                pictureBox58.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[3, 13] == "D")
            {
                pictureBox59.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[3, 14] == "D")
            {
                pictureBox60.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[4, 0] == "D")
            {
                pictureBox61.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[4, 1] == "D")
            {
                pictureBox62.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[4, 2] == "D")
            {
                pictureBox63.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[4, 3] == "D")
            {
                pictureBox64.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[4, 4] == "D")
            {
                pictureBox65.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[4, 5] == "D")
            {
                pictureBox66.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[4, 6] == "D")
            {
                pictureBox67.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[4, 7] == "D")
            {
                pictureBox68.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[4, 8] == "D")
            {
                pictureBox69.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[4, 9] == "D")
            {
                pictureBox70.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[4, 10] == "D")
            {
                pictureBox71.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[4, 11] == "D")
            {
                pictureBox72.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[4, 12] == "D")
            {
                pictureBox73.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[4, 13] == "D")
            {
                pictureBox74.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[4, 14] == "D")
            {
                pictureBox75.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[5, 0] == "D")
            {
                pictureBox76.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[5, 1] == "D")
            {
                pictureBox77.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[5, 2] == "D")
            {
                pictureBox78.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[5, 3] == "D")
            {
                pictureBox79.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[5, 4] == "D")
            {
                pictureBox80.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[5, 5] == "D")
            {
                pictureBox81.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[5, 6] == "D")
            {
                pictureBox82.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[5, 7] == "D")
            {
                pictureBox83.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[5, 8] == "D")
            {
                pictureBox84.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[5, 9] == "D")
            {
                pictureBox85.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[5, 10] == "D")
            {
                pictureBox86.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[5, 11] == "D")
            {
                pictureBox87.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[5, 12] == "D")
            {
                pictureBox88.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[5, 13] == "D")
            {
                pictureBox89.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[5, 14] == "D")
            {
                pictureBox90.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[6, 0] == "D")
            {
                pictureBox91.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[6, 1] == "D")
            {
                pictureBox92.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[6, 2] == "D")
            {
                pictureBox93.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[6, 3] == "D")
            {
                pictureBox94.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[6, 4] == "D")
            {
                pictureBox95.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[6, 5] == "D")
            {
                pictureBox96.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[6, 6] == "D")
            {
                pictureBox97.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[6, 7] == "D")
            {
                pictureBox98.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[6, 8] == "D")
            {
                pictureBox99.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[6, 9] == "D")
            {
                pictureBox100.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[6, 10] == "D")
            {
                pictureBox101.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[6, 11] == "D")
            {
                pictureBox102.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[6, 12] == "D")
            {
                pictureBox103.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[6, 13] == "D")
            {
                pictureBox104.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[6, 14] == "D")
            {
                pictureBox105.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[7, 0] == "D")
            {
                pictureBox106.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[7, 1] == "D")
            {
                pictureBox107.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[7, 2] == "D")
            {
                pictureBox108.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[7, 3] == "D")
            {
                pictureBox109.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[7, 4] == "D")
            {
                pictureBox110.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[7, 5] == "D")
            {
                pictureBox111.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[7, 6] == "D")
            {
                pictureBox112.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[7, 7] == "D")
            {
                pictureBox113.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[7, 8] == "D")
            {
                pictureBox114.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[7, 9] == "D")
            {
                pictureBox115.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[7, 10] == "D")
            {
                pictureBox116.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[7, 11] == "D")
            {
                pictureBox117.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[7, 12] == "D")
            {
                pictureBox118.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[7, 13] == "D")
            {
                pictureBox119.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[7, 14] == "D")
            {
                pictureBox120.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[8, 0] == "D")
            {
                pictureBox121.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[8, 1] == "D")
            {
                pictureBox122.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[8, 2] == "D")
            {
                pictureBox123.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[8, 3] == "D")
            {
                pictureBox124.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[8, 4] == "D")
            {
                pictureBox125.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[8, 5] == "D")
            {
                pictureBox126.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[8, 6] == "D")
            {
                pictureBox127.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[8, 7] == "D")
            {
                pictureBox128.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[8, 8] == "D")
            {
                pictureBox129.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[8, 9] == "D")
            {
                pictureBox130.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[8, 10] == "D")
            {
                pictureBox131.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[8, 11] == "D")
            {
                pictureBox132.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[8, 12] == "D")
            {
                pictureBox133.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[8, 13] == "D")
            {
                pictureBox134.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[8, 14] == "D")
            {
                pictureBox135.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[9, 0] == "D")
            {
                pictureBox136.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[9, 1] == "D")
            {
                pictureBox137.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[9, 2] == "D")
            {
                pictureBox138.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[9, 3] == "D")
            {
                pictureBox139.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[9, 4] == "D")
            {
                pictureBox140.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[9, 5] == "D")
            {
                pictureBox141.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[9, 6] == "D")
            {
                pictureBox142.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[9, 7] == "D")
            {
                pictureBox143.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[9, 8] == "D")
            {
                pictureBox144.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[9, 9] == "D")
            {
                pictureBox145.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[9, 10] == "D")
            {
                pictureBox146.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[9, 11] == "D")
            {
                pictureBox147.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[9, 12] == "D")
            {
                pictureBox148.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[9, 13] == "D")
            {
                pictureBox149.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[9, 14] == "D")
            {
                pictureBox150.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[10, 0] == "D")
            {
                pictureBox151.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[10, 1] == "D")
            {
                pictureBox152.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[10, 2] == "D")
            {
                pictureBox153.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[10, 3] == "D")
            {
                pictureBox154.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[10, 4] == "D")
            {
                pictureBox155.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[10, 5] == "D")
            {
                pictureBox156.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[10, 6] == "D")
            {
                pictureBox157.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[10, 7] == "D")
            {
                pictureBox158.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[10, 8] == "D")
            {
                pictureBox159.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[10, 9] == "D")
            {
                pictureBox160.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[10, 10] == "D")
            {
                pictureBox161.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[10, 11] == "D")
            {
                pictureBox162.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[10, 12] == "D")
            {
                pictureBox163.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[10, 13] == "D")
            {
                pictureBox164.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[10, 14] == "D")
            {
                pictureBox165.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[11, 0] == "D")
            {
                pictureBox166.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[11, 1] == "D")
            {
                pictureBox167.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[11, 2] == "D")
            {
                pictureBox168.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[11, 3] == "D")
            {
                pictureBox169.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[11, 4] == "D")
            {
                pictureBox170.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[11, 5] == "D")
            {
                pictureBox171.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[11, 6] == "D")
            {
                pictureBox172.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[11, 7] == "D")
            {
                pictureBox173.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[11, 8] == "D")
            {
                pictureBox174.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[11, 9] == "D")
            {
                pictureBox175.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[11, 10] == "D")
            {
                pictureBox176.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[11, 11] == "D")
            {
                pictureBox177.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[11, 12] == "D")
            {
                pictureBox178.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[11, 13] == "D")
            {
                pictureBox179.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[11, 14] == "D")
            {
                pictureBox180.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[12, 0] == "D")
            {
                pictureBox181.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[12, 1] == "D")
            {
                pictureBox182.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[12, 2] == "D")
            {
                pictureBox183.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[12, 3] == "D")
            {
                pictureBox184.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[12, 4] == "D")
            {
                pictureBox185.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[12, 5] == "D")
            {
                pictureBox186.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[12, 6] == "D")
            {
                pictureBox187.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[12, 7] == "D")
            {
                pictureBox188.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[12, 8] == "D")
            {
                pictureBox189.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[12, 9] == "D")
            {
                pictureBox190.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[12, 10] == "D")
            {
                pictureBox191.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[12, 11] == "D")
            {
                pictureBox192.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[12, 12] == "D")
            {
                pictureBox193.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[12, 13] == "D")
            {
                pictureBox194.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[12, 14] == "D")
            {
                pictureBox195.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[13, 0] == "D")
            {
                pictureBox196.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[13, 1] == "D")
            {
                pictureBox197.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[13, 2] == "D")
            {
                pictureBox198.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[13, 3] == "D")
            {
                pictureBox199.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[13, 4] == "D")
            {
                pictureBox200.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[13, 5] == "D")
            {
                pictureBox201.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[13, 6] == "D")
            {
                pictureBox202.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[13, 7] == "D")
            {
                pictureBox203.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[13, 8] == "D")
            {
                pictureBox204.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[13, 9] == "D")
            {
                pictureBox205.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[13, 10] == "D")
            {
                pictureBox206.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[13, 11] == "D")
            {
                pictureBox207.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[13, 12] == "D")
            {
                pictureBox208.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[13, 13] == "D")
            {
                pictureBox209.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[13, 14] == "D")
            {
                pictureBox210.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[14, 0] == "D")
            {
                pictureBox211.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[14, 1] == "D")
            {
                pictureBox212.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[14, 2] == "D")
            {
                pictureBox213.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[14, 3] == "D")
            {
                pictureBox214.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[14, 4] == "D")
            {
                pictureBox215.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[14, 5] == "D")
            {
                pictureBox216.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[14, 6] == "D")
            {
                pictureBox217.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[14, 7] == "D")
            {
                pictureBox218.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[14, 8] == "D")
            {
                pictureBox219.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[14, 9] == "D")
            {
                pictureBox220.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[14, 10] == "D")
            {
                pictureBox221.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[14, 11] == "D")
            {
                pictureBox222.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[14, 12] == "D")
            {
                pictureBox223.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[14, 13] == "D")
            {
                pictureBox224.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[14, 14] == "D")
            {
                pictureBox225.Image = Image.FromFile("Tierra.png");
            }
            if (Matriz[0, 0] == "E")
            {
                pictureBox1.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[0, 1] == "E")
            {
                pictureBox2.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[0, 2] == "E")
            {
                pictureBox3.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[0, 3] == "E")
            {
                pictureBox4.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[0, 4] == "E")
            {
                pictureBox5.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[0, 5] == "E")
            {
                pictureBox6.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[0, 6] == "E")
            {
                pictureBox7.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[0, 7] == "E")
            {
                pictureBox8.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[0, 8] == "E")
            {
                pictureBox9.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[0, 9] == "E")
            {
                pictureBox10.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[0, 10] == "E")
            {
                pictureBox11.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[0, 11] == "E")
            {
                pictureBox12.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[0, 12] == "E")
            {
                pictureBox13.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[0, 13] == "E")
            {
                pictureBox14.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[0, 14] == "E")
            {
                pictureBox15.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[1, 0] == "E")
            {
                pictureBox16.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[1, 1] == "E")
            {
                pictureBox17.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[1, 2] == "E")
            {
                pictureBox18.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[1, 3] == "E")
            {
                pictureBox19.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[1, 4] == "E")
            {
                pictureBox20.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[1, 5] == "E")
            {
                pictureBox21.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[1, 6] == "E")
            {
                pictureBox22.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[1, 7] == "E")
            {
                pictureBox23.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[1, 8] == "E")
            {
                pictureBox24.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[1, 9] == "E")
            {
                pictureBox25.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[1, 10] == "E")
            {
                pictureBox26.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[1, 11] == "E")
            {
                pictureBox27.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[1, 12] == "E")
            {
                pictureBox28.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[1, 13] == "E")
            {
                pictureBox29.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[1, 14] == "E")
            {
                pictureBox30.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[2, 0] == "E")
            {
                pictureBox31.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[2, 1] == "E")
            {
                pictureBox32.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[2, 2] == "E")
            {
                pictureBox33.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[2, 3] == "E")
            {
                pictureBox34.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[2, 4] == "E")
            {
                pictureBox35.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[2, 5] == "E")
            {
                pictureBox36.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[2, 6] == "E")
            {
                pictureBox37.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[2, 7] == "E")
            {
                pictureBox38.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[2, 8] == "E")
            {
                pictureBox39.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[2, 9] == "E")
            {
                pictureBox40.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[2, 10] == "E")
            {
                pictureBox41.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[2, 11] == "E")
            {
                pictureBox42.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[2, 12] == "E")
            {
                pictureBox43.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[2, 13] == "E")
            {
                pictureBox44.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[2, 14] == "E")
            {
                pictureBox45.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[3, 0] == "E")
            {
                pictureBox46.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[3, 1] == "E")
            {
                pictureBox47.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[3, 2] == "E")
            {
                pictureBox48.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[3, 3] == "E")
            {
                pictureBox49.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[3, 4] == "E")
            {
                pictureBox50.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[3, 5] == "E")
            {
                pictureBox51.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[3, 6] == "E")
            {
                pictureBox52.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[3, 7] == "E")
            {
                pictureBox53.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[3, 8] == "E")
            {
                pictureBox54.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[3, 9] == "E")
            {
                pictureBox55.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[3, 10] == "E")
            {
                pictureBox56.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[3, 11] == "E")
            {
                pictureBox57.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[3, 12] == "E")
            {
                pictureBox58.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[3, 13] == "E")
            {
                pictureBox59.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[3, 14] == "E")
            {
                pictureBox60.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[4, 0] == "E")
            {
                pictureBox61.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[4, 1] == "E")
            {
                pictureBox62.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[4, 2] == "E")
            {
                pictureBox63.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[4, 3] == "E")
            {
                pictureBox64.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[4, 4] == "E")
            {
                pictureBox65.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[4, 5] == "E")
            {
                pictureBox66.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[4, 6] == "E")
            {
                pictureBox67.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[4, 7] == "E")
            {
                pictureBox68.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[4, 8] == "E")
            {
                pictureBox69.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[4, 9] == "E")
            {
                pictureBox70.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[4, 10] == "E")
            {
                pictureBox71.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[4, 11] == "E")
            {
                pictureBox72.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[4, 12] == "E")
            {
                pictureBox73.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[4, 13] == "E")
            {
                pictureBox74.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[4, 14] == "E")
            {
                pictureBox75.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[5, 0] == "E")
            {
                pictureBox76.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[5, 1] == "E")
            {
                pictureBox77.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[5, 2] == "E")
            {
                pictureBox78.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[5, 3] == "E")
            {
                pictureBox79.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[5, 4] == "E")
            {
                pictureBox80.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[5, 5] == "E")
            {
                pictureBox81.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[5, 6] == "E")
            {
                pictureBox82.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[5, 7] == "E")
            {
                pictureBox83.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[5, 8] == "E")
            {
                pictureBox84.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[5, 9] == "E")
            {
                pictureBox85.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[5, 10] == "E")
            {
                pictureBox86.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[5, 11] == "E")
            {
                pictureBox87.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[5, 12] == "E")
            {
                pictureBox88.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[5, 13] == "E")
            {
                pictureBox89.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[5, 14] == "E")
            {
                pictureBox90.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[6, 0] == "E")
            {
                pictureBox91.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[6, 1] == "E")
            {
                pictureBox92.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[6, 2] == "E")
            {
                pictureBox93.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[6, 3] == "E")
            {
                pictureBox94.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[6, 4] == "E")
            {
                pictureBox95.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[6, 5] == "E")
            {
                pictureBox96.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[6, 6] == "E")
            {
                pictureBox97.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[6, 7] == "E")
            {
                pictureBox98.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[6, 8] == "E")
            {
                pictureBox99.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[6, 9] == "E")
            {
                pictureBox100.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[6, 10] == "E")
            {
                pictureBox101.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[6, 11] == "E")
            {
                pictureBox102.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[6, 12] == "E")
            {
                pictureBox103.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[6, 13] == "E")
            {
                pictureBox104.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[6, 14] == "E")
            {
                pictureBox105.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[7, 0] == "E")
            {
                pictureBox106.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[7, 1] == "E")
            {
                pictureBox107.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[7, 2] == "E")
            {
                pictureBox108.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[7, 3] == "E")
            {
                pictureBox109.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[7, 4] == "E")
            {
                pictureBox110.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[7, 5] == "E")
            {
                pictureBox111.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[7, 6] == "E")
            {
                pictureBox112.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[7, 7] == "E")
            {
                pictureBox113.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[7, 8] == "E")
            {
                pictureBox114.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[7, 9] == "E")
            {
                pictureBox115.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[7, 10] == "E")
            {
                pictureBox116.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[7, 11] == "E")
            {
                pictureBox117.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[7, 12] == "E")
            {
                pictureBox118.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[7, 13] == "E")
            {
                pictureBox119.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[7, 14] == "E")
            {
                pictureBox120.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[8, 0] == "E")
            {
                pictureBox121.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[8, 1] == "E")
            {
                pictureBox122.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[8, 2] == "E")
            {
                pictureBox123.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[8, 3] == "E")
            {
                pictureBox124.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[8, 4] == "E")
            {
                pictureBox125.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[8, 5] == "E")
            {
                pictureBox126.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[8, 6] == "E")
            {
                pictureBox127.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[8, 7] == "E")
            {
                pictureBox128.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[8, 8] == "E")
            {
                pictureBox129.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[8, 9] == "E")
            {
                pictureBox130.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[8, 10] == "E")
            {
                pictureBox131.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[8, 11] == "E")
            {
                pictureBox132.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[8, 12] == "E")
            {
                pictureBox133.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[8, 13] == "E")
            {
                pictureBox134.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[8, 14] == "E")
            {
                pictureBox135.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[9, 0] == "E")
            {
                pictureBox136.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[9, 1] == "E")
            {
                pictureBox137.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[9, 2] == "E")
            {
                pictureBox138.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[9, 3] == "E")
            {
                pictureBox139.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[9, 4] == "E")
            {
                pictureBox140.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[9, 5] == "E")
            {
                pictureBox141.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[9, 6] == "E")
            {
                pictureBox142.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[9, 7] == "E")
            {
                pictureBox143.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[9, 8] == "E")
            {
                pictureBox144.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[9, 9] == "E")
            {
                pictureBox145.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[9, 10] == "E")
            {
                pictureBox146.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[9, 11] == "E")
            {
                pictureBox147.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[9, 12] == "E")
            {
                pictureBox148.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[9, 13] == "E")
            {
                pictureBox149.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[9, 14] == "E")
            {
                pictureBox150.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[10, 0] == "E")
            {
                pictureBox151.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[10, 1] == "E")
            {
                pictureBox152.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[10, 2] == "E")
            {
                pictureBox153.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[10, 3] == "E")
            {
                pictureBox154.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[10, 4] == "E")
            {
                pictureBox155.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[10, 5] == "E")
            {
                pictureBox156.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[10, 6] == "E")
            {
                pictureBox157.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[10, 7] == "E")
            {
                pictureBox158.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[10, 8] == "E")
            {
                pictureBox159.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[10, 9] == "E")
            {
                pictureBox160.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[10, 10] == "E")
            {
                pictureBox161.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[10, 11] == "E")
            {
                pictureBox162.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[10, 12] == "E")
            {
                pictureBox163.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[10, 13] == "E")
            {
                pictureBox164.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[10, 14] == "E")
            {
                pictureBox165.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[11, 0] == "E")
            {
                pictureBox166.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[11, 1] == "E")
            {
                pictureBox167.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[11, 2] == "E")
            {
                pictureBox168.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[11, 3] == "E")
            {
                pictureBox169.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[11, 4] == "E")
            {
                pictureBox170.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[11, 5] == "E")
            {
                pictureBox171.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[11, 6] == "E")
            {
                pictureBox172.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[11, 7] == "E")
            {
                pictureBox173.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[11, 8] == "E")
            {
                pictureBox174.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[11, 9] == "E")
            {
                pictureBox175.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[11, 10] == "E")
            {
                pictureBox176.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[11, 11] == "E")
            {
                pictureBox177.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[11, 12] == "E")
            {
                pictureBox178.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[11, 13] == "E")
            {
                pictureBox179.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[11, 14] == "E")
            {
                pictureBox180.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[12, 0] == "E")
            {
                pictureBox181.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[12, 1] == "E")
            {
                pictureBox182.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[12, 2] == "E")
            {
                pictureBox183.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[12, 3] == "E")
            {
                pictureBox184.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[12, 4] == "E")
            {
                pictureBox185.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[12, 5] == "E")
            {
                pictureBox186.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[12, 6] == "E")
            {
                pictureBox187.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[12, 7] == "E")
            {
                pictureBox188.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[12, 8] == "E")
            {
                pictureBox189.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[12, 9] == "E")
            {
                pictureBox190.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[12, 10] == "E")
            {
                pictureBox191.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[12, 11] == "E")
            {
                pictureBox192.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[12, 12] == "E")
            {
                pictureBox193.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[12, 13] == "E")
            {
                pictureBox194.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[12, 14] == "E")
            {
                pictureBox195.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[13, 0] == "E")
            {
                pictureBox196.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[13, 1] == "E")
            {
                pictureBox197.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[13, 2] == "E")
            {
                pictureBox198.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[13, 3] == "E")
            {
                pictureBox199.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[13, 4] == "E")
            {
                pictureBox200.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[13, 5] == "E")
            {
                pictureBox201.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[13, 6] == "E")
            {
                pictureBox202.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[13, 7] == "E")
            {
                pictureBox203.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[13, 8] == "E")
            {
                pictureBox204.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[13, 9] == "E")
            {
                pictureBox205.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[13, 10] == "E")
            {
                pictureBox206.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[13, 11] == "E")
            {
                pictureBox207.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[13, 12] == "E")
            {
                pictureBox208.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[13, 13] == "E")
            {
                pictureBox209.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[13, 14] == "E")
            {
                pictureBox210.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[14, 0] == "E")
            {
                pictureBox211.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[14, 1] == "E")
            {
                pictureBox212.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[14, 2] == "E")
            {
                pictureBox213.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[14, 3] == "E")
            {
                pictureBox214.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[14, 4] == "E")
            {
                pictureBox215.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[14, 5] == "E")
            {
                pictureBox216.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[14, 6] == "E")
            {
                pictureBox217.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[14, 7] == "E")
            {
                pictureBox218.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[14, 8] == "E")
            {
                pictureBox219.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[14, 9] == "E")
            {
                pictureBox220.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[14, 10] == "E")
            {
                pictureBox221.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[14, 11] == "E")
            {
                pictureBox222.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[14, 12] == "E")
            {
                pictureBox223.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[14, 13] == "E")
            {
                pictureBox224.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[14, 14] == "E")
            {
                pictureBox225.Image = Image.FromFile("Cristal.png");
            }
            if (Matriz[0, 0] == "F")
            {
                pictureBox1.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[0, 1] == "F")
            {
                pictureBox2.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[0, 2] == "F")
            {
                pictureBox3.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[0, 3] == "F")
            {
                pictureBox4.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[0, 4] == "F")
            {
                pictureBox5.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[0, 5] == "F")
            {
                pictureBox6.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[0, 6] == "F")
            {
                pictureBox7.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[0, 7] == "F")
            {
                pictureBox8.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[0, 8] == "F")
            {
                pictureBox9.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[0, 9] == "F")
            {
                pictureBox10.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[0, 10] == "F")
            {
                pictureBox11.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[0, 11] == "F")
            {
                pictureBox12.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[0, 12] == "F")
            {
                pictureBox13.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[0, 13] == "F")
            {
                pictureBox14.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[0, 14] == "F")
            {
                pictureBox15.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[1, 0] == "F")
            {
                pictureBox16.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[1, 1] == "F")
            {
                pictureBox17.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[1, 2] == "F")
            {
                pictureBox18.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[1, 3] == "F")
            {
                pictureBox19.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[1, 4] == "F")
            {
                pictureBox20.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[1, 5] == "F")
            {
                pictureBox21.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[1, 6] == "F")
            {
                pictureBox22.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[1, 7] == "F")
            {
                pictureBox23.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[1, 8] == "F")
            {
                pictureBox24.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[1, 9] == "F")
            {
                pictureBox25.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[1, 10] == "F")
            {
                pictureBox26.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[1, 11] == "F")
            {
                pictureBox27.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[1, 12] == "F")
            {
                pictureBox28.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[1, 13] == "F")
            {
                pictureBox29.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[1, 14] == "F")
            {
                pictureBox30.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[2, 0] == "F")
            {
                pictureBox31.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[2, 1] == "F")
            {
                pictureBox32.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[2, 2] == "F")
            {
                pictureBox33.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[2, 3] == "F")
            {
                pictureBox34.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[2, 4] == "F")
            {
                pictureBox35.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[2, 5] == "F")
            {
                pictureBox36.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[2, 6] == "F")
            {
                pictureBox37.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[2, 7] == "F")
            {
                pictureBox38.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[2, 8] == "F")
            {
                pictureBox39.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[2, 9] == "F")
            {
                pictureBox40.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[2, 10] == "F")
            {
                pictureBox41.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[2, 11] == "F")
            {
                pictureBox42.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[2, 12] == "F")
            {
                pictureBox43.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[2, 13] == "F")
            {
                pictureBox44.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[2, 14] == "F")
            {
                pictureBox45.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[3, 0] == "F")
            {
                pictureBox46.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[3, 1] == "F")
            {
                pictureBox47.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[3, 2] == "F")
            {
                pictureBox48.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[3, 3] == "F")
            {
                pictureBox49.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[3, 4] == "F")
            {
                pictureBox50.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[3, 5] == "F")
            {
                pictureBox51.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[3, 6] == "F")
            {
                pictureBox52.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[3, 7] == "F")
            {
                pictureBox53.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[3, 8] == "F")
            {
                pictureBox54.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[3, 9] == "F")
            {
                pictureBox55.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[3, 10] == "F")
            {
                pictureBox56.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[3, 11] == "F")
            {
                pictureBox57.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[3, 12] == "F")
            {
                pictureBox58.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[3, 13] == "F")
            {
                pictureBox59.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[3, 14] == "F")
            {
                pictureBox60.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[4, 0] == "F")
            {
                pictureBox61.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[4, 1] == "F")
            {
                pictureBox62.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[4, 2] == "F")
            {
                pictureBox63.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[4, 3] == "F")
            {
                pictureBox64.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[4, 4] == "F")
            {
                pictureBox65.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[4, 5] == "F")
            {
                pictureBox66.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[4, 6] == "F")
            {
                pictureBox67.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[4, 7] == "F")
            {
                pictureBox68.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[4, 8] == "F")
            {
                pictureBox69.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[4, 9] == "F")
            {
                pictureBox70.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[4, 10] == "F")
            {
                pictureBox71.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[4, 11] == "F")
            {
                pictureBox72.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[4, 12] == "F")
            {
                pictureBox73.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[4, 13] == "F")
            {
                pictureBox74.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[4, 14] == "F")
            {
                pictureBox75.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[5, 0] == "F")
            {
                pictureBox76.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[5, 1] == "F")
            {
                pictureBox77.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[5, 2] == "F")
            {
                pictureBox78.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[5, 3] == "F")
            {
                pictureBox79.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[5, 4] == "F")
            {
                pictureBox80.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[5, 5] == "F")
            {
                pictureBox81.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[5, 6] == "F")
            {
                pictureBox82.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[5, 7] == "F")
            {
                pictureBox83.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[5, 8] == "F")
            {
                pictureBox84.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[5, 9] == "F")
            {
                pictureBox85.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[5, 10] == "F")
            {
                pictureBox86.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[5, 11] == "F")
            {
                pictureBox87.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[5, 12] == "F")
            {
                pictureBox88.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[5, 13] == "F")
            {
                pictureBox89.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[5, 14] == "F")
            {
                pictureBox90.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[6, 0] == "F")
            {
                pictureBox91.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[6, 1] == "F")
            {
                pictureBox92.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[6, 2] == "F")
            {
                pictureBox93.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[6, 3] == "F")
            {
                pictureBox94.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[6, 4] == "F")
            {
                pictureBox95.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[6, 5] == "F")
            {
                pictureBox96.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[6, 6] == "F")
            {
                pictureBox97.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[6, 7] == "F")
            {
                pictureBox98.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[6, 8] == "F")
            {
                pictureBox99.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[6, 9] == "F")
            {
                pictureBox100.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[6, 10] == "F")
            {
                pictureBox101.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[6, 11] == "F")
            {
                pictureBox102.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[6, 12] == "F")
            {
                pictureBox103.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[6, 13] == "F")
            {
                pictureBox104.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[6, 14] == "F")
            {
                pictureBox105.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[7, 0] == "F")
            {
                pictureBox106.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[7, 1] == "F")
            {
                pictureBox107.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[7, 2] == "F")
            {
                pictureBox108.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[7, 3] == "F")
            {
                pictureBox109.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[7, 4] == "F")
            {
                pictureBox110.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[7, 5] == "F")
            {
                pictureBox111.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[7, 6] == "F")
            {
                pictureBox112.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[7, 7] == "F")
            {
                pictureBox113.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[7, 8] == "F")
            {
                pictureBox114.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[7, 9] == "F")
            {
                pictureBox115.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[7, 10] == "F")
            {
                pictureBox116.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[7, 11] == "F")
            {
                pictureBox117.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[7, 12] == "F")
            {
                pictureBox118.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[7, 13] == "F")
            {
                pictureBox119.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[7, 14] == "F")
            {
                pictureBox120.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[8, 0] == "F")
            {
                pictureBox121.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[8, 1] == "F")
            {
                pictureBox122.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[8, 2] == "F")
            {
                pictureBox123.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[8, 3] == "F")
            {
                pictureBox124.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[8, 4] == "F")
            {
                pictureBox125.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[8, 5] == "F")
            {
                pictureBox126.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[8, 6] == "F")
            {
                pictureBox127.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[8, 7] == "F")
            {
                pictureBox128.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[8, 8] == "F")
            {
                pictureBox129.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[8, 9] == "F")
            {
                pictureBox130.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[8, 10] == "F")
            {
                pictureBox131.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[8, 11] == "F")
            {
                pictureBox132.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[8, 12] == "F")
            {
                pictureBox133.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[8, 13] == "F")
            {
                pictureBox134.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[8, 14] == "F")
            {
                pictureBox135.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[9, 0] == "F")
            {
                pictureBox136.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[9, 1] == "F")
            {
                pictureBox137.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[9, 2] == "F")
            {
                pictureBox138.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[9, 3] == "F")
            {
                pictureBox139.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[9, 4] == "F")
            {
                pictureBox140.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[9, 5] == "F")
            {
                pictureBox141.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[9, 6] == "F")
            {
                pictureBox142.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[9, 7] == "F")
            {
                pictureBox143.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[9, 8] == "F")
            {
                pictureBox144.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[9, 9] == "F")
            {
                pictureBox145.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[9, 10] == "F")
            {
                pictureBox146.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[9, 11] == "F")
            {
                pictureBox147.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[9, 12] == "F")
            {
                pictureBox148.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[9, 13] == "F")
            {
                pictureBox149.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[9, 14] == "F")
            {
                pictureBox150.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[10, 0] == "F")
            {
                pictureBox151.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[10, 1] == "F")
            {
                pictureBox152.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[10, 2] == "F")
            {
                pictureBox153.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[10, 3] == "F")
            {
                pictureBox154.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[10, 4] == "F")
            {
                pictureBox155.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[10, 5] == "F")
            {
                pictureBox156.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[10, 6] == "F")
            {
                pictureBox157.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[10, 7] == "F")
            {
                pictureBox158.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[10, 8] == "F")
            {
                pictureBox159.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[10, 9] == "F")
            {
                pictureBox160.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[10, 10] == "F")
            {
                pictureBox161.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[10, 11] == "F")
            {
                pictureBox162.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[10, 12] == "F")
            {
                pictureBox163.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[10, 13] == "F")
            {
                pictureBox164.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[10, 14] == "F")
            {
                pictureBox165.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[11, 0] == "F")
            {
                pictureBox166.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[11, 1] == "F")
            {
                pictureBox167.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[11, 2] == "F")
            {
                pictureBox168.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[11, 3] == "F")
            {
                pictureBox169.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[11, 4] == "F")
            {
                pictureBox170.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[11, 5] == "F")
            {
                pictureBox171.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[11, 6] == "F")
            {
                pictureBox172.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[11, 7] == "F")
            {
                pictureBox173.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[11, 8] == "F")
            {
                pictureBox174.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[11, 9] == "F")
            {
                pictureBox175.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[11, 10] == "F")
            {
                pictureBox176.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[11, 11] == "F")
            {
                pictureBox177.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[11, 12] == "F")
            {
                pictureBox178.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[11, 13] == "F")
            {
                pictureBox179.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[11, 14] == "F")
            {
                pictureBox180.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[12, 0] == "F")
            {
                pictureBox181.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[12, 1] == "F")
            {
                pictureBox182.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[12, 2] == "F")
            {
                pictureBox183.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[12, 3] == "F")
            {
                pictureBox184.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[12, 4] == "F")
            {
                pictureBox185.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[12, 5] == "F")
            {
                pictureBox186.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[12, 6] == "F")
            {
                pictureBox187.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[12, 7] == "F")
            {
                pictureBox188.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[12, 8] == "F")
            {
                pictureBox189.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[12, 9] == "F")
            {
                pictureBox190.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[12, 10] == "F")
            {
                pictureBox191.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[12, 11] == "F")
            {
                pictureBox192.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[12, 12] == "F")
            {
                pictureBox193.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[12, 13] == "F")
            {
                pictureBox194.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[12, 14] == "F")
            {
                pictureBox195.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[13, 0] == "F")
            {
                pictureBox196.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[13, 1] == "F")
            {
                pictureBox197.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[13, 2] == "F")
            {
                pictureBox198.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[13, 3] == "F")
            {
                pictureBox199.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[13, 4] == "F")
            {
                pictureBox200.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[13, 5] == "F")
            {
                pictureBox201.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[13, 6] == "F")
            {
                pictureBox202.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[13, 7] == "F")
            {
                pictureBox203.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[13, 8] == "F")
            {
                pictureBox204.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[13, 9] == "F")
            {
                pictureBox205.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[13, 10] == "F")
            {
                pictureBox206.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[13, 11] == "F")
            {
                pictureBox207.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[13, 12] == "F")
            {
                pictureBox208.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[13, 13] == "F")
            {
                pictureBox209.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[13, 14] == "F")
            {
                pictureBox210.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[14, 0] == "F")
            {
                pictureBox211.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[14, 1] == "F")
            {
                pictureBox212.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[14, 2] == "F")
            {
                pictureBox213.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[14, 3] == "F")
            {
                pictureBox214.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[14, 4] == "F")
            {
                pictureBox215.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[14, 5] == "F")
            {
                pictureBox216.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[14, 6] == "F")
            {
                pictureBox217.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[14, 7] == "F")
            {
                pictureBox218.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[14, 8] == "F")
            {
                pictureBox219.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[14, 9] == "F")
            {
                pictureBox220.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[14, 10] == "F")
            {
                pictureBox221.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[14, 11] == "F")
            {
                pictureBox222.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[14, 12] == "F")
            {
                pictureBox223.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[14, 13] == "F")
            {
                pictureBox224.Image = Image.FromFile("Pasado.png");
            }
            if (Matriz[14, 14] == "F")
            {
                pictureBox225.Image = Image.FromFile("Pasado.png");
            }

        }

        private void ReiniciarSilmulacion(object sender, EventArgs e)
        {

            Matriz = new string[15, 15];
            PosX = 0;
            PosY = 0;
            Encontrado = false;
            Timer_ir_Arriba.Enabled = false;
            Timer_ir_Abajo.Enabled = false;
            Timer_ir_Izquierda.Enabled = false;
            Timer_ir_Derecha.Enabled = false;
            Pasos = 0;
            LabelPasos.Text = "-";
            LlenarMapa(Ruta);
            button2.Enabled = false;
            button1.Enabled = true;
            HOUSTON_TENEMOS_UN_PROBLEMA.Visible = false;
            MisionFallida.Visible = false;
            LabelNombreDeLaNave.Text = "";
            textBoxApellido.Text = "";
            textBoxNombre.Text = "";
            textBoxNombre.Focus();
            textBoxApellido.Enabled = true;
            textBoxNombre.Enabled = true;
            labelInstrucciones.Text = "-";
            Instrucciones =0;
            //button4.Enabled = false;
            labelPuntos.Text = "-";
            Puntos = 0;
            label9.Visible = false;
            label8.Visible = false;
        }

        private void IniciarSimulacion(object sender, EventArgs e)
        {
            if (textBoxNombre.Text !="" && textBoxApellido.Text != "")
            {
                textBoxApellido.Enabled = false;
                textBoxNombre.Enabled = false;
                NombreDeLaNave(textBoxNombre.Text, textBoxApellido.Text);
                button1.Enabled = false;
                button2.Enabled = false;
                SimulacionIniciada =true;
                LlenarMapa(Ruta);
            }
            else
            {
                //si no llena ninguno de los datos, se enfoca en el textbox indicando que debe llenarlos
                textBoxNombre.Focus();
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            //cerramos el formulario actual
            this.Hide();
            //abrimos el de inicio
            var frm = new FormInicio();
            frm.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var frm = new FormComandos();
            frm.Show();
        }

      
        private void label6_Click(object sender, EventArgs e)
        {
            var frm = new FormComandos();
            frm.Show();
        }

    }
}
