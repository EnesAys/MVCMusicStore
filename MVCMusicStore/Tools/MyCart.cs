using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCMusicStore.Tools
{
    public class MyCart
    {
        #region Sepette Olmasi Gerekenler
        /*
         * ----Metotlar----
         * SepeteEkle
         * SepettenCikar
         * SepetiGuncelle
         * SepetiTemizle
         * 
         * ---Ozellikler---
         * Toplam Urun Sayisi
         * Toplam Tutar
         * Toplam Urun Cesidi Sayisi
         */
        #endregion

        private List<CartItem> _cartItems;
        public List<CartItem> CartItems
        {
            get
            {
                return _cartItems;
            }
        }

        public MyCart()
        {
            _cartItems = new List<CartItem>();
        }

        #region Methods
        public void AddToCart(CartItem cItem)
        {
            CartItem c = _cartItems.FirstOrDefault(ci => ci.Id == cItem.Id);
            if (c == null)
                _cartItems.Add(cItem);
            else
                c.Count++;
        }

        public void RemoveFromcart(int cItemId)
        {
            CartItem removed = _cartItems.FirstOrDefault(ci => ci.Id == cItemId);
            _cartItems.Remove(removed);
        }

        public void UpdateCart(int cItemId, int count)
        {
            CartItem updated = _cartItems.FirstOrDefault(ci => ci.Id == cItemId);
            if (updated != null)
            {
                updated.Count = count;
                if (updated.Count <= 0)
                    RemoveFromcart(cItemId);
            }
        }

        public void ClearCart()
        {
            _cartItems.Clear();
        }
        #endregion

        #region Properties
        public decimal Total
        {
            get
            {
                return _cartItems.Sum(c => c.SubTotal);
            }
        }

        public int TotalItemCount
        {
            get
            {
                return _cartItems.Sum(c => c.Count);
            }
        }

        public int TotalItemKindCount
        {
            get
            {
                return _cartItems.Count;
            }
        }
        #endregion
    }
}