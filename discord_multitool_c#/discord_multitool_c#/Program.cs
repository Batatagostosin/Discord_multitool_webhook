using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DiscordMultiTool
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.Title = "Discord Webhook Multi-Tool";
                DisplayBanner();
                Console.WriteLine("1. Send a Message");
                Console.WriteLine("2. Send an Embed");
                Console.WriteLine("3. Exit");
                Console.Write("\nSelect an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        await SendWebhookMessage();
                        break;
                    case "2":
                        await SendWebhookEmbed();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void DisplayBanner()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(@"
     ▄▄▄· ▄• ▄▌▄▄▄▄▄      • ▌ ▄ ·.  ▄▄▄· ▄▄▄▄▄▪         ▐ ▄ 
    ▐█ ▀█ █▪██▌•██  ▪     ·██ ▐███▪▐█ ▀█ •██  ██ ▪     •█▌▐█
    ▄█▀▀█ █▌▐█▌ ▐█.▪ ▄█▀▄ ▐█ ▌▐▌▐█·▄█▀▀█  ▐█.▪▐█· ▄█▀▄ ▐█▐▐▌
    ▐█ ▪▐▌▐█▄█▌ ▐█▌·▐█▌.▐▌██ ██▌▐█▌▐█ ▪▐▌ ▐█▌·▐█▌▐█▌.▐▌██▐█▌
     ▀  ▀  ▀▀▀  ▀▀▀  ▀█▄▀▪▀▀  █▪▀▀▀ ▀  ▀  ▀▀▀ ▀▀▀ ▀█▄▀▪▀▀ █▪
                                                                                          

                                    Discord Multi-Tool - Updt_JP
");
            Console.ResetColor();
        }

        static async Task SendWebhookMessage()
        {
            Console.Clear();
            Console.Write("Enter Webhook URL: ");
            string webhookUrl = Console.ReadLine();

            Console.Write("Enter Message: ");
            string message = Console.ReadLine();

            string payload = $"{{\"content\":\"{message}\"}}";

            using (HttpClient client = new HttpClient())
            {
                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(webhookUrl, content);

                if (response.IsSuccessStatusCode)
                    Console.WriteLine("Message sent successfully!");
                else
                    Console.WriteLine($"Failed to send message. Status Code: {response.StatusCode}");
            }
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }

        static async Task SendWebhookEmbed()
        {
            Console.Clear();
            Console.Write("Enter Webhook URL: ");
            string webhookUrl = Console.ReadLine();

            Console.Write("Enter Embed Title: ");
            string title = Console.ReadLine();

            Console.Write("Enter Embed Description: ");
            string description = Console.ReadLine();

            Console.WriteLine("Choose an Embed Color:");
            Console.WriteLine("1. Red");
            Console.WriteLine("2. Orange");
            Console.WriteLine("3. Yellow");
            Console.WriteLine("4. Green");
            Console.WriteLine("5. Blue");
            Console.WriteLine("6. Magenta");
            Console.WriteLine("7. Grey");
            Console.Write("\nSelect a color (1-7): ");

            string colorChoice = Console.ReadLine();
            int colorCode = GetColorCode(colorChoice);

            string payload = $@"{{
                ""embeds"": [
                    {{
                        ""title"": ""{title}"",
                        ""description"": ""{description}"",
                        ""color"": {colorCode}
                    }}
                ]
            }}" ;

            using (HttpClient client = new HttpClient())
            {
                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(webhookUrl, content);

                if (response.IsSuccessStatusCode)
                    Console.WriteLine("Embed sent successfully!");
                else
                    Console.WriteLine($"Failed to send embed. Status Code: {response.StatusCode}");
            }

            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }

        static int GetColorCode(string choice)
        {
            switch (choice)
            {
                case "1":
                    return 16711680;
                case "2":
                    return 16753920;
                case "3":
                    return 16776960;
                case "4":
                    return 65280;
                case "5":
                    return 40960;
                case "6":
                    return 12303291;
                case "7":
                    return 8421504;
                default:
                    Console.WriteLine("Invalid color choice. Defaulting to White.");
                    return 16777215;
            }
        }
    }
}
