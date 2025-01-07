using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkTracker.Components;
using WorkTracker.Components.ViewModels;
using WorkTracker.Services;
using WorkTracker.Stores;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.ViewModel
{
    public class MyProfileViewModel:BaseViewModel
    {
        private string _newEmail;
        private string _newPhone;
        private byte[]? _newImage;
        private UserService _userService;
        private UserStore _userStore;
        private string _oldPassword;
        private string _newPassword;
        private string _newPasswordRepeated;

        private bool _isUpdateable=false;
        public string NameSurname { get { return UserStore.User.Name + " " + UserStore.User.Surname; } }
        public string NewEmail { get { return _newEmail; } set { _newEmail = value; CheckUpdateable(); OnPropertyChanged(); } }
        public string NewPhone { get { return _newPhone; } set { _newPhone = value; CheckUpdateable(); OnPropertyChanged(); } }
        public bool IsUpateable { get { return _isUpdateable; } set { _isUpdateable = value; OnPropertyChanged(); } }
        public byte[]? NewImage { get { return _newImage; } set { _newImage = value; CheckUpdateable(); OnPropertyChanged(); } }
        public RelayCommand ImagePickCommand { get; set; }
        public RelayCommand SaveChangesCommand { get; set; }

        public string OldPassword { get { return _oldPassword; } set { _oldPassword = value; OnPropertyChanged(); } }
        public string NewPassword { get { return _newPassword; } set { _newPassword = value; OnPropertyChanged(); } }
        public string NewPasswordRepeated { get { return _newPasswordRepeated; } set { _newPasswordRepeated = value; OnPropertyChanged(); } }

        public UserStore UserStore { get { return _userStore; } }
        public MyProfileViewModel(UserStore userStore,UserService userService)
        {
            _userService = userService;
            _userStore = userStore;
            _newEmail = userStore.User.Email;
            _newPhone = userStore.User.PhoneNumber;
            _newImage = userStore.User.Image;
            ImagePickCommand = new RelayCommand(o => { PickImage(); }, o => true);
            SaveChangesCommand=new RelayCommand(async o=> { await SaveChanges(); },o=>true);
        }
        private void CheckUpdateable()
        {
            if (!_newEmail.Equals(_userStore.User.Email) || !_newPhone.Equals(_userStore.User.PhoneNumber) || (_newImage!=null&&!_newImage.SequenceEqual(UserStore.User.Image ?? Array.Empty<byte>())))
                IsUpateable = true;
            else
                IsUpateable = false;
        }
        private void PickImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                NewImage = File.ReadAllBytes(filePath);
                
            }
        }
        private async Task SaveChanges()
        {
            ResourceDictionary resources=App.Current.Resources;
            App.serviceProvider.GetRequiredService<LoadingCircleViewModel>().IsLoading = true;
            _userStore.User.Email= _newEmail;
            _userStore.User.PhoneNumber= _newPhone;
            _userStore.User.Image= _newImage;
            await _userService.UpdateUserInfo(NewEmail, NewPhone, NewImage, _userStore.User.Username);
            App.serviceProvider.GetRequiredService<LoadingCircleViewModel>().IsLoading = false;
            IsUpateable= false;
            new CustomDialog(false, true, (string)resources["ProfileUpdatedTitle"] , (string)resources["ProfileUpdatedDescription"]).Show();       
        }
        public bool CheckOldPassword()
        {
            ResourceDictionary resources = App.Current.Resources;
            if (OldPassword.Equals(_userStore.User.Password))
            {
                return true;
            }
            else
            {
                new CustomDialog(false, false, (string)resources["IncorrectPassword"], (string)resources["IncorrectPasswordDescription"]).Show();
                return false;
            }
        }
        public async Task<bool> ChangePassword()
        {
            ResourceDictionary resources = App.Current.Resources;
            if (NewPassword.Equals(NewPasswordRepeated))
            {
                App.serviceProvider.GetRequiredService<LoadingCircleViewModel>().IsLoading = true;
                await _userService.UpdateUserPassword(_userStore.User.Username, NewPassword);
                App.serviceProvider.GetRequiredService<LoadingCircleViewModel>().IsLoading = false;
                new CustomDialog(false, true, (string)resources["PasswordUpdated"], (string)resources["PasswordUpdatedDescription"]).Show();
                return true;
            }
            else
            {
                new CustomDialog(false, false, (string)resources["Erorr"], (string)resources["PasswordsDontMatch"]).Show();
                return false;
            }
        }
        public override void Dispose()
        {
            NewEmail=_userStore.User.Email;
            NewPhone=_userStore.User.PhoneNumber;
            NewImage=_userStore.User.Image;
        }
    }
}
