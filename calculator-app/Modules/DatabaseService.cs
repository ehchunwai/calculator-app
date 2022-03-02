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
        Dictionary<Guid, List<string>> _dictUid_MemoryList = new Dictionary<Guid, List<string>>();

        public DatabaseService()
        {
            ////todo later comment
            User tmpUser = new User()
            {
                ID = "098",
                Name = "Superuser"
            };
            _userList.Add(tmpUser);
            _dictUid_MemoryList.Add(tmpUser.Uid, new List<string>());
        }

        public void ClearMemory(Guid uid)
        {
            if (_dictUid_MemoryList.ContainsKey(uid))
            {
                _dictUid_MemoryList[uid] = new List<string>();
            }
        }

        public void StoreValue(Guid uid, string value)
        {
            if (_dictUid_MemoryList.ContainsKey(uid))
            {
                if (value != "0")
                {
                    _dictUid_MemoryList[uid].Add(value);
                }
            }
        }

        public List<string> GetMemoryList(Guid uid)
        {
            if (_dictUid_MemoryList.ContainsKey(uid))
            {
                return _dictUid_MemoryList[uid];
            }
            return new List<string>();
        }
    }
}
