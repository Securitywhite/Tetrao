/*
 *  Author: OldNutMan/ArachisH
 *  Desc: Class to manage Login
 */
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;

namespace Tetrao___Client
{
    /// <summary>
    /// Provides methods for simulating an active habbo session, as if you were in a browser. Does not provide methods for in-game simulation, besides the HSession.NavigateRoom method.
    /// </summary>
    public class HSession
    {
        #region Properties
        /// <summary>
        /// Gets the source of the page.
        /// </summary>
        /// <param name="Page">The page of the source you want to grab.</param>
        /// <returns></returns>
        public string this[HPages Page]
        {
            get
            {
                string Body = null;
                using (WebClient WC = new WebClient())
                {
                    WC.Headers["Cookie"] = Cookies.AsCookies(Hotel.ToHost());
                    WC.Headers[HttpRequestHeader.UserAgent] = SKore.ChromeAgent;
                    switch (Page)
                    {
                        case HPages.Me:
                            {
                                Body = WC.DownloadString(string.Format("{0}/me", Hotel.ToHost()));
                                break;
                            }
                        case HPages.Home:
                            {
                                Body = WC.DownloadString(string.Format("{0}/home/{1}", Hotel.ToHost(), PlayerName));
                                break;
                            }
                        case HPages.Client:
                            {
                                Body = WC.DownloadString(string.Format("{0}/client", Hotel.ToHost()));
                                SWFBuild = Body.GetChild("/gordon/", '/', 0);
                                PlayerName_ = Body.GetChild("var habboName = \"", '\"', 0);
                                SSOTicket = Body.GetChild("\"sso.ticket\" : \"", '\"', 0);
                                GameHost = Body.GetChild("\"connection.info.host\" : \"", '\"', 0);
                                GamePort = int.Parse(Body.GetChild("\"connection.info.port\" : \"", '\"', 0).Split(',')[0]);
                                GameIP = Dns.GetHostAddresses(GameHost)[0].ToString();
                                Body = Body.Replace("\"\\//habboo-a.akamaihd.net", "\"http://habboo-a.akamaihd.net");
                                Body = Body.Replace("\"\\//images-eussl.habbo.com", "\"http://images-eussl.habbo.com");
                                SWFLocation = Body.GetChild("\"flash.client.url\" : \"", '\"', 0);
                                Body = Body.Replace("var habboReqPath = \"\";", string.Format("var habboReqPath = \"{0}\";", Hotel.ToHost()));
                                break;
                            }
                        case HPages.Profile:
                            {
                                Body = WC.DownloadString(string.Format("{0}/profile", Hotel.ToHost(true)));
                                break;
                            }
                        case HPages.IDAvatars:
                            {
                                Body = WC.DownloadString(string.Format("{0}/identity/avatars", Hotel.ToHost()));
                                break;
                            }
                        case HPages.IDSettings:
                            {
                                Body = WC.DownloadString(string.Format("{0}/identity/settings", Hotel.ToHost()));
                                break;
                            }
                    }
                }
                return Body;
            }
        }

        /// <summary>
        /// Gets the email of the current session.
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Gets the password of the current session.
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// Gets the Sulakore.HHotels object of the current session.
        /// </summary>
        public HHotels Hotel { get; private set; }

        /// <summary>
        /// Gets the PlayerID of the current session.
        /// </summary>
        public int PlayerID { get; private set; }

        /// <summary>
        /// Gets the cookies of the current session.
        /// </summary>
        public CookieContainer Cookies { get; private set; }

        public string SWFLocation { get; private set; }

        public string SSOTicket { get; private set; }
        public string SWFBuild { get; private set; }
        public int GamePort { get; private set; }
        public string GameHost { get; private set; }
        public string GameIP { get; private set; }

        private string CSRFToken_;
        /// <summary>
        /// Gets the token that is used to properly update the player profile, and sign out.
        /// </summary>
        public string CSFRToken
        {
            get
            {
                if (!string.IsNullOrEmpty(CSRFToken_)) return CSRFToken_;
                return (CSRFToken_ = this[HPages.Profile].GetChild("<meta name=\"csrf-token\" content=\"", '\"', 0));
            }
        }

        private string URLToken_;
        /// <summary>
        /// Gets the token that is used to properly update the player profile.
        /// </summary>
        public string URLToken
        {
            get
            {
                if (!string.IsNullOrEmpty(URLToken_)) return URLToken_;
                return (URLToken_ = this[HPages.Profile].GetChild("name=\"urlToken\" value=\"", '\"', 0));
            }
        }

        private int Age_;
        /// <summary>
        /// Gets the age of the current player in the session.
        /// </summary>
        public int Age
        {
            get
            {
                if (Age_ != 0) return Age_;
                return Age_ = int.Parse(this[HPages.Me].GetChild("var ad_key_value = \"", '\"', 0).GetChild("kvage=", ';', 0));
            }
        }

        private string PlayerName_;
        /// <summary>
        /// Gets the name of the currently selected player.
        /// </summary>
        public string PlayerName
        {
            get
            {
                if (!string.IsNullOrEmpty(PlayerName_)) return PlayerName_;
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/rd/{1}", Hotel.ToHost(), PlayerID));
                Request.Headers["Cookie"] = SKore.IPCookie;
                Request.UserAgent = SKore.ChromeAgent;
                Request.AllowAutoRedirect = false;
                Request.Method = "GET";

                using (HttpWebResponse Response = (HttpWebResponse)Request.GetResponse())
                    return PlayerName_ = Uri.UnescapeDataString(Response.Headers["Location"].Split('/')[4]);
            }
        }

        /// <summary>
        /// Gets the gender of the currently selected player.
        /// </summary>
        public HGenders Gender
        {
            get { return SKore.ToHGender(this[HPages.Me].GetChild("var ad_key_value = \"", '\"', 0).GetChild("kvgender=", ';', 0)); }
        }

        /// <summary>
        /// Gets a System.Boolean value that indicates whether the session is still logged in.
        /// </summary>
        public bool isLoggedIn
        {
            get
            {
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/me", Hotel.ToHost()));
                Request.Headers["Cookie"] = Cookies.AsCookies(Hotel.ToHost());
                Request.UserAgent = SKore.ChromeAgent;
                Request.AllowAutoRedirect = false;
                Request.Method = "GET";

                using (HttpWebResponse Response = (HttpWebResponse)Request.GetResponse())
                    return string.IsNullOrEmpty(Response.Headers["Location"]);
            }
        }
        #endregion

        #region Constructor/Initializer
        /// <summary>
        /// Creates a new instance of the Sulakore.HSession class with the specified credentials.
        /// </summary>
        /// <param name="Email">The email of the account.</param>
        /// <param name="Password">The password of the account.</param>
        /// <param name="Hotel">The hotel in-which the account is registered/located on.</param>
        public HSession(string Email, string Password, HHotels Hotel)
        {
            ServicePointManager.DefaultConnectionLimit = int.MaxValue;
            Initialize();

            this.Email = Email;
            this.Password = Password;
            this.Hotel = Hotel;
        }
        private void Initialize()
        {
            CSRFToken_ = URLToken_ = string.Empty;
            (Cookies = new CookieContainer()).SetCookies(new Uri(Hotel.ToHost()), SKore.IPCookie);
        }
        #endregion

        #region Login
        private delegate bool Login_1();
        private Login_1 LoginCallback_1;

        /// <summary>
        /// Attempts to authenticate the session with the credentials that have been initialized with this instance.
        /// </summary>
        /// <returns></returns>
        public bool Login()
        {
            Initialize();
            #region Step #1: Authentication
            byte[] PostData = Encoding.Default.GetBytes(string.Format("credentials.username={0}&credentials.password={1}", Email, Password));
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/account/submit", Hotel.ToHost(true)));
            Request.ContentType = "application/x-www-form-urlencoded";
            Request.UserAgent = SKore.ChromeAgent;
            Request.AllowAutoRedirect = false;
            Request.CookieContainer = Cookies;
            Request.Method = "POST";

            Stream DataStream = Request.GetRequestStream();
            DataStream.Write(PostData, 0, PostData.Length);
            DataStream.Dispose();

            HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
            Cookies.SetCookies(new Uri(Hotel.ToHost()), Response.Cookies.AsCookies());

            StreamReader SR = new StreamReader(Response.GetResponseStream());
            string Body = SR.ReadToEnd();
            SR.Dispose(); Response.Close();
            #endregion

            if (Body.Contains("useOrCreateAvatar"))
            {
                #region Step #2: Player Selection
                PlayerID = int.Parse(Body.GetChild("useOrCreateAvatar/", '?', 0));
                Request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/identity/useOrCreateAvatar/{1}?next=", Hotel.ToHost(), PlayerID));
                Request.UserAgent = SKore.ChromeAgent;
                Request.CookieContainer = Cookies;
                Request.AllowAutoRedirect = false;
                Request.Method = "GET";

                Response = (HttpWebResponse)Request.GetResponse();
                Cookies.SetCookies(new Uri(Hotel.ToHost()), Response.Cookies.AsCookies());
                string RedirectTo = Response.Headers["Location"];
                Response.Close();
                #endregion

                #region Step #3: Manual Redirect
                Request = (HttpWebRequest)WebRequest.Create(RedirectTo);
                Request.UserAgent = SKore.ChromeAgent;
                Request.CookieContainer = Cookies;
                Request.AllowAutoRedirect = false;
                Request.Method = "GET";

                Response = (HttpWebResponse)Request.GetResponse();
                Cookies.SetCookies(new Uri(Hotel.ToHost()), Response.Cookies.AsCookies());
                #endregion

                if (RedirectTo.EndsWith("/me"))
                    return true;
                else
                {
                    SR = new StreamReader(Response.GetResponseStream());
                    Body = SR.ReadToEnd();
                    SR.Dispose(); Response.Close();

                    if (Body.Contains("/account/updateIdentityProfileTerms"))
                    {
                        #region Step #1(TOS Bypass): Accept TOS
                        PostData = Encoding.Default.GetBytes("termsSelection=true");
                        Request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/account/updateIdentityProfileTerms", Hotel.ToHost(true)));
                        Request.ContentType = "application/x-www-form-urlencoded";
                        Request.Headers["Origin"] = Hotel.ToHost(true);
                        Request.UserAgent = SKore.ChromeAgent;
                        Request.AllowAutoRedirect = false;
                        Request.CookieContainer = Cookies;
                        Request.Referer = RedirectTo;
                        Request.Method = "POST";

                        DataStream = Request.GetRequestStream();
                        DataStream.Write(PostData, 0, PostData.Length);
                        DataStream.Dispose();

                        Response = (HttpWebResponse)Request.GetResponse();
                        Cookies.SetCookies(new Uri(Hotel.ToHost()), Response.Cookies.AsCookies());
                        Response.Close();
                        #endregion
                    }
                    else if (Body.Contains("/account/updateIdentityProfileEmail"))
                    {
                        #region Step #1(EV Bypass): Skip Email Change
                        PostData = Encoding.Default.GetBytes("email=&skipEmailChange=true");
                        Request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/account/updateIdentityProfileEmail", Hotel.ToHost(true)));
                        Request.ContentType = "application/x-www-form-urlencoded";
                        Request.Headers["Origin"] = Hotel.ToHost(true);
                        Request.UserAgent = SKore.ChromeAgent;
                        Request.AllowAutoRedirect = false;
                        Request.CookieContainer = Cookies;
                        Request.Referer = RedirectTo;
                        Request.Method = "POST";

                        DataStream = Request.GetRequestStream();
                        DataStream.Write(PostData, 0, PostData.Length);
                        DataStream.Dispose();

                        Response = (HttpWebResponse)Request.GetResponse();
                        Cookies.SetCookies(new Uri(Hotel.ToHost()), Response.Cookies.AsCookies());
                        Response.Close();
                        #endregion
                    }

                    if (Body.Contains("/account/updateIdentityProfileTerms") || Body.Contains("/account/updateIdentityProfileEmail"))
                    {
                        #region Step #2(EV Bypass): Reselect Player
                        Request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/identity/useOrCreateAvatar/{1}?disableFriendLinking=false&combineIdentitiesSelection=2&next=&selectedAvatarId=", Hotel.ToHost(), PlayerID));
                        Request.UserAgent = SKore.ChromeAgent;
                        Request.CookieContainer = Cookies;
                        Request.AllowAutoRedirect = false;
                        Request.Method = "GET";

                        Response = (HttpWebResponse)Request.GetResponse();
                        Cookies.SetCookies(new Uri(Hotel.ToHost()), Response.Cookies.AsCookies());
                        RedirectTo = Response.Headers["Location"];
                        Response.Close();
                        #endregion

                        #region Step #3(EV Bypass): Manual Redirect
                        Request = (HttpWebRequest)WebRequest.Create(RedirectTo);
                        Request.UserAgent = SKore.ChromeAgent;
                        Request.CookieContainer = Cookies;
                        Request.AllowAutoRedirect = false;
                        Request.Method = "GET";

                        Response = (HttpWebResponse)Request.GetResponse();
                        Cookies.SetCookies(new Uri(Hotel.ToHost()), Response.Cookies.AsCookies());
                        Response.Close();
                        #endregion
                        return true;
                    }
                }
            }
            Initialize();
            return false;
        }
        /// <summary>
        /// Begins an asynchronous operation to attempt to authenticate the session with the credentials that have been initialized with this instance.
        /// </summary>
        /// <param name="callback">The System.AsyncCallback delegate.</param>
        /// <param name="state">The state object for this operation.</param>
        /// <returns></returns>
        public IAsyncResult BeginLogin(AsyncCallback callback, object state)
        {
            if (LoginCallback_1 == null)
                LoginCallback_1 = Login;

            return LoginCallback_1.BeginInvoke(callback, state);
        }
        /// <summary>
        /// Ends an asynchronous operation for an authentication attempt.
        /// </summary>
        /// <param name="result">The pending operation for an attempted authenticate.</param>
        /// <returns></returns>
        public bool EndLogin(IAsyncResult result)
        {
            return LoginCallback_1.EndInvoke(result);
        }
        #endregion

        #region Report
        /// <summary>
        /// Creates a report against the specified object ID.
        /// </summary>
        /// <param name="ObjectID">The ID of the object to be reported. Limited To: PlayerID/RoomID</param>
        /// <param name="Report">The report type to create.</param>
        public void Report(int ObjectID, HReports Report)
        {
            using (WebClient WC = new WebClient())
            {
                WC.Headers["X-App-Key"] = CSFRToken;
                WC.Headers["User-Agent"] = SKore.ChromeAgent;
                WC.Headers["Cookie"] = Cookies.AsCookies(Hotel.ToHost());
                WC.DownloadStringAsync(new Uri(string.Format("{0}/mod/{1}?objectId={2}", Hotel.ToHost(), SKore.ToString(Report), ObjectID)));
            }
        }
        /// <summary>
        /// Creates a report against the specified player.
        /// </summary>
        /// <param name="PlayerName">The name of the player to report.</param>
        /// <param name="Report">The report type to create. Limited To: Motto/Name</param>
        public void Report(string PlayerName, HReports Report)
        {
            using (WebClient WC = new WebClient())
            {
                WC.Headers["X-App-Key"] = CSFRToken;
                WC.Headers["User-Agent"] = SKore.ChromeAgent;
                WC.Headers["Cookie"] = Cookies.AsCookies(Hotel.ToHost());
                WC.DownloadStringAsync(new Uri(string.Format("{0}/mod/{1}?objectId={2}", Hotel.ToHost(), SKore.ToString(Report), SKore.GetPlayerID(PlayerName, Hotel))));
            }
        }
        #endregion

        #region AddFriend
        private delegate bool AddFriend_1(int i);
        private delegate bool AddFriend_2(string s);
        private AddFriend_1 AddFriendCallback_1;
        private AddFriend_2 AddFriendCallback_2;

        /// <summary>
        /// Sends a friend request to the specified player ID.
        /// </summary>
        /// <param name="PlayerID">The ID of the player to add.</param>
        public bool AddFriend(int PlayerID)
        {
            try
            {
                using (WebClient WC = new WebClient())
                {
                    WC.Headers["X-App-Key"] = CSFRToken;
                    WC.Headers["User-Agent"] = SKore.ChromeAgent;
                    WC.Headers["Cookie"] = Cookies.AsCookies(Hotel.ToHost());
                    WC.DownloadString(new Uri(string.Format("{0}/myhabbo/friends/add?accountId={1}", Hotel.ToHost(), PlayerID)));
                }
                return true;
            }
            catch { return false; }
        }
        public IAsyncResult BeginAddFriend(int PlayerID, AsyncCallback callback, object state)
        {
            if (AddFriendCallback_1 == null)
                AddFriendCallback_1 = AddFriend;

            return AddFriendCallback_1.BeginInvoke(PlayerID, callback, state);
        }

        /// <summary>
        /// Sends a friend request to the specified player.
        /// </summary>
        /// <param name="PlayerName">The name of the player to add.</param>
        public bool AddFriend(string PlayerName)
        {
            try
            {
                using (WebClient WC = new WebClient())
                {
                    WC.Headers["X-App-Key"] = CSFRToken;
                    WC.Headers["User-Agent"] = SKore.ChromeAgent;
                    WC.Headers["Cookie"] = Cookies.AsCookies(Hotel.ToHost());
                    WC.DownloadString(new Uri(string.Format("{0}/myhabbo/friends/add?accountId={1}", Hotel.ToHost(), SKore.GetPlayerID(PlayerName, Hotel))));
                }
                return true;
            }
            catch { return false; }
        }
        public IAsyncResult BeginAddFriend(string PlayerName, AsyncCallback callback, object state)
        {
            if (AddFriendCallback_2 == null)
                AddFriendCallback_2 = AddFriend;

            return AddFriendCallback_2.BeginInvoke(PlayerName, callback, state);
        }

        public bool EndAddFriend(IAsyncResult result)
        {
            try { return AddFriendCallback_1.EndInvoke(result); }
            catch { return AddFriendCallback_2.EndInvoke(result); }
        }
        #endregion

        #region RemoveFriend
        private delegate void RemoveFriend_1(int i);
        private delegate void RemoveFriend_2(string s);
        private RemoveFriend_1 RemoveFriendCallback_1;
        private RemoveFriend_2 RemoveFriendCallback_2;

        /// <summary>
        /// Removes the player from your friends list that is connected to the specified player ID.
        /// </summary>
        /// <param name="PlayerID">The player ID of the player you wish to remove.</param>
        public void RemoveFriend(int PlayerID)
        {
            using (WebClient WC = new WebClient())
            {
                WC.Headers["X-App-Key"] = CSFRToken;
                WC.Headers["User-Agent"] = SKore.ChromeAgent;
                WC.Headers["Cookie"] = Cookies.AsCookies(Hotel.ToHost());
                WC.DownloadString(new Uri(string.Format("{0}/friendmanagement/ajax/deletefriends?friendId={1}&pageSize=30", Hotel.ToHost(true), PlayerID)));
            }
        }
        public IAsyncResult BeginRemoveFriend(int PlayerID, AsyncCallback callback, object state)
        {
            if (RemoveFriendCallback_1 == null)
                RemoveFriendCallback_1 = RemoveFriend;

            return RemoveFriendCallback_1.BeginInvoke(PlayerID, callback, state);
        }

        /// <summary>
        /// Removes the specified player from your friends list.
        /// </summary>
        /// <param name="PlayerName">The name of the player you wish to remove.</param>
        public void RemoveFriend(string PlayerName)
        {
            using (WebClient WC = new WebClient())
            {
                WC.Headers["X-App-Key"] = CSFRToken;
                WC.Headers["User-Agent"] = SKore.ChromeAgent;
                WC.Headers["Cookie"] = Cookies.AsCookies(Hotel.ToHost());
                WC.DownloadString(new Uri(string.Format("{0}/friendmanagement/ajax/deletefriends?friendId={1}&pageSize=30", Hotel.ToHost(true), SKore.GetPlayerID(PlayerName, Hotel))));
            }
        }
        public IAsyncResult BeginRemoveFriend(string PlayerName, AsyncCallback callback, object state)
        {
            if (RemoveFriendCallback_2 == null)
                RemoveFriendCallback_2 = RemoveFriend;

            return RemoveFriendCallback_2.BeginInvoke(PlayerName, callback, state);
        }

        public void EndRemoveFriend(IAsyncResult result)
        {
            try { RemoveFriendCallback_1.EndInvoke(result); }
            catch { RemoveFriendCallback_2.EndInvoke(result); }
        }
        #endregion

        #region SaveOutfit
        private delegate bool SaveOutfit_1(string s, HGenders hg);
        private SaveOutfit_1 SaveOutfitCallback_1;

        /// <summary>
        /// Changes the clothes/gender of the currently selected player.
        /// </summary>
        /// <param name="ClothesID">The clothes ID of the outfit.</param>
        /// <param name="Gender">The desired gender for the player.</param>
        public bool SaveOutfit(string ClothesID, HGenders Gender)
        {
            try
            {
                using (WebClient WC = new WebClient())
                {
                    WC.Headers["X-App-Key"] = CSFRToken;
                    WC.Headers["User-Agent"] = SKore.ChromeAgent;
                    WC.Headers["Cookie"] = Cookies.AsCookies(Hotel.ToHost());
                    WC.DownloadString(new Uri(string.Format("{0}/new-user-reception/save-outfit?figure={1}&gender={2}", Hotel.ToHost(), ClothesID, (char)((int)Gender + 32))));
                    return true;
                }
            }
            catch { return false; }
        }
        public IAsyncResult BeginSaveOutfit(string ClothesID, HGenders Gender, AsyncCallback callback, object state)
        {
            if (SaveOutfitCallback_1 == null) SaveOutfitCallback_1 = SaveOutfit;
            return SaveOutfitCallback_1.BeginInvoke(ClothesID, Gender, callback, state);
        }
        public bool EndSaveOutfit(IAsyncResult result)
        {
            return SaveOutfitCallback_1.EndInvoke(result);
        }
        #endregion

        #region NavigateRoom
        /// <summary>
        /// Loads the specified room via its ID, this is only effective when the current session has the client loaded.
        /// </summary>
        /// <param name="RoomID">The desired room to navigate to.</param>
        public void NavigateRoom(int RoomID)
        {
            using (WebClient WC = new WebClient())
            {
                WC.Headers["X-App-Key"] = CSFRToken;
                WC.Headers["User-Agent"] = SKore.ChromeAgent;
                WC.Headers["Cookie"] = Cookies.AsCookies(Hotel.ToHost());
                WC.DownloadStringAsync(new Uri(string.Format("{0}/components/roomNavigation?targetId={1}&roomType=private&move=true", Hotel.ToHost(), RoomID)));
            }
        }
        #endregion

        #region UpdateProfile
        private delegate bool Update_1(string s1, bool b1, bool b2, bool b3, bool b4, bool b5);
        private Update_1 UpdateCallback_1;

        /// <summary>
        /// Updates the profile of the currently selected player.
        /// </summary>
        /// <param name="Motto">The new motto of the player.</param>
        /// <param name="HomepageVisible">Determines whether other users are able to visit the currently selected player's homepage. (http://www.Habbo-/home/-)</param>
        /// <param name="FriendRequestAllowed">Determines whether other players are allowed to send you friend request.</param>
        /// <param name="ShowOnlineStatus">Determines whether your online status is publicly shown to others, otherwise you will appear offline.</param>
        /// <param name="OfflineMessaging">Determines whether your friends can send you pm's regardless of being offline. (Messages will save)</param>
        /// <param name="FriendsCanFollow">Determines whether your friends will be able to follow you room-to-room.</param>
        public bool UpdateProfile(string Motto, bool HomepageVisible, bool FriendRequestAllowed, bool ShowOnlineStatus, bool OfflineMessaging, bool FriendsCanFollow)
        {
            byte[] PostData = Encoding.Default.GetBytes(string.Format("__app_key={0}&urlToken={1}&tab=2&motto={2}&visibility={3}&friendRequestsAllowed={4}&showOnlineStatus={5}&persistentMessagingAllowed={6}&followFriendMode={7}", CSFRToken, URLToken, Motto, HomepageVisible ? "EVERYONE" : "NOBODY", FriendRequestAllowed.ToString().ToLower(), ShowOnlineStatus.ToString().ToLower(), OfflineMessaging.ToString().ToLower(), FriendsCanFollow == true ? "1" : "0"));
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/profile/profileupdate", Hotel.ToHost(true)));
            Request.Headers["Cookie"] = Cookies.AsCookies(Hotel.ToHost());
            Request.ContentType = "application/x-www-form-urlencoded";
            Request.ContentLength = PostData.Length;
            Request.UserAgent = SKore.ChromeAgent;
            Request.Method = "POST";

            try
            {
                using (Stream DataStream = Request.GetRequestStream())
                    DataStream.Write(PostData, 0, PostData.Length);
                return true;
            }
            catch { return false; }
        }
        public IAsyncResult BeginUpdateProfile(string Motto, bool HomepageVisible, bool FriendRequestAllowed, bool ShowOnlineStatus, bool OfflineMessaging, bool FriendsCanFollow, AsyncCallback callback, object state)
        {
            if (UpdateCallback_1 == null) UpdateCallback_1 = UpdateProfile;
            return UpdateCallback_1.BeginInvoke(Motto, HomepageVisible, FriendRequestAllowed, ShowOnlineStatus, OfflineMessaging, FriendsCanFollow, callback, state);
        }
        public bool EndUpdateProfile(IAsyncResult result)
        {
            return UpdateCallback_1.EndInvoke(result);
        }
        //public override string ToString()
        //{
        //    return string.Format("{0}:{1}:{2}", Email, Password, Hotel.ToDomain());
        //}
        #endregion
    }
}
