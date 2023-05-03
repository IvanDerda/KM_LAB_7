using Newtonsoft.Json.Linq;
using System;

using System.Drawing;
using System.IO;

using System.Net;

using System.Windows.Forms;

namespace KM_Lab_7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var _name = textBox1.Text;

            var request = new GetRequest("https://restcountries.com/v3.1/name/"+_name);
            request.Run();

            var response = request.Response;
           
            if(response != null)
            {
                JArray j_arr = JArray.Parse(response);
                JObject j_obj = j_arr[0].ToObject<JObject>();
                string country_name = j_obj["name"]["official"].ToString();
                string country_capital = j_obj["capital"][0].ToString();
                string country_reg = j_obj["region"].ToString();
                string country_continents = j_obj["continents"][0].ToString();
                string imageUrl = j_obj["flags"]["png"].ToString();
                label1.Text = 
                    "Країна: " + country_name + "\n" + 
                    "Столиця: " + country_capital + "\n" + 
                    "Регіон: " + country_reg + "\n" +
                    "Континент: " + country_continents;
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(imageUrl) ;
                Bitmap bitmap = new Bitmap(stream);
                FlagIMG.Image= bitmap;
                FlagIMG.BackgroundImageLayout = ImageLayout.Stretch;
                
            }
            else
            {
                label1.Text = "Помилка:\n" +
                    "- Назву вводити латиницею\n" +
                    "- Перевірьте введене\n" +
                    "- Країни не існує";
                FlagIMG.Image = null;
            }
            
            

            
            
            

        }
    }
}
