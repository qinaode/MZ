/**  智客知识管理平台。
* PWDPROTECTION.cs
*
* 功 能： N/A
* 类 名： PWDPROTECTION
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:38   N/A    初版
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
	/// PWDPROTECTION:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class PWDPROTECTION
	{
		public PWDPROTECTION()
		{}
		#region Model
		private int _userid;
		private string _question1;
		private string _answer1;
		private string _question2;
		private string _answer2;
		private string _question3;
		private string _answer3;
		private DateTime _lastmodifytime;
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
		public string QUESTION1
		{
			set{ _question1=value;}
			get{return _question1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ANSWER1
		{
			set{ _answer1=value;}
			get{return _answer1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string QUESTION2
		{
			set{ _question2=value;}
			get{return _question2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ANSWER2
		{
			set{ _answer2=value;}
			get{return _answer2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string QUESTION3
		{
			set{ _question3=value;}
			get{return _question3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ANSWER3
		{
			set{ _answer3=value;}
			get{return _answer3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime LASTMODIFYTIME
		{
			set{ _lastmodifytime=value;}
			get{return _lastmodifytime;}
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

