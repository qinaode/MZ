using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZK.Model
{
	[Serializable]
	public partial class JsonCommon
    {
        public JsonCommon()
        {}

    	#region JsonCommon
		private bool  _success;
        private DiskUser _msg;

		/// <summary>
		/// 
		/// </summary>
		public bool success
		{
			set{ _success=value;}
			get{return _success;}
		}


        public DiskUser msg
        {
            set { _msg = value; }
            get { return _msg; }
        }
        #endregion JsonCommon
    }
}
