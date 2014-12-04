/**  智客知识管理平台。
* USERS.cs
*
* 功 能： N/A
* 类 名： USERS
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:39   N/A    初版
*
* Copyright (c) 2012 BeiJing HaoLian Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：北京浩联教育科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace ZK.Model
{
	/// <summary>
	/// 1
	/// </summary>
	[Serializable]
	public partial class USERS
	{
		public USERS()
		{}
		#region Model
		private int _userid;
		private string _username;
		private int _usertype;
		private int? _canfindbypublicusers;
		private string _nickname;
		private string _signature;
		private string _actualname;
		private int _sex;
		private int _age;
		private int _birth_year;
		private int _birth_month;
		private int _birth_day;
		private int _country;
		private int _province;
		private int _city;
		private int _area;
		private string _address;
		private string _telephone;
		private string _mobile;
		private string _fax;
		private string _qq;
		private string _msn;
		private string _email;
		private string _homepage;
		private int _departid;
		private string _departname;
		private string _jobtitle;
		private string _jobnumber;
		private string _introduction;
		private string _facefile;
		private string _photofile;
		private int _loginstatus;
		private string _loginstatustext;
		private long _logintimes;
		private DateTime? _lastlogintime;
		private string _clientipaddr;
		private string _clientlocation;
		private string _lastclientipaddr;
		private string _lastclientlocation;
		private int _hascamera;
		private int _hasmic;
		private int _vip;
		private int _onlinelevel;
		private int _integral;
		private string _pwd;
		private string _salt;
		private string _token;
		private DateTime? _tokenupdatetime;
		private int _userlock;
		private int _onlyfindmebyid;
		private int _joinsetting;
		private string _joinquestion;
		private string _joinanswer;
		private DateTime? _lastrecvsysmsgs;
		private DateTime _modifytime;
		private DateTime _createtime;
		/// <summary>
		/// 
		/// </summary>
		public int USERID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string USERNAME
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int USERTYPE
		{
			set{ _usertype=value;}
			get{return _usertype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CANFINDBYPUBLICUSERS
		{
			set{ _canfindbypublicusers=value;}
			get{return _canfindbypublicusers;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NICKNAME
		{
			set{ _nickname=value;}
			get{return _nickname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SIGNATURE
		{
			set{ _signature=value;}
			get{return _signature;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ACTUALNAME
		{
			set{ _actualname=value;}
			get{return _actualname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int SEX
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int AGE
		{
			set{ _age=value;}
			get{return _age;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int BIRTH_YEAR
		{
			set{ _birth_year=value;}
			get{return _birth_year;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int BIRTH_MONTH
		{
			set{ _birth_month=value;}
			get{return _birth_month;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int BIRTH_DAY
		{
			set{ _birth_day=value;}
			get{return _birth_day;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int COUNTRY
		{
			set{ _country=value;}
			get{return _country;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int PROVINCE
		{
			set{ _province=value;}
			get{return _province;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int CITY
		{
			set{ _city=value;}
			get{return _city;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int AREA
		{
			set{ _area=value;}
			get{return _area;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ADDRESS
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TELEPHONE
		{
			set{ _telephone=value;}
			get{return _telephone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MOBILE
		{
			set{ _mobile=value;}
			get{return _mobile;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FAX
		{
			set{ _fax=value;}
			get{return _fax;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string QQ
		{
			set{ _qq=value;}
			get{return _qq;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MSN
		{
			set{ _msn=value;}
			get{return _msn;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EMAIL
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HOMEPAGE
		{
			set{ _homepage=value;}
			get{return _homepage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int DEPARTID
		{
			set{ _departid=value;}
			get{return _departid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DEPARTNAME
		{
			set{ _departname=value;}
			get{return _departname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string JOBTITLE
		{
			set{ _jobtitle=value;}
			get{return _jobtitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string JOBNUMBER
		{
			set{ _jobnumber=value;}
			get{return _jobnumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string INTRODUCTION
		{
			set{ _introduction=value;}
			get{return _introduction;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FACEFILE
		{
			set{ _facefile=value;}
			get{return _facefile;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PHOTOFILE
		{
			set{ _photofile=value;}
			get{return _photofile;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int LOGINSTATUS
		{
			set{ _loginstatus=value;}
			get{return _loginstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LOGINSTATUSTEXT
		{
			set{ _loginstatustext=value;}
			get{return _loginstatustext;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long LOGINTIMES
		{
			set{ _logintimes=value;}
			get{return _logintimes;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? LASTLOGINTIME
		{
			set{ _lastlogintime=value;}
			get{return _lastlogintime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CLIENTIPADDR
		{
			set{ _clientipaddr=value;}
			get{return _clientipaddr;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CLIENTLOCATION
		{
			set{ _clientlocation=value;}
			get{return _clientlocation;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LASTCLIENTIPADDR
		{
			set{ _lastclientipaddr=value;}
			get{return _lastclientipaddr;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LASTCLIENTLOCATION
		{
			set{ _lastclientlocation=value;}
			get{return _lastclientlocation;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int HASCAMERA
		{
			set{ _hascamera=value;}
			get{return _hascamera;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int HASMIC
		{
			set{ _hasmic=value;}
			get{return _hasmic;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int VIP
		{
			set{ _vip=value;}
			get{return _vip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ONLINELEVEL
		{
			set{ _onlinelevel=value;}
			get{return _onlinelevel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int INTEGRAL
		{
			set{ _integral=value;}
			get{return _integral;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PWD
		{
			set{ _pwd=value;}
			get{return _pwd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SALT
		{
			set{ _salt=value;}
			get{return _salt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TOKEN
		{
			set{ _token=value;}
			get{return _token;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? TOKENUPDATETIME
		{
			set{ _tokenupdatetime=value;}
			get{return _tokenupdatetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int USERLOCK
		{
			set{ _userlock=value;}
			get{return _userlock;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ONLYFINDMEBYID
		{
			set{ _onlyfindmebyid=value;}
			get{return _onlyfindmebyid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int JOINSETTING
		{
			set{ _joinsetting=value;}
			get{return _joinsetting;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string JOINQUESTION
		{
			set{ _joinquestion=value;}
			get{return _joinquestion;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string JOINANSWER
		{
			set{ _joinanswer=value;}
			get{return _joinanswer;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? LASTRECVSYSMSGS
		{
			set{ _lastrecvsysmsgs=value;}
			get{return _lastrecvsysmsgs;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime MODIFYTIME
		{
			set{ _modifytime=value;}
			get{return _modifytime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CREATETIME
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		#endregion Model

	}
}

