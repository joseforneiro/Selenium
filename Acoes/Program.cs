using System;
using System.Threading;
using System.Collections.Generic;
using dotenv.net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using Newtonsoft.Json;
using System.Linq;

namespace Acoes
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = Utils.InitializeSelenium();

            IJavaScriptExecutor js = driver as IJavaScriptExecutor; // Utilizado para Clicar no botão pelo Java Script.

            string pagina = "https://www.infomoney.com.br/ferramentas/altas-e-baixas/";

            driver.Navigate().GoToUrl(pagina);
            driver.Manage().Window.Maximize();

            //Thread.Sleep(5000);

            // Clicar no Botão do Popup:

            try // Se eu não fechar essa caixa popup não abre a próxima caixa no meio da raspagem.
            {
                //        js.ExecuteScript("document.querySelector('body > cookies-policy').shadowRoot.querySelector('cookies-policy-modal > div:nth-child(3) > cookies-policy-modal-footer').shadowRoot.querySelector('div > div > soma-button').click();");
                // Para pegar o código js acima eu inspeciono o botão, vou onde sombreou no código, clico sobre o sombreado com o botão direito, seleciono copy e em seguida: copy JS Path
                // Colo o código copiado dentro das aspas duplas acima, troco as aspas duplas do código copiado por aspas simples e coloco no final .click
                // Código js copiado:             document.querySelector("body > cookies-policy").shadowRoot.querySelector("cookies-policy-modal > div:nth-child(3) > cookies-policy-modal-footer").shadowRoot.querySelector("div > div > soma-button")
                // Código js depoia de alterado:  document.querySelector('body > cookies-policy').shadowRoot.querySelector('cookies-policy-modal > div:nth-child(3) > cookies-policy-modal-footer').shadowRoot.querySelector('div > div > soma-button').click();.click();

                //        Console.WriteLine("Caixa Popup Fechada.");
            }
            catch
            {
                //        Console.WriteLine("Não teve Caixa Popup!!!");
            }


            Thread.Sleep(2000);

            List<iacoes> acoesAlta = new List<iacoes>();
            List<iacoes> acoesBaixa = new List<iacoes>();

            String Ativo, Data, Ultimo, VarDia, VarSemana, VarMes, VarAno, Var12M, ValMin, ValMax, Volume;

            // Para contar o número de linhas da tabela:
            int contlinhastabela = driver.FindElement(By.Id("altas_e_baixas")).FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).Count;

            Console.WriteLine("Número de linhas na tabela: " + (contlinhastabela - 1));

            // Pegando elementos da tabela:
            int contador = 1;

            foreach (var item in driver.FindElement(By.Id("altas_e_baixas")).FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")))

            {
                try
                {
                    Ativo = item.FindElements(By.TagName("td"))[0].Text;
                    Data = item.FindElements(By.TagName("td"))[1].Text;
                    Ultimo = item.FindElements(By.TagName("td"))[2].Text;
                    VarDia = item.FindElements(By.TagName("td"))[3].Text;
                    VarSemana = item.FindElements(By.TagName("td"))[4].Text;
                    VarMes = item.FindElements(By.TagName("td"))[5].Text;
                    VarAno = item.FindElements(By.TagName("td"))[6].Text;
                    Var12M = item.FindElements(By.TagName("td"))[7].Text;
                    ValMin = item.FindElements(By.TagName("td"))[8].Text;
                    ValMax = item.FindElements(By.TagName("td"))[9].Text;
                    Volume = item.FindElements(By.TagName("td"))[10].Text;

                    // Imprimindo os valores
                    Console.WriteLine("Linha: " + contador + " - Ativo: " + Ativo + " - Data: " + Data + " - Ultimo: " + Ultimo + " - VarDia: " + VarDia + " - VarSemana: " + VarSemana + " - VarMes: " + VarMes + " - VarAno: " + VarAno + " - Var12M: " + Var12M + " - ValMin: " + ValMin + " - ValMax: " + ValMax + " - Volume: " + Volume);

                    contador++;

                    // Utilizando o método construtor:
                    iacoes ac = new iacoes(Ativo, Data, Ultimo, VarDia, VarSemana, VarMes, VarAno, Var12M, ValMin, ValMax, Volume);

                    //Thread.Sleep(500);

                    // Convertendo String para double para saber em qual lista jogar

                    VarDia = VarDia.Replace(",", "."); // Converte a , (virgula) que vem da string VarDia para . para que possa ser convertida.
                    decimal verificador = Convert.ToDecimal(VarDia); // Faz a conversão de string para decimal que é mais preciso.

                    if (verificador >= 0)
                    {
                        // Adicionado a Lista acoesAltaAlta
                        acoesAlta.Add(ac);
                    }
                    else
                    {
                        // Adicionado a Lista acoesBaixa
                        acoesBaixa.Add(ac);
                    }
                }
                catch // Essa caixa popup não será fechada, pois não fechei a primeira caixa popup, portanto essa caixa não aparece.
                {
                    // Fechando Popup Procura-se que aparece de vez em quando
                    js.ExecuteScript("document.querySelector('#om-fwftglp4r3snrtegfikr-optin > div > button').click()");
                    // Para pegar o código js acima eu inspeciono o botão, vou onde sombreou no código, clico sobre o sombreado com o botão direito, seleciono copy e em seguida: copy JS Path
                    // Colo o código copiado dentro das aspas duplas acima, troco as aspas duplas do código copiado por aspas simples e coloco no final .click
                    // Código js copiado:             document.querySelector("#om-fwftglp4r3snrtegfikr-optin > div > button")
                    // Código js depoia de alterado:  document.querySelector('#om-fwftglp4r3snrtegfikr-optin > div > button').click()

                    //Console.WriteLine("Fechando Caixa Popup Procura-se.");
                }

            }

            // Imprimindo Ações em Alta:
            Console.WriteLine("*****************************************************************************************************************************************************************************************************************");
            Console.WriteLine("Ações em Alta:\n");

            foreach (var acoesmais in acoesAlta)
            {
                Console.WriteLine("Ativo: " + acoesmais.Ativo + " - Data: " + acoesmais.Data + " - Ultimo: " + acoesmais.Ultimo + " - VarDia: " + acoesmais.VarDia + " - VarSemana: " + acoesmais.VarSemana + " - VarMes: " + acoesmais.VarMes + " - VarAno: " + acoesmais.VarAno + " - Var12M: " + acoesmais.Var12M + " - ValMin: " + acoesmais.ValMin + " - ValMax: " + acoesmais.ValMax + " - Volume: " + acoesmais.Volume);
            }

            // Imprimindo Ações em Baixa:
            Console.WriteLine("*****************************************************************************************************************************************************************************************************************");
            Console.WriteLine("Ações em Baixa:\n");

            foreach (var acoesmenos in acoesBaixa)
            {
                Console.WriteLine("Ativo: " + acoesmenos.Ativo + " - Data: " + acoesmenos.Data + " - Ultimo: " + acoesmenos.Ultimo + " - VarDia: " + acoesmenos.VarDia + " - VarSemana: " + acoesmenos.VarSemana + " - VarMes: " + acoesmenos.VarMes + " - VarAno: " + acoesmenos.VarAno + " - Var12M: " + acoesmenos.Var12M + " - ValMin: " + acoesmenos.ValMin + " - ValMax: " + acoesmenos.ValMax + " - Volume: " + acoesmenos.Volume);
            }


            Console.WriteLine("\n Ação com maior alta no dia: " + acoesAlta[0].Ativo + " - Variação Diária: " + acoesAlta[0].VarDia + "\n");

            Console.WriteLine("\n Ação com maior baixa no dia: " + acoesBaixa[acoesBaixa.Count - 1].Ativo + " - Variação Diária: " + acoesBaixa[acoesBaixa.Count - 1].VarDia + "\n");

            Console.WriteLine("Fim da execução do programa.");

            //acoesAlta.Max(x => x.VarDia) // Esse seria um método mais avançado de pegar o maior valor
        }
    }
}