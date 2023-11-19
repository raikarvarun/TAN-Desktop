using Caliburn.Micro;
using DataBaseManger;
using DataBaseManger.Model;
using System;
using System.Threading.Tasks;
using TAN.EventModels;
using TAN.Helpers;
using TAN.Models;

namespace TAN.ViewModels
{
    internal class LoginViewModel : Screen
    {
        private string _userName;
        private string _password;
        private IAPIHelper _apiHelper;
        private IEventAggregator _events;

        public LoginViewModel(IAPIHelper aPIHelper, IEventAggregator events)
        {
            _apiHelper = aPIHelper;
            _events = events;
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }

        //public bool CanLogIn(string userName , string password)
        //{
        //	//For chack format of Username and Passoword
        //	bool ans = false;
        //	if(userName.Length>0 && password.Length >0)
        //	{
        //		ans = true;
        //	}
        //	return ans;
        //}

        public async Task LogIn()
        {
            
            //UserName = "varun123";
            //Password = "123";
            //UserName = "TANNIBM";
            //Password = "Saurav2333@";
            UserName = "test1";
            Password = "test2";
            try
            {
                adminResponse result = await _apiHelper.Authicate(UserName, Password);
                var token = result.data1.adminToken;
                adminModel adminData = new adminModel(1, UserName, Password, token, result.data1.isAdmin);


                DbConnection.deleteDb();
                CommanSQlite.initAll();

                var temp = await commanHelper.fetchAllDataAsync(_apiHelper, token, adminData);



                

                //calling Dashboard data
                _ = _events.PublishOnUIThreadAsync(new LogOnEventModel());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}
