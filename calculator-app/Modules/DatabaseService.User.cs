using calculator_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator_app.Modules
{
    public partial class DatabaseService
    {
        List<User> _userList = new List<User>();

        public void StoreUserInfo(string id, string name)
        {
            User tmpUser = new User()
            {
                ID = id,
                Name = name
            };
            _userList.Add(tmpUser);

            _dictUid_MemoryList.Add(tmpUser.Uid, new List<string>());
        }

        public bool IsIDExist(string id)
        {
            var isExist = _userList.Any(t => t.ID == id);

            return isExist;
        }

        public User GetUserByID(string id)
        {
            var target = _userList.Where(t => t.ID == id).FirstOrDefault();

            return target;
        }
    }
}
