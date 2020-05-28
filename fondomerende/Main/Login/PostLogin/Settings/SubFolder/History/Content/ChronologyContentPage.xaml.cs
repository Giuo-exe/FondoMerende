using fondomerende.Main.Services.RESTServices;
using System;
using FormsControls.Base;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using Xamarin.Essentials;
using fondomerende.Main.Utilities;
using fondomerende.Main.Manager;

namespace fondomerende.Main.Login.PostLogin.Settings.SubFolder.History.Content
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChronologyContentPage : AnimationPage
    {
        private double diametro = 40;
        private double larghezzaLinea = 3;
        private double altezzaLinea = 20;

        private double diametroMod;

        Dictionary<string, Color> colorByName = new Dictionary<string, Color>();
        Dictionary<string, double> sizeByName = new Dictionary<string, double>();
        Dictionary<string, string> dateByTime = new Dictionary<string, string>();
        Dictionary<string, string> mangione = new Dictionary<string, string>();
        Dictionary<Color, bool> colorReserved = new Dictionary<Color, bool>();
        Dictionary<string, string> traduttore = new Dictionary<string, string>();
        

        string[] cronologia;
        public ChronologyContentPage()
        {          
            InitializeComponent();

            if (TabletManager.Instance.tablet)
            {
                switch (Device.RuntimePlatform)   //                                              ||\\
                {              //                                                                 || \\                                    
                               //                         ||  \\ Se il dispositivo è Android non mostra la Top Bar della Navigation Page,
                    case Device.Android: //                                             \\        ||   \\   Se è iOS invece si (perchè senza è una schifezza)
                        NavigationPage.SetHasNavigationBar(this, false);//                \\      ||    \\        \                
                        break;     //                                                      ||||||||||||||||\/\/|    |
                                   //      ||    //        /       
                    default:                                                            //        ||   //
                        NavigationPage.SetHasNavigationBar(this, true);//                         ||  //
                        break;  //                                                                || //
                }
            }
            Aspetta();
            
        }



        private void Fusione()
        {
            traduttore.Add("added", "ha aggiunto");
            traduttore.Add("changed", "ha cambiato il nome di");
            traduttore.Add("name", "");
            traduttore.Add("from", "da");
            traduttore.Add("to", "a");
            traduttore.Add("ate", "ha mangiato");
            traduttore.Add("bought", "ha comprato");
            traduttore.Add("deposited", "ha depositato");
            mangione.Add("mangione", "");
            dateByTime.Add("Intervallo", "");

            ColorBack();
            SizeFrame();
            AddMangione();

            for (int i=0;i<cronologia.Length;i++)
            {
                AddAction(i);
                if(i!=cronologia.Length-1)  AddTimeLine(i);

            }
        }
        public void AddMangione()
        {

            var stackPrincipale = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
            };

            var persona = new Label
            {
                Text = "Mangione: " + mangione["mangione"],
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Margin = 5,
                FontSize = 14,
                FontAttributes = FontAttributes.Bold,
                TextColor = (colorByName[mangione["mangione"]]),
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
            };

            stackPrincipale.Children.Add(persona);

            ContentLayout.Children.Add(stackPrincipale);

        }
        private void AddAction(int posizione)
        {
            string dataLabel = "";
            string[] strSplit = cronologia[posizione].Split();
            var fs = new FormattedString();
            

            for (int i = 2; i < strSplit.Length; i++)
            {
                    
                if (i==2)
                {
                    fs.Spans.Add(new Span { Text = strSplit[2], TextColor = colorByName[strSplit[2]] });
                }

                else if(traduttore.ContainsKey(strSplit[i]))
                {
                    fs.Spans.Add(new Span { Text = " " + traduttore[strSplit[i]], TextColor = Color.Black });
                }
                else
                    {
                        fs.Spans.Add(new Span { Text = " " + strSplit[i], TextColor = Color.Black });
                    }          
            }
            

            var stackPrincipale = new StackLayout
            {
                Orientation = StackOrientation.Horizontal
            };


            var cerchio = new RoundedCornerView
            {
                HeightRequest = diametro + ((diametro * sizeByName[strSplit[2]]) * 2),
                WidthRequest = diametro + ((diametro * sizeByName[strSplit[2]]) * 2),
                MinimumHeightRequest = diametro + ((diametro * sizeByName[strSplit[2]]) * 2),
                MinimumWidthRequest = diametro + ((diametro * sizeByName[strSplit[2]]) * 2),
                RoundedCornerRadius = diametro + ((diametro * sizeByName[strSplit[2]]) * 2),
                Margin = new Thickness(3, 0, 0, 0),
                BorderColor = Color.Black,
                BorderWidth = 3,
            };

            var cerchioiOS = new RoundedCornerView
            {
                HeightRequest = diametro + ((diametro * sizeByName[strSplit[2]]) * 2),
                WidthRequest = diametro + ((diametro * sizeByName[strSplit[2]]) * 2),
                MinimumHeightRequest = diametro + ((diametro * sizeByName[strSplit[2]]) * 2),
                MinimumWidthRequest = diametro + ((diametro * sizeByName[strSplit[2]]) * 2),
                RoundedCornerRadius = (diametro + ((diametro * sizeByName[strSplit[2]]) * 2))/2,
                Margin = new Thickness(3, 0, 0, 0),
                BackgroundColor = colorByName[strSplit[2]],
                BorderColor = Color.Black,
                BorderWidth = 1,
            };

            var stackLabel = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = colorByName[strSplit[2]]
            };

            var firstLetter = new Label
            {
                Text = First_letter(strSplit[2]),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 16,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.WhiteSmoke,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
            };

            var textAction = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                FontSize = 12,
                Opacity = 0.6,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
                VerticalTextAlignment = TextAlignment.Center,
            };
            textAction.FormattedText = fs; //per il testo colorato

            diametroMod = diametro + (diametro * sizeByName[strSplit[2]]);

            switch (Device.RuntimePlatform)             //Se il dispositivo è Android non mostra la Top Bar della Navigation Page, se è iOS la mostra
            {
                default:
                    stackLabel.Children.Add(firstLetter);
                    cerchioiOS.Children.Add(stackLabel);
                    stackPrincipale.Children.Add(cerchioiOS);

                    break;
                case Device.Android:
                    stackLabel.Children.Add(firstLetter);
                    cerchio.Children.Add(stackLabel);
                    stackPrincipale.Children.Add(cerchio);
                    break;

            }
            stackPrincipale.Children.Add(textAction);
            ContentLayout.Children.Add(stackPrincipale);
        }


        //probabile problema della cronologia//
        public void AddTimeLine(int posizione)
        {
            string[] strSplit = cronologia[posizione].Split();
            double paddingLinea = (diametroMod + (diametroMod * sizeByName[strSplit[2]]) )/ 2 - larghezzaLinea/2;
            

            var stackPrincipale = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Horizontal,
                Margin = new Thickness(3, 0, 0, 0)
            };

            var linea = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                HeightRequest = altezzaLinea+ (altezzaLinea * LenghtLine(posizione)),
                WidthRequest = larghezzaLinea,
                Margin = new Thickness(paddingLinea, 0, 0, 0),
            };

            var app = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                HeightRequest = altezzaLinea + (altezzaLinea * LenghtLine(posizione)),
                WidthRequest = larghezzaLinea,
                Margin = new Thickness(paddingLinea, 0, 0, 0),
            };

            var orario = new Label
            {
                Text = TestoOrario(posizione),
                VerticalOptions = LayoutOptions.Center,
                FontSize = 12,
                Opacity = 0.6,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
                VerticalTextAlignment = TextAlignment.Center,
            };

            var gradiente = new GradientColorStack
            {
                //E' causato da queste due righe//
                //StartColor = colorByName[strSplit[2]],
                //EndColor = NextColor(posizione),
                BackgroundColor = colorByName[strSplit[2]],
            };

            gradiente.Children.Add(app);
            linea.Children.Add(gradiente);
            stackPrincipale.Children.Add(linea);
            stackPrincipale.Children.Add(orario);


            ContentLayout.Children.Add(stackPrincipale);

        }

        public string First_letter(string app)        //Grafica
        {      
            string firstLetter = "";

            //error control
            app = app.Replace(" ","");
            
            
            firstLetter = (app.Substring(0, 1));
            if (app == "added") firstLetter = "@";

            return firstLetter;
        }

        private Color NextColor(int posizione)           //appoggio
        {
            string[] strString = cronologia[posizione + 1].Split();

            return colorByName[strString[2]];
        }

        private Color LastColor(int posizione)
        {
            if(posizione == 0)
            {

            }
            else
            { 
                string[] strString = cronologia[posizione - 1].Split();
                return colorByName[strString[2]];
            }
            return Color.FromHex("#000001");
        }


        public void ColorBack()
        {
            ColorRandom c = new ColorRandom();

            for (int i = 0; i < cronologia.Length; i++)
            {
                string[] strSplit = cronologia[i].Split();

                if (!colorByName.ContainsKey(strSplit[2]))
                {
                    Color colorapp = c.GetRandomColor();

                    while (colorapp == Color.FromHex(Xamarin.Essentials.Preferences.Get("Colore", "#CCCCCC")) || colorapp == LastColor(i))
                    {
                        colorapp = c.GetRandomColor();
                    }


                    if ((strSplit[2]).Equals(Xamarin.Essentials.Preferences.Get("friendly-name", " ")))
                    {
                        colorapp = Color.FromHex(Xamarin.Essentials.Preferences.Get("Colore", "#CCCCCC"));

                        if (!colorByName.ContainsKey(Xamarin.Essentials.Preferences.Get("friendly-name", " ")))
                        {
                            colorByName.Add(strSplit[2], colorapp);
                        }

                    }

                    if (!colorByName.ContainsKey(strSplit[2]))
                    {
                        colorByName.Add(strSplit[2], colorapp);
                    }
                }
            }
        }

        public void SizeFrame()
        {
            double app = 0;
            string app2 = "";
            for(int i = 0; i < cronologia.Length; i++)
            {
                string[] strSplit = cronologia[i].Split();

                if (!sizeByName.ContainsKey(strSplit[2]))
                {
                    sizeByName.Add(strSplit[2], 0.001);  //grandezza incrementata 
                }
                else
                {
                    sizeByName[strSplit[2]] += 0.001;
                }

                if (sizeByName[strSplit[2]] > app)
                {
                    app = sizeByName[strSplit[2]];
                    app2 = strSplit[2];
                }
            }
            mangione["mangione"] = app2;
        }

        public string TestoOrario(int posizione)
        {
            DateTime current = TrimDate(posizione);
            DateTime next = TrimDate(posizione + 1);

            int[] app = new int[5];

            TimeSpan ris = current.Subtract(next);

            double giorni = ris.TotalDays;
            double temp = 0;

            double ore = giorni * 24;
            if (giorni < 1)
            {
                double minuti = ore * 60;
                if (ore < 1)
                {
                    double secondi = minuti * 60;
                    if (minuti < 1)
                    {
                        return "<1 min";
                    }
                    else
                    {
                        temp = Math.Abs((minuti - Convert.ToInt32(minuti)) * 60);
                        return Convert.ToInt32(minuti) + " min";//+ Convert.ToInt32(temp)+"s";
                    }

                }
                else
                {
                    temp = Math.Abs((ore - Convert.ToInt32(ore)) * 60);
                    return Convert.ToInt32(ore) + "h " + Convert.ToInt32(temp)+"min";
                }

            }
            else
            {
                return Convert.ToInt32(giorni) + "g";               
            }
        }

        public double LenghtLine(int posizione)
        {
            DateTime current = TrimDate(posizione);
            DateTime next = TrimDate(posizione + 1);

            int[] app = new int[5];

            TimeSpan ris = current.Subtract(next);

            double giorni = ris.TotalDays;
            double multipler = 0;

            double ore = giorni * 24;
            if (giorni < 1)
            {
                double minuti = ore * 60;
                if (ore < 1)
                {
                    double secondi = minuti * 60;
                    if (minuti < 1)
                    {
                        multipler += ris.TotalSeconds * 0.0005;
                    }
                    else
                    {
                        if (ris.TotalMinutes < 30)
                            multipler += ris.TotalMinutes * 0.09;
                        else
                        {
                            multipler += 0.09*30;
                            multipler += (ris.TotalMinutes-30) * 0.05;
                        }
                    }
                    
                }
                else
                {
                    if (ris.TotalHours < 8)
                        multipler += ris.TotalHours * 1.8;
                    else
                    {
                        multipler += 1.8 * 8;
                        multipler += (ris.TotalHours-12) * 1.5;
                    }
                }
                
            }
            else
            {
                multipler += ris.Days*24;
                if (multipler > 600) multipler = 60;
            }
            return multipler;
        }

        private DateTime TrimDate(int posizione)     //metodo 100% appoggio
        {
            string[] strSplit = cronologia[posizione].Split();
            string[] anni = strSplit[0].Split('-');
            string[] minuti = strSplit[1].Split(':');

            DateTime current = new DateTime(int.Parse(anni[0]), int.Parse(anni[1]), int.Parse(anni[2]),
                                    int.Parse(minuti[0]), int.Parse(minuti[1]), int.Parse(minuti[2]));
            return current;
        }
        public async Task GetLastActions()
        {
            LastActionServiceManager lastAction = new LastActionServiceManager();
            var result = await lastAction.GetLastActions();
            if (result != null)
            {
                if (result.success == true)
                {
                    cronologia = result.data.actions;
                    Fusione();
                    LenghtLine(0);
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Fondo Merende", "Guarda, sta cosa non ha senso", "OK");
                }
            }
            else
            {
                await Navigation.PopAsync();
            }
        }

        public async void Aspetta()
        {
            await GetLastActions();
        }
    }
}