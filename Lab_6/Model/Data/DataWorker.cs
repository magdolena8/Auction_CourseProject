using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Text.RegularExpressions;

namespace Lab_6.Model
{
    public static partial class DataWorker
    {


        //получить отсортированные продукты
        //получить все продукты
        public static ObservableCollection<PRODUCTS> GetAllProducts() //все продукты, доступые пользователю (исключая свои товары)
        {
            using (AUCTION_DBEntities db = new AUCTION_DBEntities())
            {
                if (UserDataWorker.CurrentUser == null)
                {
                    var guestProducts = (from p in db.PRODUCTS
                                         where p.ENDTIME >= DateTime.Now
                                         select p);
                    ObservableCollection<PRODUCTS> gurstProductsCollection = new ObservableCollection<PRODUCTS>(guestProducts);
                    return gurstProductsCollection;
                }
                else
                {
                    var selector = (from p in db.PRODUCTS
                                    where p.OWNER_ID != UserDataWorker.CurrentUser.ID_USER
                                    where p.ENDTIME >= DateTime.Now
                                    select p);
                    ObservableCollection<PRODUCTS> productsCollection = new ObservableCollection<PRODUCTS>(selector);
                    return productsCollection;
                }
            }
        }
        public static ObservableCollection<USERS> GetAllUsers()
        {
            using (AUCTION_DBEntities db = new AUCTION_DBEntities())
            {
                ObservableCollection<USERS> usersCollection = new ObservableCollection<USERS>(db.USERS.ToList());
                return usersCollection;
            }
        }
        public static ObservableCollection<PRODUCTS> GetUserBasket(USERS user)
        {
            using (AUCTION_DBEntities db = new AUCTION_DBEntities())
            {
                try
                {
                    var selector = from p in db.PRODUCTS
                                   where p.USERS2.Any(u => u.ID_USER == user.ID_USER)
                                   select p;
                    //var selector1 = db.PRODUCTS.Any(x =>)
                    ObservableCollection<PRODUCTS> userBasket = new ObservableCollection<PRODUCTS>(selector);
                    return userBasket;
                }
                catch (Exception ex) { return null; }
            }
        }
        public static PRODUCTS AddProductToBasket(int user_id, PRODUCTS product)
        {

            using (AUCTION_DBEntities db = new AUCTION_DBEntities())
            {
                USERS dbUser = db.USERS.Where(u => u.ID_USER == user_id).Include(c => c.PRODUCTS2).Single();
                PRODUCTS dbProd = db.PRODUCTS.Where(u => u.ID_PRODUCT == product.ID_PRODUCT).Include(c => c.USERS2).Single();
                //userBasket.Add(product);
                //userBasket.FirstOrDefault.
                //product.USERS2.Add(user);
                //qwe.PRODUCTS2.Attach(product);
                dbUser.PRODUCTS2.Add(dbProd);
                db.SaveChanges();
                return dbProd;
            }
        }

        public static PRODUCTS CreateProduct(PRODUCTS product)
        {
            using (AUCTION_DBEntities db = new AUCTION_DBEntities())
            {
                bool isProductExist = db.PRODUCTS.Any(el => el.ID_PRODUCT == product.ID_PRODUCT);   // Бесполезно
                if (!isProductExist)
                {                    
                    IMAGES img = new IMAGES();
                    img.LINK = product.PRODUCT_IMAGE_LINK; ////////////////////////////////??????
                    IMAGES q = db.IMAGES.Add(img);
                    db.SaveChanges();

                    product.OWNER_ID = UserDataWorker.CurrentUser.ID_USER;
                    product.BIDS = 0;
                    product.IMAGE_PROD = q.ID_IMAGE;

                    db.PRODUCTS.Add(product);
                    db.SaveChanges();
                    //product.IMAGES.LINK = q.LINK;
                    //product.PRODUCT_IMAGE_LINK = q.LINK;
                    return product;
                }
                else return null;
            }
        }
        public static bool DeleteProduct(PRODUCTS product)
        {
            using (AUCTION_DBEntities db = new AUCTION_DBEntities())
            {
                try
                {
                    PRODUCTS tempProduct = db.PRODUCTS.FirstOrDefault(el => el.ID_PRODUCT == product.ID_PRODUCT);
                    var qwe = db.PRODUCTS.Where(el => el.ID_PRODUCT == product.ID_PRODUCT).Include(c => c.USERS).Include(x => x.USERS1).Include(v => v.USERS2).Single();
                    
                    //tempProduct = null;
                    db.PRODUCTS.Remove(qwe);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex) { return false; }
            }
        }
        public static PRODUCTS EditProduct(int IDProduct, PRODUCTS newProduct)
        {
            using (AUCTION_DBEntities db = new AUCTION_DBEntities())
            {
                PRODUCTS tempProduct = db.PRODUCTS.FirstOrDefault(el => el.ID_PRODUCT == IDProduct);
                if (tempProduct.ID_PRODUCT != newProduct.ID_PRODUCT) return null;
                if (IDProduct != newProduct.ID_PRODUCT) return null;
                tempProduct.TITLE = newProduct.TITLE;
                tempProduct.PRICE = newProduct.PRICE;
                tempProduct.ENDTIME = newProduct.ENDTIME;
                tempProduct.BIDS = newProduct.BIDS;
                tempProduct.PROD_DESCRIPTION = newProduct.PROD_DESCRIPTION;
                tempProduct.TYPE_PROD = newProduct.TYPE_PROD;
                IMAGES img = db.IMAGES.Where(e => e.ID_IMAGE == tempProduct.IMAGE_PROD).Single();
                img.LINK = newProduct.PRODUCT_IMAGE_LINK;
                //tempProduct.PRODUCT_IMAGE_LINK = newProduct.PRODUCT_IMAGE_LINK;
                //SetImageLinkToProduct(tempProduct, tempProduct.PRODUCT_IMAGE_LINK);
                //temproduct.IMAGES = newProduct.IMAGES;
                //temproduct.PRODUCT_IMAGE_LINK = newProduct.PRODUCT_IMAGE_LINK;
                //IMAGES productImage = db.IMAGES.FirstOrDefault(el => el.PRODUCTS.Contains(PRODUCTS) == IDProduct);
                //var productImageLink = (from p in db.PRODUCTS
                //                        join i in db.IMAGES on p.IMAGE_PROD equals i.ID_IMAGE
                //                        select i.LINK).Take(1).Single();
                //productImageLink = newProduct.PRODUCT_IMAGE_LINK;

                db.SaveChanges();
                return tempProduct;
            }
        }

        public static PRODUCTS PlaceBid(PRODUCTS product, decimal bid, USERS user)
        {
            //string result = "Invalid Bid";
            if (product != null)
            {
                using (AUCTION_DBEntities db = new AUCTION_DBEntities())
                {
                    PRODUCTS tempProduct = db.PRODUCTS.FirstOrDefault(el => el.ID_PRODUCT == product.ID_PRODUCT);
                    if (bid <= tempProduct.PRICE) return null;
                    tempProduct.PRICE = bid;
                    tempProduct.TOP_BID_USER_ID = user.ID_USER;
                    tempProduct.BIDS += 1;
                    db.SaveChanges();
                    AddProductToBasket(user.ID_USER, product);
                    return tempProduct;
                    //result = "Bid placed";
                }
            }
            return null;
        }

        public static ObservableCollection<PRODUCTS> FindProducts(string str)
        {
            using (AUCTION_DBEntities db = new AUCTION_DBEntities())
            {
                if (!string.IsNullOrEmpty(str))
                {
                    Regex qwe = new Regex(str);
                    try
                    {
                        var userResult = from i in db.PRODUCTS
                                         where i.OWNER_ID != UserDataWorker.CurrentUser.ID_USER
                                         where (i.TITLE.Contains(str) || i.PROD_DESCRIPTION.Contains(str) || i.TYPE_PROD.Contains(str))
                                         select i;

                        ObservableCollection<PRODUCTS> x = new ObservableCollection<PRODUCTS>(userResult);
                        return x;
                    }
                    catch (Exception ex)
                    {
                        var guestResult = from i in db.PRODUCTS
                                         where (i.TITLE.Contains(str) || i.PROD_DESCRIPTION.Contains(str) || i.TYPE_PROD.Contains(str))
                                         select i;

                        ObservableCollection<PRODUCTS> x = new ObservableCollection<PRODUCTS>(guestResult);
                        return x;
                    }
                }
                else return GetAllProducts();
            }
        }

        //получение ссылок на картинке по PRODUCT_ID
        //public static List<string> GetImageLinksByProdImgID(int? prodImgID)
        //{
        //    using (AUCTION_DBEntities db = new AUCTION_DBEntities())
        //    {
        //        //List<string> result = new List<string>();
        //        List<string> result = (List<String>)
        //                from p in db.PRODUCTS where p.ID_PRODUCT == prodImgID
        //                join i in db.IMAGES on p.IMAGE_PROD equals i.ID_IMAGE
        //                select i.LINK;
        //        return result;
        //    }
        //    return null;
        //}


        public static string GetImageLinkByProdImgID(int? prodImgID)
        {
            using (AUCTION_DBEntities db = new AUCTION_DBEntities())
            {
                //List<string> result = new List<string>();
                try
                {
                    var result =
                            (from p in db.PRODUCTS
                             where p.IMAGE_PROD == prodImgID
                             join i in db.IMAGES on p.IMAGE_PROD equals i.ID_IMAGE
                             select i.LINK).Take(1).Single();

                    return result;
                }
                catch
                {
                    return null;
                }
            }
        }
        public static string SetImageLinkToProduct(PRODUCTS product, string link)
        {
            using (AUCTION_DBEntities db = new AUCTION_DBEntities())
            {
                try
                {
                    PRODUCTS prod = db.PRODUCTS.FirstOrDefault(el => el.ID_PRODUCT == product.ID_PRODUCT);
                    IMAGES image = (from i in db.IMAGES
                                    where i.ID_IMAGE == product.IMAGE_PROD
                                    select i).Take(1).Single();
                    if (image != null)
                    {
                        image.LINK = link;
                    }
                    else
                    {
                        IMAGES newImg = new IMAGES();
                        newImg.LINK = link;
                        IMAGES q = db.IMAGES.Add(newImg);
                        prod.IMAGE_PROD = q.ID_IMAGE;
                        db.SaveChanges();
                    }
                    return link;
                }
                catch
                {
                    return null;
                }
            }
        }
        public static ObservableCollection<PRODUCTS> GetUserProducts(USERS user)
        {
            using (AUCTION_DBEntities db = new AUCTION_DBEntities())
            {
                try
                {

                    var prod = (from p in db.PRODUCTS
                                where p.OWNER_ID == user.ID_USER
                                select p).ToList();
                    ObservableCollection<PRODUCTS> result = new ObservableCollection<PRODUCTS>(prod);
                    return result;
                }
                catch { return null; }
            }
        }


    }
}
