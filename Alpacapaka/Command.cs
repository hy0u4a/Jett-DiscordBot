using Discord;
using Discord.Audio;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Alpacapaka
{
    public class Command : ModuleBase<SocketCommandContext>
    {
        #region CommandList
        [Command("명령어")]
        public async Task CommandList()
        {
            EmbedBuilder eb = new EmbedBuilder();

            eb.Title = "제트봇 명령어 리스트";
            eb.AddField("명령어", "@제트", false);
            eb.AddField("리그오브레전드", "솔랭 / 로테이션", false);
            eb.AddField("발로란트", "조준선", false);
            eb.AddField("제트봇 초대", "[여기](<https://discord.com/oauth2/authorize?client_id=848737096377696302&permissions=37013568&scope=bot>)", false);

            await Context.Channel.SendMessageAsync("", false, eb.Build());
        }
        #endregion

        #region Rotation
        [Command("로테이션")]
        public async Task RotationCommand()
        {
            string[] Rotations = LeagueOfLegends.Rotations();

            EmbedBuilder eb = new EmbedBuilder();

            eb.Title = "오늘의 로테이션";
            eb.Description = Rotations[0] + Rotations[1] + Rotations[2] + Rotations[3] + Rotations[4] + Rotations[5]
                 + Rotations[6] + Rotations[7] + Rotations[8] + Rotations[9] + Rotations[10] + Rotations[11] + Rotations[12] + Rotations[13]
                 + Rotations[14] + Rotations[15];

            await Context.Channel.SendMessageAsync("", false, eb.Build());
        }
        #endregion

        #region LoLSearch
        [Command("솔랭")]
        public async Task LeagueOfLegendsSearchCommand(string SummonerName = "")
        {
            try
            {
                if (SummonerName == "")
                {
                    EmbedBuilder eb = new EmbedBuilder();

                    eb.Title = "@제트 솔랭 \"닉네임\"";
                    eb.Description = "소환사명을 입력해 주세요.";
                    await Context.Channel.SendMessageAsync("", false, eb.Build());
                    return;
                }

                /* if (!SummonerName.Contains("\""))
                 {
                     await Context.Channel.SendMessageAsync("소환사명에 큰따옴표를 입력해주세요. ex) \"닉네임\"");
                     return;
                 } */

                LeagueOfLegends.summonerName = SummonerName;

                string[] Champion = LeagueOfLegends.Champion();
                string[] GameResult = LeagueOfLegends.GameResult();
                string[] Kill = LeagueOfLegends.Kill();
                string[] Death = LeagueOfLegends.Death();
                string[] Assist = LeagueOfLegends.Assist();
                string[] SummonerInfo = LeagueOfLegends.SummonerInfo();

                if (SummonerInfo != null)
                {
                    double Percent = Convert.ToDouble(SummonerInfo[3]) / (Convert.ToDouble(SummonerInfo[3]) + Convert.ToDouble(SummonerInfo[4]));
                    Percent = Math.Truncate(Percent * 100) / 100;

                    EmbedBuilder eb = new EmbedBuilder();

                    eb.Title = SummonerInfo[1] + " " + LeagueOfLegends.summonerName;
                    eb.Description = "**개인/2인 랭크**\n" +
                        SummonerInfo[0] + " " + SummonerInfo[2] + " " + SummonerInfo[5] + "LP\n" +
                        SummonerInfo[3] + "승 " + SummonerInfo[4] + "패 " + Percent * 100 + "%" + "\n\n" +
                        "**최근 전적**\n" +
                        GameResult[0] + Champion[0] + " " + Kill[0] + "/" + Death[0] + "/" + Assist[0] + "\n" +
                        GameResult[1] + Champion[1] + " " + Kill[1] + "/" + Death[1] + "/" + Assist[1] + "\n" +
                        GameResult[2] + Champion[2] + " " + Kill[2] + "/" + Death[2] + "/" + Assist[2] + "\n" +
                        GameResult[3] + Champion[3] + " " + Kill[3] + "/" + Death[3] + "/" + Assist[3] + "\n" +
                        GameResult[4] + Champion[4] + " " + Kill[4] + "/" + Death[4] + "/" + Assist[3] + "\n" +
                        GameResult[5] + Champion[5] + " " + Kill[5] + "/" + Death[5] + "/" + Assist[5] + "\n" +
                        GameResult[6] + Champion[6] + " " + Kill[6] + "/" + Death[6] + "/" + Assist[6] + "\n" +
                        GameResult[7] + Champion[7] + " " + Kill[7] + "/" + Death[7] + "/" + Assist[7] + "\n" +
                        GameResult[8] + Champion[8] + " " + Kill[8] + "/" + Death[8] + "/" + Assist[8] + "\n" +
                        GameResult[9] + Champion[9] + " " + Kill[9] + "/" + Death[9] + "/" + Assist[9];

                    await Context.Channel.SendMessageAsync("", false, eb.Build());
                }
                else
                {
                    EmbedBuilder eb = new EmbedBuilder();

                    eb.Title = "<:Unranked:848936295010533469>" + " " + LeagueOfLegends.summonerName;
                    eb.Description = "**최근 전적**\n" +
                        GameResult[0] + Champion[0] + " " + Kill[0] + "/" + Death[0] + "/" + Assist[0] + "\n" +
                        GameResult[1] + Champion[1] + " " + Kill[1] + "/" + Death[1] + "/" + Assist[1] + "\n" +
                        GameResult[2] + Champion[2] + " " + Kill[2] + "/" + Death[2] + "/" + Assist[2] + "\n" +
                        GameResult[3] + Champion[3] + " " + Kill[3] + "/" + Death[3] + "/" + Assist[3] + "\n" +
                        GameResult[4] + Champion[4] + " " + Kill[4] + "/" + Death[4] + "/" + Assist[3] + "\n" +
                        GameResult[5] + Champion[5] + " " + Kill[5] + "/" + Death[5] + "/" + Assist[5] + "\n" +
                        GameResult[6] + Champion[6] + " " + Kill[6] + "/" + Death[6] + "/" + Assist[6] + "\n" +
                        GameResult[7] + Champion[7] + " " + Kill[7] + "/" + Death[7] + "/" + Assist[7] + "\n" +
                        GameResult[8] + Champion[8] + " " + Kill[8] + "/" + Death[8] + "/" + Assist[8] + "\n" +
                        GameResult[9] + Champion[9] + " " + Kill[9] + "/" + Death[9] + "/" + Assist[9];

                    await Context.Channel.SendMessageAsync("", false, eb.Build());
                }
            }
            catch (Exception e)
            {
                await Context.Channel.SendMessageAsync("랭크 전적이 10전 미만이거나 찾을 수 없는 소환사입니다.");
                Console.WriteLine(e);
            }
        }
        #endregion

        #region Crosshair
        [Command("조준선")]
        public async Task Crosshair(string crosshair = "")
        {
            #region Function
            bool isexistence = true;
            string crosshaircolor = "null";
            string centerdot = "null";
            string centerdotopacity = "null";
            string centerdotthickness = "null";
            string outline = "null";
            string outlineopacity = "null";
            string outlinethickness = "null";
            string innerlineopacity = "null";
            string innerlinelength = "null";
            string innerlinethickness = "null";
            string innerlineoffset = "null";
            string outerlineopacity = "null";
            string outerlinelength = "null";
            string outerlinethickness = "null";
            string outerlineoffset = "null";
            string movementerror = "null";
            string firingerror = "null";
            #endregion

            #region crosshair
            switch (crosshair.ToLower())
            {
                case "aceu":
                    crosshaircolor = "녹색";
                    centerdot = "꺼짐";
                    outline = "꺼짐";
                    outlineopacity = "1";
                    outlinethickness = "1";

                    innerlineopacity = "1";
                    innerlinelength = "5";
                    innerlinethickness = "2";
                    innerlineoffset = "3";

                    outerlineopacity = "0";
                    outerlinelength = "0";
                    outerlinethickness = "0";
                    outerlineoffset = "0";
                    movementerror = "꺼짐";
                    firingerror = "꺼짐";
                    break;

                case "brax":
                    crosshaircolor = "흰색";
                    centerdot = "꺼짐";
                    outline = "꺼짐";
                    outlineopacity = "1";
                    outlinethickness = "1";

                    innerlineopacity = "1";
                    innerlinelength = "5";
                    innerlinethickness = "2";
                    innerlineoffset = "2";

                    outerlineopacity = "0";
                    outerlinelength = "0";
                    outerlinethickness = "0";
                    outerlineoffset = "0";
                    movementerror = "꺼짐";
                    firingerror = "꺼짐";
                    break;

                case "dizzy":
                    crosshaircolor = "빨간색";
                    centerdot = "꺼짐";
                    outline = "꺼짐";
                    outlineopacity = "1";
                    outlinethickness = "1";

                    innerlineopacity = "1";
                    innerlinelength = "6";
                    innerlinethickness = "2";
                    innerlineoffset = "0";

                    outerlineopacity = "0";
                    outerlinelength = "0";
                    outerlinethickness = "0";
                    outerlineoffset = "0";
                    movementerror = "꺼짐";
                    firingerror = "꺼짐";
                    break;

                case "hiko":
                    crosshaircolor = "녹색";
                    centerdot = "꺼짐";
                    outline = "켜짐";
                    outlineopacity = "1";
                    outlinethickness = "1";

                    innerlineopacity = "1";
                    innerlinelength = "4";
                    innerlinethickness = "2";
                    innerlineoffset = "3";

                    outerlineopacity = "0";
                    outerlinelength = "0";
                    outerlinethickness = "0";
                    outerlineoffset = "0";
                    movementerror = "꺼짐";
                    firingerror = "꺼짐";
                    break;

                case "mendo":
                    crosshaircolor = "녹색";
                    centerdot = "꺼짐";
                    outline = "꺼짐";
                    outlineopacity = "1";
                    outlinethickness = "1";

                    innerlineopacity = "1";
                    innerlinelength = "4";
                    innerlinethickness = "2";
                    innerlineoffset = "5";

                    outerlineopacity = "0";
                    outerlinelength = "0";
                    outerlinethickness = "0";
                    outerlineoffset = "0";
                    movementerror = "꺼짐";
                    firingerror = "꺼짐";
                    break;

                case "rb":
                    crosshaircolor = "청록색";
                    centerdot = "꺼짐";
                    outline = "꺼짐";
                    outlineopacity = "1";
                    outlinethickness = "1";

                    innerlineopacity = "1";
                    innerlinelength = "4";
                    innerlinethickness = "2";
                    innerlineoffset = "2";

                    outerlineopacity = "0";
                    outerlinelength = "0";
                    outerlinethickness = "0";
                    outerlineoffset = "0";
                    movementerror = "꺼짐";
                    firingerror = "꺼짐";
                    break;

                case "scream":
                    crosshaircolor = "흰색";
                    centerdot = "꺼짐";
                    centerdotopacity = "1";
                    centerdotthickness = "2";
                    outline = "꺼짐";
                    outlineopacity = "1";
                    outlinethickness = "1";

                    innerlineopacity = "0";
                    innerlinelength = "0";
                    innerlinethickness = "0";
                    innerlineoffset = "0";

                    outerlineopacity = "0";
                    outerlinelength = "0";
                    outerlinethickness = "0";
                    outerlineoffset = "0";
                    movementerror = "꺼짐";
                    firingerror = "꺼짐";
                    break;

                case "shahzam":
                    crosshaircolor = "녹색";
                    centerdot = "꺼짐";
                    outline = "켜짐";
                    outlineopacity = "1";
                    outlinethickness = "1";

                    innerlineopacity = "0.754";
                    innerlinelength = "5";
                    innerlinethickness = "2";
                    innerlineoffset = "4";

                    outerlineopacity = "0";
                    outerlinelength = "0";
                    outerlinethickness = "0";
                    outerlineoffset = "0";
                    movementerror = "꺼짐";
                    firingerror = "꺼짐";
                    break;

                case "shroud":
                    crosshaircolor = "청록색";
                    centerdot = "꺼짐";
                    outline = "켜짐";
                    outlineopacity = "1";
                    outlinethickness = "1";

                    innerlineopacity = "1";
                    innerlinelength = "7";
                    innerlinethickness = "3";
                    innerlineoffset = "4";

                    outerlineopacity = "0";
                    outerlinelength = "0";
                    outerlinethickness = "0";
                    outerlineoffset = "0";
                    movementerror = "꺼짐";
                    firingerror = "꺼짐";
                    break;

                case "sinatraa":
                    crosshaircolor = "녹색";
                    centerdot = "꺼짐";
                    outline = "꺼짐";
                    outlineopacity = "1";
                    outlinethickness = "1";

                    innerlineopacity = "1";
                    innerlinelength = "5";
                    innerlinethickness = "2";
                    innerlineoffset = "4";

                    outerlineopacity = "0";
                    outerlinelength = "0";
                    outerlinethickness = "0";
                    outerlineoffset = "0";
                    movementerror = "꺼짐";
                    firingerror = "꺼짐";
                    break;

                case "skadoodle":
                    crosshaircolor = "녹색";
                    centerdot = "꺼짐";
                    outline = "켜짐";
                    outlineopacity = "1";
                    outlinethickness = "1";

                    innerlineopacity = "1";
                    innerlinelength = "6";
                    innerlinethickness = "2";
                    innerlineoffset = "4";

                    outerlineopacity = "1";
                    outerlinelength = "5";
                    outerlinethickness = "1";
                    outerlineoffset = "4";
                    movementerror = "꺼짐";
                    firingerror = "꺼짐";
                    break;

                case "tenz":
                    crosshaircolor = "청록색";
                    centerdot = "꺼짐";
                    outline = "꺼짐";
                    outlineopacity = "1";
                    outlinethickness = "1";

                    innerlineopacity = "1";
                    innerlinelength = "4";
                    innerlinethickness = "2";
                    innerlineoffset = "2";

                    outerlineopacity = "0";
                    outerlinelength = "0";
                    outerlinethickness = "0";
                    outerlineoffset = "0";
                    movementerror = "꺼짐";
                    firingerror = "꺼짐";
                    break;

                default:
                    isexistence = false;
                    break;
            }
            #endregion

            if (crosshair != "" && isexistence == true)
            {
                EmbedBuilder eb = new EmbedBuilder();

                eb.Title = crosshair + " 조준선";
                eb.Description = "조준점 색깔 : " + crosshaircolor + "\n\n" +
                                 "화면 중앙 도트 : " + centerdot + "\n" +
                                 "화면 중앙 도트 투명도 : " + centerdotopacity + "\n" +
                                 "화면 중앙 도트 두께 : " + centerdotthickness + "\n" +
                                 "윤곽선 : " + outline + "\n" +
                                 "윤곽선 투명도 : " + outlineopacity + "\n" +
                                 "윤곽선 두께 : " + outlinethickness + "\n\n" +
                                 "안쪽 선 투명도 : " + innerlineopacity + "\n" +
                                 "안쪽 선 길이 : " + innerlinelength + "\n" +
                                 "안쪽 선 두께 : " + innerlinethickness + "\n" +
                                 "안쪽 선 편차 : " + innerlineoffset + "\n\n" +
                                 "바깥쪽 선 투명도 : " + outerlineopacity + "\n" +
                                 "바깥쪽 선 길이 : " + outerlinelength + "\n" +
                                 "바깥쪽 선 두께 : " + outerlinethickness + "\n" +
                                 "바깥쪽 선 편차 : " + outerlineoffset + "\n" +
                                 "움직임 오차 : " + movementerror + "\n" +
                                 "발사 오차 : " + firingerror + "\n";
                await Context.Channel.SendFileAsync("Crosshair\\" + crosshair + "\\" + crosshair + ".png");
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
            else if (crosshair == "")
            {

                EmbedBuilder eb = new EmbedBuilder();

                eb.Title = "@제트 조준선 \"닉네임\"";
                eb.Description = "Aceu, " + "Brax, " + "Dizzy, " + "Hiko\n" +
                    "Mendo, " + "Rb, " + "ScreaM, " + "ShahZaM\n" +
                    "Shroud, " + "Sinatraa, " + "Skadoodle, " + "TenZ";
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
        }
        #endregion

        #region Admin
        [Command("기본상태")]
        public async Task PlayingNormal()
        {
            if (Context.Message.Author.Id == 807127879128449025)
            {
                await Context.Client.SetGameAsync("@제트 명령어");
            }
        }

        [Command("게임중")]
        public async Task PlayingGame(string text = "")
        {
            if (Context.Message.Author.Id == 807127879128449025)
            {
                if (text == "")
                {
                    await Context.Channel.SendMessageAsync("@제트 게임중 \"제목\"");
                }
                else
                {
                    await Context.Client.SetGameAsync(text);
                }
            }
        }

        [Command("방송중")]
        public async Task PlayingStream(string url = "")
        {
            if (Context.Message.Author.Id == 807127879128449025)
            {
                if (url == "")
                {
                    await Context.Channel.SendMessageAsync("@제트 방송중 \"링크\"");
                }
                else
                {
                    string[] title = url.Split(new string[] { "https://www.twitch.tv/" }, StringSplitOptions.None);

                    await Context.Client.SetGameAsync(title[1], url, type: ActivityType.Streaming);
                }
            }
        }
        #endregion
    }
}
