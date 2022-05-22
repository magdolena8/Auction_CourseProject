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
                    //USERS newUser = db.USERS.Add(user);
                    //IMAGES img = new IMAGES();
                    //img.LINK = product.PRODUCT_IMAGE_LINK;
                    //IMAGES q = db.IMAGES.Add(img);
                    //db.SaveChanges();

                    //db.PRODUCTS.Add(product);
                    //db.SaveChanges();
                    //product.PRODUCT_IMAGE_LINK = q.LINK;
                    //return product;
                    return null;
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
        
        


    }
}