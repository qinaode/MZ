/**  智客知识管理平台。
* ZK_LessonClass.cs
*
* 功 能： N/A
* 类 名： ZK_LessonClass
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:49   N/A    初版
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
	public partial class ZK_LessonClass
	{
		public ZK_LessonClass()
		{}
		#region Model
		private int _classid;
		private int? _courseid;
		private int? _gradeid;
		private int? _editionid;
		/// <summary>
		/// 
		/// </summary>
		public int classID
		{
			set{ _classid=value;}
			get{return _classid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? courseID
		{
			set{ _courseid=value;}
			get{return _courseid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? gradeID
		{
			set{ _gradeid=value;}
			get{return _gradeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? editionID
		{
			set{ _editionid=value;}
			get{return _editionid;}
		}
		#endregion Model

	}
}

