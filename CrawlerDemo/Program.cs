using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using HtmlAgilityPack;
using System.Text;
using System.Data.Odbc;
using System.Threading.Tasks;
using System.IO;

namespace CrawlerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            startCrawlerasync();
            Console.ReadLine();
            
        }

        private static async Task startCrawlerasync()
        {
            //var url = "http://www.automobile.tn/neuf/bmw.3/";
            //var url = "https://www.magazineluiza.com.br/kit-barebone-vinik-vb200-gabinete-mouse-teclado-caixa-som-fonte-200w-preto/p/kf57h57e96/in/gbpc/";7
            Console.WriteLine("Digite a URL: (No momento, o código só funciona para Magazine Luiza, mas ele baixa todas as páginas que tentar)");
           var url = Console.ReadLine();

            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            string log = html.ToString();
            Log(log);
            var cars = new List<Car>();
            var divs =
            htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").StartsWith("price-template-price-block")).ToList();


            //var divs =
            //htmlDocument.DocumentNode.Descendants("div")
            //.Where(node => node.GetAttributeValue("class", "").StartsWith("product")).ToList();

            foreach (var div in divs)
            {
                var car = new Car();

                //car.Price = div.Descendants("div").FirstOrDefault().InnerText;
                car.Price = div.Descendants("span").Where(node => node.GetAttributeValue("class", "").StartsWith("price-template__text")).FirstOrDefault().InnerText;

                //car.Price = div.Descendants("span").Where(node => node.GetAttributeValue("class", "").StartsWith("price__SalesPrice")).FirstOrDefault().InnerText;

                car.Model = ""; //  div.Descendants("h2").FirstOrDefault().InnerText;
                car.Link = ""; // div.Descendants("a").FirstOrDefault().ChildAttributes("href").FirstOrDefault().Value;
                car.ImageUrl = ""; // div.Descendants("img").FirstOrDefault().ChildAttributes("src").FirstOrDefault().Value;
                cars.Add(car);              
            }
            // Connection string 
            //string MyConnection = "DRIVER={MySQL ODBC 3.51 Driver};Server=localhost;Database=crawlerdemo;User Id=root;Password=";
            ////string MyConnection = "datasource=localhost;username=root;password=";  
            //OdbcConnection con = new OdbcConnection(MyConnection);
            //con.Open();

            try
            {
                int count = cars.Count;
                foreach(var item in cars)
                {
                    for(int i = 0; i < count; i++)
                    {
                        //string query = "insert into carinfor(Model,Price,Link,ImageUrl) value(?,?,?,?);";
                        //OdbcCommand cmd = new OdbcCommand(query, con);
                        //cmd.Parameters.Add("?Model", OdbcType.VarChar).Value = cars[i].Model;
                        //cmd.Parameters.Add("?Price", OdbcType.VarChar).Value = cars[i].Price;
                        //cmd.Parameters.Add("?Link", OdbcType.VarChar).Value = cars[i].Link;
                        //cmd.Parameters.Add("?ImageUrl", OdbcType.VarChar).Value = cars[i].ImageUrl;
                        //OdbcDataReader reader = cmd.ExecuteReader();
                        //reader.Close();
                        //Console.WriteLine(cars[i].Model);
                        Console.WriteLine(cars[i].Price);
                        //Console.WriteLine(cars[i].Link);
                        //Console.WriteLine(cars[i].ImageUrl);
                        //Console.WriteLine();


                    }

                    count = 0;
                }
            }
            catch(Exception ex)
            {

                Console.WriteLine(ex.Message);
            }  
           
            Console.WriteLine("Successful....");
            Console.WriteLine("Press Enter to exit the program...");
            ConsoleKeyInfo keyinfor = Console.ReadKey(true);
            if(keyinfor.Key == ConsoleKey.Enter)
            {
                System.Environment.Exit(0);
            }

        }

        public static void Log(string log)
        {
            try
            {
                // Cria o nome do arquivo com ano, mês, dia, hora minuto e segundo

                string nomeArquivo = @"c:\log" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".html";

                // Cria um novo arquivo e devolve um StreamWriter para ele

                StreamWriter writer = new StreamWriter(nomeArquivo);

                // Agora é só sair escrevendo

                writer.WriteLine(log);

                // Não esqueça de fechar o arquivo ao terminar

                writer.Close();
            }catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
