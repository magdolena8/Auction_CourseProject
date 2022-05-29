using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;

namespace Lab_6.Model
{
    public static partial class UserDataWorker
    {
        private static USERS _currentUser;
        public static USERS CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }
        public static ObservableCollection<USERS> GetAllUsers()
        {
            using (AUCTION_DBEntities db = new AUCTION_DBEntities())
            {

                ObservableCollection<USERS> usersCollection = new ObservableCollection<USERS>(db.USERS.ToList());
                return usersCollection;
            }
        }

        public static USERS RegisterUser(USERS user)
        {
            using (AUCTION_DBEntities db = new AUCTION_DBEntities())
            {
                bool isUserExist = db.USERS.Any(el => el.LOGIN_USER == user.LOGIN_USER);

                if (!isUserExist)
                {
                    try
                    {
                        user.USER_TYPE = "user";
                        db.USERS.Add(user);
                        db.SaveChanges();
                        USERS res = db.USERS.Where(u => u.LOGIN_USER == user.LOGIN_USER).Single();
                        UserDataWorker.CurrentUser = res;
                        return res;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
                else return null;
            }
        }
        public static USERS LogInUser(USERS user)
        {
            using (AUCTION_DBEntities db = new AUCTION_DBEntities())
            {
                try
                {
                    USERS dbUser = (from u in db.USERS
                                    where ((u.LOGIN_USER == user.LOGIN_USER) && (u.PASSWORD_USER == user.PASSWORD_USER))
                                    select u).Single();
                    CurrentUser = dbUser;
                    return dbUser;
                }
                catch { return null; }

            }
        }

        public static int GetUserGoodsCount(USERS user)
        {
            using (AUCTION_DBEntities db = new AUCTION_DBEntities())
            {
                int count = (from p in db.PRODUCTS
                             where p.OWNER_ID == user.ID_USER
                             select p).Count();
                return count;
            }
        }
        public static string GetUserAvatarLink(USERS user)
        {
            using (AUCTION_DBEntities db = new AUCTION_DBEntities())
            {
                try
                {
                    IMAGES img = db.IMAGES.Where(i => i.ID_IMAGE == user.AVATAR_USER).Single();
                    return img.LINK ?? null;
                }
                catch
                {
                    return null;
                }
            }
        }
        public static string SetUserAvatarLink(USERS user, string newLink)
        {
            using (AUCTION_DBEntities db = new AUCTION_DBEntities())
            {
                try
                {
                    if (user.AVATAR_USER == null)
                    {
                        IMAGES newImg = new IMAGES();
                        newImg.LINK = newLink;
                        IMAGES dbImg = db.IMAGES.Add(newImg);
                        USERS dbUser = db.USERS.Where(u => u.ID_USER == user.ID_USER).Single();
                        dbUser.AVATAR_USER = dbImg.ID_IMAGE;
                        db.SaveChanges();
                        return dbImg.LINK;

                    }
                    else
                    {
                        IMAGES img = db.IMAGES.Where(i => i.ID_IMAGE == user.AVATAR_USER).Single();
                        img.LINK = newLink;
                        db.SaveChanges();
                        return img.LINK ?? null;
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        public static USERS EditUser(int userID, USERS user)
        {
            using (AUCTION_DBEntities db = new AUCTION_DBEntities())
            {
                try
                {
                    USERS dbUser = (from u in db.USERS
                                    where u.ID_USER == userID
                                    select u).Single();
                    dbUser = user;
                    return dbUser;
                }
                catch
                {
                    return null;
                }
            }
        }

    }
}