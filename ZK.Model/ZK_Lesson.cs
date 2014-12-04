/**  智客知识管理平台。
* ZK_Lesson.cs
*
* 功 能： N/A
* 类 名： ZK_Lesson
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:48   N/A    初版
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
	public partial class ZK_Lesson
	{
		public ZK_Lesson()
		{}
		#region Model
		private int _lessonid;
		private string _lessonname;
		private string _lessondesc;
		private int? _lessonparent;
		private int? _lessonlevel;
		private int? _classid;
		private string _teachmb;
		private string _teachnd;
		private string _teachzd;
		/// <summary>
		/// 
		/// </summary>
		public int lessonID
		{
			set{ _lessonid=value;}
			get{return _lessonid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string lessonName
		{
			set{ _lessonname=value;}
			get{return _lessonname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string lessonDesc
		{
			set{ _lessondesc=value;}
			get{return _lessondesc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? lessonParent
		{
			set{ _lessonparent=value;}
			get{return _lessonparent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? lessonLevel
		{
			set{ _lessonlevel=value;}
			get{return _lessonlevel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? classID
		{
			set{ _classid=value;}
			get{return _classid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string teachMB
		{
			set{ _teachmb=value;}
			get{return _teachmb;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string teachND
		{
			set{ _teachnd=value;}
			get{return _teachnd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string teachZD
		{
			set{ _teachzd=value;}
			get{return _teachzd;}
		}
		#endregion Model

	}
}

