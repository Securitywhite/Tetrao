/*
 *  Author: OldNutMan/ArachisH
 *  Desc: Required for HSession class, contains useful functions
 */
using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;

namespace Tetrao___Client
{
    /// <summary>
    /// Provides global methods for extracting/checking public information from a specific hotel.
    /// </summary>
    public static class SKore
    {
        /// <summary>
        /// The user-agent that is used by every method in the Sulakore library that requires the use of an external internet resource to receive/send data.
        /// </summary>
        public const string ChromeAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.1916.153 Safari/537.36";

        private static string IPCookie_;
        /// <summary>
        /// The cookie that is required by most hotels to retrieve an internet resource properly.
        /// </summary>
        public static string IPCookie
        {
            get
            {
                if (!string.IsNullOrEmpty(IPCookie_)) return IPCookie_;
                using (WebClient WC = new WebClient())
                {
                    WC.Headers["User-Agent"] = ChromeAgent;
                    return (IPCookie_ = "YPF8827340282Jdskjhfiw_928937459182JAX666=" + WC.DownloadString("http://www.habbo.com").
                        GetChild("setCookie<'", '\'', 3));
                }
            }
        }

        /// <summary>
        /// Returns an array of non-authenticated Sulakore.HSession accounts extracted from the specified file path. If pre-set account list format is found, it will not use the delimiter.
        /// </summary>
        /// <param name="Path">The complete file path to grab the accounts from. path can be a file name.</param>
        /// <param name="Delimiter">The char in-which to start splitting the credentials of a single account. Ex: Email:Password:Domain, where ':' is the default delimiter.</param>
        /// <returns></returns>
        public static HSession[] GetAccounts(string Path, char Delimiter = ':')
        {
            string[] Lines = File.ReadAllLines(Path);
            List<HSession> Accounts = new List<HSession>();
            for (int i = 0; i < Lines.Length; i++)
            {
                if (Lines[i].Contains(Delimiter) && Lines[i].Split(Delimiter).Count(x => !string.IsNullOrEmpty(x)) == 3)
                    Accounts.Add(ToHSession(Lines[i], Delimiter));
                else if (!Lines[i].Contains(Delimiter) && Lines[i].Contains('@') && Lines[i + 2].Contains('/') && isValidHotel(Lines[i + 2].Split('/')[1]))
                    Accounts.Add(new HSession(Lines[i].Trim(), Lines[i + 1].Trim(), Lines[i + 2].Trim().Split('/')[1].Trim().ToHotel()));
            }
            return Accounts.ToArray();
        }

        /// <summary>
        /// Determines whether the specified player name already exist in the specified hotel.
        /// </summary>
        /// <param name="PlayerName">The name of the player to check.</param>
        /// <param name="Hotel">The hotel in-which to check for the name.</param>
        /// <returns></returns>
        public static bool isNameAvailable(string PlayerName, HHotels Hotel)
        {
            return GetPlayerID(PlayerName, Hotel) == -1;
        }

        #region GetPlayerID
        private delegate int PlayerID_1(string s, HHotels hh);
        private static PlayerID_1 PlayerIDCallback_1;

        /// <summary>
        /// Gets the player id that is connected to the specified player name, on the desired hotel.
        /// </summary>
        /// <param name="PlayerName">The name of the player you wish to grab the player id from.</param>
        /// <param name="Hotel">The hotel where the specified player name exist on.</param>
        /// <returns></returns>
        public static int GetPlayerID(string PlayerName, HHotels Hotel)
        {
            using (WebClient WC = new WebClient())
            {
                WC.Headers["Cookie"] = IPCookie;
                WC.Headers["User-Agent"] = ChromeAgent;
                string Body = WC.DownloadString(string.Format("{0}/habblet/ajax/new_habboid?habboIdName={1}", Hotel.ToHost(), PlayerName));
                return Body.Contains("rounded rounded-red") ? -1 : int.Parse(Body.Split('>')[1].Split('<')[0].Replace(" ", string.Empty));
            }
        }
        /// <summary>
        /// Begins an asynchronous operation to get the player id that is connected to the specified player name, on the desired hotel.
        /// </summary>
        /// <param name="PlayerName">The name of the player you wish to grab the player id from.</param>
        /// <param name="Hotel">The hotel where the specified player name exist on.</param>
        /// <param name="callback">The System.AsyncCallback delegate.</param>
        /// <param name="state">The state object for this operation.</param>
        /// <returns></returns>
        public static IAsyncResult BeginGetPlayerID(string PlayerName, HHotels Hotel, AsyncCallback callback, object state)
        {
            if (PlayerIDCallback_1 == null)
                PlayerIDCallback_1 = GetPlayerID;

            return PlayerIDCallback_1.BeginInvoke(PlayerName, Hotel, callback, state);
        }
        /// <summary>
        /// Ends an asynchronous operation for grabbing a player's id.
        /// </summary>
        /// <param name="result">The pending operation for grabbing a player's id.</param>
        /// <returns></returns>
        public static int EndGetPlayerID(IAsyncResult result)
        {
            return PlayerIDCallback_1.EndInvoke(result);
        }
        #endregion

        #region GetPlayerName
        private delegate string PlayerName_1(int i, HHotels hh);
        private static PlayerName_1 PlayerNameCallback_1;

        /// <summary>
        /// Gets the player name that is connected to the specified player id, on the desired hotel.
        /// </summary>
        /// <param name="PlayerID">The id of the player you wish to grab the player name from.</param>
        /// <param name="Hotel">The hotel where the specified player id exist on.</param>
        /// <returns></returns>
        public static string GetPlayerName(int PlayerID, HHotels Hotel)
        {
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/rd/{1}", Hotel.ToHost(), PlayerID));
            Request.Headers["Cookie"] = IPCookie;
            Request.AllowAutoRedirect = false;
            Request.UserAgent = ChromeAgent;
            Request.Method = "GET";

            using (HttpWebResponse Response = (HttpWebResponse)Request.GetResponse())
                return Uri.UnescapeDataString(Response.Headers["Location"].Split('/')[4]);
        }
        /// <summary>
        /// Begins an asynchronous operation to get the player name that is connected to the specified player id, on the desired hotel.
        /// </summary>
        /// <param name="PlayerID">The id of the player you wish to grab the player name from.</param>
        /// <param name="Hotel">The hotel where the specified player id exist on.</param>
        /// <param name="callback">The System.AsyncCallback delegate.</param>
        /// <param name="state">The state object for this operation.</param>
        /// <returns></returns>
        public static IAsyncResult BeginGetPlayerName(int PlayerID, HHotels Hotel, AsyncCallback callback, object state)
        {
            if (PlayerNameCallback_1 == null)
                PlayerNameCallback_1 = GetPlayerName;

            return PlayerNameCallback_1.BeginInvoke(PlayerID, Hotel, callback, state);
        }
        /// <summary>
        /// Ends an asynchronous operation for grabbing a player's name.
        /// </summary>
        /// <param name="result">The pending operation for grabbing a player's name.</param>
        /// <returns></returns>
        public static string EndGetPlayerName(IAsyncResult result)
        {
            return PlayerNameCallback_1.EndInvoke(result);
        }
        #endregion

        #region GetPlayerLastOnline
        private delegate string PlayerLastOnline_1(int i, HHotels hh, bool b);
        private delegate string PlayerLastOnline_2(string s, HHotels hh, bool b);
        private static PlayerLastOnline_1 PlayerLastOnlineCallback_1;
        private static PlayerLastOnline_2 PlayerLastOnlineCallback_2;

        /// <summary>
        /// Gets the last online time of the player connected to the specified player id, on the desired hotel.
        /// </summary>
        /// <param name="PlayerID">The id of the player you wish to grab the last online time from.</param>
        /// <param name="Hotel">The hotel where the specified player id exist on.</param>
        /// <param name="Estimate">Determines whether to return the amount of time the specified player was last online, else return the last online date.</param>
        /// <returns></returns>
        public static string GetPlayerLastOnline(int PlayerID, HHotels Hotel, bool Estimate = false)
        {
            string PlayerName = GetPlayerName(PlayerID, Hotel);
            using (WebClient WC = new WebClient())
            {
                WC.Headers["Cookie"] = IPCookie;
                WC.Headers["User-Agent"] = ChromeAgent;
                string Body = WC.DownloadString(string.Format("{0}/habblet/habbosearchcontent?searchString={1}", Hotel.ToHost(), PlayerName)).
                    GetChild(string.Format("<b>{0}</b><br />", PlayerName)).GetChild("<span title=\"");
                return !Estimate ? Body.Split('\"')[0] : Body.Split('>')[1].Split('<')[0];
            }
        }
        /// <summary>
        /// Begins an asynchronous operation to get the last online time of the player that is connected to the specified player id, on the desired hotel.
        /// </summary>
        /// <param name="PlayerID">The id of the player you wish to grab the last online time from.</param>
        /// <param name="Hotel">The hotel where the specified player id exist on.</param>
        /// <param name="Estimate">Determines whether to return the amount of time the specified player was last online, else return the last online date.</param>
        /// <param name="callback">The System.AsyncCallback delegate.</param>
        /// <param name="state">The state object for this operation.</param>
        /// <returns></returns>
        public static IAsyncResult BeginGetPlayerLastOnline(int PlayerID, HHotels Hotel, bool Estimate, AsyncCallback callback, object state)
        {
            if (PlayerLastOnlineCallback_1 == null)
                PlayerLastOnlineCallback_1 = GetPlayerLastOnline;

            return PlayerLastOnlineCallback_1.BeginInvoke(PlayerID, Hotel, Estimate, callback, state);
        }

        /// <summary>
        /// Gets the last online time of the specified player, on the desired hotel.
        /// </summary>
        /// <param name="PlayerName">The name of the player you wish to grab the last online time from.</param>
        /// <param name="Hotel">The hotel where the specified player id exist on.</param>
        /// <param name="Estimate">Determines whether to return the amount of time the specified player was last online, else return the last online date.</param>
        /// <returns></returns>
        public static string GetPlayerLastOnline(string PlayerName, HHotels Hotel, bool Estimate = false)
        {
            using (WebClient WC = new WebClient())
            {
                WC.Headers["Cookie"] = IPCookie;
                WC.Headers["User-Agent"] = ChromeAgent;
                string Body = WC.DownloadString(string.Format("{0}/habblet/habbosearchcontent?searchString={1}", Hotel.ToHost(), PlayerName)).
                    GetChild(string.Format("<b>{0}</b><br />", PlayerName)).GetChild("<span title=\"");
                return !Estimate ? Body.Split('\"')[0] : Body.Split('>')[1].Split('<')[0];
            }
        }
        /// <summary>
        /// Begins an asynchronous operation to get the last online time of the specified player, on the desired hotel.
        /// </summary>
        /// <param name="PlayerName">The name of the player you wish to grab the last online time from.</param>
        /// <param name="Hotel">The hotel where the specified player id exist on.</param>
        /// <param name="Estimate">Determines whether to return the amount of time the specified player was last online, else return the last online date.</param>
        /// <param name="callback">The System.AsyncCallback delegate.</param>
        /// <param name="state">The state object for this operation.</param>
        /// <returns></returns>
        public static IAsyncResult BeginGetPlayerLastOnline(string PlayerName, HHotels Hotel, bool Estimate, AsyncCallback callback, object state)
        {
            if (PlayerLastOnlineCallback_2 == null)
                PlayerLastOnlineCallback_2 = GetPlayerLastOnline;

            return PlayerLastOnlineCallback_2.BeginInvoke(PlayerName, Hotel, Estimate, callback, state);
        }

        /// <summary>
        /// Ends an asynchronous operation for grabbing a player's last online time.
        /// </summary>
        /// <param name="result">The pending operation for grabbing a player's last online time.</param>
        /// <returns></returns>
        public static string EndGetPlayerLastOnline(IAsyncResult result)
        {
            try
            { return PlayerLastOnlineCallback_1.EndInvoke(result); }
            catch
            { return PlayerLastOnlineCallback_2.EndInvoke(result); }
        }
        #endregion

        #region GetPlayerMotto
        private delegate string PlayerMotto_1(int i, HHotels hh);
        private delegate string PlayerMotto_2(string s, HHotels hh);
        private static PlayerMotto_1 PlayerMottoCallback_1;
        private static PlayerMotto_2 PlayerMottoCallback_2;

        /// <summary>
        /// Gets the player motto that is connected to the specified player id, on the desired hotel.
        /// </summary>
        /// <param name="PlayerID">The id of the player you wish to grab the player motto from.</param>
        /// <param name="Hotel">The hotel where the specified player id exist on.</param>
        /// <returns></returns>
        public static string GetPlayerMotto(int PlayerID, HHotels Hotel)
        {
            string PlayerName = GetPlayerName(PlayerID, Hotel);
            using (WebClient WC = new WebClient())
            {
                WC.Headers["Cookie"] = IPCookie;
                WC.Headers["User-Agent"] = ChromeAgent;
                return WC.DownloadString(string.Format("{0}/habblet/habbosearchcontent?searchString={1}", Hotel.ToHost(), PlayerName)).
                    GetChild(string.Format("<b>{0}</b><br />", PlayerName), '<', 0);
            }
        }
        /// <summary>
        /// Begins an asynchronous operation to get the player motto that is connected to the specified player id, on the desired hotel.
        /// </summary>
        /// <param name="PlayerID">The id of the player you wish to grab the player motto from.</param>
        /// <param name="Hotel">The hotel where the specified player id exist on.</param>
        /// <param name="callback">The System.AsyncCallback delegate.</param>
        /// <param name="state">The state object for this operation.</param>
        /// <returns></returns>
        public static IAsyncResult BeginGetPlayerMotto(int PlayerID, HHotels Hotel, AsyncCallback callback, object state)
        {
            if (PlayerMottoCallback_1 == null)
                PlayerMottoCallback_1 = GetPlayerMotto;

            return PlayerMottoCallback_1.BeginInvoke(PlayerID, Hotel, callback, state);
        }

        /// <summary>
        /// Gets the player motto of the specified player, on the desired hotel.
        /// </summary>
        /// <param name="PlayerName">The name of the player you wish to grab the player motto from.</param>
        /// <param name="Hotel">The hotel where the specified player name exist on.</param>
        /// <returns></returns>
        public static string GetPlayerMotto(string PlayerName, HHotels Hotel)
        {
            using (WebClient WC = new WebClient())
            {
                WC.Headers["Cookie"] = IPCookie;
                WC.Headers["User-Agent"] = ChromeAgent;
                return WC.DownloadString(string.Format("{0}/habblet/habbosearchcontent?searchString={1}", Hotel.ToHost(), PlayerName)).
                    GetChild(string.Format("<b>{0}</b><br />", PlayerName), '<', 0);
            }
        }
        /// <summary>
        /// Begins an asynchronous operation to get the player motto of the specified player, on the desired hotel.
        /// </summary>
        /// <param name="PlayerName">The name of the player you wish to grab the player motto from.</param>
        /// <param name="Hotel">The hotel where the specified player name exist on.</param>
        /// <param name="callback">The System.AsyncCallback delegate.</param>
        /// <param name="state">The state object for this operation.</param>
        /// <returns></returns>
        public static IAsyncResult BeginGetPlayerMotto(string PlayerName, HHotels Hotel, AsyncCallback callback, object state)
        {
            if (PlayerMottoCallback_2 == null)
                PlayerMottoCallback_2 = GetPlayerMotto;

            return PlayerMottoCallback_2.BeginInvoke(PlayerName, Hotel, callback, state);
        }

        /// <summary>
        /// Ends an asynchronous operation for grabbing a player's motto.
        /// </summary>
        /// <param name="result">The pending operation for grabbing a player's motto.</param>
        /// <returns></returns>
        public static string EndGetPlayerMotto(IAsyncResult result)
        {
            try { return PlayerMottoCallback_1.EndInvoke(result); }
            catch { return PlayerMottoCallback_2.EndInvoke(result); }
        }
        #endregion

        #region GetPlayerClothes
        private delegate string PlayerClothes_1(int i, HHotels hh);
        private delegate string PlayerClothes_2(string s, HHotels hh);
        private static PlayerClothes_1 PlayerClothesCallback_1;
        private static PlayerClothes_2 PlayerClothesCallback_2;

        /// <summary>
        /// Gets the player clothes that is connected to the specified player id, on the desired hotel.
        /// </summary>
        /// <param name="PlayerID">The id of the player you wish to grab the player clothes from.</param>
        /// <param name="Hotel">The hotel where the specified player id exist on.</param>
        /// <returns></returns>
        public static string GetPlayerClothes(int PlayerID, HHotels Hotel)
        {
            string PlayerName = GetPlayerName(PlayerID, Hotel);
            using (WebClient WC = new WebClient())
            {
                WC.Headers["Cookie"] = IPCookie;
                WC.Headers["User-Agent"] = ChromeAgent;
                return WC.DownloadString(string.Format("{0}/habblet/habbosearchcontent?searchString={1}", Hotel.ToHost(), PlayerName)).
                    GetChild("habbo-imaging/avatar/", ',', 0);
            }
        }
        /// <summary>
        /// Begins an asynchronous operation to get the player clothes that is connected to the specified player id, on the desired hotel.
        /// </summary>
        /// <param name="PlayerID">The id of the player you wish to grab the player clothes from.</param>
        /// <param name="Hotel">The hotel where the specified player id exist on.</param>
        /// <param name="callback">The System.AsyncCallback delegate.</param>
        /// <param name="state">The state object for this operation.</param>
        /// <returns></returns>
        public static IAsyncResult BeginGetPlayerClothes(int PlayerID, HHotels Hotel, AsyncCallback callback, object state)
        {
            if (PlayerClothesCallback_1 == null)
                PlayerClothesCallback_1 = GetPlayerClothes;

            return PlayerClothesCallback_1.BeginInvoke(PlayerID, Hotel, callback, state);
        }

        /// <summary>
        /// Gets the player clothes of the specified player, on the desired hotel.
        /// </summary>
        /// <param name="PlayerName">The name of the player you wish to grab the player clothes from.</param>
        /// <param name="Hotel">The hotel where the specified player name exist on.</param>
        /// <returns></returns>
        public static string GetPlayerClothes(string PlayerName, HHotels Hotel)
        {
            using (WebClient WC = new WebClient())
            {
                WC.Headers["Cookie"] = IPCookie;
                WC.Headers["User-Agent"] = ChromeAgent;
                return WC.DownloadString(string.Format("{0}/habblet/habbosearchcontent?searchString={1}", Hotel.ToHost(), PlayerName)).
                    GetChild("habbo-imaging/avatar/", ',', 0);
            }
        }
        /// <summary>
        /// Begins an asynchronous operation to get the player clothes of the specified player, on the desired hotel.
        /// </summary>
        /// <param name="PlayerName">The name of the player you wish to grab the player clothes from.</param>
        /// <param name="Hotel">The hotel where the specified player name exist on.</param>
        /// <param name="callback">The System.AsyncCallback delegate.</param>
        /// <param name="state">The state object for this operation.</param>
        /// <returns></returns>
        public static IAsyncResult BeginGetPlayerClothes(string PlayerName, HHotels Hotel, AsyncCallback callback, object state)
        {
            if (PlayerClothesCallback_2 == null)
                PlayerClothesCallback_2 = GetPlayerClothes;

            return PlayerClothesCallback_2.BeginInvoke(PlayerName, Hotel, callback, state);
        }

        /// <summary>
        /// Ends an asynchronous operation for grabbing a player's clothes.
        /// </summary>
        /// <param name="result">The pending operation for grabbing a player's clothes.</param>
        /// <returns></returns>
        public static string EndGetPlayerClothes(IAsyncResult result)
        {
            try
            { return PlayerClothesCallback_1.EndInvoke(result); }
            catch
            { return PlayerClothesCallback_2.EndInvoke(result); }
        }
        #endregion

        #region GetPlayerAvatar
        private delegate Image PlayerAvatar_1(int i, HHotels hh);
        private delegate Image PlayerAvatar_2(string s, HHotels hh);
        private delegate Image PlayerAvatar_3(string s);
        private static PlayerAvatar_1 PlayerAvatarCallback_1;
        private static PlayerAvatar_2 PlayerAvatarCallback_2;
        private static PlayerAvatar_3 PlayerAvatarCallback_3;

        /// <summary>
        /// Gets the player avatar that is connected to the specified player id, on the desired hotel.
        /// </summary>
        /// <param name="PlayerID">The id of the player you wish to grab the player avatar from.</param>
        /// <param name="Hotel">The hotel where the specified player id exist on.</param>
        /// <returns></returns>
        public static Image GetPlayerAvatar(int PlayerID, HHotels Hotel)
        {
            try
            {
                string PlayerName = GetPlayerName(PlayerID, Hotel);
                using (WebClient WC = new WebClient())
                {
                    WC.Headers["Cookie"] = IPCookie;
                    WC.Headers["User-Agent"] = ChromeAgent;
                    byte[] AvatarData = WC.DownloadData(string.Format("{0}/habbo-imaging/avatarimage?user={1}&action=&direction=&head_direction=&gesture=&size=", Hotel.ToHost(), PlayerName));
                    using (MemoryStream MS = new MemoryStream(AvatarData))
                        return Image.FromStream(MS);
                }
            }
            catch { return null; }
        }
        /// <summary>
        /// Begins an asynchronous operation to get the player avatar that is connected to the specified player id, on the desired hotel.
        /// </summary>
        /// <param name="PlayerID">The id of the player you wish to grab the player avatar from.</param>
        /// <param name="Hotel">The hotel where the specified player id exist on.</param>
        /// <param name="callback">The System.AsyncCallback delegate.</param>
        /// <param name="state">The state object for this operation.</param>
        /// <returns></returns>
        public static IAsyncResult BeginGetPlayerAvatar(int PlayerID, HHotels Hotel, AsyncCallback callback, object state)
        {
            if (PlayerAvatarCallback_1 == null)
                PlayerAvatarCallback_1 = GetPlayerAvatar;

            return PlayerAvatarCallback_1.BeginInvoke(PlayerID, Hotel, callback, state);
        }

        /// <summary>
        /// Gets the player avatar of the specified player, on the desired hotel.
        /// </summary>
        /// <param name="PlayerName">The name of the player you wish to grab the player avatar from.</param>
        /// <param name="Hotel">The hotel where the specified player name exist on.</param>
        /// <returns></returns>
        public static Image GetPlayerAvatar(string PlayerName, HHotels Hotel)
        {
            try
            {
                using (WebClient WC = new WebClient())
                {
                    WC.Headers["Cookie"] = IPCookie;
                    WC.Headers["User-Agent"] = ChromeAgent;
                    byte[] AvatarData = WC.DownloadData(string.Format("{0}/habbo-imaging/avatarimage?user={1}&action=&direction=&head_direction=&gesture=&size=", Hotel.ToHost(), PlayerName));
                    using (MemoryStream MS = new MemoryStream(AvatarData))
                        return Image.FromStream(MS);
                }
            }
            catch { return null; }
        }
        /// <summary>
        /// Begins an asynchronous operation to get the player avatar of the specified player, on the desired hotel.
        /// </summary>
        /// <param name="PlayerName">The name of the player you wish to grab the avatar clothes from.</param>
        /// <param name="Hotel">The hotel where the specified player name exist on.</param>
        /// <param name="callback">The System.AsyncCallback delegate.</param>
        /// <param name="state">The state object for this operation.</param>
        /// <returns></returns>
        public static IAsyncResult BeginGetPlayerAvatar(string PlayerName, HHotels Hotel, AsyncCallback callback, object state)
        {
            if (PlayerAvatarCallback_2 == null)
                PlayerAvatarCallback_2 = GetPlayerAvatar;

            return PlayerAvatarCallback_2.BeginInvoke(PlayerName, Hotel, callback, state);
        }

        /// <summary>
        /// Gets the player avatar that can be constructed using the specified clothes id.
        /// </summary>
        /// <param name="ClothesID">The clothes id of the avatar.</param>
        /// <returns></returns>
        public static Image GetPlayerAvatar(string ClothesID)
        {
            try
            {
                using (WebClient WC = new WebClient())
                {
                    WC.Headers["Cookie"] = IPCookie;
                    WC.Headers["User-Agent"] = ChromeAgent;
                    byte[] AvatarData = WC.DownloadData(string.Format("http://www.habbo.com/habbo-imaging/avatarimage?figure={0}&action=&direction=&head_direction=&gesture=&size=", ClothesID));
                    using (MemoryStream MS = new MemoryStream(AvatarData))
                        return Image.FromStream(MS);
                }
            }
            catch { return null; }
        }
        /// <summary>
        /// Begins an asynchronous operation to get the player avatar that can be constructed using the specified clothes id.
        /// </summary>
        /// <param name="ClothesID">The clothes id of the avatar.</param>
        /// <param name="callback">The System.AsyncCallback delegate.</param>
        /// <param name="state">The state object for this operation.</param>
        /// <returns></returns>
        public static IAsyncResult BeginGetPlayerAvatar(string ClothesID, AsyncCallback callback, object state)
        {
            if (PlayerAvatarCallback_3 == null)
                PlayerAvatarCallback_3 = GetPlayerAvatar;

            return PlayerAvatarCallback_3.BeginInvoke(ClothesID, callback, state);
        }

        /// <summary>
        /// Ends an asynchronous operation for grabbing an avatar.
        /// </summary>
        /// <param name="result">The pending operation for grabbing an avatar.</param>
        /// <returns></returns>
        public static Image EndGetPlayerAvatar(IAsyncResult result)
        {
            try
            { return PlayerAvatarCallback_1.EndInvoke(result); }
            catch
            {
                try
                { return PlayerAvatarCallback_2.EndInvoke(result); }
                catch
                { return PlayerAvatarCallback_3.EndInvoke(result); }
            }
        }
        #endregion

        #region Extensions
        public static HBans ToHBan(string BanType)
        {
            switch (BanType.ToLower())
            {
                case "d":
                case "da":
                case "day":
                case "rwuam_ban_user_day": return HBans.Day;

                case "h":
                case "ho":
                case "hou":
                case "hour":
                case "rwuam_ban_user_hour": return HBans.Hour;

                case "p":
                case "pe":
                case "per":
                case "perm":
                case "perma":
                case "perman":
                case "permane":
                case "permanen":
                case "permanent":
                case "rwuam_ban_user_perm": return HBans.Permanent;
                default: return HBans.Day;
            }
        }
        public static string Juice(this HBans BanType)
        {
            switch (BanType)
            {
                case HBans.Day: return "RWUAM_BAN_USER_DAY";
                case HBans.Hour: return "RWUAM_BAN_USER_HOUR";
                case HBans.Permanent: return "RWUAM_BAN_USER_PERM";
                default: return "RWUAM_BAN_USER_DAY";
            }
        }

        public static int ToInteger(HThemes Theme)
        {
            if ((int)Theme != 8)
                return (int)Theme;
            else
            {
                int Result = new Random(DateTime.Now.Millisecond).Next(8);
                return Result == 1 || Result == 2 ? Result + 2 : Result;
            }
        }
        public static HThemes ToHTheme(int Theme)
        {
            if ((Theme >= 0 && Theme != 1 && Theme != 2 && Theme != 9 && Theme != 10) || (Theme == 29))
                return (HThemes)Theme;
            else
                return HThemes.White;
        }
        public static HThemes ToHTheme(string Theme)
        {
            switch (Theme.ToLower())
            {
                case "w":
                case "white": return HThemes.White;

                case "re":
                case "red": return HThemes.Red;

                case "blu":
                case "blue": return HThemes.Blue;

                case "y":
                case "yellow": return HThemes.Yellow;

                case "g":
                case "green": return HThemes.Green;

                case "bla":
                case "black": return HThemes.Black;

                case "ra":
                case "rand":
                case "random": return HThemes.Random;
                default: return HThemes.White;
            }
        }

        public static string ToString(HGenders Gender)
        {
            return Gender.ToString()[0].ToString();
        }
        public static HGenders ToHGender(string Gender)
        {
            Gender = Gender.ToLower();
            if (Gender == "m" || Gender == "f")
                return (HGenders)Gender.ToUpper()[0];
            else
                return HGenders.Male;
        }

        public static string ToString(HReports Report)
        {
            switch (Report)
            {
                case HReports.Name: return "add_name_report";
                case HReports.Motto: return "add_motto_report";
                case HReports.Room: return "add_room_report";
                default: return "add_name_report";
            }
        }

        public static HHotels ToHotel(this string Hotel)
        {
            Hotel = Hotel.Trim();
            if (Hotel[0] == '.') Hotel = Hotel.Remove(0, 1);
            Hotel = Hotel.Replace(".", "_");
            HHotels Output = HHotels.com;
            Enum.TryParse<HHotels>(Hotel, out Output);
            return Output;
        }
        public static string ToDomain(this HHotels Hotel)
        {
            return Hotel.ToString().Replace("_", ".");
        }
        public static string ToHost(this HHotels Hotel, bool HTTPS = false)
        {
            return (HTTPS ? "https://www.habbo." : "http://www.habbo.") + Hotel.ToDomain();
        }

        public static string GetChild(this string Body, string Parent, bool withParent = false)
        {
            return Body.Substring(Body.IndexOf(Parent) + (!withParent ? Parent.Length : 0)).Trim();
        }
        public static string GetChild(this string Body, string Parent, char SplitBy, int Position, bool withSkin = false)
        {
            return (((withSkin ? Parent : string.Empty) + Body.Substring(Body.IndexOf(Parent) + Parent.Length).Split(SplitBy)[Position] + (withSkin ? SplitBy.ToString() : string.Empty))).Trim();
        }
        public static string GetChild(this string Body, string Parent, string SplitBy, int Position, bool withSkin = false)
        {
            return (((withSkin ? Parent : string.Empty) + Body.Substring(Body.IndexOf(Parent) + Parent.Length).Split(SplitBy.ToCharArray())[Position] + (withSkin ? SplitBy : string.Empty))).Trim();
        }

        public static void ClearCache()
        {
            DirectoryInfo ICDI = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.InternetCache));
            foreach (FileInfo File in ICDI.GetFiles())
                try { File.Delete(); }
                catch { continue; }

            foreach (DirectoryInfo Folder in ICDI.GetDirectories())
                try { Folder.Delete(true); }
                catch { continue; }
        }
        public static HSession ToHSession(string Account, char Delimiter = ':')
        {
            if (Account.Contains(Delimiter))
            {
                if (Account.Split(Delimiter).Length == 3)
                {
                    string[] Credentials = Account.Split(Delimiter);
                    if (!string.IsNullOrEmpty(Credentials[0]) && !string.IsNullOrEmpty(Credentials[1]) && !string.IsNullOrEmpty(Credentials[2]))
                    {
                        if (Credentials[0].Contains("@"))
                        {
                            if (isValidHotel(Credentials[2]))
                                return new HSession(Credentials[0], Credentials[1], Credentials[2].ToHotel());
                            else
                                throw new Exception(string.Format("Unable to convert ('{0}') to a valid Sulakore.HHotels object.", Credentials[2]));
                        }
                        else throw new Exception("Incorrect email format, email must contain '@'.");
                    }
                    else throw new Exception(string.Format("Account string contains empty arguments, must contain: Email{0}Password{0}Hotel", Delimiter));
                }
                else throw new Exception(string.Format("Expected formatted account string to hold '3' arguments, instead found '{0}'.", Account.Split(Delimiter).Length));
            }
            else throw new Exception("Unable to find the specified delimiter in the formatted account string.");

        }

        internal static bool isValidHotel(string Hotel)
        {
            Hotel = Hotel.Trim();
            if (Hotel[0] == '.')
                Hotel = Hotel.Remove(0, 1);
            Hotel = Hotel.Replace(".", "_");
            return Enum.IsDefined(typeof(HHotels), Hotel.ToLower());
        }
        internal static string FormatCookie(this Cookie C)
        {
            return string.Format("{0}={1}", C.Name, C.Value);
        }
        internal static string AsCookies(this CookieContainer CC, Uri Host)
        {
            string CCs = string.Empty;
            foreach (Cookie C in CC.GetCookies(Host))
                CCs += C.FormatCookie() + ";";
            return CCs;
        }
        internal static string AsCookies(this CookieContainer CC, string Host)
        {
            string CCs = string.Empty;
            foreach (Cookie C in CC.GetCookies(new Uri(Host)))
                CCs += C.FormatCookie() + ";";
            return CCs;
        }
        internal static string AsCookies(this CookieCollection CC, char Delimiter = ',', bool RemoveEnd = true)
        {
            string CCs = string.Empty;
            foreach (Cookie C in CC)
                CCs += C.FormatCookie() + Delimiter.ToString();
            return RemoveEnd && CCs.Length > 0 ? CCs.Remove(CCs.Length - 1, 1) : CCs;
        }
        #endregion
    }
}
