using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;
using System.Data;


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
    }
}