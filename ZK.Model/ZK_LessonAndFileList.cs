/**  智客知识管理平台。
* ZK_LessonAndFileList.cs
*
* 功 能： N/A
* 类 名： ZK_LessonAndFileList
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/12/16 10:52:07   N/A    初版
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
	/// ZK_LessonAndFileList:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ZK_LessonAndFileList
	{
		public ZK_LessonAndFileList()
		{}
		#region Model
		private int _id;
		private int _lessonid;
		private int _fileid;
		private int? _categoryid;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
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
		public int fileID
		{
			set{ _fileid=value;}
			get{return _fileid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CategoryId
		{
			set{ _categoryid=value;}
			get{return _categoryid;}
		}
		#endregion Model

	}
}

