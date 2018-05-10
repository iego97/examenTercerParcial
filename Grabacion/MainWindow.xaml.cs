using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NAudio;
using NAudio.Wave;
using NAudio.Dsp;
using System.Diagnostics;
using System.Threading;


namespace Grabacion
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WaveIn wavein;
        WaveFormat formato;
        WaveFileWriter writer;
        AudioFileReader reader;
        WaveOutEvent waveOut;

        Stopwatch stopwatch;
        double segundos;

        public MainWindow()
        {
            InitializeComponent();
           stopwatch = new Stopwatch();
        }

        private void btniniciar_Click(object sender, RoutedEventArgs e)
        {
            
            wavein = new WaveIn();
            wavein.WaveFormat = new WaveFormat(44100, 16, 1);
            formato = wavein.WaveFormat;

            wavein.DataAvailable += OnDaraAvailable;
            wavein.RecordingStopped += OnRectodingStopped;
            writer =
                new WaveFileWriter("sonido.wav", formato);

            wavein.StartRecording();
        }

        void OnRectodingStopped(object sender, StoppedEventArgs e)
        {
            writer.Dispose();
        }

        void OnDaraAvailable(object sender, WaveInEventArgs e)
        {
            byte[] buffer = e.Buffer;
            int bytesGrabados = e.BytesRecorded;

            double acumulador = 0;
            double nummuestras = bytesGrabados / 2;
            int exponente = 1;
            int numeroMuestrasComplejas = 0;
            int bitsMaximos = 0;
            do //1,200
            {
                bitsMaximos = (int)Math.Pow(2, exponente);
                exponente++;

            } while (bitsMaximos < nummuestras);

            //bitsMaximos = 2048
            //exponente = 12

            //numeroMuestrasComplejas = 1024
            //exponente = 10

            exponente -= 2;
            numeroMuestrasComplejas = bitsMaximos / 2;

            Complex[] muestrasComplejas =
                new Complex[numeroMuestrasComplejas];

            for (int i = 0; i < bytesGrabados; i += 2)
            {
                short muestra = (short)(buffer[i + 1] << 8 | buffer[i]);
                //lblmuestras.TextInput = muestra.ToString();
                //slbvolumen.Value = muestra;

                float muestra32bits = (float)muestra / 32768.0f;
                slbvolumen.Value = Math.Abs(muestra32bits);
                if (i / 2 < numeroMuestrasComplejas)
                {
                    muestrasComplejas[i / 2].X = muestra32bits;
                }



                /*acumulador += muestra;
                nummuestras++;*/
            }
            // double promedio = acumulador / nummuestras;
            // slbvolumen.Value = promedio;
            //writer.Write(buffer, 0, bytesGrabados);
            FastFourierTransform.FFT(true, exponente, muestrasComplejas);
            float[] valoresAbsolutos =
                new float[muestrasComplejas.Length];

            for (int i = 0; i < muestrasComplejas.Length; i++)
            {
                valoresAbsolutos[i] = (float)
                Math.Sqrt((muestrasComplejas[i].X * muestrasComplejas[i].X) +
                (muestrasComplejas[i].Y * muestrasComplejas[i].Y));
            }
            int indiceMaximo =
                valoresAbsolutos.ToList().IndexOf(
                    valoresAbsolutos.Max());
            float frecuenciaFundamental =
                (float)(indiceMaximo * wavein.WaveFormat.SampleRate) / 
                (float) valoresAbsolutos.Length;


            lblfrecuencia.Text = frecuenciaFundamental.ToString();


            stopwatch.Start();

            if (frecuenciaFundamental > 80 && frecuenciaFundamental < 120)
            {

                if (stopwatch.Elapsed.Seconds >= 3)
                {
                    stopwatch.Stop();
                }


                segundos = stopwatch.Elapsed.TotalSeconds;


                if (segundos > 3 && segundos < 4)
                {
                    txtEntrada.Text += 'A';
                    stopwatch.Restart();
                }
            }

            if (frecuenciaFundamental > 180 && frecuenciaFundamental < 220)
            {

                if (stopwatch.Elapsed.Seconds >= 3)
                {
                    stopwatch.Stop();
                }


                segundos = stopwatch.Elapsed.TotalSeconds;


                if (segundos > 3 && segundos < 4)
                {
                    txtEntrada.Text += 'B';
                    stopwatch.Restart();
                }
            }

            if (frecuenciaFundamental > 280 && frecuenciaFundamental < 320)
            {

                if (stopwatch.Elapsed.Seconds >= 3)
                {
                    stopwatch.Stop();
                }


                segundos = stopwatch.Elapsed.TotalSeconds;


                if (segundos > 3 && segundos < 4)
                {
                    txtEntrada.Text += 'C';
                    stopwatch.Restart();
                }
            }

            if (frecuenciaFundamental > 380 && frecuenciaFundamental < 420)
            {

                if (stopwatch.Elapsed.Seconds >= 3)
                {
                    stopwatch.Stop();
                }


                segundos = stopwatch.Elapsed.TotalSeconds;


                if (segundos > 3 && segundos < 4)
                {
                    txtEntrada.Text += 'D';
                    stopwatch.Restart();
                }
            }

            if (frecuenciaFundamental > 480 && frecuenciaFundamental < 520)
            {

                if (stopwatch.Elapsed.Seconds >= 3)
                {
                    stopwatch.Stop();
                }


                segundos = stopwatch.Elapsed.TotalSeconds;


                if (segundos > 3 && segundos < 4)
                {
                    txtEntrada.Text += 'E';
                    stopwatch.Restart();
                }
            }

            if (frecuenciaFundamental > 580 && frecuenciaFundamental < 620)
            {

                if (stopwatch.Elapsed.Seconds >= 3)
                {
                    stopwatch.Stop();
                }


                segundos = stopwatch.Elapsed.TotalSeconds;


                if (segundos > 3 && segundos < 4)
                {
                    txtEntrada.Text += 'F';
                    stopwatch.Restart();
                }
            }

            if (frecuenciaFundamental > 680 && frecuenciaFundamental < 720)
            {

                if (stopwatch.Elapsed.Seconds >= 3)
                {
                    stopwatch.Stop();
                }


                segundos = stopwatch.Elapsed.TotalSeconds;


                if (segundos > 3 && segundos < 4)
                {
                    txtEntrada.Text += 'G';
                    stopwatch.Restart();
                }
            }

            if (frecuenciaFundamental > 780 && frecuenciaFundamental < 820)
            {

                if (stopwatch.Elapsed.Seconds >= 3)
                {
                    stopwatch.Stop();
                }


                segundos = stopwatch.Elapsed.TotalSeconds;


                if (segundos > 3 && segundos < 4)
                {
                    txtEntrada.Text += 'H';
                    stopwatch.Restart();
                }
            }

            if (frecuenciaFundamental>880 && frecuenciaFundamental< 920)
            {
                
                if(stopwatch.Elapsed.Seconds >= 3)
                {
                    stopwatch.Stop();
                }


                segundos = stopwatch.Elapsed.TotalSeconds;


                if (segundos > 3 && segundos < 4)
                {
                    txtEntrada.Text += 'I';
                    stopwatch.Restart();
                }
            }

            if (frecuenciaFundamental > 980 && frecuenciaFundamental < 1020)
            {

                if (stopwatch.Elapsed.Seconds >= 3)
                {
                    stopwatch.Stop();
                }


                segundos = stopwatch.Elapsed.TotalSeconds;


                if (segundos > 3 && segundos < 4)
                {
                    txtEntrada.Text += 'J';
                    stopwatch.Restart();
                }
            }

            if (frecuenciaFundamental > 1080 && frecuenciaFundamental < 1120)
            {

                if (stopwatch.Elapsed.Seconds >= 3)
                {
                    stopwatch.Stop();
                }


                segundos = stopwatch.Elapsed.TotalSeconds;


                if (segundos > 3 && segundos < 4)
                {
                    txtEntrada.Text += 'K';
                    stopwatch.Restart();
                }
            }

            if (frecuenciaFundamental > 1180 && frecuenciaFundamental < 1220)
            {

                if (stopwatch.Elapsed.Seconds >= 3)
                {
                    stopwatch.Stop();
                }


                segundos = stopwatch.Elapsed.TotalSeconds;


                if (segundos > 3 && segundos < 4)
                {
                    txtEntrada.Text += 'L';
                    stopwatch.Restart();
                }
            }

            if (frecuenciaFundamental > 1280 && frecuenciaFundamental < 1320)
            {

                if (stopwatch.Elapsed.Seconds >= 3)
                {
                    stopwatch.Stop();
                }


                segundos = stopwatch.Elapsed.TotalSeconds;


                if (segundos > 3 && segundos < 4)
                {
                    txtEntrada.Text += 'M';
                    stopwatch.Restart();
                }
            }

            if (frecuenciaFundamental > 1380 && frecuenciaFundamental < 1420)
            {

                if (stopwatch.Elapsed.Seconds >= 3)
                {
                    stopwatch.Stop();
                }


                segundos = stopwatch.Elapsed.TotalSeconds;


                if (segundos > 3 && segundos < 4)
                {
                    txtEntrada.Text += 'N';
                    stopwatch.Restart();
                }
            }

            if (frecuenciaFundamental > 1480 && frecuenciaFundamental < 1520)
            {

                if (stopwatch.Elapsed.Seconds >= 3)
                {
                    stopwatch.Stop();
                }


                segundos = stopwatch.Elapsed.TotalSeconds;


                if (segundos > 3 && segundos < 4)
                {
                    txtEntrada.Text += 'O';
                    stopwatch.Restart();
                }
            }

            if (frecuenciaFundamental > 1580 && frecuenciaFundamental < 1620)
            {

                if (stopwatch.Elapsed.Seconds >= 3)
                {
                    stopwatch.Stop();
                }


                segundos = stopwatch.Elapsed.TotalSeconds;


                if (segundos > 3 && segundos < 4)
                {
                    txtEntrada.Text += 'P';
                    stopwatch.Restart();
                }
            }

            if (frecuenciaFundamental > 1680 && frecuenciaFundamental < 1720)
            {

                if (stopwatch.Elapsed.Seconds >= 3)
                {
                    stopwatch.Stop();
                }


                segundos = stopwatch.Elapsed.TotalSeconds;


                if (segundos > 3 && segundos < 4)
                {
                    txtEntrada.Text += 'Q';
                    stopwatch.Restart();
                }
            }

            if (frecuenciaFundamental > 1780 && frecuenciaFundamental < 1820)
            {

                if (stopwatch.Elapsed.Seconds >= 3)
                {
                    stopwatch.Stop();
                }


                segundos = stopwatch.Elapsed.TotalSeconds;


                if (segundos > 3 && segundos < 4)
                {
                    txtEntrada.Text += 'R';
                    stopwatch.Restart();
                }
            }

            if (frecuenciaFundamental > 1880 && frecuenciaFundamental < 1920)
            {

                if (stopwatch.Elapsed.Seconds >= 3)
                {
                    stopwatch.Stop();
                }


                segundos = stopwatch.Elapsed.TotalSeconds;


                if (segundos > 3 && segundos < 4)
                {
                    txtEntrada.Text += 'S';
                    stopwatch.Restart();
                }
            }

            if (frecuenciaFundamental > 1980 && frecuenciaFundamental < 2020)
            {

                if (stopwatch.Elapsed.Seconds >= 3)
                {
                    stopwatch.Stop();
                }


                segundos = stopwatch.Elapsed.TotalSeconds;


                if (segundos > 3 && segundos < 4)
                {
                    txtEntrada.Text += 'T';
                    stopwatch.Restart();
                }
            }

            if (frecuenciaFundamental > 2080 && frecuenciaFundamental < 2120)
            {

                if (stopwatch.Elapsed.Seconds >= 3)
                {
                    stopwatch.Stop();
                }


                segundos = stopwatch.Elapsed.TotalSeconds;


                if (segundos > 3 && segundos < 4)
                {
                    txtEntrada.Text += 'U';
                    stopwatch.Restart();
                }
            }


            if (frecuenciaFundamental > 2180 && frecuenciaFundamental < 2220)
            {

                if (stopwatch.Elapsed.Seconds >= 3)
                {
                    stopwatch.Stop();
                }


                segundos = stopwatch.Elapsed.TotalSeconds;


                if (segundos > 3 && segundos < 4)
                {
                    txtEntrada.Text += 'V';
                    stopwatch.Restart();
                }
            }


            if (frecuenciaFundamental > 2280 && frecuenciaFundamental < 2320)
            {

                if (stopwatch.Elapsed.Seconds >= 3)
                {
                    stopwatch.Stop();
                }


                segundos = stopwatch.Elapsed.TotalSeconds;


                if (segundos > 3 && segundos < 4)
                {
                    txtEntrada.Text += 'W';
                    stopwatch.Restart();
                }
            }

            if (frecuenciaFundamental > 2380 && frecuenciaFundamental < 2420)
            {

                if (stopwatch.Elapsed.Seconds >= 3)
                {
                    stopwatch.Stop();
                }


                segundos = stopwatch.Elapsed.TotalSeconds;


                if (segundos > 3 && segundos < 4)
                {
                    txtEntrada.Text += 'X';
                    stopwatch.Restart();
                }
            }


            if (frecuenciaFundamental > 2480 && frecuenciaFundamental < 2520)
            {

                if (stopwatch.Elapsed.Seconds >= 3)
                {
                    stopwatch.Stop();
                }


                segundos = stopwatch.Elapsed.TotalSeconds;


                if (segundos > 3 && segundos < 4)
                {
                    txtEntrada.Text += 'Y';
                    stopwatch.Restart();
                }
            }


            if (frecuenciaFundamental > 2580 && frecuenciaFundamental < 2620)
            {

                if (stopwatch.Elapsed.Seconds >= 3)
                {
                    stopwatch.Stop();
                }


                segundos = stopwatch.Elapsed.TotalSeconds;


                if (segundos > 3 && segundos < 4)
                {
                    txtEntrada.Text += 'Z';
                    stopwatch.Restart();
                }
            }


            if (frecuenciaFundamental > 2680 && frecuenciaFundamental < 2720)
            {

                if (stopwatch.Elapsed.Seconds >= 3)
                {
                    stopwatch.Stop();
                }


                segundos = stopwatch.Elapsed.TotalSeconds;


                if (segundos > 3 && segundos < 4)
                {
                    txtEntrada.Text += ' ';
                    stopwatch.Restart();
                }
            }







        }

        private void btnfinalizar_Click(object sender, RoutedEventArgs e)
        {
            wavein.StopRecording();
        }

        private void btnReproducir_Click(object sender, RoutedEventArgs e)
        {
            reader = new AudioFileReader("sonido.wav");
            waveOut = new WaveOutEvent();
            waveOut.Init(reader);
            waveOut.Play();
        }

        private void slbvolumen_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
