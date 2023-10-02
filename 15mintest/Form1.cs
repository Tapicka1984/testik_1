﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _15mintest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                StreamReader cti = new StreamReader("matematika.txt");
                int vysledek = 0;
                int pocetvysledku = 0;
                int soucet = 0;
                while (!cti.EndOfStream)
                {
                    string radek = cti.ReadLine();
                    listBox1.Items.Add(radek);
                    string[] pole = radek.Split(' ');
                    if (pole[1] == "+")
                    {
                        vysledek = int.Parse(pole[0]) + int.Parse(pole[2]);
                        listBox2.Items.Add(pole[0] + " " + pole[1] + " " + pole[2] + " = " + vysledek);
                        pocetvysledku++;
                    }
                    else if (pole[1] == "-")
                    {
                        vysledek = int.Parse(pole[0]) - int.Parse(pole[2]);
                        listBox2.Items.Add(pole[0] + " " + pole[1] + " " + pole[2] + " = " + vysledek);
                        pocetvysledku++;
                    }
                    else if (pole[1] == "*")
                    {
                        vysledek = int.Parse(pole[0]) * int.Parse(pole[2]);
                        listBox2.Items.Add(pole[0] + " " + pole[1] + " " + pole[2] + " = " + vysledek);
                        pocetvysledku++;
                    }
                    else if (pole[1] == "/")
                    {
                        if (int.Parse(pole[2]) != 0)
                        {
                            checked { vysledek = int.Parse(pole[0]) / int.Parse(pole[2]); }
                            listBox2.Items.Add(pole[0] + " " + pole[1] + " " + pole[2] + " = " + vysledek);
                            pocetvysledku++;
                        }
                    }
                    soucet = soucet + vysledek;
                }
                cti.Close();

                StreamWriter pis = new StreamWriter("matematika.txt", false);
                foreach (string p in listBox2.Items)
                {
                    pis.WriteLine(p);
                }
                pis.Close();

                int vysledek2 = soucet / pocetvysledku;
                using (BinaryWriter zapisovacbinarni = new BinaryWriter(File.Open("prumer.dat", FileMode.OpenOrCreate), Encoding.UTF8))
                {
                    zapisovacbinarni.Write(vysledek2);
                }
                using (BinaryReader ctenardat = new BinaryReader(File.Open("prumer.dat", FileMode.Open), Encoding.UTF8))
                {
                    MessageBox.Show(ctenardat.Read() + "");
                }
            }
            catch (DivideByZeroException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message);

            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
    }
}
