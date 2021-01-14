using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using Xamarin.Forms;

namespace mikrotik_switcher
{
    public partial class OptionsPage : ContentPage
    {
        public static string[] numbers;

        public static string ports;

        public OptionsPage()
        {
            InitializeComponent();
            InputPorts.Text = ports;
        }

        void InputPortsTextChanged(System.Object sender, System.EventArgs e)
        {
            // Проверка, если ввод больше 3-х чисел

            Regex regnum3 = new Regex(@"([0-9]{3}\Z)");
            if (regnum3.IsMatch(InputPorts.Text))
            {
                InputPorts.Text = InputPorts.Text.Remove(InputPorts.Text.Length - 1);
            };


            // Проверка, если пустой ввод

            if (InputPorts.Text.Length == 0)
            {
                Array.Resize(ref numbers, 0);
            };

            // Проверка если ввод больше 48

            Regex regnum48 = new Regex(@"([0-9]{2}\Z)");
            if (regnum48.IsMatch(InputPorts.Text))
            {
                int PortNo = Int32.Parse(InputPorts.Text.Substring(InputPorts.Text.Length - 2, 2));
                if (PortNo > 48)
                {
                    InputPorts.Text = InputPorts.Text.Remove(InputPorts.Text.Length - 2);
                }
            }

            // Проверка  на ноль

            Regex regzero = new Regex(@"(\A01?)");

            if (regzero.IsMatch(InputPorts.Text) || InputPorts.Text.EndsWith(",0"))
            {
                InputPorts.Text = InputPorts.Text.Remove(InputPorts.Text.Length - 1);
            }

            // Проверка - первые символы должны быть числа

            if (InputPorts.Text.Length == 1)
            {
                if (!EntNumCheck.EntryNumCheck(InputPorts.Text))
                {
                    InputPorts.Text = InputPorts.Text.Remove(InputPorts.Text.Length - 1);

                }
                numbers = InputPorts.Text.Split(new Char[] { ' ', ',', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            }

            // Проверка - символы больше 2-х должны быть числа, запятые и тире

            else if (InputPorts.Text.Length >= 2)
            {
                if (!EntNumCheck.EntryNumCheck(InputPorts.Text) && (!(InputPorts.Text.EndsWith(",") || InputPorts.Text.EndsWith("-"))))
                {
                    InputPorts.Text = InputPorts.Text.Remove(InputPorts.Text.Length - 1);
                }

                // Проверка на случайные повторения типа ",," ",-" "-," "--"

                if (InputPorts.Text.EndsWith(",,") || InputPorts.Text.EndsWith(",-") || InputPorts.Text.EndsWith("-,") || InputPorts.Text.EndsWith("--"))
                {
                    InputPorts.Text = InputPorts.Text.Remove(InputPorts.Text.Length - 1);
                }

                // Проверка на двойное тире, типа "x-x-", где х - число

                Regex regdoubleminus = new Regex(@"([0-9]+-[0-9]+-\Z)");

                if (regdoubleminus.IsMatch(InputPorts.Text))
                {
                    InputPorts.Text = InputPorts.Text.Remove(InputPorts.Text.Length - 1);
                }

                // Расчет диапазона портов

                Regex regrange = new Regex(@"([0-9]+-[0-9]+,\Z)");
                if (regrange.IsMatch(InputPorts.Text))
                {
                    string[] rports = InputPorts.Text.Split(new Char[] { ',', '-' }, StringSplitOptions.RemoveEmptyEntries);
                    int startrport = Int32.Parse((rports[rports.Length - 2]));
                    int finishrport = Int32.Parse((rports[rports.Length - 1]));
                    string str = "";
                    for (int i = startrport; i <= finishrport; i++)
                    {
                        str += i + ",";
                    }
                    InputPorts.Text = InputPorts.Text.Remove(InputPorts.Text.Length - (Convert.ToString(startrport).Length + Convert.ToString(finishrport).Length + 2)) + str;
                }

                numbers = InputPorts.Text.Split(new Char[] { ' ', ',', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        void Clear(System.Object sender, System.EventArgs e)
        {
            InputPorts.Text = "";
            Array.Resize(ref numbers, 0); // очистка массива
        }

        async void ToMainPageClick(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            Regex reg = new Regex(@"(-[0-9]+\Z)");
            if (reg.IsMatch(InputPorts.Text))
            {
                InputPorts.Text += ",";
            };
            ports = InputPorts.Text;
        }
    }
}