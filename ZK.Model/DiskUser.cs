using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZK.Model
{
    [Serializable]
    public partial class DiskUser
    {
        public DiskUser()
        {}

        #region DiskUser
        private int _id;
		private string _name;
        private string _nick;
        private bool _isAdmin;

		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string name
		{
			set{ _name=value;}
			get{return _name;}
        }
        public string nick
        {
            set { _nick = value; }
            get { return _nick; }

        }
        public bool isAdmin
        {
            set { _isAdmin = value; }
            get { return _isAdmin; }
        }
        #endregion DiskUser
    }
}
