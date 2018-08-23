using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Test.ViewModel
{
    public class Draggings : ViewModelBase
    {
        private Model.Foto fotoModel;
        public Draggings(Model.Foto deFotoModel)
        {
            fotoModel = deFotoModel;
        }

        public List<BitmapImage> FotoLijst
        {
            get
            {
                return fotoModel.FotoLijst;
            }
            set
            {
                fotoModel.FotoLijst = value;
                RaisePropertyChanged("FotoLijst");
            }
        }

        public BitmapImage GridImg
        {
            get
            {
                return fotoModel.GridFoto;
            }
            set
            {
                fotoModel.GridFoto = value;
                RaisePropertyChanged("GridImg");
            }
        }

        private Image FindItem(Object dragItem)
        {
            DependencyObject choice = (DependencyObject)dragItem;
            while (choice != null)
            {
                if (choice is Image)
                    return (Image)choice;
                choice = VisualTreeHelper.GetParent(choice);
            }
            return null;
        }

        public RelayCommand<MouseEventArgs> Iimage_MouseMove
        {
            get { return new RelayCommand<MouseEventArgs>(OnMouseMove); }
        }

        public void OnMouseMove(MouseEventArgs e)
        {
            Image movingImage = FindItem(e.OriginalSource);
            if (movingImage != null && movingImage.Source.ToString() != "pack://application:,,,/Images/leeg33.jpg")
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    DataObject dataObjImage = new DataObject("Source", movingImage);
                    DragDrop.DoDragDrop(movingImage, dataObjImage, DragDropEffects.Move);
                }
            }
        }

        //private ListBoxItem FindListItem(Object sleepitem)
        //{
        //    DependencyObject keuze = (DependencyObject)sleepitem;
        //    while (keuze != null)
        //    {
        //        if (keuze is ListBoxItem)
        //            return (ListBoxItem)keuze;
        //        keuze = VisualTreeHelper.GetParent(keuze);
        //    }
        //    return null;
        //}

        //public RelayCommand<MouseEventArgs> IList_MouseMove
        //{
        //    get { return new RelayCommand<MouseEventArgs>(OnListMove); }
        //}

        //public void OnListMove(MouseEventArgs e)
        //{
        //    if (e.LeftButton == MouseButtonState.Pressed)
        //    {
        //        ListBoxItem movingImage = FindListItem(e.OriginalSource);
        //        if (movingImage != null)
        //        {
        //            DependencyObject dragLijst = VisualTreeHelper.GetParent(movingImage);
        //            ListBox Lijst = new ListBox();
        //            if (dragLijst is ListBox)
        //                Lijst = (ListBox)dragLijst;
        //            if (Lijst.SelectedIndex >= 0)
        //            {
        //                DataObject dataObjImage = new DataObject("Source", movingImage.Content);
        //                DragDrop.DoDragDrop(movingImage, dataObjImage, DragDropEffects.Move);
        //            }
        //        }
        //    }
        //}


        public RelayCommand<DragEventArgs> Iimage_Drop
        {
            get { return new RelayCommand<DragEventArgs>(OnDrop); }
        }

        public void OnDrop(DragEventArgs e)
        {
            if (e.Data.GetDataPresent("Source"))
            {
                Image dropImage = (Image)e.OriginalSource;
                Image dragImage = (Image)e.Data.GetData("Source");
                ImageSource tempImage = dropImage.Source;
                if (dropImage.Name == "BoundImg")
                {
                    GridImg = new BitmapImage(new Uri(dragImage.Source.ToString(), UriKind.Absolute));

                }
                else
                {
                    dropImage.Source = dragImage.Source;
                    dragImage.Source = tempImage;
                }
            }
        }

        public Grid FindGrid(Object dragObj)
        {
            DependencyObject obj = (DependencyObject)dragObj;
            while (obj != null)
            {
                if (obj is Grid)
                    return (Grid)obj;
                obj = VisualTreeHelper.GetParent(obj);
            }
            return null;
        }

        public RelayCommand<DragEventArgs> InBorder_Drop
        {
            get { return new RelayCommand<DragEventArgs>(OnBorderDrop); }
        }

        public void OnBorderDrop(DragEventArgs e)
        {
            if (e.Data.GetDataPresent("Source"))
            {
                Grid dropGrid = FindGrid(e.OriginalSource);
                Image dragImage = (Image)e.Data.GetData("Source");
                Image copyImage = new Image();
                copyImage.Source = dragImage.Source;
                dropGrid.Children.Clear();
                dropGrid.Children.Add(copyImage);
            }
        }

        public RelayCommand ResetImageCommand
        {
            get { return new RelayCommand(Resetter); }
        }

        public void Resetter()
        {
            GridImg = new BitmapImage(new Uri("pack://application:,,,/Images/bold.png", UriKind.Absolute));
        }
    }
}
