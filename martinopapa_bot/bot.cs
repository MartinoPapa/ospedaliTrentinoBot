using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace martinopapa_bot
{
    class bot
    {
        private TelegramBotClient client;
        public void Start()
        {
            var handler = new HttpClientHandler();
            handler.DefaultProxyCredentials = CredentialCache.DefaultNetworkCredentials;
            var httpClient = new HttpClient(handler);
            this.client = new TelegramBotClient("962709189:AAEvpnHTlDZmxa-dN6T0Ihcxb_BeGT9nkjk", httpClient); //token per connettersi al proprio bot
            this.client.OnMessage += Client_OnMessage;
            this.client.StartReceiving(); //long polling
        }
        private async void Client_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            long chatId = e.Message.Chat.Id;
            string input = e.Message.Text;
            if (input == "/start")
            {
                StatoPS stato = new StatoPS();
                IEnumerable<ProntoSoccorso> lista = await stato.Lista();

                IReplyMarkup tastiera = new ReplyKeyboardMarkup(
                    lista.Select(x => new List<KeyboardButton>
                    {
                        new KeyboardButton(x.nome)
                    })
                    );

                await this.client.SendTextMessageAsync(
                    chatId,
                    "Selezionare un pronto soccorso dalla lista",
                    replyMarkup: tastiera
                    );
            }
            else
            {
                StatoPS stato = new StatoPS();
                IEnumerable<ProntoSoccorso> lista = await stato.Lista();

                ProntoSoccorso ps = lista.FirstOrDefault(x => x.nome == input);

                if(ps != null)
                {
                    string text = $"{ps.nome}\n\n";
                    text += $"⚪ {ps.attesa.bianco} in attesa\n";
                    text += $"🟢 {ps.attesa.verde} in attesa\n";
                    text += $"🟡 {ps.attesa.giallo} in attesa\n";
                    text += $"🔴 {ps.attesa.rosso} in attesa\n";

                    await this.client.SendTextMessageAsync(chatId, text);
                }

            }
        }
    }
}
