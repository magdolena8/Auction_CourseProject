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
    public static partial class DataWorker
    {


        //получить отсортированные продукты
        //получить все продукты
        public static ObservableCollection<PRODUCTS> GetAllProducts() //все продукты, доступые пользователю для полкупки(исключая свою товары)
        {
            using (AUCTION_DBEntities db = new AUCTION_DBEntities())
            {
                var selector = (from p in db.PRODUCTS
                                where p.OWNER_ID != UserDataWorker.CurrentUser.ID_USER
                                select p);
                ObservableCollection<PRODUCTS> productsCollection = new ObservableCollection<PRODUCTS>(selector);
                return productsCollection;
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
        public static ObservableCollection<PRODUCTS> GetUserBasket()
        {
            using (AUCTION_DBEntities db = new AUCTION_DBEntities())
            {
                var selector = from p in db.PRODUCTS
                               join b in db.Basket
                               


            }
        }
        //добавить USER
        //удалить USER
        //изменить USER

        public static PRODUCTS CreateProduct(PRODUCTS product)
        {
            using (AUCTION_DBEntities db = new AUCTION_DBEntities())
            {
                bool isProductExist = db.PRODUCTS.Any(el => el.ID_PRODUCT == product.ID_PRODUCT);
                if (!isProductExist)
                {
                    ///////Добавить owner'а////////
                    IMAGES img = new IMAGES();
                    img.LINK = product.PRODUCT_IMAGE_LINK;
                    IMAGES q = db.IMAGES.Add(img);
                    db.SaveChanges();


                    product.BIDS = 0;
                    product.IMAGE_PROD = q.ID_IMAGE;

                    db.PRODUCTS.Add(product);
                    db.SaveChanges();
                    product.PRODUCT_IMAGE_LINK = q.LINK;
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
                    db.PRODUCTS.Remove(tempProduct);
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
                    //temproduct.TOP_BID_USER_ID = UserId; //////!!!!!!!!!upading top bid user!!!!!!!!!!!!!
                    db.SaveChanges();
                    return tempProduct;
                    //result = "Bid placed";
                }
            }
            return null;
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
