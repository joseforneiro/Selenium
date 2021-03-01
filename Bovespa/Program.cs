using System;
using System.Threading;
using dotenv.net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using Newtonsoft.Json;
using SeleniumExtras.WaitHelpers;
// Para se usar o using SeleniumExtras.WaitHelpers:
// 1) Vou no terminal e instalo através do comando: dotnet add package DotNetSeleniumExtras.WaitHelpers --version 3.11.0
// 2) Coloco o código: WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20)); Esse código aguarta até 20 segundo o botão aparecer
// 3) Cólogo o código: var botao = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/cookies-policy//cookies-policy-modal/div[3]/cookies-policy-modal-footer//div/div/soma-button//div")));
// 4) botao.Click();


namespace Bovespa
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = Utils.InitializeSelenium();

            // WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));

            IJavaScriptExecutor js = driver as IJavaScriptExecutor; // Utilizado para Clicar no botão pelo Java Script.

            string pagina = "https://www.infomoney.com.br/";
            string pesquisa = "Ibovespa";

            driver.Navigate().GoToUrl(pagina);
            driver.Manage().Window.Maximize();

            //var botao = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/cookies-policy//cookies-policy-modal/div[3]/cookies-policy-modal-footer//div/div/soma-button//div")));
            //botao.Click();

            // Clicar no Botão do Popup:
            Thread.Sleep(3000);
            js.ExecuteScript("document.querySelector('body > cookies-policy').shadowRoot.querySelector('cookies-policy-modal > div:nth-child(3) > cookies-policy-modal-footer').shadowRoot.querySelector('div > div > soma-button').click();");
            // Para pegar o código js acima eu inspeciono o botão, vou onde sombreou no código, clico sobre o sombreado com o botão direito, seleciono copy e em seguida: copy JS Path
            // Colo o código copiado dentro das aspas duplas acima, troco as aspas duplas do código copiado por aspas simples e coloco no final .click
            // Código js copiado:             document.querySelector("body > cookies-policy").shadowRoot.querySelector("cookies-policy-modal > div:nth-child(3) > cookies-policy-modal-footer").shadowRoot.querySelector("div > div > soma-button")
            // Código js depoia de alterado:  document.querySelector('body > cookies-policy').shadowRoot.querySelector('cookies-policy-modal > div:nth-child(3) > cookies-policy-modal-footer').shadowRoot.querySelector('div > div > soma-button').click();

            // Seleciono Caixa de Pesquisa e Preencho com Ibovespa
            driver.FindElement(By.Id("select2-cotacoes-search-container")).Click();
            driver.FindElement(By.ClassName("select2-search__field")).SendKeys(pesquisa);

            // Clico no Ibovespa
            Thread.Sleep(3000);
            driver.FindElement(By.ClassName("select2-results")).FindElement(By.TagName("ul")).FindElements(By.TagName("li"))[0].Click();

            ibovespa ibov = new ibovespa();

            //Console.WriteLine(driver.FindElement(By.ClassName("value")).Text);

            ibov.Pontos = driver.FindElement(By.ClassName("value")).Text;
            ibov.Pontos = driver.FindElement(By.ClassName("line-info")).FindElement(By.ClassName("value")).FindElement(By.TagName("p")).Text;
            ibov.VariacaoDia = driver.FindElement(By.ClassName("percentage")).FindElement(By.TagName("p")).Text;
            ibov.MinDia = driver.FindElement(By.ClassName("minimo")).FindElement(By.TagName("p")).Text;
            ibov.MaxDia = driver.FindElement(By.ClassName("maximo")).FindElement(By.TagName("p")).Text;
            ibov.Volume = driver.FindElement(By.ClassName("volume")).FindElement(By.TagName("p")).Text;


            // ibovespa ibov = new ibovespa();
            // ibov.Pontos = "5";
            // ibov.MaxDia = "10";
            // ibov.MinDia = "20";
            // ibov.Volume = "30";
            // ibov.VariacaoDia = "40";

            Console.WriteLine(JsonConvert.SerializeObject(ibov));
        }
    }
}
