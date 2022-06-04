using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EcoCareApp.ViewModels;
using EcoCareApp.Models;

namespace EcoCareApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Products : ContentView
    {
        public Products()
        {
            

            ProductsViewModel context = new ProductsViewModel();
            //Register to the event
            //so the view model will be able to navigate to the productPage
            context.NavigateToPageEvent += NavigateToAsync;
            this.BindingContext = context;
            InitializeComponent();

        }
        //Allow ViewModel to call this function if needed to navigate to another page!
        public async void NavigateToAsync(Page p)
        {
            await Navigation.PushAsync(p);
        }


        #region Fields

        Image leftImage;
        Image rightImage;
        int itemIndex = -1;

        #endregion

        //#region Private Methods

        //private void ToEditPage()
        //{
        //    if (itemIndex >= 0)
        //    {
        //        var item = viewModel.InboxInfo[itemIndex];
        //        item.IsFavorite = !item.IsFavorite;
        //    }
        //    this.listView.ResetSwipe();
        //}

        //private void Delete()
        //{
        //    if (itemIndex >= 0)
        //        viewModel.InboxInfo.RemoveAt(itemIndex);
        //    this.listView.ResetSwipe();
        //}

        //#endregion

        //#region CallBacks

        //private void leftImage_BindingContextChanged(object sender, EventArgs e)
        //{
        //    if (leftImage == null)
        //    {
        //        leftImage = sender as Image;
        //        (leftImage.Parent as View).GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(SetFavorites) });
        //        leftImage.Source = ImageSource.FromResource("ListViewSwiping.Images.Favorites.png");
        //    }
        //}

        //private void rightImage_BindingContextChanged(object sender, EventArgs e)
        //{
        //    if (rightImage == null)
        //    {
        //        rightImage = sender as Image;
        //        (rightImage.Parent as View).GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(Delete) });
        //        rightImage.Source = ImageSource.FromResource("ListViewSwiping.Images.Delete.png");
        //    }
        //}

        //private void ListView_SwipeStarted(object sender, Syncfusion.ListView.XForms.SwipeStartedEventArgs e)
        //{
        //    itemIndex = -1;
        //}

        //private void ListView_Swiping(object sender, SwipingEventArgs e)
        //{
        //    if (e.ItemIndex == 1 && e.SwipeOffSet > 70)
        //        e.Handled = true;
        //}

        //private void ListView_SwipeEnded(object sender, Syncfusion.ListView.XForms.SwipeEndedEventArgs e)
        //{
        //    itemIndex = e.ItemIndex;
        //}
        //#endregion

        private void DeleteImage_BindingContextChanged(object sender, EventArgs e)
        {

        }
        private void EditImage_BindingContextChanged(object sender, EventArgs e)
        {

        }
        private void listView_SwipeStarted(object sender, Syncfusion.ListView.XForms.SwipeStartedEventArgs e)
        {

        }

        private void listView_SwipeEnded(object sender, Syncfusion.ListView.XForms.SwipeEndedEventArgs e)
        {

        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            //לקרוא לפעולה לשלוח את המוצר
            ProductsViewModel vm = (ProductsViewModel)this.BindingContext;
            Product p = (Product)(((ImageButton)sender).BindingContext);
            vm.ToEditPage(p);
        }

      

        private void Delete_Clicked(object sender, EventArgs e)
        {
            ProductsViewModel vm = (ProductsViewModel)this.BindingContext;
            Product p = (Product)(((ImageButton)sender).BindingContext);
            vm.Delete(p);
        }
    }
}