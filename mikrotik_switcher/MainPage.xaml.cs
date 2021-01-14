using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tik4net;
using tik4net.Api;
using tik4net.Objects;
using tik4net.Objects.Interface;
using tik4net.Objects.Ip;
using tik4net.Objects.Ip.Dns;
using tik4net.Objects.Ip.Firewall;
using tik4net.Objects.Queue;
using tik4net.Objects.System;
using Xamarin.Forms;

namespace mikrotik_switcher
{
    public class EntNumCheck
    {
        public static bool EntryNumCheck(string str)
        {
            List<bool> entNumChecks = str.Select(numCh => Char.IsNumber(str, str.Length - 1)).ToList();
            return entNumChecks.Min();
        }
    }

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            ConnectionStatus.Text = "";
            buttonOn.IsVisible = false;
            buttonOff.IsVisible = false;
            ToOptionsPage.IsVisible = false;
        }

        void EntryIP1(System.Object sender, System.EventArgs e)
        {
            if (IP_adress_1.Text.Length > 0)
            {
                if (IP_adress_1.Text.EndsWith("."))
                {
                    IP_adress_2.CursorPosition = 1;
                    char[] MyChar = { '.' };
                    IP_adress_1.Text = IP_adress_1.Text.TrimEnd(MyChar);
                }
                if (!EntNumCheck.EntryNumCheck(IP_adress_1.Text) && !IP_adress_1.Text.EndsWith("."))
                {
                    IP_adress_1.Text = "";
                    IP_adress_1.CursorPosition = 1;
                }
            }
            try
            {   
                if (IP_adress_1.Text.Length > 2)
                {
                    IP_adress_2.CursorPosition = 1;
                    if (Int32.Parse(IP_adress_1.Text) > 255)
                    {
                        IP_adress_1.Text = "";
                        IP_adress_1.CursorPosition = 1;
                    }
                }
            }
            catch
            {
            }
        }

        void EntryIP2(System.Object sender, System.EventArgs e)
        {
            if (IP_adress_2.Text.Length > 0)
            {
                if (IP_adress_2.Text.EndsWith("."))
                {
                    IP_adress_3.CursorPosition = 1;
                    char[] MyChar = { '.' };
                    IP_adress_2.Text = IP_adress_2.Text.TrimEnd(MyChar);
                }
                if (!EntNumCheck.EntryNumCheck(IP_adress_2.Text) && !IP_adress_2.Text.EndsWith("."))
                {
                    IP_adress_2.Text = "";
                    IP_adress_2.CursorPosition = 1;
                }

            }
            try
            {
                if (IP_adress_2.Text.Length > 2)
                {
                    IP_adress_3.CursorPosition = 1;
                    if (Int32.Parse(IP_adress_2.Text) > 255)
                    {
                        IP_adress_2.Text = "";
                        IP_adress_2.CursorPosition = 1;
                    }
                }
            }
            catch
            {
            }
        }

        void EntryIP3(System.Object sender, System.EventArgs e)
        {
            if (IP_adress_3.Text.Length > 0)
            {
                if (IP_adress_3.Text.EndsWith("."))
                {
                    IP_adress_4.CursorPosition = 1;
                    char[] MyChar = { '.' };
                    IP_adress_3.Text = IP_adress_3.Text.TrimEnd(MyChar);
                }
                if (!EntNumCheck.EntryNumCheck(IP_adress_3.Text) && !IP_adress_3.Text.EndsWith("."))
                {
                    IP_adress_3.Text = "";
                    IP_adress_3.CursorPosition = 1;
                }
            }
            try
            {
                if (IP_adress_3.Text.Length > 2)
                {
                    IP_adress_4.CursorPosition = 1;
                    if (Int32.Parse(IP_adress_3.Text) > 255)
                    {
                        IP_adress_3.Text = "";
                        IP_adress_3.CursorPosition = 1;
                    }
                }
            }
            catch
            {
            }
        }

        void EntryIP4(System.Object sender, System.EventArgs e)
        {
            if (IP_adress_4.Text.Length > 0)
            {
                if (IP_adress_4.Text.EndsWith("."))
                {
                    Login.CursorPosition = 1;
                    char[] MyChar = { '.' };
                    IP_adress_4.Text = IP_adress_4.Text.TrimEnd(MyChar);
                }
                if (!EntNumCheck.EntryNumCheck(IP_adress_4.Text) && !IP_adress_4.Text.EndsWith("."))
                {
                    IP_adress_4.Text = "";
                    IP_adress_4.CursorPosition = 1;
                }
            }

            try
            {
                if (IP_adress_4.Text.Length > 2)
                {
                    Login.CursorPosition = 1;
                    if (Int32.Parse(IP_adress_4.Text) > 255)
                    {
                        IP_adress_4.Text = "";
                        IP_adress_4.CursorPosition = 1;
                    }
                }
            }
            catch
            {
            }
        }

        private string ipAdress()
        {
            string ip_adress_1 = IP_adress_1.Text;
            string ip_adress_2 = IP_adress_2.Text;
            string ip_adress_3 = IP_adress_3.Text;
            string ip_adress_4 = IP_adress_4.Text;
            string ip_adress = ip_adress_1 + "." + ip_adress_2 + "." + ip_adress_3 + "." + ip_adress_4;
            return ip_adress;
        }

        Dictionary<int, string> ports = new Dictionary<int, string>();

        void ButtonClickedConnect(System.Object sender, System.EventArgs e)
        {
            try
            {
                using (ITikConnection connection = ConnectionFactory.CreateConnection(TikConnectionType.Api))
                {
                    connection.Open(ipAdress(), Login.Text, Password.Text);
                    ITikCommand cmd = connection.CreateCommand("/system/identity/print");
                    Console.WriteLine(cmd.ExecuteScalar());
                    if (connection.IsOpened)
                    {
                        ConnectionStatus.Text = "OK";
                        buttonOn.IsVisible = true;
                        buttonOff.IsVisible = true;
                        ToOptionsPage.IsVisible = true;
                        buttonClear.Text = "CANCEL";
                        IP_adress_1.IsEnabled = false;
                        IP_adress_2.IsEnabled = false;
                        IP_adress_3.IsEnabled = false;
                        IP_adress_4.IsEnabled = false;
                        Login.IsEnabled = false;
                        Password.IsEnabled = false;
                        buttonConnect.IsEnabled = false;
                        var poe_status = connection.LoadList<InterfaceEthernet>();
                        int portNum = 0;
                        foreach (InterfaceEthernet interfaceEthernet in poe_status)
                        {
                            ports.Add(portNum, interfaceEthernet.Name);
                            portNum++;
                        }
                        //Console.WriteLine("Имя порта ", ports[0]); // имя порта
                        Console.WriteLine(ports.Keys.Count); // количество портов
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Exception: " + ex.Message);
                ConnectionStatus.Text = ex.Message;
            }
        }

        void ButtonClickedClear(System.Object sender, System.EventArgs e)
        {
            IP_adress_1.Text = "";
            IP_adress_2.Text = "";
            IP_adress_3.Text = "";
            IP_adress_4.Text = "";
            Login.Text = "";
            Password.Text = "";
            IP_adress_1.CursorPosition = 1;
            IP_adress_1.IsEnabled = true;
            IP_adress_2.IsEnabled = true;
            IP_adress_3.IsEnabled = true;
            IP_adress_4.IsEnabled = true;
            Login.IsEnabled = true;
            Password.IsEnabled = true;
            buttonConnect.IsEnabled = true;
            buttonOn.IsVisible = false;
            buttonOff.IsVisible = false;
            ToOptionsPage.IsVisible = false;
            buttonClear.Text = "CLEAR";
            ConnectionStatus.Text = "";
            ports.Clear();
        }

        void clickedOn(System.Object sender, System.EventArgs e)
        {
            try
            {
                using (ITikConnection connection = ConnectionFactory.CreateConnection(TikConnectionType.Api))
                {
                    connection.Open(ipAdress(), Login.Text, Password.Text);
                    ITikCommand cmd = connection.CreateCommand("/system/identity/print");
                    Console.WriteLine(cmd.ExecuteScalar());
                    var poe = connection.CreateCommand("/interface/ethernet/set");

                    foreach (string i in OptionsPage.numbers)
                    {
                        //Console.WriteLine("i: " + i);
                        int numPort = Int32.Parse(i);
                        poe.AddParameter(TikSpecialProperties.Id, ports[numPort - 1]);
                        poe.AddParameter("poe-out", "auto-on");
                        poe.AddParameter("disabled", "no");
                        //Console.WriteLine("ports: " + ports[numPort]);
                        poe.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
            }
        }

        void clickedOff(System.Object sender, System.EventArgs e)
        {
            try
            {
                using (ITikConnection connection = ConnectionFactory.CreateConnection(TikConnectionType.Api))
                {
                    connection.Open(ipAdress(), Login.Text, Password.Text);
                    ITikCommand cmd = connection.CreateCommand("/system/identity/print");
                    Console.WriteLine(cmd.ExecuteScalar());
                    var poe = connection.CreateCommand("/interface/ethernet/set");

                    foreach (string i in OptionsPage.numbers)
                    {
                        int numPort = Int32.Parse(i);
                        poe.AddParameter(TikSpecialProperties.Id, ports[numPort - 1]);
                        poe.AddParameter("poe-out", "off");
                        poe.AddParameter("disabled", "yes");
                        //Console.WriteLine("ports: " + ports[numPort]);
                        poe.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
            }
        }

        async void ToOptionsPageClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OptionsPage());
        }
    }
}

//Отключать порт лучше сначала по PoE, потом логически, включать - наоборот.
//Отключение PoE: /interface ethernet poe set ether1 poe-out off
//Отключение порта: /interface ethernet set ether1 disabled=yes
//Включение порта: /interface ethernet set ether1 disabled=no
//Включение PoE: /interface ethernet poe set ether1 poe-out auto-on