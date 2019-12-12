using CelebsApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CelebApi.DAL
{
    public class CelebsDal
    {
        static WebClient web = new WebClient();
        static string baseUrl = "https://www.imdb.com";
        static string filePath = AppDomain.CurrentDomain.BaseDirectory + @"Celebs.json";

        // create json file and for reset all use
        public string CreateJsonFile()
        {
            string json = string.Empty;

            if (!File.Exists(filePath))
            {
                string html = web.DownloadString(baseUrl + "/list/ls052283250/");
                MatchCollection clbs = Regex.Matches(html, "=\"/name/[mn/?0-9]{11}ref_=nmls_pst", RegexOptions.Singleline);

                HashSet<Celeb> celebs = GetCelebs(clbs);

                json = JsonConvert.SerializeObject(celebs);
                File.WriteAllText(filePath, json);
            }
            else
            {
                json = File.ReadAllText(filePath);
            }
            return json;
        }

        // get all celebs from web site
        private HashSet<Celeb> GetCelebs(MatchCollection coll)
        {
            HashSet<Celeb> celebs = new HashSet<Celeb>();
            int id = 0;
            Parallel.ForEach<string>(coll.Cast<Match>().Select(p => p.Value).ToArray(), (m) =>
            {
                string url = m.Replace("=\"", baseUrl);
                string html = GetHtml(url);

                Celeb celeb = GetCeleb(id, html);

                celebs.Add(celeb);
                id++;
            });
            return celebs;
        }

        // get specific celeb data from site. mainlly for "ReseetRecord" method
        public Celeb GetCeleb(int id, string html)
        {
            Celeb celeb = new Celeb
            {
                HtmlPath = html
            };

            MatchCollection clb = Regex.Matches(html, "<span class=\"itemprop\">(.+?)</span>", RegexOptions.Singleline);
            celeb.Id = id;
            celeb.Name = clb[0].Groups[1].Value;
            for (int i = 1; i < clb.Count; i++)
            {
                celeb.Role += clb[i].Groups[1].Value.Replace("\n", "") + " ";
            }
            clb = Regex.Matches(html, "<time datetime=\"(.+?)\">", RegexOptions.Singleline);
            celeb.BirthDate = DateTime.Parse(clb[0].Groups[1].Value).ToString("dd/MM/yyyy");

            if (celeb.Role.IndexOf("Actress") != -1)
                celeb.Gender = "Female";

            clb = Regex.Matches(html, "Picture\"\nsrc=\"https://m.media-amazon.com/images/(.+?).jpg", RegexOptions.Singleline);

            celeb.Image = clb[0].Value.Replace("Picture\"\nsrc=\"", "");
            return celeb;
        }

        // get page html content for url
        private string GetHtml(string url)
        {
            WebClient client = new WebClient();
            string clbHtml = client.DownloadString(url);
            client = null;
            return clbHtml;
        }

        public string AddRecord(JObject celeb)
        {
            return EditRecord(Action.ADD, 0, celeb);
        }

        public string RemoveRecord(int id)
        {
            return EditRecord(Action.REMOVE, id, null);
        }

        public string UpdateRecord(int id, JObject celeb)
        {
            return EditRecord(Action.UPDATE, id, celeb);
        }

        // reset single record -/ optional /-
        public string ReseetRecord(int id, string htmlPath)
        {
            Celeb celeb = GetCeleb(id, htmlPath);
            return EditRecord(Action.UPDATE, id, JObject.FromObject(celeb));
        }

        // reuse for all json file modification methods
        private string EditRecord(Action type, int id, JObject celeb)
        {
            string json = string.Empty;

            if (File.Exists(filePath))
            {
                json = File.ReadAllText(filePath);
                List<Celeb> data = JsonConvert.DeserializeObject<List<Celeb>>(json);

                switch (type)
                {
                    case Action.REMOVE:
                        {
                            var itemToRemove = data.SingleOrDefault(r => r.Id == id);
                            data.Remove(itemToRemove);
                            break;
                        }
                    case Action.ADD:
                        {
                            Celeb clb = celeb.ToObject<Celeb>();
                            clb.Id = data.Last().Id + 1;
                            data.Add(clb);
                            break;
                        }
                    case Action.UPDATE:
                        {
                            int index = data.FindIndex(a => a.Id == id);
                            Celeb clb = celeb.ToObject<Celeb>();
                            clb.Id = id;
                            data[index] = clb;
                            break;
                        }
                }
                json = JsonConvert.SerializeObject(data);
                File.WriteAllText(filePath, json);
            }
            return json;
        }

        // type of modification
        enum Action { UPDATE, ADD, REMOVE }
    }
}