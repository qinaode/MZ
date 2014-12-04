/**  智客知识管理平台。
* ZK_Course.cs
*
* 功 能： N/A
* 类 名： ZK_Course
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:43   N/A    初版
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
	public partial class ZK_Course
	{
		public ZK_Course()
		{}
		#region Model
		private int _courseid;
		private string _coursename;
		private string _coursedesc;
		/// <summary>
		/// 
		/// </summary>
		public int courseID
		{
			set{ _courseid=value;}
			get{return _courseid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string courseName
		{
			set{ _coursename=value;}
			get{return _coursename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string courseDesc
		{
			set{ _coursedesc=value;}
			get{return _coursedesc;}
		}
		#endregion Model

	}
}

